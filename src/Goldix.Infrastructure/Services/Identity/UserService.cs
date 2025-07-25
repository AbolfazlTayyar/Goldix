using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Repositories.Common;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Services.Identity;

public class UserService(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager) : IUserService
{
    public async Task<ApplicationUser> RegisterUserAsync(string phoneNumber, string firstName, string lastName, string password, string roleName, CancellationToken cancellationToken = default)
    {
        var isUserExistByPhoneNumber = await unitOfWork.UserRepository.IsUserExistByPhoneNumberAsync(phoneNumber, cancellationToken);
        if (isUserExistByPhoneNumber)
            throw new BadRequestException(".کاربری با این شماره موبایل در سیستم ثبت شده است");

        var createdUser = await unitOfWork.UserRepository.CreateUserAsync(firstName, lastName, phoneNumber, password, cancellationToken);

        var role = await roleManager.FindByNameAsync(roleName);
        if (role is null)
            throw new InvalidOperationException("نقش کاربر در سیستم یافت نشد.");

        await unitOfWork.UserRepository.AssignRoleToUser(createdUser.Id, role, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);   

        return createdUser;
    }
}

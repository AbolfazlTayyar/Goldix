﻿namespace Goldix.Application.Mappings.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in mapFromTypes)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping")
                ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}

namespace Goldix.Application.Interfaces.Infrastructure;

public interface IService { }
public interface IScopedService : IService { }
public interface ISingletonService : IService { }
public interface ITransientService : IService { }

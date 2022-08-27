namespace WebApiTemplate.Api;


public interface IContainer
{
    TService Resolve<TService>() where TService : notnull;
}


//   container.Register<IContainer>(() => new ContainerServiceProviderWrapper(container));
public class ContainerServiceProviderWrapper : IContainer
{
    private readonly IServiceProvider _container;

    public ContainerServiceProviderWrapper(IServiceProvider container)
    {
        _container = container;
    }

    public TService Resolve<TService>() where TService : notnull
        => _container.GetRequiredService<TService>();
}
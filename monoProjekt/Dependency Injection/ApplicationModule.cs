using Autofac;
using monoProjekt.Services;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VehicleService>().As<IVehicleService>().InstancePerLifetimeScope();
        
    }
}
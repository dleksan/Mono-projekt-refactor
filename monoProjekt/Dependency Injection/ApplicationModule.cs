using Autofac;
using monoProjekt.Services;
using Projekt.Sevice.Services;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>().InstancePerLifetimeScope();
        builder.RegisterType<VehicleModelService>().As<IVehicleModelService>().InstancePerLifetimeScope();

    }
}
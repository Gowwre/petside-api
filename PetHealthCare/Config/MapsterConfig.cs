namespace PetHealthCare.Config;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        //  .Map(dest => dest.SummaryNow, src => src.Summary);
        //TypeAdapterConfig<Providers, ProvidersDTO>
        //   .NewConfig()
        //   .TwoWays();
        //TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

    }
}

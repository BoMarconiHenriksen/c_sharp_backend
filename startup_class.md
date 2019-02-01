# Startup klassen
startup klassen konfigurer services og applikationen request pipeline.  

startup klassen indeholder ConfigureServices metode til at konfigurer applikationens services.  
En service er en genbruglig komponent, der giver applikationen funktionalitet.  
Services bliver konfigureret(registret) i ConfigurServices og bliver brugt hen over applikationen via dependency injection eller ApploicationServices.  
Indeholder også  en Configure metode til at lave en applikations request processing pipeline.  

ConfigureServices og Configure bliver kaldt ved runtime når applikationen starter.  
```
public class Startup
{
    // Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        ...
    }

    // Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
        ...
    }
}
```
startup klassen bliver specificeret til appen når appens host bliver build. 

```
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}
```
Hosten provides services, der bliver tilgængelige for startup klassens konstruktør.  
Appen tilfører yderligere services via ConfigureServices.  
Både host og app services er tilgængelige i Configure og i resten af appen.  

En almindelige dependency injection i startup kunne være:  
IHostingEnviroment - For at kunne konfigurer services ved miljø.  
IConfiguretionBuilder - For at kunne læse konfigurationen.  
ILoggerFactory - Laver en logger i startup.ConfigureServices.  
```
public class Startup
{
    private readonly IHostingEnvironment _env;
    private readonly IConfiguration _config;
    private readonly ILoggerFactory _loggerFactory;

    // Dependency injection i startup kunne
    public Startup(IHostingEnvironment env, IConfiguration config, 
        ILoggerFactory loggerFactory)
    {
        _env = env;
        _config = config;
        _loggerFactory = loggerFactory;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var logger = _loggerFactory.CreateLogger<Startup>();

        if (_env.IsDevelopment())
        {
            // Development service configuration

            logger.LogInformation("Development environment");
        }
        else
        {
            // Non-development service configuration

            logger.LogInformation($"Environment: {_env.EnvironmentName}");
        }

        // Configuration is available during startup.
        // Examples:
        //   _config["key"]
        //   _config["subsection:suboption1"]
    }
}
```
### ConfigureServices metoden
ConfigureServices metoden er:  
- Optional.  
- Bliver kaldt af host før Configure metoden for at konfigurer appens services.  
- 

```
public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    services.AddMvc();

    // Add application services.
    services.AddTransient<IEmailSender, AuthMessageSender>();
    services.AddTransient<ISmsSender, AuthMessageSender>();
}
```
### Configure metoden
Configure metoden bliver brugt til at specificer, hvordan appen reager på http requests.  
request pipelinen bliver konfigureret ved at tilføje middleware komponenter til en IApplicationBuilder instans.  
IApplicationBuilder er tilgængelig for Configure metoden, men er ikke registret som en service kontainer.  
Hosting laver en IApplicationBuilder og giver den direkte til Configure.  
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCookiePolicy();

    app.UseMvc();
}
```
Hver Use extention metode tilfører en eller flere middleware komponenter til request pipelinen. F.eks. Usemvc tilfører routing middleware til request pipelinen og konfigurer MVC som default handler.  

Hver middleware komponent i request pipelinen er ansvarlig for at kalde den næste komponent i pipelinen.  

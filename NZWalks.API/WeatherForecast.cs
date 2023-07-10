namespace NZWalks.API;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    //My Nuget Packages List
    // Entity Framework Nuget packages
    // 1. Microsoft.EntityFrameWorkCore.SqlServer
    // 2. Microsoft.EntityFrameWorkCore.Tools

    //Automapper Nuget Package
    // 1. Automapper Nugget
    // 2. AutoMapper.Extensions.Microsoft.DependencyInjection

    //Authentication
    // 1. Microsoft.AspNetCore.Authentication.JwtBearer
    // 2. Microsoft.IdentityModel.Tokens
    // 3. System.IdentityModel.Tokens.Jwt
    // 4. Microsoft.AspNetCore.Identity.EntityFrameworkCore

    // Logging
    // 1. Serilog
    // 2. Serilog.AspNetCore
    // 3. Serilog.Sinks.Console - this or 4
    // 4. Serilog.Sinks.File

    // Versioning
    // 1. Microsoft.AspNetCore.Mvc.Versioning
    // 2. Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer


}


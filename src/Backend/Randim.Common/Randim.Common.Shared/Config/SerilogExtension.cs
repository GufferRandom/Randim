﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Randim.Common.Shared.Config;

public static class SerilogExtension
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder)
    {
        builder
            .UseSerilog(
                (ctx, lc) =>
                {
                    lc.ReadFrom.Configuration(ctx.Configuration);
                }
            )
            .ConfigureAppConfiguration(
                (hostContext, configBuilder) =>
                {
                    var env = hostContext.HostingEnvironment;
                    var relativePath = Path.Combine("Settings", "serilogsettings.json");
                    var fullPath = Path.GetFullPath(
                        Path.Combine(env.ContentRootPath, relativePath)
                    );
                    Console.WriteLine(
                        "CONFIG DEBUG: Loading Serilog settings from: "
                            + Path.Combine(env.ContentRootPath, "Settings", "serilogsettings.json")
                    );
                    Console.WriteLine(
                        "Does config file exist? "
                            + File.Exists(
                                Path.Combine(
                                    env.ContentRootPath,
                                    "Settings",
                                    "serilogsettings.json"
                                )
                            )
                    );
                    configBuilder.AddJsonFile(fullPath, false, true);
                    configBuilder.AddEnvironmentVariables();
                }
            );
        return builder;
    }
}

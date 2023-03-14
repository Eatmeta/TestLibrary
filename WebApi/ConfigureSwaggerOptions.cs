﻿using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly string _authorityString;

    public ConfigureSwaggerOptions(string authorityString)
    {
        _authorityString = authorityString;
    }

    public void Configure(SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = $"Book Api",
                Description = $"Book Api Test Task From Modsen. Please autorize with client_id: 8aaac42f-5ba5-4714-8fba-9b2947b7f04a and client_secret: secret",
                Contact = new OpenApiContact
                {
                    Name = "by Aliaksandr Shamak",
                    Email = string.Empty,
                    Url = new Uri("https://www.linkedin.com/in/eatmeta")
                }
            });

        swaggerGenOptions.AddSecurityDefinition("oauth2",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(_authorityString + "/connect/token"),
                        Scopes = {{"https://www.example.com/api", "API"}}
                    }
                }
            });

        swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                },
                new List<string> {"https://www.example.com/api"}
            }
        });

        swaggerGenOptions.CustomOperationIds(apiDescription =>
            apiDescription.TryGetMethodInfo(out var methodInfo)
                ? methodInfo.Name
                : null);
    }
}
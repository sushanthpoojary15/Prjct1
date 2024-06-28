using H2H.Physiotherapy.API.Middleware;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Data.Abstractions;
using H2H.Physiotherapy.Data.Datastore;
using H2H.Physiotherapy.Data.Features.DatastoreManangement;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Abstractions.OtherServices;
using H2H.Physiotherapy.Services.Configs;
using H2H.Physiotherapy.Services.Features;
using H2H.Physiotherapy.Services.Features.OtherServices;
using H2H.Physiotherapy.Services.Logging;
using H2H.Physiotherapy.Services.Request;
using H2H.Physiotherapy.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace H2H.Physiotherapy.API.Extensions
{
    public static class PhysioTheraphy_RegistrationExtension
    {

        public static void AddPhysiotheraphyServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<PhysioTheraphyConfigurations>(configuration.GetSection("PhysiotheraphyConfiguartions"));
            services.AddSingleton<IParamManager, ParamManager>();
            services.AddSingleton<IDatabaseHandler, DatabaseHandler>();
            services.AddSingleton<IDatabaseManager, DatabaseManager>();
            services.AddScoped<ISampleService, SampleService>();
            services.AddScoped<ISampleStore, SampleStore>();
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IMasterStore,MasterStore>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddSingleton<ILoginStore, LoginStore>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddSingleton<IAssessmentStore, AssessmentStore>();
            services.AddScoped<IAssessmentService, AssessmentService>();

            services.AddSingleton<IAccountStore, AccountStore>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IUserStore, UserStore>();
            services.AddSingleton<IEnquiryStore, EnquiryStore>();
            services.AddScoped<IEnquiryService, EnquiryService>();
            services.AddScoped<IRequestContext, RequestContext>();
            services.AddScoped<ICommonServices,CommonServices>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            services.AddSingleton(x =>
            {
                var blobStorageSettings = configuration.GetSection("PhysiotheraphyConfiguartions:BlobStorageSettings").Get<BlobStorageSettings>();
                return new Azure.Storage.Blobs.BlobServiceClient(blobStorageSettings.StorageConnectionString);
            });


            // services.AddAuthServices(configuration);

        }



        public static void AddAuthServices(this IServiceCollection services, ConfigurationManager configuration)
        {


            services.AddScoped<IRequestContext, RequestContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["PhysiotheraphyConfiguartions:TokenSettings:TokenSigningKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static IApplicationBuilder AddCustomMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestContextMiddleware>();
            builder.UseMiddleware<ErrorHandlerMiddleware>();
            return builder;
        }
    }
}

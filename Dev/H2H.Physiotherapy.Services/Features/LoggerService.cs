using H2H.Physiotherapy.Services.Configs;
using H2H.Physiotherapy.Services.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Features
{
    public class LoggerService : ILoggerService
    {

        private readonly string connectionString;
        private const string tableName = "Logs";

        public LoggerService(IConfiguration configuration)
        {
            connectionString = configuration["PhysiotheraphyConfiguartions:ConnectionString"];

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connectionString, new MSSqlServerSinkOptions { TableName = tableName })
                .CreateLogger();
        }
        public void LogError(object obj, Exception ex, string context = "No context available")
        {
            Log.Error(ex, "{context} ; {@object} ; {message}", context, obj, $"Error:{ex.Message}");
            Log.CloseAndFlush();
        }
    }
}

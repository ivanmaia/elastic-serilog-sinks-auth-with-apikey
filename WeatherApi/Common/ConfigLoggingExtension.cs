using Elastic.Apm.SerilogEnricher;
using Elastic.Serilog.Sinks;
using Serilog;

public static class ConfigLoggingExtension
{
    
    //In a production software you should hide these info. For example, you could use a vault and sercets inside a protected container.
    const string apiKey = "<insert your base64 encoded apikey here>";// "Example JjBzYUpJY0JoT1RpMjdmTTBBWEg6SHJjTExIVDVRanlnRzdrdTBOMlY4dy=="
    const string serviceName = "<insert your service name here>"; // "Example MyService"
    const string elasticsearchServerUrl = "<insert your elasticsearch url here>"; // Example "https://elk-xxxx-dev.westus2.azure.elastic-cloud.com"

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithElasticApmCorrelationInfo()
            .Enrich.WithProperty("service.name", serviceName)
            .WriteTo.Elasticsearch(new[] { new Uri(elasticsearchServerUrl) },
                opts =>
                {
                    opts.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                    opts.ConfigureChannel = channel =>
                    {
                        channel.ExportResponseCallback = (response, buffer) => Console.WriteLine($"Written  {buffer.Count} logs to Elasticsearch: {response.ApiCallDetails.HttpStatusCode} {Utils.GetStringFromBulkResponseBites(response)}");
                    };
                },
                trans =>
                {
                    var headers = new System.Collections.Specialized.NameValueCollection();
                    headers.Add("Authorization", $"ApiKey {apiKey}");
                    trans.GlobalHeaders(headers);
                })
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();
    }
}
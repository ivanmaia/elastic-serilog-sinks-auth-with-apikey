# elastic-serilog-sinks-auth-with-apikey
Sample dotnet8 webapi project that sinks logs to elasticsearch using api-key as auth method.

1. Clone this repo.
2. Open the working folder in vscode
3. Edit Common/ConfigLoggingExtension.cs
4. Update apiKey constant value.
5. Update serviceName constant value.
6. Update elasticsearchServerUrl constant value.
7. Open Termnal (ctrl+shift+').
8. > dotnet run --project WeatherApi
9. Open your Elasticsearch Logs Stream page (example: https://elk-xxxx-dev.kb.westus2.azure.elastic-cloud.com:9243/app/logs/stream).
10. Search using this filter: "service.name":"[insert here the value used in step 5]".



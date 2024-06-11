using System.Text;
using Elastic.Ingest.Elasticsearch.Serialization;

public static class Utils
{
    public static string GetStringFromBulkResponseBites(BulkResponse response)
    {
        try{
            return Encoding.UTF8.GetString(response.ApiCallDetails.ResponseBodyInBytes, 0, response.ApiCallDetails.ResponseBodyInBytes.Length);
        } catch
        {
            return string.Empty;
        }
    }
}
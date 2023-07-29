using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TradingApp.Core.Utilities;

public static class HttpUtilities
{
    public static ByteArrayContent ConvertToHttpContent(object model)
    {
        var jsonContent = JsonSerializer.Serialize(model);
        var buffer = Encoding.UTF8.GetBytes(jsonContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return byteContent;
    }

    public static FormUrlEncodedContent ConvertToUrlEncoded(object model)
    {
        var content = new FormUrlEncodedContent(ObjectToDictionary(model));
        content.Headers.Clear();
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        return content;
    }

    private static Dictionary<string, string> ObjectToDictionary(object obj)
    {
        var dict = new Dictionary<string, string>();

        foreach (var prop in obj.GetType().GetProperties())
        {
            var value = prop.GetValue(obj, null)?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                dict.Add(prop.Name, value);
            }
        }

        return dict;
    }
}

using System.Text.Json.Serialization;
using System.Text.Json;

namespace Cfa.Clientes.Domain.Models;

public class ResponseProblem
{
    [JsonPropertyName("type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Type { get; set; }

    [JsonPropertyName("title"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Title { get; set; }

    [JsonPropertyName("status"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Status { get; set; }

    [JsonPropertyName("detail"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Detail { get; set; }

    [JsonPropertyName("statusCode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? StatusCode { set; get; }

    [JsonPropertyName("statusMessage"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string StatusMessage { set; get; }

    public override string ToString() => JsonSerializer.Serialize(this);
}

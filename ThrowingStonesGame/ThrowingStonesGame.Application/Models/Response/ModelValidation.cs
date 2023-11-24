using System.Text.Json.Serialization;

namespace ThrowingStonesGame.Application.Models.Response
{
    public class ModelValidation
    {
        [JsonPropertyName("mensagem")]
        public string Message { get; set; }

        [JsonPropertyName("erros")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object[]? Errors { get; set; }

        public ModelValidation(string message, object[]? errors = null)
        {
            Message = message;
            Errors = errors;
        }
    }
}

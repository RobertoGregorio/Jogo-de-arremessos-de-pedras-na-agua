using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.ValueObjects;

public class MatchDTO
{
    [JsonPropertyName("jogada")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo jogada é obrigatório")]
    public string? Play { get; set; }

    [JsonPropertyName("resultado")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo resultado é obrigatório")]
    public string? Result { get; set; }
}

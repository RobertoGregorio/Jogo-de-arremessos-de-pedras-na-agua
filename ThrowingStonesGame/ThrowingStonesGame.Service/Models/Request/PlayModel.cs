using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThrowingStonesGame.Application.Models;

public class PlayModel
{
    [JsonPropertyName("jogada")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo jogada é obrigatório")]
    public string? Players { get; set; }

    [JsonPropertyName("resultado")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo resultado é obrigatório")]
    public string? PlayResult { get; set; }
}
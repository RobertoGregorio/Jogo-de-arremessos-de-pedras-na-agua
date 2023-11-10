using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.ValueObjects;

public class GameMatchVO
{
    [JsonPropertyName("jogadas")]
    [Required(AllowEmptyStrings = false,  ErrorMessage = "A lista de jogdas não pode ser vazia")]
    public List<MatchDTO>? Matches { get; set; }
}

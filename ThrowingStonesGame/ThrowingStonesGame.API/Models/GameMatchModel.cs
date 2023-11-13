using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThrowingStonesGame.API.Models;

public class GameMatchModel
{
    [JsonPropertyName("jogadas")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "A lista de jogadas não pode ser vazia")]
    public List<PlayMatchModel>? Plays { get; set; }
}

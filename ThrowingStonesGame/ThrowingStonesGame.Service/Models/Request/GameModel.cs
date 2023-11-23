using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThrowingStonesGame.Application.Models.Request;

public class GameModel
{
    [JsonPropertyName("jogadas")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "A lista de jogadas não pode ser nula ou vazia")]
    public List<PlayModel>? Plays { get; set; }
}

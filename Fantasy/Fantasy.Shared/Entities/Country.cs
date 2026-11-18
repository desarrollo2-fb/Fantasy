using Fantasy.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.Entities;

public class Country
{
    public int Id { get; set; }

    [Display(Name = "Country", ResourceType = typeof(LIterals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(LIterals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(LIterals))]
    public string Name { get; set; } = null!;

    public ICollection<Team>? Teams { get; set; }

    public int TeamsCount => Teams == null ? 0 : Teams.Count; // propiedad de lectura
}
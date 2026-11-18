using Fantasy.Shared.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fantasy.Shared.Entities;

public class Team
{
    public int Id { get; set; }

    [Display(Name = "Team", ResourceType = typeof(LIterals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(LIterals))]
    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(LIterals))]
    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public Country? Country { get; set; }

    public int CountryId { get; set; }
}
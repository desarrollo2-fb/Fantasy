using Fantasy.Shared.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fantasy.Frontend.Shared;

public partial class GenericList<Titem>
{
    public GenericList()
    {
    }

    [Inject] private IStringLocalizer<LIterals> Localizer { get; set; } = null!;

    [Parameter] public RenderFragment? Loading { get; set; }
    [Parameter] public RenderFragment? NoRecords { get; set; }
    [EditorRequired, Parameter] public RenderFragment Body { get; set; } = null!;
    [EditorRequired, Parameter] public List<Titem> MyList { get; set; } = null!;
}
using CipherWeb.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace CipherWeb.Shared.Components.Buttons
{
    public partial class CipherAddBtn : CipherButton
    {
        public CipherAddBtn() : base()
        {
            Icon = Icons.Symbols.Plus.add_circle_outline;
            Padding = "5px";
        }
    }

    public partial class CipherCancelButton : CipherButton
    {
        public CipherCancelButton() : base()
        {
            Variant = Variant.Outlined;
            ColorStyle = ButtonStyle.Danger;
            ColorShade = Shade.Dark;
            Size = ButtonSize.Large;
            Icon = Icons.Documents.Delete.cancel;
            Text = "ביטול";
        }
    }

    public partial class CipherCloseButton : CipherButton
    {
        public CipherCloseButton() : base()
        {
            ColorStyle = ButtonStyle.Danger;
            ColorShade = Shade.Dark;
            Icon = Icons.Documents.Delete.close;
        }
    }

    public partial class CipherDeleteButton : CipherButton
    {
        public CipherDeleteButton() : base()
        {
            ColorStyle = ButtonStyle.Danger;
            ColorShade = Shade.Dark;
            Icon = Icons.Documents.Delete.delete;
        }
    }
    public partial class CipherDeleteAllButton : CipherButton
    {
        public CipherDeleteAllButton() : base()
        {
            Variant = Variant.Outlined;
            Icon = Icons.Documents.Delete.delete_sweep;
        }
    }

    public partial class CipherEditButton : CipherButton
    {
        public CipherEditButton() : base()
        {
            ColorStyle = ButtonStyle.Primary;
            ColorShade = Shade.Default;
            Icon = Icons.Documents.Edit.edit;
        }
    }

    public partial class CipherSearchButton: CipherButton
    {
        public CipherSearchButton() : base()
        { 
            Icon = Icons.SearchAndFilter.search;
        }
    }

    public partial class CipherSubmitButton : CipherButton
    {
        public CipherSubmitButton() : base()
        {
            Variant = Variant.Outlined;
            ButtonType = ButtonType.Submit;
            Size = ButtonSize.Large;
            Icon = Icons.Documents.Edit.save;
            Text = "שמירה";
            ColorStyle = ButtonStyle.Success;
            ColorShade = Shade.Dark;
        }
    }

    public partial class CipherSuccessButton : CipherButton
    {
        public CipherSuccessButton() : base()
        {
            Icon = Icons.Symbols.V.done;
            ColorStyle = ButtonStyle.Success;
            ColorShade = Shade.Dark;
        }
    }

    public partial class CipherPDFButton : CipherButton
    {
        public CipherPDFButton() : base()
        {
            HelpText = "ייצוא ל-PDF";
            Icon = Icons.Documents.picture_as_pdf;
            ColorStyle = ButtonStyle.Danger;
            ColorShade = Shade.Dark;
            Variant = Variant.Outlined;
        }
    }

    public partial class CipherNavButton : CipherButton
    {
        [Parameter]
        public string? Path { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private void NavigateToPath(MouseEventArgs args)
        {
            if (Path != null && NavigationManager != null) NavigationManager.NavigateTo(Path, forceLoad: true);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CipherButton>(0);
            builder.AddAttribute(1, "Icon", Icon);
            builder.AddAttribute(2, "Text", Text);
            builder.AddAttribute(3, "Width", Width);
            builder.AddAttribute(4, "HelpText", HelpText);
            builder.AddAttribute(5, "Size", Size);
            builder.AddAttribute(6, "Variant", Variant);
            builder.AddAttribute(7, "ButtonTextAlign", ButtonTextAlign);
            builder.AddAttribute(8, "HelpTextPosition", HelpTextPosition);
            builder.AddAttribute(9, "Height", Height);
            builder.AddAttribute(10, "MarginBottom", MarginBottom);
            builder.AddAttribute(11, "ColorShade", ColorShade);
            builder.AddAttribute(12, "ColorStyle", ColorStyle);
            builder.AddAttribute(13, "Style", Style);
            builder.AddAttribute(14, "Disabled", Disabled);
            builder.AddAttribute(15, "Click", EventCallback.Factory.Create<MouseEventArgs>(this, NavigateToPath));
            builder.CloseComponent();
        }
    }
}

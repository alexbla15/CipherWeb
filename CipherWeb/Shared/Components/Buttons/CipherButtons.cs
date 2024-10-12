using CipherWeb.Data;
using CipherData.General;
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
            Icon = Icons.Symbols.Plus.add;
            Padding = "5px";
            Variant = Variant.Outlined;
        }
    }

    public partial class CipherAddResourceBtn : CipherNavButton
    {
        public CipherAddResourceBtn() : base()
        {
            Icon = Icons.Symbols.Plus.add_circle_outline;
            Variant = Variant.Outlined;
        }
    }

    public partial class CipherAddProcessDefinitionBtn : CipherAddResourceBtn
    {
        public CipherAddProcessDefinitionBtn() : base()
        {
            Path = CipherNavLinks.AddProcessDefinition.Href;
            HelpText = "הוספת תהליך";
        }
    }

    public partial class CipherAddCategoryBtn : CipherAddResourceBtn
    {
        public CipherAddCategoryBtn() : base()
        {
            Path = CipherNavLinks.AddCategory.Href;
            HelpText = "הוספת קטגוריה";
        }
    }

    public partial class CipherAddPackageBtn : CipherAddResourceBtn
    {
        public CipherAddPackageBtn() : base()
        {
            Path = CipherNavLinks.AddPackage.Href;
            HelpText = "הוספת תעודה";
        }
    }

    public partial class CipherAddSystemBtn : CipherAddResourceBtn
    {
        public CipherAddSystemBtn() : base()
        {
            Path = CipherNavLinks.AddStorageSystem.Href;
            HelpText = "הוספת מערכת";
        }
    }

    public partial class CipherAddUnitBtn : CipherAddResourceBtn
    {
        public CipherAddUnitBtn() : base()
        {
            Path = CipherNavLinks.AddUnit.Href;
            HelpText = "הוספת יחידה";
        }
    }

    public partial class CipherUpdateResourceBtn : CipherNavButton
    {
        [Parameter]
        public string? ObjectId { get; set; }

        [Parameter]
        public CipherNavLink? NavLink { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CipherNavButton>(0);
            builder.AddAttribute(1, "ObjectId", ObjectId);
            builder.AddAttribute(2, "NavLink", NavLink);
            builder.AddAttribute(3, "Path", $"{NavLink?.Href}?Id={ObjectId}");
            builder.AddAttribute(4, "Disabled", ObjectId is null);
            builder.AddAttribute(5, "Variant", Variant.Outlined);
            builder.AddAttribute(6, "HelpText", "עריכת נתונים");
            builder.AddAttribute(7, "Icon", Icons.Documents.Edit.edit);
            builder.CloseComponent();
        }
    }

    public partial class CipherUpdateCategoryBtn : CipherUpdateResourceBtn
    {
        public CipherUpdateCategoryBtn() : base()
        {
            NavLink = CipherNavLinks.UpdateCategory;
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

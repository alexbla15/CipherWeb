using CipherData;
using CipherWeb.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;

using System.Text;

namespace CipherWeb.Shared.Components.Buttons
{
    public partial class CipherAddBtn : CipherButton
    {
        public CipherAddBtn() : base()
        {
            Icon = Icons.Add;
            Padding = "5px";
        }
    }

    public partial class CipherCancelButton : CipherButton
    {
        public CipherCancelButton() : base()
        {
            Variant = Variant.Outlined;
            ColorStyle = ButtonStyle.Danger;
            ColorShade = Shade.Default;
            Size = ButtonSize.Large;
            Icon = Icons.Cancel;
            Text = "ביטול";
        }
    }

    public partial class CipherCloseButton : CipherButton
    {
        public CipherCloseButton() : base()
        {
            ColorStyle = ButtonStyle.Danger;
            Icon = Icons.Close;
        }
    }

    public partial class CipherDeleteButton : CipherButton
    {
        public CipherDeleteButton() : base()
        {
            ColorStyle = ButtonStyle.Danger;
            Icon = Icons.Delete;
        }
    }

    public partial class CipherEditButton : CipherButton
    {
        public CipherEditButton() : base()
        {
            ColorStyle = ButtonStyle.Primary;
            Icon = Icons.Edit;
        }
    }

    public partial class CipherSearchButton: CipherButton
    {
        public CipherSearchButton() : base()
        { 
            Icon = Icons.Search;
        }
    }

    public partial class CipherSubmitButton : CipherButton
    {
        public CipherSubmitButton() : base()
        {
            ButtonType = ButtonType.Submit;
            Size = ButtonSize.Large;
            Icon = Icons.Save;
            Text = "שמירה";
            ColorStyle = ButtonStyle.Success;
            ColorShade = Shade.Default;
        }
    }

    public partial class CipherSuccessButton : CipherButton
    {
        public CipherSuccessButton() : base()
        {
            Icon = Icons.Done;
            ColorStyle = ButtonStyle.Success;
        }
    }

    public partial class CipherPDFButton : CipherButton
    {
        public CipherPDFButton() : base()
        {
            HelpText = "ייצוא ל-PDF";
            Icon = Icons.PDF;
            ColorStyle = ButtonStyle.Danger;
            ColorShade = Shade.Default;
            Variant = Variant.Outlined;
        }
    }

    public partial class CipherNavButton : CipherButton
    {
        [Parameter]
        public string Path { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private void NavigateToPath(MouseEventArgs args)
        {
            NavigationManager.NavigateTo(Path);
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
            builder.AddAttribute(8, "Click", EventCallback.Factory.Create<MouseEventArgs>(this, NavigateToPath));
            builder.CloseComponent();
        }
    }
}

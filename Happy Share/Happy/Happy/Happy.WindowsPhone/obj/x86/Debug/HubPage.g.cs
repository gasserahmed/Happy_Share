﻿

#pragma checksum "W:\Microsoft\Applictaions\Happy Share\Happy\Happy\Happy.WindowsPhone\HubPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "09EF5B80CD304E3E992B868876D54A77"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Happy
{
    partial class HubPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 161 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.video_page;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 153 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.image_page;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 145 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.quotes_page;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 139 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.tips_page;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 85 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.my_list_Loaded;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 110 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.post_button;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 111 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.post_text_Loaded;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 96 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Share;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 191 "..\..\..\HubPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.About_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



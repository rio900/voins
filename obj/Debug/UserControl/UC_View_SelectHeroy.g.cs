﻿

#pragma checksum "D:\Unity\VoinGit\voins\UserControl\UC_View_SelectHeroy.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BC6E123401167DCFD815962517A9AF19"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Voins
{
    partial class UC_View_SelectHeroy : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 122 "..\..\UserControl\UC_View_SelectHeroy.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.InputChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 132 "..\..\UserControl\UC_View_SelectHeroy.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.C_Group_SelectionChanged;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



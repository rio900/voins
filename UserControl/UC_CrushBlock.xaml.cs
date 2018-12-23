using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Voins.AppCode;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Voins
{
    public sealed partial class UC_CrushBlock : UserControl, IGameControl
    {
        Canvas _rootCanvas;
        public Canvas RootCanvas
        {
            get
            {
                return _rootCanvas;
            }
            set
            {
                _rootCanvas = value;
            }
        }
        public UC_CrushBlock()
        {
            this.InitializeComponent();
        }

        EAngel _angel = EAngel.Left;
        public EAngel Angel
        {
            get
            {
                return _angel;
            }
            set
            {
                _angel = value;
            }
        }

        public void ChengAngel(EAngel Angel)
        {
        }
        public void GetDemage(string value)
        {
            C_DemageView.Text = value;
            St_GetDemage.Begin();
        }
        public void Silenced(bool visibilitySilenc)
        {
            if (visibilitySilenc)
                C_Silence.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                C_Silence.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        public void Hexed(bool visibilityHex)
        {
            if (visibilityHex)
                C_Hex.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                C_Hex.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        public void Remove(Canvas rootCanvas) { }
        public void ShowAttack(EAngel angel, double attackSpeed) { }
    }
}

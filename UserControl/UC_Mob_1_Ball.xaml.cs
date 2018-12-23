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
    public sealed partial class UC_Mob_1_Ball : UserControl, IGameControl, IUnitControl
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
        public UC_Mob_1_Ball()
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

        public void ChengAngel(EAngel angel)
        {
            if (angel != Angel)
            {
                Angel = angel;

                C_Top.Visibility = Visibility.Collapsed;
                C_Left.Visibility = Visibility.Collapsed;
                C_Right.Visibility = Visibility.Collapsed;
                C_Bot.Visibility = Visibility.Collapsed;

                if (angel == EAngel.Top)
                    C_Top.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Left)
                    C_Left.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Right)
                    C_Right.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Bottom)
                    C_Bot.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        public void ShowHealth(int helth, int maxHels)
        {
            C_Heals.Maximum = maxHels;
            C_Heals.Value = helth;

            St_ShowHeals.Begin();
        }

        public void GetDemage(string value)
        {
            C_Demage.Text = value;
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

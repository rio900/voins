﻿using System;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Voins
{
    public sealed partial class UC_Jinx_Zap_Roket : UserControl, IGameControl
    {
        public int ExplosionStyle { get; set; }

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

        public UC_Jinx_Zap_Roket()
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
            Angel = angel;

            if (ExplosionStyle == 1)
            {

                FrameworkElement[] all = { C_Left1 ,
                                           C_Right1,
                                           C_Top1,
                                           C_Bottom1
                };
                foreach (var item in all)
                    item.Visibility = Visibility.Collapsed;

                if (angel == EAngel.Top)
                    C_Top1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Left)
                    C_Left1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Right)
                    C_Right1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Bottom)
                    C_Bottom1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {

                FrameworkElement[] all = { C_Left ,
                                     C_Right,
                                     C_Top,
                                     C_Bottom,
                                     C_RoketBottom,
                                     C_RoketLeft,
                                     C_RoketRight,
                                     C_RoketTop};
                foreach (var item in all)
                    item.Visibility = Visibility.Collapsed;

                if (angel == EAngel.Top)
                    C_Top.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Left)
                    C_Left.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Right)
                    C_Right.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (angel == EAngel.Bottom)
                    C_Bottom.Visibility = Windows.UI.Xaml.Visibility.Visible;

            }
        }

        public void ChengAngelCenter()
        {
            if (ExplosionStyle == 1)
            {
                FrameworkElement[] all = { C_Left1 ,
                                           C_Right1,
                                           C_Top1,
                                           C_Bottom1
                };
                foreach (var item in all)
                    item.Visibility = Visibility.Collapsed;

                C_Center1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                FrameworkElement[] all = { C_Left ,
                                       C_Right,
                                       C_Top,
                                       C_Bottom,
                                       C_RoketBottom,
                                       C_RoketLeft,
                                       C_RoketRight,
                                       C_RoketTop};

                foreach (var item in all)
                    item.Visibility = Visibility.Collapsed;

                C_Center.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        public void ChengAngelArrow(EAngel angel)
        {

            FrameworkElement[] all = { C_Left ,
                                     C_Right,
                                     C_Top,
                                     C_Bottom,
                                     C_RoketBottom,
                                     C_RoketLeft,
                                     C_RoketRight,
                                     C_RoketTop};
            foreach (var item in all)
                item.Visibility = Visibility.Collapsed;

            if (angel == EAngel.Top)
                C_RoketTop.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (angel == EAngel.Left)
                C_RoketLeft.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (angel == EAngel.Right)
                C_RoketRight.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (angel == EAngel.Bottom)
                C_RoketBottom.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        public void GetDemage(string value) { }
        public void Silenced(bool visibilitySilenc)
        {

        }
        public void Hexed(bool visibilityHex)
        {

        }
        public void Remove(Canvas rootCanvas)
        {
            RootCanvas = rootCanvas;
            St_Remove.Completed += St_Remove_Completed;
            St_Remove.Begin();
        }
        public void ShowAttack(EAngel angel, double attackSpeed) { }
        void St_Remove_Completed(object sender, object e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            RootCanvas.Children.Remove(this);
        }

        internal void LoadStyle(int explosionStyle)
        {
            ExplosionStyle = explosionStyle;
            if (explosionStyle == 1)
            {
                C_Jinx.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                C_Black.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                C_Jinx.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_Black.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

        }
    }
}

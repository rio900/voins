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
    public sealed partial class UC_Jakiro_IcePath : UserControl, IGameControl, IAnimationControl
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

        public UC_Jakiro_IcePath()
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



        public void StartMuveAnimation(double duration)
        {
            St_Anomation.Begin();
        }

        public void Pause()
        {
            St_Anomation.Pause();
        }

        public void StopPause()
        {
            St_Anomation.Resume();
        }
    }
}

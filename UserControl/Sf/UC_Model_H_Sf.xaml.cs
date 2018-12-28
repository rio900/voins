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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Voins
{
    public sealed partial class UC_Model_H_Sf : UserControl, IAnimationControl
    {
        Storyboard _firstTimer;

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

        private int _playerNumber = 1;

        public int PlayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; }
        }

        public UC_Model_H_Sf()
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

        int animationCounter = 0;
        List<Image> _caderList;

        public void ChengAngel(EAngel angel)
        {
            Angel = angel;

            foreach (var item in C_Root.Children)
            {
                Image itemImage = item as Image;
                if (itemImage != null)
                    itemImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
       

            if (angel == EAngel.Top)
                C_1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (angel == EAngel.Left)
                C_2_Left.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (angel == EAngel.Right)
                C_3_Right.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (angel == EAngel.Bottom)
                C_4_Bottom.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        public void StartMuveAnimation(double duration)
        {
            animationCounter = 0;

            if (Angel == EAngel.Top)
                _caderList = new List<Image>() { C_1, C_1_1, C_1_2 };
            else if (Angel == EAngel.Left)
                _caderList = new List<Image>() { C_2_Left, C_2_Left_1, C_2_Left_2 };
            else if (Angel == EAngel.Right)
                _caderList = new List<Image>() { C_3_Right, C_3_Right_1, C_3_Right_2 };
            else if (Angel == EAngel.Bottom)
                _caderList = new List<Image>() { C_4_Bottom, C_4_Bottom_1, C_4_Bottom_2 };

            _caderList[0].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _caderList[1].Visibility = Windows.UI.Xaml.Visibility.Visible;

            _firstTimer = new Storyboard() { Duration = TimeSpan.FromSeconds(duration / 3) };
            _firstTimer.Completed += _firstTimer_Completed;
            _firstTimer.Begin();
        }

        void _firstTimer_Completed(object sender, object e)
        {
            foreach (var item in C_Root.Children)
            {
                Image itemImage = item as Image;
                if (itemImage != null)
                    itemImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            if (animationCounter == 0)
            {
                _caderList[2].Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (animationCounter == 1)
            {
                _caderList[1].Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (animationCounter == 2)
            {
                _caderList[0].Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            if (animationCounter == 2)
            {
                _firstTimer.Completed -= _firstTimer_Completed;
                _firstTimer = null;
            }
            else
            {
                _firstTimer.Begin();
                animationCounter++;
            }
        }

        public void Pause()
        {
            if (_firstTimer != null)
                _firstTimer.Pause();
        }

        public void StopPause()
        {
            if (_firstTimer != null)
                _firstTimer.Resume();
        }
    }
}

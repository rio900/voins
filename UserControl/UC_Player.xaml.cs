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
    public sealed partial class UC_Player : UserControl, IGameControl, IUnitControl, IAnimationControl
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

        private int _playerNumber = 1;

        public int PlayerNumber
        {
            get { return _playerNumber; }
            set { _playerNumber = value; }
        }

        public UC_Player()
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

        List<Image> _imageStoryboardList;
        /// <summary>
        /// Список картинок которые анимируются
        /// </summary>
        public List<Image> ImageStoryboardList
        {
            get
            {
                return _imageStoryboardList;
            }
            set
            {
                _imageStoryboardList = value;
            }
        }

        public void ChengAngel(EAngel angel)
        {
            Angel = angel;

            if (PlayerNumber == 1)
                C_Model_H_Bonik.ChengAngel(angel);
            else if (PlayerNumber == 2)
                C_Model_H_Alhim.ChengAngel(angel);
            else if (PlayerNumber == 3)
                C_Model_H_Sniper.ChengAngel(angel);
            else if (PlayerNumber == 4)
                C_Model_H_Jakiro.ChengAngel(angel);
            else if (PlayerNumber == 5)
                C_Model_H_Mirana.ChengAngel(angel);
            else if (PlayerNumber == 6)
                C_Model_H_Jinx.ChengAngel(angel);
            else
                C_Model_Player1.ChengAngel(angel);

        }

        public void StartMuveAnimation(double duration)
        {
            duration = duration - 0.1;
            if (PlayerNumber == 1)
                C_Model_H_Bonik.StartMuveAnimation(duration);
            else if (PlayerNumber == 2)
                C_Model_H_Alhim.StartMuveAnimation(duration);
            else if (PlayerNumber == 3)
                C_Model_H_Sniper.StartMuveAnimation(duration);
            else if (PlayerNumber == 4)
                C_Model_H_Jakiro.StartMuveAnimation(duration);
            else if (PlayerNumber == 5)
                C_Model_H_Mirana.StartMuveAnimation(duration);
            else if (PlayerNumber == 6)
                C_Model_H_Jinx.StartMuveAnimation(duration);
            else
                C_Model_Player1.StartMuveAnimation(duration);
        }

        public void GetDemage(string value)
        {
            C_Demage.Text = value;
            St_GetDemage.Begin();
        }

        public void ShowAttack(EAngel angel, double attackSpeed)
        {
            DispatcherTimer dtr = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.25) };
            dtr.Tick += dtr_Tick;
            dtr.Start();
            if (angel == EAngel.Top)
            {
                C_TopAttack.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (angel == EAngel.Left)
            {
                C_LeftAttack.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (angel == EAngel.Right)
            {
                C_RightAttack.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (angel == EAngel.Bottom)
            {
                C_BotAttack.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        void dtr_Tick(object sender, object e)
        {
            ((DispatcherTimer)sender).Stop();
            C_TopAttack.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_LeftAttack.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_RightAttack.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_BotAttack.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void UpLevel()
        {
            path.Visibility = Windows.UI.Xaml.Visibility.Visible;
            St_UpLevel.Begin();
        }
        public void HiddeUpLevel()
        {
            path.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void Silenced(bool visibilitySilenc)
        {
            if (visibilitySilenc)
                C_Silence.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                C_Silence.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        public void Remove(Canvas rootCanvas) { }
        public void ShowHealth(int helth, int maxHels)
        {
            C_Heals.Maximum = maxHels;
            C_Heals.Value = helth;

            St_ShowHeals.Begin();
        }


        public void Pause()
        {
            C_Model_Player1.Pause();
            C_Model_H_Bonik.Pause();
            C_Model_H_Alhim.Pause();
        }

        public void StopPause()
        {
            C_Model_Player1.StopPause();
            C_Model_H_Bonik.StopPause();
            C_Model_H_Alhim.StopPause();
        }
    }
}

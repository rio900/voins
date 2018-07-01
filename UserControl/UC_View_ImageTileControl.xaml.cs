using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Voins.AppCode;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class UC_View_ImageTileControl : UserControl
    {
        double _counter;

        ISpell _spell;
        public ISpell Spell { get { return _spell; } set { _spell = value; } }
        DispatcherTimer _mouveTimerCuldaun;
        public UC_View_ImageTileControl(string tileName, ISpell spell)
        {
            this.InitializeComponent();

            _spell = spell;
            _spell.PausedEvent += _spell_PausedEvent;
            _spell.StartUseSpell += _spell_StartUseSpell;
            St_Culdaun.Completed += St_Culdaun_Completed;
            ChengImage(tileName);
        }

        public void ChengImage(string tileName)
        {
            for (int i = 0; i < C_Root.Children.Count; i++)
            {
                Image img = C_Root.Children[i] as Image;
                if (img != null)
                    img.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            for (int i = 0; i < C_Root.Children.Count; i++)
            {
                if ("C_" + tileName == (C_Root.Children[i] as FrameworkElement).Name)
                    (C_Root.Children[i] as FrameworkElement).Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        void _spell_PausedEvent(object sender, EventArgs e)
        {
            if (_spell.Paused)
                Pause();
            else
                StopPause();
        }

        void _spell_StartUseSpell(object sender, EventArgs e)
        {
            _counter = 0;
            C_Culdaun.Visibility = Windows.UI.Xaml.Visibility.Visible;
            if (Spell.Duration != 0)
            {
                C_Timer.Text = Spell.Duration.ToString();
                C_Timer.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            _mouveTimerCuldaun = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            _mouveTimerCuldaun.Tick += mouveTimerCuldaun_Tick;
            _mouveTimerCuldaun.Start();

            St_Culdaun.Duration = TimeSpan.FromSeconds(_spell.Culdaun);
            foreach (var item in St_Culdaun.Children)
            {
                DoubleAnimationUsingKeyFrames da = item as DoubleAnimationUsingKeyFrames;
                if (da != null)
                {
                    da.KeyFrames[1].KeyTime = TimeSpan.FromSeconds(_spell.Culdaun);
                    da.Duration = TimeSpan.FromSeconds(_spell.Culdaun);
                }
            }
            St_Culdaun.Begin();
        }
        void St_Culdaun_Completed(object sender, object e)
        {
            C_Culdaun.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            St_Completed.Begin();
        }

        void mouveTimerCuldaun_Tick(object sender, object e)
        {
            _counter++;

            if (_counter >= _spell.Duration)
            {
                ((DispatcherTimer)sender).Stop();
                C_Timer.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                C_Timer.Text = (_spell.Duration - _counter).ToString();
            }

        }
        public void UpSpell()
        {
            C_UpSpell.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void Pause()
        {
            if (_mouveTimerCuldaun != null)
                _mouveTimerCuldaun.Stop();

            St_Culdaun.Pause();
        }

        public void StopPause()
        {
            if (_mouveTimerCuldaun != null)
                _mouveTimerCuldaun.Start();

            St_Culdaun.Resume();
        }
    }
}

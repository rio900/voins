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
    public sealed partial class UC_View_ItemImage : UserControl
    {
        public int ImageIndex { get; set; }
        
        ISpell _spell;
        public ISpell Spell { get { return _spell; } set { _spell = value; } }

        double _counter;
        DispatcherTimer _mouveTimerCuldaun;

        public UC_View_ItemImage()
        {
            this.InitializeComponent();
        }

        public UC_View_ItemImage(int imageNumber)
        {
            this.InitializeComponent();

            ShowImage(imageNumber);
        }
         public void ShowPrice(int price)
         {
             C_PriceGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
             C_Price.Text = price.ToString();
         }
        
        public void ShowImage(int imageNumber)
        {
            ImageIndex = imageNumber;
            for (int i = 0; i < C_Root.Children.Count; i++)
            {
                Image img = C_Root.Children[i] as Image;
                if (img != null && imageNumber == i)
                    img.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else if (img != null)
                    img.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        public void ApplayMargin(Thickness margin)
        {
            for (int i = 0; i < C_Root.Children.Count; i++)
            {
                Image img = C_Root.Children[i] as Image;
                if (img != null)
                    img.Margin = margin;
            }
        }
        /// <summary>
        /// Если данный итем можно использовать
        /// </summary>
        public void UsingItem(ISpell spell)
        {
            _spell = spell;
            _spell.PausedEvent += _spell_PausedEvent;
            _spell.StartUseSpell += _spell_StartUseSpell;
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
        /// <summary>
        /// Если вызвать этот метод итем станет рецептом
        /// </summary>
        public void Recept()
        {
            C_IsRecept.Visibility = Windows.UI.Xaml.Visibility.Visible;
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

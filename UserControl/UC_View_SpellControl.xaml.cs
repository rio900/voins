using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Voins.AppCode;
using Voins.Spell;
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
    public sealed partial class UC_View_SpellControl : UserControl
    {
        private Player _player;
        public Player CurrentPlayer
        {
            get { return _player; }
            set { _player = value; }
        }

        public UC_View_SpellControl()
        {
            this.InitializeComponent();
          
            for (int i = 0; i < C_UpGrid.Children.Count; i++)
                C_UpGrid.Children[i].Tapped += UC_View_SpellControl_Tapped;
        }

        void UC_View_SpellControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int indexUpSpell = C_UpGrid.Children.IndexOf(sender as UIElement);
            CurrentPlayer.UpPoint -= 1;
            ///Тут учитываем что первый скил был мув
            CurrentPlayer.Spells[indexUpSpell + 1].UpSpell(CurrentPlayer);

            C_UpGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            for (int i = 0; i < C_UpGrid.Children.Count; i++)
                C_UpGrid.Children[i].Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (CurrentPlayer.UpPoint > 0)
                LevelUpMethod();
        }

        public void Show()
        {
            if (CurrentPlayer != null)
            {
                CurrentPlayer.UpdateItemPlayerData += CurrentPlayer_UpdateItemPlayerData;
                CurrentPlayer.LevelUpEvent += CurrentPlayer_LevelUpEvent;
                CurrentPlayer.SpellUpedEvent += CurrentPlayer_SpellUpedEvent;

                ShowSkills();
            }
        }

        void CurrentPlayer_SpellUpedEvent(object sender, EventArgs e)
        {
            CurrentPlayer.AllowUpSpell = false;
            for (int i = 0; i < C_UpGrid.Children.Count; i++)
                C_UpGrid.Children[i].Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_UpGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            C_KeyUpGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if(CurrentPlayer.UpPoint > 0)
                LevelUpMethod();
        }

        void CurrentPlayer_LevelUpEvent(object sender, EventArgs e)
        {
            LevelUpMethod();
        }

        private void LevelUpMethod()
        {
            C_UpGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;

            for (int i = 0; i < 4; i++)
            {
                if (CurrentPlayer.Spells[i + 1].Name != CurrentPlayer.PreviousSkill && i != 3 && CurrentPlayer.Spells[i + 1].LevelCast < CurrentPlayer.Spells[i + 1].MaxLevelCast)
                    C_UpGrid.Children[i].Visibility = Windows.UI.Xaml.Visibility.Visible;

                ///Отображение квадратика для ульты
                if(i == 3
                    && CurrentPlayer.Level > 4
                    && CurrentPlayer.Spells.Single(p=>p.IsUlt).LevelCast == 0)
                    C_UpGrid.Children[i].Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        void CurrentPlayer_UpdateItemPlayerData(object sender, EventArgs e)
        {
        }

        public void KeyUpSpellView(bool viewUpSpellPanel) {

            if (viewUpSpellPanel)
                C_KeyUpGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                C_KeyUpGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        /// <summary>
        /// Отображение скилов 
        /// </summary>
        private void ShowSkills()
        {
            C_Spells.Children.Clear();

            int index =0;
            foreach (var item in CurrentPlayer.Spells)
            {
                if (item.GetType() != typeof(SP_Move))
                {
                    item.ImageTile.Width = 35;
                    item.ImageTile.Height = 35;
                    item.ImageTile.Margin = new Thickness(3, 0, 0, 0);
                    C_Spells.Children.Add(item.ImageTile);

                    /////Запись то какой скил нельзя будет качать на след уровне
                    //if (item.LevelCast != 0)
                    //    _beforSkill = index;

                    index++;
                }
            }
        }
    }
}

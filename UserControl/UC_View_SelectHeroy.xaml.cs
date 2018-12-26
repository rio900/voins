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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Voins
{
    public sealed partial class UC_View_SelectHeroy : UserControl
    {
        private Player _returnPlayer;

        public Player ReturnPlayer
        {
            get { return _returnPlayer; }
            set { _returnPlayer = value; }
        }

        private List<Player> _players;

        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        private int _inputTypeNumber;
        public int InputTypeNumber
        {
            get { return _inputTypeNumber; }
            set { _inputTypeNumber = value; }
        }

        private int _groupNumber;
        public int GroupNumber
        {
            get { return _groupNumber; }
            set { _groupNumber = value; }
        }

        int _selectHero;
        public UC_View_SelectHeroy()
        {
            this.InitializeComponent();

            Players = new List<Player>() { 
            UnitGenerator.Player_Alchemist("Alchemist",0,null),
            UnitGenerator.Player_Bonik("Fire Bonik",0,null),
            UnitGenerator.Player_Sniper("Sniper",0,null),
            UnitGenerator.Player_Jakiro("Jakiro",0,null),
            UnitGenerator.Player_Mirana("Mirana",0,null),
            UnitGenerator.Player_Jinx("Jinx",0,null),
            UnitGenerator.Player_Nature("Nature",0,null)
            };
            C_HeroList.SelectionChanged += C_HeroList_SelectionChanged;
            C_HeroList.SelectedIndex = 1;
            C_SpallList.SelectionChanged += C_SpallList_SelectionChanged;
            C_SpallList.SelectedIndex = 1;
        }

        void C_HeroList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectHero = C_HeroList.SelectedIndex;
            ReturnPlayer = Players[C_HeroList.SelectedIndex];
            foreach (var item in C_ViewGrid.Children)
                (item as FrameworkElement).Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (C_HeroList.SelectedIndex == 0)
                C_View0.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (C_HeroList.SelectedIndex == 1)
                C_View1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (C_HeroList.SelectedIndex == 2)
                C_View2.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (C_HeroList.SelectedIndex == 3)
                C_View3.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (C_HeroList.SelectedIndex == 4)
                C_View4.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else if (C_HeroList.SelectedIndex == 5)
                C_View5.Visibility = Windows.UI.Xaml.Visibility.Visible;

            C_Name.Text = ReturnPlayer.Name;

            C_Agility.Foreground = new SolidColorBrush(Colors.White);
            C_Intelligence.Foreground = new SolidColorBrush(Colors.White);
            C_Strength.Foreground = new SolidColorBrush(Colors.White);

            if (ReturnPlayer.HeroType == EHeroType.Agility)
                C_Agility.Foreground = new SolidColorBrush(Colors.DarkRed);
            else if (ReturnPlayer.HeroType == EHeroType.Intelligence)
                C_Intelligence.Foreground = new SolidColorBrush(Colors.DarkRed);
            else if (ReturnPlayer.HeroType == EHeroType.Strength)
                C_Strength.Foreground = new SolidColorBrush(Colors.DarkRed);

            C_Agility.Text = ReturnPlayer.Agility.ToString();
            C_Intelligence.Text = ReturnPlayer.Intelligence.ToString();
            C_Strength.Text = ReturnPlayer.Strength.ToString();

            C_Agility_Plus.Text = ReturnPlayer.AddAgility.ToString();
            C_Intelligence_Plus.Text = ReturnPlayer.AddIntelligence.ToString();
            C_Strength_Plus.Text = ReturnPlayer.AddStrength.ToString();

            C_Health.Text = ReturnPlayer.Health.ToString();
            C_Mana.Text = ReturnPlayer.Mana.ToString();
            C_Armor.Text = ReturnPlayer.Arrmor.ToString();

            C_SpallList.Items.Clear();
            foreach (var item in ReturnPlayer.Spells)
            {
                if (item.GetType() != typeof(Spell.SP_Move))
                {
                    item.ImageTile.UpSpell();
                    C_SpallList.Items.Add(item.ImageTile);
                }
            }
            if (C_SpallList.SelectedIndex != -1)
                C_SpallList.SelectedIndex = 0;
        }

        void C_SpallList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (C_SpallList.SelectedIndex != -1)
            {
                C_SpallLevels.Text = ReturnPlayer.Spells[C_SpallList.SelectedIndex + 1].SpellDescriptionInfo.LevelDescription;
                C_SpallDescription.Text = ReturnPlayer.Spells[C_SpallList.SelectedIndex + 1].SpellDescriptionInfo.Description;
                C_SpallName.Text = ReturnPlayer.Spells[C_SpallList.SelectedIndex + 1].Name;
            }
        }

        public Player ReturnHero()
        {
            Player retPlayer = null;

            if (_selectHero != -1)
            {
                switch (_selectHero)
                {
                    case 0: retPlayer = UnitGenerator.Player_Alchemist("Alchemist", 2, null); break;
                    case 1: retPlayer = UnitGenerator.Player_Bonik("Fire Bonik", 1, null); break;
                    case 2: retPlayer = UnitGenerator.Player_Sniper("Sniper", 3, null); break;
                    case 3: retPlayer = UnitGenerator.Player_Jakiro("Jakiro", 4, null); break;
                    case 4: retPlayer = UnitGenerator.Player_Mirana("Mirana", 5, null); break;
                    case 5: retPlayer = UnitGenerator.Player_Jinx("Jinx", 6, null); break;
                    case 6: retPlayer = UnitGenerator.Player_Nature("Nature", 7, null); break;
                }
            }

            return retPlayer;
        }

        private void C_Group_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupNumber = ((ComboBox)sender).SelectedIndex;
        }

        private void InputChanged(object sender, SelectionChangedEventArgs e)
        {
            InputTypeNumber = ((ComboBox)sender).SelectedIndex;
        }

        public void GetName(string name)
        {
            C_NameTextBox.Text = name;
        }
    }
}

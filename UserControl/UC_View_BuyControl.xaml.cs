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
    public sealed partial class UC_View_BuyControl : UserControl
    {
        List<ItemClass> _itemsEasy;
        List<ItemClass> _itemsNormal;
        List<ItemClass> _itemsStrong;
        List<FrameworkElement> _itemsFelementEasy = new List<FrameworkElement>();
        List<FrameworkElement> _itemsFelementNormal = new List<FrameworkElement>();
        List<FrameworkElement> _itemsFelementStrong = new List<FrameworkElement>();

        private Player _player;
        public Player CurrentPlayer
        {
            get { return _player; }
            set { _player = value; }
        }

        public UC_View_BuyControl()
        {
            this.InitializeComponent();

            _itemsEasy = new List<ItemClass>() { 
             UnitGenerator.I1_SageMask(),
             UnitGenerator.I2_RobeOfTheMagi(),
             UnitGenerator.I3_Quarterstuff(),
             UnitGenerator.I6_BootsOfSpeed(),
             UnitGenerator.I7_GlovesOfHaste(),
             UnitGenerator.I8_BeltOfStreangth(),
             UnitGenerator.I10_MithrilHammer(),
             UnitGenerator.I16_Javelin(),
             UnitGenerator.I18_VitalityBooster(),
             UnitGenerator.I21_BladesOfAttack(),
             UnitGenerator.I23_Boots_Of_Elvenskin(),
             UnitGenerator.I24_Blade_Of_Alacrity(),
             UnitGenerator.I26_Ogre_Club(),
             UnitGenerator.I30_ShadowAmulet(),
             UnitGenerator.I31_Claymore(),
             UnitGenerator.I33_Point_Booster(),
             UnitGenerator.I34_Staff_Of_Wizardry(),
             UnitGenerator.I37_Chainmail(),
             UnitGenerator.I39_VoidStone()
            };

            _itemsNormal = new List<ItemClass>() { 
             UnitGenerator.I9_PowerTreads(),
             UnitGenerator.I22_PhaseBoots(),
             UnitGenerator.I4_OblivionStuff(),
             UnitGenerator.I12_Maelstrom(),
             UnitGenerator.I15_DemonEdge(),
             UnitGenerator.I19_Reaver(),
             UnitGenerator.I25_Yasha(),
             UnitGenerator.I27_Sange(),
             UnitGenerator.I13_Hyperstone(),
             UnitGenerator.I38_Platemail(),
             UnitGenerator.I40_Ultimate_Orb(),
             UnitGenerator.I41_Mystic_Staff(),
             UnitGenerator.I45_TrevelBoots()

            };

            _itemsStrong = new List<ItemClass>() { 
             UnitGenerator.I5_Orchid(),
             UnitGenerator.I14_Mjollnir(),
             UnitGenerator.I17_MonkeyKingBar(),
             UnitGenerator.I20_HeartOfTarrasque(),
             UnitGenerator.I29_Sange_And_Yasha(),
             UnitGenerator.I32_ShadowBlade(),
             UnitGenerator.I35_Aghanims_Scepter(),
             UnitGenerator.I36_Desolator(),
             UnitGenerator.I38_AssaultCuirass(),
             UnitGenerator.I42_Scythe_Of_Vyse(),
             UnitGenerator.I43_Skadi(),
             UnitGenerator.I47_TrevelBoots2()
            };

            foreach (var item in _itemsEasy)
            {
                item.View.Width = 40;
                item.View.Height = 40;
                _itemsFelementEasy.Add(item.View);
            }
            C_Items.ItemsSource = _itemsFelementEasy;

            foreach (var item in _itemsNormal)
            {
                item.View.Width = 40;
                item.View.Height = 40;
                _itemsFelementNormal.Add(item.View);
            }
            C_ItemsNormal.ItemsSource = _itemsFelementNormal;

            foreach (var item in _itemsStrong)
            {
                item.View.Width = 40;
                item.View.Height = 40;
                _itemsFelementStrong.Add(item.View);
            }
            C_ItemsStrong.ItemsSource = _itemsFelementStrong;


            C_Items.SelectionChanged += C_Items_SelectionChanged;
            C_ItemsNormal.SelectionChanged += C_ItemsNormal_SelectionChanged;
            C_ItemsStrong.SelectionChanged += C_ItemsStrong_SelectionChanged;
            C_Items.SelectedIndex = 0;
            C_ItemsParts.SelectionChanged += C_ItemsParts_SelectionChanged;
        }

        void C_ItemsParts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (C_ItemsParts.SelectedIndex != -1)
            {
                UC_View_ItemImage itemShowed = C_Items.Items.FirstOrDefault(p =>
                    (p as UC_View_ItemImage).ImageIndex == (C_ItemsParts.SelectedItem as UC_View_ItemImage).ImageIndex) as UC_View_ItemImage;
                if (itemShowed != null)
                    C_Items.SelectedItem = itemShowed;
                else
                {
                    itemShowed = C_ItemsNormal.Items.FirstOrDefault(p =>
                (p as UC_View_ItemImage).ImageIndex == (C_ItemsParts.SelectedItem as UC_View_ItemImage).ImageIndex) as UC_View_ItemImage;
                    if (itemShowed != null)
                        C_ItemsNormal.SelectedItem = itemShowed;
                    else
                    {
                        itemShowed = C_ItemsStrong.Items.FirstOrDefault(p =>
              (p as UC_View_ItemImage).ImageIndex == (C_ItemsParts.SelectedItem as UC_View_ItemImage).ImageIndex) as UC_View_ItemImage;

                        if (itemShowed != null)
                            C_ItemsStrong.SelectedItem = itemShowed;
                        else
                        {
                          
                        }
                    }
                }
            }
            C_ItemsParts.SelectionChanged -= C_ItemsParts_SelectionChanged;
            C_ItemsParts.SelectedIndex = -1;
            C_ItemsParts.SelectionChanged += C_ItemsParts_SelectionChanged;
        }

        void C_ItemsStrong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            C_ItemsNormal.SelectionChanged -= C_ItemsNormal_SelectionChanged;
            C_Items.SelectionChanged -= C_Items_SelectionChanged;
            C_ItemsNormal.SelectedIndex = -1;
            C_Items.SelectedIndex = -1;
            C_ItemsNormal.SelectionChanged += C_ItemsNormal_SelectionChanged;
            C_Items.SelectionChanged += C_Items_SelectionChanged;

            if (C_ItemsStrong.SelectedIndex != -1)
                ShowItemDetail(C_ItemsStrong.SelectedItem as UC_View_ItemImage);
        }

        void C_ItemsNormal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            C_Items.SelectionChanged -= C_Items_SelectionChanged;
            C_ItemsStrong.SelectionChanged -= C_ItemsStrong_SelectionChanged;
            C_Items.SelectedIndex = -1;
            C_ItemsStrong.SelectedIndex = -1;
            C_Items.SelectionChanged += C_Items_SelectionChanged;
            C_ItemsStrong.SelectionChanged += C_ItemsStrong_SelectionChanged;

            if (C_ItemsNormal.SelectedIndex != -1)
                ShowItemDetail(C_ItemsNormal.SelectedItem as UC_View_ItemImage);
        }

        void C_Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            C_ItemsNormal.SelectionChanged -= C_ItemsNormal_SelectionChanged;
            C_ItemsStrong.SelectionChanged -= C_ItemsStrong_SelectionChanged;
            C_ItemsNormal.SelectedIndex = -1;
            C_ItemsStrong.SelectedIndex = -1;
            C_ItemsNormal.SelectionChanged += C_ItemsNormal_SelectionChanged;
            C_ItemsStrong.SelectionChanged += C_ItemsStrong_SelectionChanged;

            if (C_Items.SelectedIndex != -1)
                ShowItemDetail(C_Items.SelectedItem as UC_View_ItemImage);
        }

        private void ShowItemDetail(UC_View_ItemImage itemView)
        {
            UC_View_ItemImage.ShowImage(itemView.ImageIndex);

            foreach (var item in C_ItemStats.Children)
                (item as FrameworkElement).Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            ItemClass itemShowed = _itemsEasy.FirstOrDefault(p => p.View == itemView);
            if (itemShowed == null)
                itemShowed = _itemsNormal.FirstOrDefault(p => p.View == itemView);
            if (itemShowed == null)
                itemShowed = _itemsStrong.FirstOrDefault(p => p.View == itemView);

            C_Price.Text = itemShowed.Price.ToString();
            C_Name.Text = itemShowed.Name;
            C_DescriptionInfo.Text = itemShowed.Info.ShortDescription;
            if (!string.IsNullOrEmpty(itemShowed.Info.BuffDescription))
                C_BuffInfo.Text = itemShowed.Info.BuffDescription;
            else
                C_BuffInfo.Text = "";

            if (itemShowed.Demage != 0)
            {
                C_DemageView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_Demage.Text = itemShowed.Demage.ToString();
            }

            if (itemShowed.AttackSpeed != 0)
            {
                C_AttackSpeedView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_AttackSpeed.Text = (itemShowed.AttackSpeed * 100).ToString();
            }

            if (itemShowed.Speed != 0)
            {
                C_SpeedView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_Speed.Text = (itemShowed.Speed * 100).ToString();
            }

            if (itemShowed.ManaRegen != 0)
            {
                C_ManaRegenerationView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_ManaRegeneration.Text = itemShowed.ManaRegen.ToString();
            }

            if (itemShowed.HealthRegen != 0)
            {
                C_HealthRegenerationView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_HealthRegeneration.Text = itemShowed.HealthRegen.ToString();
            }

            if (itemShowed.ManaBonus != 0)
            {
                C_ManaBonusView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_ManaBonus.Text = itemShowed.ManaBonus.ToString();
            }

            if (itemShowed.HealthBonus != 0)
            {
                C_HealthBonusView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_HealthBonus.Text = itemShowed.HealthBonus.ToString();
            }

            if (itemShowed.HealthBonus != 0)
            {
                C_HealthBonusView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_HealthBonus.Text = itemShowed.HealthBonus.ToString();
            }

            if (itemShowed.Strength != 0)
            {
                C_StrengthView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_Strength.Text = itemShowed.Strength.ToString();
            }

            if (itemShowed.Agility != 0)
            {
                C_AgilityView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_Agility.Text = itemShowed.Agility.ToString();
            }

            if (itemShowed.Intelligence != 0)
            {
                C_IntelligenceView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_Intelligence.Text = itemShowed.Intelligence.ToString();
            }

            if (itemShowed.Armor != 0)
            {
                C_ArmorView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                C_Armor.Text = itemShowed.Armor.ToString();
            }

            if (itemShowed.Parts != null)
            {
                C_ItemsParts.Visibility = Windows.UI.Xaml.Visibility.Visible;
                List<FrameworkElement> partsView = new List<FrameworkElement>();
                foreach (var item in itemShowed.Parts)
                {
                    item.View.Width = 40;
                    item.View.Height = 40;
                    (item.View as UC_View_ItemImage).ShowPrice(item.Price);
                    partsView.Add(item.View);
                }
                C_ItemsParts.ItemsSource = partsView;
            }
            else
                C_ItemsParts.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }
        public void IndexLeft()
        {
            if (C_SaleGrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                if (C_Items.SelectedIndex != -1)
                {
                    if (C_Items.SelectedIndex - 1 >= 0)
                        C_Items.SelectedIndex -= 1;
                    else
                        C_ItemsStrong.SelectedIndex = C_ItemsStrong.Items.Count - 1;
                }

                else if (C_ItemsNormal.SelectedIndex != -1)
                {
                    if (C_ItemsNormal.SelectedIndex - 1 >= 0)
                        C_ItemsNormal.SelectedIndex -= 1;
                    else
                        C_Items.SelectedIndex = C_Items.Items.Count - 1;
                }

                else if (C_ItemsStrong.SelectedIndex != -1)
                {
                    if (C_ItemsStrong.SelectedIndex - 1 >= 0)
                        C_ItemsStrong.SelectedIndex -= 1;
                    else
                        C_ItemsNormal.SelectedIndex = C_ItemsNormal.Items.Count - 1;
                }
            }
            else
            {
                if (C_SellItems.SelectedIndex - 1 >= 0)
                    C_SellItems.SelectedIndex -= 1;
                else
                    C_SellItems.SelectedIndex = C_SellItems.Items.Count - 1;
            }
            //  C_Price.Text = _itemsEasy[C_Items.SelectedIndex].Price.ToString();
        }

        public void IndexRight()
        {
            if (C_SaleGrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                if (C_Items.SelectedIndex != -1)
                {
                    if (C_Items.SelectedIndex + 1 < C_Items.Items.Count)
                        C_Items.SelectedIndex += 1;
                    else
                        C_ItemsNormal.SelectedIndex = 0;
                }

                else if (C_ItemsNormal.SelectedIndex != -1)
                {
                    if (C_ItemsNormal.SelectedIndex + 1 < C_ItemsNormal.Items.Count)
                        C_ItemsNormal.SelectedIndex += 1;
                    else
                    {
                        C_ItemsStrong.SelectedIndex = 0;
                    }
                }

                else if (C_ItemsStrong.SelectedIndex != -1)
                {
                    if (C_ItemsStrong.SelectedIndex + 1 < C_ItemsStrong.Items.Count)
                        C_ItemsStrong.SelectedIndex += 1;
                    else
                        C_Items.SelectedIndex = 0;
                }
            }
            else
            {
                if (CurrentPlayer.Items.Any())
                {
                    if (C_SellItems.SelectedIndex + 1 < C_ItemsStrong.Items.Count)
                        C_SellItems.SelectedIndex += 1;
                    else
                        C_SellItems.SelectedIndex = 0;
                }
            }
            //  C_Price.Text = _itemsEasy[C_Items.SelectedIndex].Price.ToString();
        }

        public ItemClass BuyItem()
        {
            if (C_SaleGrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {

                ItemClass itemBuing = null;

                if (C_Items.SelectedIndex != -1)
                {
                    switch (C_Items.SelectedIndex)
                    {
                        case 0: itemBuing = UnitGenerator.I1_SageMask(); break;
                        case 1: itemBuing = UnitGenerator.I2_RobeOfTheMagi(); break;
                        case 2: itemBuing = UnitGenerator.I3_Quarterstuff(); break;
                        case 3: itemBuing = UnitGenerator.I6_BootsOfSpeed(); break;
                        case 4: itemBuing = UnitGenerator.I7_GlovesOfHaste(); break;
                        case 5: itemBuing = UnitGenerator.I8_BeltOfStreangth(); break;
                        case 6: itemBuing = UnitGenerator.I10_MithrilHammer(); break;
                        case 7: itemBuing = UnitGenerator.I16_Javelin(); break;
                        case 8: itemBuing = UnitGenerator.I18_VitalityBooster(); break;
                        case 9: itemBuing = UnitGenerator.I21_BladesOfAttack(); break;
                        case 10: itemBuing = UnitGenerator.I23_Boots_Of_Elvenskin(); break;
                        case 11: itemBuing = UnitGenerator.I24_Blade_Of_Alacrity(); break;
                        case 12: itemBuing = UnitGenerator.I26_Ogre_Club(); break;
                        case 13: itemBuing = UnitGenerator.I30_ShadowAmulet(); break;
                        case 14: itemBuing = UnitGenerator.I31_Claymore(); break;
                        case 15: itemBuing = UnitGenerator.I33_Point_Booster(); break;
                        case 16: itemBuing = UnitGenerator.I34_Staff_Of_Wizardry(); break;
                        case 17: itemBuing = UnitGenerator.I37_Chainmail(); break;
                        case 18: itemBuing = UnitGenerator.I39_VoidStone(); break;
                    }
                }
                else if (C_ItemsNormal.SelectedIndex != -1)
                {
                    switch (C_ItemsNormal.SelectedIndex)
                    {
                        case 0: itemBuing = UnitGenerator.I9_PowerTreads(); break;
                        case 1: itemBuing = UnitGenerator.I22_PhaseBoots(); break;
                        case 2: itemBuing = UnitGenerator.I4_OblivionStuff(); break;
                        case 3: itemBuing = UnitGenerator.I12_Maelstrom(); break;
                        case 4: itemBuing = UnitGenerator.I15_DemonEdge(); break;
                        case 5: itemBuing = UnitGenerator.I19_Reaver(); break;
                        case 6: itemBuing = UnitGenerator.I25_Yasha(); break;
                        case 7: itemBuing = UnitGenerator.I27_Sange(); break;
                        case 8: itemBuing = UnitGenerator.I13_Hyperstone(); break;
                        case 9: itemBuing = UnitGenerator.I38_Platemail(); break;
                        case 10: itemBuing = UnitGenerator.I40_Ultimate_Orb(); break;
                        case 11: itemBuing = UnitGenerator.I41_Mystic_Staff(); break;
                        case 12: itemBuing = UnitGenerator.I45_TrevelBoots(); break;
                    }

                }
                else if (C_ItemsStrong.SelectedIndex != -1)
                {
                    switch (C_ItemsStrong.SelectedIndex)
                    {
                        case 0: itemBuing = UnitGenerator.I5_Orchid(); break;
                        case 1: itemBuing = UnitGenerator.I14_Mjollnir(); break;
                        case 2: itemBuing = UnitGenerator.I17_MonkeyKingBar(); break;
                        case 3: itemBuing = UnitGenerator.I20_HeartOfTarrasque(); break;
                        case 4: itemBuing = UnitGenerator.I29_Sange_And_Yasha(); break;
                        case 5: itemBuing = UnitGenerator.I32_ShadowBlade(); break;
                        case 6: itemBuing = UnitGenerator.I35_Aghanims_Scepter(); break;
                        case 7: itemBuing = UnitGenerator.I36_Desolator(); break;
                        case 8: itemBuing = UnitGenerator.I38_AssaultCuirass(); break;
                        case 9: itemBuing = UnitGenerator.I42_Scythe_Of_Vyse(); break;
                        case 10: itemBuing = UnitGenerator.I43_Skadi(); break;
                        case 11: itemBuing = UnitGenerator.I47_TrevelBoots2(); break;
                    }
                }

                BuyItemMethod(CurrentPlayer, itemBuing);
            }
            else
            {
                SalleDelectItem();
            }


            return null;
        }

        private void BuyItemMethod(Player _player, ItemClass itemClass)
        {
            int playerItemCount = _player.Items.Count;
            ItemClass addedItem = itemClass;

            ///Если предмет сборный 
            ///Названия частей которых есть (чтобы потом их удалить)
            List<string> yesAllParts = new List<string>();
            ///Сума золота необходимая для покупки итема
            int allPrice = itemClass.Price;

            ///С начала проверим, может игроку нужен рецепт итема а не сам итем
            if (addedItem.Parts != null)
            {
                allPrice = 0;

                foreach (var item in addedItem.Parts)
                {
                    if (CurrentPlayer.Items.Count(p => p.Name == item.Name) == 0)
                    {
                        ///Проверим может есть части итема, частей
                        if (item.Parts != null)
                            foreach (var item2 in item.Parts)
                            {
                                if (!CurrentPlayer.Items.Any(p => p.Name == item2.Name))
                                    allPrice += item2.Price;
                                else if (CurrentPlayer.Items.Count(p => p.Name == item2.Name) > yesAllParts.Count(p => p == item2.Name))
                                    yesAllParts.Add(item2.Name);
                                else
                                    allPrice += item2.Price;
                            }
                        else
                            allPrice += item.Price;
                    }
                    else if (CurrentPlayer.Items.Count(p => p.Name == item.Name) > yesAllParts.Count(p => p == item.Name))
                        yesAllParts.Add(item.Name);
                    else
                    {
                        ///Проверим может есть части итема, частей
                        if (item.Parts != null)
                            foreach (var item2 in item.Parts)
                            {
                                if (!CurrentPlayer.Items.Any(p => p.Name == item2.Name))
                                    allPrice += item2.Price;
                                else if (CurrentPlayer.Items.Count(p => p.Name == item2.Name) > yesAllParts.Count(p => p == item2.Name))
                                    yesAllParts.Add(item2.Name);
                                else
                                    allPrice += item2.Price;
                            }
                        else
                            allPrice += item.Price;
                    }
                }
            }


            if (_player.Gold >= allPrice &&
                !_player.Dead)
            {
                int oldPrice = itemClass.Price;

                if (allPrice == addedItem.Price)
                {///Значит никаких частей у игрока нету

                    ///Смотрим можно ли данный итем во что то собрать
                    ItemClass newItem = UnitGenerator.CreateNewItem(_player, itemClass);
                    if (newItem != null)
                    {
                        ///Можно собрать
                        ///Разрешаем пропустить итем
                        playerItemCount = 0;
                        addedItem = newItem;
                    }

                    if (playerItemCount < 4)
                    {
                        _player.Gold -= oldPrice;
                        ///Подбираем итем
                        _player.AddItem(addedItem);

                        if (playerItemCount == 0)
                        {
                            ///Смотрим можно ли во что то собрать части в инвинтаре
                            for (int i = 0; i < 4; i++)
                            {
                                ItemClass newItemPart2 = UnitGenerator.CreateNewItem(_player, addedItem);
                                if (newItemPart2 != null)
                                {
                                    _player.RemoveItem(addedItem, false);
                                    _player.AddItem(newItemPart2);
                                }
                                else
                                    break;
                            }
                        }

                    }
                    else
                    {
                        ///Нет места в инвинтаре
                    }
                }
                else
                {///Части есть, значит место для итема найдется 
                    _player.Gold -= allPrice;
                    ///Удаляем части которые уже есть
                    foreach (var item in yesAllParts)
                        CurrentPlayer.RemoveItem(CurrentPlayer.Items.FirstOrDefault(p => p.Name == item), false);
                    ///Подбираем итем
                    _player.AddItem(addedItem);
                }
            }
        }

        public void Show(Player currentPlayer)
        {
            if (currentPlayer.PlayerInShopPoint())
            {
                CurrentPlayer = currentPlayer;

                ///Ставим паузу
                CurrentPlayer.CurrentMap.Pause();

                this.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
        public void Close()
        {
            if (C_SaleGrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                ///Ставим паузу
                CurrentPlayer.CurrentMap.StopPause();
            }
            else
                C_SaleGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }


        private void Sell_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (CurrentPlayer.Items.Any())
            {
                C_SaleGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                UpdateSalleView();
            }
        }

        private void UpdateSalleView()
        {
            C_SellItems.ItemsSource = null;
            List<FrameworkElement> partsView = new List<FrameworkElement>();
            foreach (var item in CurrentPlayer.Items)
            {
                UC_View_ItemImage itemImage = new Voins.UC_View_ItemImage();
                //itemImage.Width = 40;
                // itemImage.Height = 40;
                itemImage.ShowPrice((int)(item.Price / 2));
                itemImage.ShowImage((item.View as UC_View_ItemImage).ImageIndex);
                partsView.Add(itemImage);
            }
            C_SellItems.ItemsSource = partsView;
        }

        private void SellSelectItem(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SalleDelectItem();
        }

        private void SalleDelectItem()
        {
            if (CurrentPlayer.Items.Any() && C_SellItems.SelectedIndex != -1)
            {
                ItemClass itemSell = CurrentPlayer.Items.FirstOrDefault(p => (p.View as UC_View_ItemImage).ImageIndex == (C_SellItems.SelectedItem as UC_View_ItemImage).ImageIndex);
                _player.Gold += (int)(itemSell.Price / 2);
                CurrentPlayer.RemoveItem(itemSell, false);
                UpdateSalleView();
            }
        }

        private void CloseSellWindow(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            C_SaleGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void BuyItemButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (C_SaleGrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                BuyItem();
        }

        private void CloseButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (C_SaleGrid.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                Close();
        }
    }
}

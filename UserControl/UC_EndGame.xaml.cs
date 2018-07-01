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
    public sealed partial class UC_EndGame : UserControl
    {
        public UC_EndGame()
        {
            this.InitializeComponent();
        }

        public void Show(Map map,string winPlayer)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Visible;

            C_Win_name.Text = winPlayer;

            if (map.Players != null && map.Players.Count > 0)
            {
                C_1_Kill.Text = map.Players[0].Kills.ToString();
                C_1_Name.Text = map.Players[0].Name;
                C_1_Death.Text = map.Players[0].Death.ToString();
                C_1_BlocksDeleted.Text = map.Players[0].StatisticData.BlockKills.ToString();
                C_1_DemagePut.Text = map.Players[0].StatisticData.Demage.ToString();
                C_1_DemageRec.Text = map.Players[0].StatisticData.DemageSelfs.ToString();
                C_1_AllGold.Text = map.Players[0].StatisticData.AllGold.ToString();
                C_1_UnitKilled.Text = map.Players[0].StatisticData.MobKills.ToString();
            }

            if (map.Players != null && map.Players.Count > 1)
            {
                C_2_Kill.Text = map.Players[1].Kills.ToString();
                C_2_Name.Text = map.Players[1].Name;
                C_2_Death.Text = map.Players[1].Death.ToString();
                C_2_BlocksDeleted.Text = map.Players[1].StatisticData.BlockKills.ToString();
                C_2_DemagePut.Text = map.Players[1].StatisticData.Demage.ToString();
                C_2_DemageRec.Text = map.Players[1].StatisticData.DemageSelfs.ToString();
                C_2_AllGold.Text = map.Players[1].StatisticData.AllGold.ToString();
                C_2_UnitKilled.Text = map.Players[1].StatisticData.MobKills.ToString();
            }
        }

        public void Hidde()
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

      

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}

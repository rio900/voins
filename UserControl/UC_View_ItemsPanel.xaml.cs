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
    public sealed partial class UC_View_ItemsPanel : UserControl
    {
        private Player _player;
        public Player CurrentPlayer
        {
            get { return _player; }
            set { _player = value; }
        }

        public UC_View_ItemsPanel()
        {
            this.InitializeComponent();

            for (int i = 0; i < C_DropGrid.Children.Count; i++)
                C_DropGrid.Children[i].Tapped += UC_View_ItemsPanel_Tapped;

        }
        public void Show()
        {
            if (CurrentPlayer != null)
            {
                CurrentPlayer.UpdateItemPlayerData += CurrentPlayer_UpdateItemPlayerData;
                UpdateItem();
            }
        }

        private void UpdateItem()
        {
             for (int i = 0; i < C_DropGrid.Children.Count; i++)
                  C_DropGrid.Children[i].Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            C_Items.Children.Clear();

            int index = 0;
            foreach (var item in CurrentPlayer.Items)
            {
                item.View.Width = 35;
                item.View.Height = 35;
                item.View.Margin = new Thickness(3, 0, 0, 0);
                item.View.ApplayMargin(new Thickness(0));
                C_Items.Children.Add(item.View);

                C_DropGrid.Children[index++].Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        void CurrentPlayer_UpdateItemPlayerData(object sender, EventArgs e)
        {
            UpdateItem();
        }

        void UC_View_ItemsPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CurrentPlayer.DropItem(CurrentPlayer.Items[C_DropGrid.Children.IndexOf(sender as UIElement)]);
        }

      
    }
}

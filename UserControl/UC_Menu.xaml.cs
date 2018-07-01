using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class UC_Menu : UserControl
    {
        public event EventHandler PlayeVsPlayerStart;
        public event EventHandler CampaningStart;
        public event EventHandler Coop1PlayerStart;
        public event EventHandler Coop2PlayerStart;
        public UC_Menu()
        {
            this.InitializeComponent();
        }

        private void PlayeVsPlayerClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (PlayeVsPlayerStart != null)
                PlayeVsPlayerStart(this,null);
        }

        private void CampaningClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (CampaningStart != null)
                CampaningStart(this, null);
        }

        private void Coop1PlayerClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (Coop1PlayerStart != null)
                Coop1PlayerStart(this, null);
        }

        private void Coop2PlayerClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (Coop2PlayerStart != null)
                Coop2PlayerStart(this, null);
        }
    }
}

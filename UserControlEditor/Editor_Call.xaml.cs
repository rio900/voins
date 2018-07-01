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

namespace Voins.UserControlEditor
{
    public sealed partial class Editor_Call : UserControl
    {
        private int _indexTop;

        public int IndexTop
        {
            get { return _indexTop; }
            set { _indexTop = value; }
        }

        private int _indexLeft;

        public int IndexLeft
        {
            get { return _indexLeft; }
            set { _indexLeft = value; }
        }

        private int _imageIndex=-1;

        public int ImageIndex
        {
            get { return _imageIndex; }
            set { _imageIndex = value; }
        }

        public Editor_Call()
        {
            this.InitializeComponent();
        }

        public void ShowImage(int imageIndex)
        {
            ImageIndex = imageIndex;
            for (int i = 0; i < C_Root.Children.Count; i++)
            {
                var item = C_Root.Children[i];
                if (i == imageIndex)
                    (item as FrameworkElement).Visibility = Windows.UI.Xaml.Visibility.Visible;
                else
                    (item as FrameworkElement).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}

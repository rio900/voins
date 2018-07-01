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
    public sealed partial class UC_Editor : UserControl
    {
        private int _pointerIndex = -1;

        public int PointerIndex
        {
            get { return _pointerIndex; }
            set { _pointerIndex = value; }
        }

        public UC_Editor()
        {
            this.InitializeComponent();
            C_PaintListView.SelectionChanged += C_PaintListView_SelectionChanged;
            CreateCalls();
        }

        void C_PaintListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PointerIndex = C_PaintListView.SelectedIndex;
        }

        public void CreateCalls() 
        {
            for (int i = 0; i < C_Root.Width / 50; i++)
            {
                for (int j = 0; j < C_Root.Height / 50; j++)
                {
                    Editor_Call map_Call = new Editor_Call()
                    {
                        IndexLeft = i,
                        IndexTop = j
                    };
                    map_Call.Tapped += map_Call_Tapped;
                    ///Это для размещения блока стены(не пробиваймые блоки)
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        map_Call.ShowImage(0);
                    }
                    Canvas.SetLeft(map_Call, i * 50);
                    Canvas.SetTop(map_Call, j * 50);

                    C_Root.Children.Add(map_Call);
                }
            }
        }

        void map_Call_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Editor_Call call = sender as Editor_Call;
            C_Position.Text = C_Root.Children.IndexOf(call as UIElement).ToString() + "/ " + call.IndexLeft + ", " + call.IndexTop;
            call.ShowImage(PointerIndex);
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            C_PaintListView.SelectedIndex = -1;
        }

        private void GetMapText(object sender, RoutedEventArgs e)
        {
            C_MapText.Text = "";
            if (C_MapText.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                C_MapText.Visibility = Windows.UI.Xaml.Visibility.Visible;

                List<int> unitIndex = new List<int>();

                for (int i = 0; i < C_PaintListView.Items.Count; i++)
                {
               //     if (i != 0)
                 //   {
                        C_MapText.Text += "List<int> unitIndex" + i + " = new List<int>(){";
                        foreach (var item in C_Root.Children.Where(p => (p as Editor_Call).ImageIndex == i))
                        {
                            C_MapText.Text += C_Root.Children.IndexOf(item) + ",";
                        }
                        C_MapText.Text += "};" + Environment.NewLine;
               //     }
                //    else 
               //     {
                // //     
                    
                 //   }
                }
              //  C_MapText.
            }
            else
                C_MapText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}

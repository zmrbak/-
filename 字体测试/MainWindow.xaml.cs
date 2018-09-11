using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace 字体测试
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChars();
        }

        private void LoadChars()
        {
            myStackPanel.Children.Clear();

            Thread thread = new Thread(DoTheWork);
            //thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        private void DoTheWork()
        {
            String text = "\u0000";
            byte[] aa = Encoding.Unicode.GetBytes(text);
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                for (int i = 0; i < 65535; i++)
                {
                    aa[0]++;
                    if (aa[0] == 0)
                    {
                        aa[1]++;
                    }
                    string bb = Encoding.Unicode.GetString(aa);

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.VerticalAlignment = VerticalAlignment.Center;
                    stackPanel.Margin = new Thickness(3);
                    stackPanel.Background = Brushes.Wheat;

                    TextBlock textBlock = new TextBlock();
                    //textBlock.FontFamily = new FontFamily("iconfont");
                    textBlock.FontFamily = new FontFamily("/字体测试;component/#iconfont");
                    textBlock.FontSize = 24;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Text = bb;
                    stackPanel.Children.Add(textBlock);

                    TextBlock textBlock2 = new TextBlock();
                    textBlock2.FontSize = 24;
                    textBlock2.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock2.VerticalAlignment = VerticalAlignment.Center;
                    textBlock2.Text = "\\u" + aa[1].ToString("x2") + aa[0].ToString("x2");
                    stackPanel.Children.Add(textBlock2);

                    myStackPanel.Children.Add(stackPanel);
                }
            }));
        }


    }
}

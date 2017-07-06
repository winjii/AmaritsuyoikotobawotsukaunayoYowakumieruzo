using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TextBox> chars;
        double fontSize = 0;
        public MainWindow()
        {
            InitializeComponent();
            fontSize = textBox.FontSize;
            FontFamily font = textBox.FontFamily;
            string str = "TextBoxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            textBox.Text = str;
            textBox.TextWrapping = TextWrapping.Wrap;
            chars = new List<TextBox>();
            Canvas.SetLeft(textBox, 0);
            Canvas.SetTop(textBox, 0);
            for (int i = 0; i < str.Length; i++)
            {
                TextBox charBox = new TextBox();
                charBox.FontFamily = font;
                charBox.FontSize = fontSize;
                charBox.Text = str[i].ToString();
                charBox.BorderThickness = new Thickness(0);
                charBox.IsReadOnly = true;
                chars.Add(charBox);
                canvas.Children.Add(charBox);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBox.MaxWidth = canvas.ActualWidth;
            double x = 0, y = 200;
            foreach (TextBox charBox in chars)
            {
                Canvas.SetLeft(charBox, x);
                Canvas.SetTop(charBox, y);
                x += fontSize/2;
                if (x > canvas.ActualWidth)
                {
                    y += fontSize;
                    x = 0;
                }
            }
        }
    }
}

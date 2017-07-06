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
using System.Windows.Media.Animation;
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
        private List<TextBox> chars;
        private KeyTime animationTime;
        public MainWindow()
        {
            InitializeComponent();
            textBox.TextWrapping = TextWrapping.Wrap;
            animationTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1500));
        }

        private double getCharSize(char target)
        {
            int d = Util.IsHalfByRegex(target) ? 2 : 1;
            return textBox.FontSize/d;
        }

        private List<Point> getPos(string str)
        {
            List<Point> res = new List<Point>();
            double x = 0, y = 0;
            for (int i = 0; i < str.Length; i++)
            {
                res.Add(new Point(x, y));
                x += getCharSize(str[i]);
                if (x > textBox.ActualWidth)
                {
                    x = 0;
                    y += textBox.FontSize;
                }
            }
            return res;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DistinctString pastStr = new DistinctString(textBox.Text);
            StringRebuilder rebuilder = new StringRebuilder(pastStr);
            Random rand = new Random();
            int p = rand.Next(pastStr.Str.Length), cnt = rand.Next(pastStr.Str.Length - p) + 1;
            rebuilder.ReserveDeletion(p, cnt);
            rebuilder.ReserveAddition(p, "lattemaltamalta");
            DistinctString newStr = rebuilder.Rebuild();

            List<Point> pastPos = getPos(pastStr.Str);
            List<Point> newPos = getPos(newStr.Str);
            textBox.Text = "";
            chars = new List<TextBox>();
            for (int i = 0; i < pastStr.Str.Length; i++)
            {
                TextBox charBox = new TextBox();
                charBox.FontFamily = textBox.FontFamily;
                charBox.Text = pastStr.Str[i].ToString();
                charBox.BorderThickness = new Thickness(0);
                charBox.IsReadOnly = true;
                chars.Add(charBox);
                canvas.Children.Add(charBox);
                Canvas.SetLeft(charBox, pastPos[i].X);
                Canvas.SetTop(charBox, pastPos[i].Y);

                DoubleKeyFrame frameX = new EasingDoubleKeyFrame(newPos[i].X, animationTime);
                DoubleAnimationUsingKeyFrames animationX = new DoubleAnimationUsingKeyFrames();
                animationX.KeyFrames.Add(frameX);
                Storyboard storyboardX = new Storyboard();
                storyboardX.Children.Add(animationX);
                Storyboard.SetTarget(animationX, charBox);
                Storyboard.SetTargetProperty(animationX, new PropertyPath("(Canvas.Left)"));

                DoubleKeyFrame frameY = new EasingDoubleKeyFrame(newPos[i].Y, animationTime);
                DoubleAnimationUsingKeyFrames animationY = new DoubleAnimationUsingKeyFrames();
                animationY.KeyFrames.Add(frameY);
                Storyboard storyboardY = new Storyboard();
                storyboardY.Children.Add(animationY);
                Storyboard.SetTarget(animationY, charBox);
                Storyboard.SetTargetProperty(animationY, new PropertyPath("(Canvas.Top)"));

                //storyboardX.Begin();
                //storyboardY.Begin();
            }
        }
    }
}

﻿using System;
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
using CoreTweet;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace AmaritsuyoikotobawotsukaunayoYowakumieruzo
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TextBox> chars;
        private KeyTime animationTime;
        private Tokens tokens;
        private OAuth.OAuthSession session;
        private Uri url;
        private DispatcherTimer timer;
    
        public MainWindow()
        {
            InitializeComponent();
            textBox.TextWrapping = TextWrapping.Wrap;
            animationTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1500));
            session = CoreTweet.OAuth.Authorize("TCw7aPpxrpUTHM1NeKTnx0txi", "APIESaACn9G6vFoVN5Be2rDjciwniFaIaPp3Sm4zFqq4LbaI3n");
            url = session.AuthorizeUri; // -> user open in browser
            


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
                if (x + getCharSize(str[i]) > textBox.ActualWidth)
                {
                    x = 0;
                    y += textBox.FontSize;
                }
                res.Add(new Point(x, y));
                x += getCharSize(str[i]);
            }
            return res;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private Storyboard getTranslationalAnimation(UIElement e, PropertyPath path, double target)
        {
            DoubleKeyFrame frame = new SplineDoubleKeyFrame(target, animationTime, new KeySpline(0.9, 0.05, 1 - 0.9, 1 - 0.05));
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(frame);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, e);
            Storyboard.SetTargetProperty(animation, path);
            return storyboard;
        }

        private Storyboard getAppearanceAnimation(UIElement e)
        {
            e.Opacity = 0;
            DoubleKeyFrame frame = new SplineDoubleKeyFrame(1, animationTime, new KeySpline(0.9, 0.05, 1 - 0.9, 1 - 0.05));
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(frame);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, e);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            return storyboard;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DistinctString pastStr = new DistinctString(textBox.Text);
            StringRebuilder rebuilder = new StringRebuilder(pastStr);
            /*Random rand = new Random();
            int p = rand.Next(Math.Max(1, pastStr.Str.Length/2)), cnt = rand.Next(pastStr.Str.Length - p) + 1;
            rebuilder.ReserveDeletion(p, cnt);
            rebuilder.ReserveAddition(p, "lattemaltamalta");
            DistinctString newStr = rebuilder.Rebuild();*/
            DistinctString newStr = TweetConverter.Convert(pastStr);

            try
            {
                tokens.Statuses.Update(newStr.Str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ツイート出来ませんでした。");
            }

            List<Point> pastPos = getPos(pastStr.Str);
            List<Point> newPos = getPos(newStr.Str);
            textBox.Text = "";
            textBox.Visibility = Visibility.Hidden;
            chars = new List<TextBox>();
            for (int i = 0; i < newStr.Str.Length; i++)
            {
                TextBox charBox = new TextBox();
                charBox.FontFamily = textBox.FontFamily;
                charBox.FontSize = textBox.FontSize;
                charBox.Text = newStr.Str[i].ToString();
                charBox.BorderThickness = new Thickness(0);
                charBox.Background = Brushes.Transparent;
                charBox.IsReadOnly = true;
                chars.Add(charBox);
                canvas.Children.Add(charBox);

                int id = newStr.Ids[i];
                if (id == -1)
                {
                    Canvas.SetLeft(charBox, newPos[i].X);
                    Canvas.SetTop(charBox, newPos[i].Y);
                    getAppearanceAnimation(charBox).Begin();
                }
                else
                {
                    Canvas.SetLeft(charBox, pastPos[id].X);
                    Canvas.SetTop(charBox, pastPos[id].Y);

                    //TODO: Beginしっぱなしでいいのか？
                    getTranslationalAnimation(charBox, new PropertyPath("(Canvas.Left)"), newPos[i].X).Begin();
                    getTranslationalAnimation(charBox, new PropertyPath("(Canvas.Top)"), newPos[i].Y).Begin();
                }
            }
            timer = new DispatcherTimer();
            timer.Interval = animationTime.TimeSpan + TimeSpan.FromMilliseconds(1000);
            timer.Tick += (s, e_) =>
            {
                timer.Stop();
                foreach (TextBox charBox in chars)
                {
                    canvas.Children.Remove(charBox);
                }
                textBox.Visibility = Visibility.Visible;
            };
            timer.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start(url.ToString());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                tokens = OAuth.GetTokens(session, pinInput.Text);
                pinInput.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("（認証）だめみたいですね");
            }
        }
    }
}

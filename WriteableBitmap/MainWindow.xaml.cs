using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace WriteableBitmap类
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WriteableBitmap wb = new WriteableBitmap(640, 480, 96, 96, PixelFormats.Bgra32, null);
        readonly ScaleTransform scale = new ScaleTransform(1, 1);
        readonly TranslateTransform translate = new TranslateTransform();
        readonly TransformGroup transformGroup = new TransformGroup();
        public MainWindow()
        {
            InitializeComponent();
            this.myImage.Source = wb;

            transformGroup.Children.Add(scale);
            transformGroup.Children.Add(translate);
            this.myImage.RenderTransform = transformGroup;

            InitAllPixels();
        }

        private void InitAllPixels()
        {
            //因为像素格式是32位的，所以这里直接定义为UInt32类型表示一个像素的大小。
            //因为转成四字节的顺序必须为blue green red alpha。所以在UInt32数据中blue放在低位
            UInt32[] pixels = new UInt32[wb.PixelWidth * wb.PixelHeight];
            Random random = new Random();
            for (int y = 0; y < wb.PixelHeight; y++)
            {
                for (int x = 0; x < wb.PixelWidth; x++)
                {
                    int offset = y * wb.PixelWidth + x;

                    byte blue = (byte)random.Next(0, 255);
                    byte green = (byte)random.Next(0, 255);
                    byte red = (byte)random.Next(0, 255);
                    byte alpha = 255;

                    if ((y + 1) % 10 == 0 || (x + 1) % 10 == 0)
                        alpha = 0;
                    byte[] pixel = new byte[4] { blue, green, red, alpha };

                    pixels[offset] = BitConverter.ToUInt32(pixel, 0);
                }
            }
            int stride = wb.PixelWidth * (wb.Format.BitsPerPixel / 8);

            wb.WritePixels(new Int32Rect(0, 0, wb.PixelWidth, wb.PixelHeight), pixels, stride, 0);

        }

        UInt32 black = 0xFF000000;
        readonly Int32 blackWidth = 3;
        private void DrawPixels(int x, int y)
        {
            int width, height;
            width = Math.Min(blackWidth, wb.PixelWidth - x);
            height = Math.Min(blackWidth, wb.PixelHeight - y);

            UInt32[] blacks = new uint[width * height];
            for (int i = 0; i < blacks.Length; i++)
            {
                blacks[i] = black;
            }

            int stride = width * (wb.Format.BitsPerPixel / 8);

            wb.WritePixels(new Int32Rect(x, y, width, height), blacks, stride, 0);
        }


        double scaleDelta = 0.5;
        private void MyImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                scale.ScaleX += scaleDelta;
                scale.ScaleY += scaleDelta;
            }
            else
            {
                scale.ScaleX = (scale.ScaleX - scaleDelta) <= 0 ? scale.ScaleX : (scale.ScaleX - scaleDelta);
                scale.ScaleY = (scale.ScaleY - scaleDelta) <= 0 ? scale.ScaleY : (scale.ScaleY - scaleDelta);
            }
        }

        double translateDelta = 5;
        private void MyImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
                translate.Y -= translateDelta;

            if (e.Key == Key.S)
                translate.Y += translateDelta;

            if (e.Key == Key.A)
                translate.X -= translateDelta;

            if (e.Key == Key.D)
                translate.X += translateDelta;
        }

        private void MyImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int y = (int)e.GetPosition(myImage).Y;
            int x = (int)e.GetPosition(myImage).X;

            DrawPixels(x, y);
        }

        private void MyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                int y = (int)e.GetPosition(myImage).Y;
                int x = (int)e.GetPosition(myImage).X;
                DrawPixels(x, y);
                //Console.WriteLine(e.GetPosition(myImage));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            scale.CenterX = this.myImage.ActualWidth / 2.0;
            scale.CenterY = this.myImage.ActualHeight / 2.0;
        }

        private void MyImage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitAllPixels();
        }
    }
}

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
using System.Drawing;


namespace GDI_
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"A.png";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(@"C:\Users\DELL\Desktop\A.png");

            using (System.Drawing.Graphics graphics = Graphics.FromImage(image))
            {
                string waterMask = $"{DateTime.Now:G}";
                Font font = new Font("yahei", 20);
                SizeF size = graphics.MeasureString(waterMask, font);
                graphics.DrawString(waterMask, font, System.Drawing.Brushes.Red, new PointF(image.Width - size.Width - 20, image.Height - size.Height - 20));
            }

            image.Save(@"C:\Users\DELL\Desktop\B.png");
        }
    }
}

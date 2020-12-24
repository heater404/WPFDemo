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

namespace ROI
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

        private void ROICtrl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ROICtrl)
            {
                //ROICtrl img = (ROICtrl)e.OriginalSource;
                //鼠标捕获此图片
                //roi.CaptureMouse();
                roi.IsSelected = true;

                roi.InvalidateVisual();
                //SetOtherUnSelect(img);
            }
            else
            {
                //roi.CaptureMouse();
                roi.IsSelected = false;

                roi.InvalidateVisual();
                //SetOtherUnSelect(img);
            }
        }
    }
}

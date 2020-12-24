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
    /// ROI.xaml 的交互逻辑
    /// </summary>
    public partial class ROICtrl : UserControl
    {
        public bool IsSelected { get; set; }
        public ROICtrl()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (IsSelected)
            {
                try
                {
                    //绘制边框
                    Rect rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
                    Pen p = new Pen(new SolidColorBrush(Colors.Red), 2);
                    p.DashStyle = DashStyles.Dot;
                    p.StartLineCap = PenLineCap.Triangle;
                    dc.DrawRectangle(Brushes.Transparent, p, rect);

                    ////在4个角画出对应的表示方向的小方块
                    //double area = 20;
                    //dc.DrawRectangle(Brushes.Red, p, new Rect(0, 0, area, area));
                    //dc.DrawRectangle(Brushes.Red, p, new Rect(this.ActualWidth - area, 0, area, area));
                    //dc.DrawRectangle(Brushes.Red, p, new Rect(0, this.ActualHeight - area, area, area));
                    //dc.DrawRectangle(Brushes.Red, p, new Rect(this.ActualWidth - area, this.ActualHeight - area, area, area));
                }
                catch { }
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

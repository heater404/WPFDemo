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

namespace 绘制可视化对象
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

        /// <summary>
        /// 点击左键时绘制一个Circle 注意host需要设置background属性，才会响应鼠标事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void host_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //绘制circle的时候需要实例化一个可视化对象，然后添加到host中，不然不会显示
            DrawingVisual visual = new DrawingVisual();
            host.AddVisual(visual);

            DrawMyCircle(visual, e.GetPosition(host), false);
        }

        private void host_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void host_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingRegion)
            {

                DrawMyRegion(regionTopLeftCorner, e.GetPosition(host));
            }
        }

        /// <summary>
        /// 使用DrawingContext绘制一个圆环
        /// </summary>
        /// <param name="visual">可视化对象</param>
        /// <param name="point">需要绘制的位置，相对于host的坐标</param>
        /// <param name="isSelected">是否被选中 根据这个参数选择不一样的画刷</param>
        private void DrawMyCircle(DrawingVisual visual, Point point, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                //这里的选用了DrawingContext中的DrawGeometry()方法，这个方法可以画复杂的图形
                CombinedGeometry geometry = new CombinedGeometry
                {
                    Geometry1 = new EllipseGeometry(point, 20, 20),
                    Geometry2 = new EllipseGeometry(point, 50, 50),
                    GeometryCombineMode = GeometryCombineMode.Xor,
                };

                Pen pen;
                if (isSelected)
                {
                    pen = new Pen(Brushes.Black, 1)
                    {
                        DashStyle = new DashStyle(new List<double>() { 10 }, 1)
                    };
                }
                else
                {
                    pen = new Pen(Brushes.Black, 1);
                }

                dc.DrawGeometry(Brushes.Chocolate, pen, geometry);
            }
        }

        private void ReDrawMyCircle(DrawingVisual visual, bool isSelelcted)
        {
            Point point = new Point((visual.ContentBounds.TopLeft.X + visual.ContentBounds.BottomRight.X) / 2, (visual.ContentBounds.TopLeft.Y + visual.ContentBounds.BottomRight.Y) / 2);
            DrawMyCircle(visual, point, isSelelcted);
        }

        private DrawingVisual regionvisual;
        private bool isDrawingRegion;
        private Point regionTopLeftCorner;
        private void DrawMyRegion(Point point1, Point point2)
        {
            using (DrawingContext dc = regionvisual.RenderOpen())
            {
                dc.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Black, 1)
                {
                    DashStyle = new DashStyle(new List<double>() { 10 }, 1)
                }, new Rect(point1, point2));
            }
        }

        /// <summary>
        ///右键的时候则是选中可视化对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void host_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DrawingVisual visual = host.GetVisual(e.GetPosition(host));
            //if (visual != null)
            //{
            //    //计算选中的可视化图形的中心点，因为我们知道是圆形，因为选中之后需要变换颜色，所以需要重新绘制，需要绘制在原本的位置
            //    //TODO：选中后是否可以不用重新绘制，只在原有的基础上进行修改。
            //    ReDrawMyCircle(visual, true);
            //}

            if (!isDrawingRegion)
            {
                isDrawingRegion = true;
                regionTopLeftCorner = e.GetPosition(host);
                regionvisual = new DrawingVisual();
                host.AddVisual(regionvisual);
            }
        }

        private void host_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawingRegion)
            {
                //检测
                Geometry geometry = new RectangleGeometry(new Rect(regionTopLeftCorner, e.GetPosition(host)));

                //选中之前需要把之前选中的恢复原状
                foreach (var hit in host.GetHits())
                    ReDrawMyCircle(hit, false);

                var hits = host.GetVisuals(geometry);

                foreach (var hit in hits)
                    ReDrawMyCircle(hit, true);                

                MessageBox.Show($"Selected {hits.Count} circle(s)");
                host.DeleteVisual(regionvisual);
                isDrawingRegion = false;
            }
        }
    }
}

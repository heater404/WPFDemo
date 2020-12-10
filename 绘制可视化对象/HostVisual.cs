using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace 绘制可视化对象
{
    /// <summary>
    /// 创建一个可视化对象的容器，这个容器继承WPF元素，所以可以直接显示
    /// </summary>
    public class HostVisual : Canvas
    {
        /// <summary>
        /// 这个集合用于存储可视化对象
        /// </summary>
        private List<Visual> visuals = new List<Visual>();

        /// <summary>
        /// 重写VisualChildrenCount属性
        /// </summary>
        protected override int VisualChildrenCount => visuals.Count;

        /// <summary>
        /// 重写GetVisualChild()方法
        /// </summary>
        /// <param name="index">索引号</param>
        /// <returns>可视化对象</returns>
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        /// <summary>
        /// 添加可视化对象的方法
        /// </summary>
        /// <param name="visual">需要添加的可视化对象</param>
        public void AddVisual(Visual visual)
        {
            //注意 这里除了添加到集合中 还需要添加到容器的可视化树和逻辑树中
            this.visuals.Add(visual);
            base.AddLogicalChild(visual);
            base.AddVisualChild(visual);
        }

        /// <summary>
        /// 剔除可视化对象
        /// </summary>
        /// <param name="visual">想要剔除的可视化对象</param>
        public void DeleteVisual(Visual visual)
        {
            this.visuals.Remove(visual);
            base.RemoveLogicalChild(visual);
            base.RemoveVisualChild(visual);
        }

        /// <summary>
        /// 单点命中检测
        /// </summary>
        /// <param name="point">需要检测的点</param>
        /// <returns></returns>
        public DrawingVisual GetVisual(Point point)
        {
            //命中检测使用的VisualTreeHelper的静态方法
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);

            return hitResult.VisualHit as DrawingVisual;
        }

        List<DrawingVisual> hits = new List<DrawingVisual>();
        public List<DrawingVisual> GetHits()
        {
            return hits;
        }

        /// <summary>
        /// 区域命中检测
        /// </summary>
        /// <param name="geometry">选择的区域</param>
        /// <returns>命中的可视化对象集合</returns>
        public List<DrawingVisual> GetVisuals(Geometry geometry)
        {
            hits.Clear();

            VisualTreeHelper.HitTest(this, FilterCallback, ResultCallback, new GeometryHitTestParameters(geometry));
            //1、有一个HitTestFilterCallback回调函数
            //
            //2、还有一个HitTestResultCallback回调函数
            //   该函数用于返回命中的可视化对象，我们将命中的对象存储在hits集合中
            //3、最后一个参数是HitTestParameters,HitTestParameters是一个抽象类。
            //   它的派生类有PointHitTestParameters和GeometryHitTestParameters。

            return hits;
        }

        private HitTestResultBehavior ResultCallback(HitTestResult result)
        {
            GeometryHitTestResult geometryHitTestResult = result as GeometryHitTestResult;

            if (geometryHitTestResult.IntersectionDetail == IntersectionDetail.FullyInside)
                hits.Add(result.VisualHit as DrawingVisual);

            return HitTestResultBehavior.Continue;
        }

        private HitTestFilterBehavior FilterCallback(DependencyObject potentialHitTestTarget)
        {
            return HitTestFilterBehavior.Continue;
        }
    }
}

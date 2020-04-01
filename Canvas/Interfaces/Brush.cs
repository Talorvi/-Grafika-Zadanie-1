using System.Runtime.Loader;
using System.Windows.Input;
using System.Windows.Media;
using Canvas.Brushes;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;

namespace Canvas.Interfaces
{
    public abstract class Brush
    {
        private Brush _currentBrush;
        public SolidColorBrush CurrentColor { get; set; }

        protected Brush()
        {
            this.CurrentColor = new SolidColorBrush(Colors.Black);
        }

        public virtual void ChangeBrush(Brush brush)
        {
            this._currentBrush = brush;
        }

        public abstract void MouseDown(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint);
        public abstract void MouseUp(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint);
        public abstract void MouseMove(object sender, MouseEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint);
        public abstract void RMouseDown(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint);
    }
}
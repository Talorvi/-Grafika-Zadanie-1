using System.Runtime.Loader;
using System.Windows;
using System.Windows.Input;
using Canvas.Brushes;

namespace Canvas.Interfaces
{
    public abstract class Brush
    {
        private Brush _currentBrush;

        protected Brush()
        {
            
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
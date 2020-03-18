using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Canvas.Interfaces;

namespace Canvas.Brushes
{
    public class NormalBrush : Brush
    {
        public override void MouseDown(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                currentPoint = e.GetPosition(canvas);
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            
        }

        public override void MouseMove(object sender, MouseEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            
            var line = new Line
            {
                Stroke = SystemColors.WindowFrameBrush,
                X1 = currentPoint.X,
                Y1 = currentPoint.Y,
                X2 = e.GetPosition(canvas).X,
                Y2 = e.GetPosition(canvas).Y
            };

            currentPoint = e.GetPosition(canvas);

            canvas.Children.Add(line);
        }

        public override void RMouseDown(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            
        }
    }
}
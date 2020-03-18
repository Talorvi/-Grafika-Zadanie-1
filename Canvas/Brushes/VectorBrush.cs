using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Canvas.Helpers;
using Canvas.Interfaces;

namespace Canvas.Brushes
{
    public class VectorBrush : Brush
    {
        //do rysowania lini
        private Point ?_a = null;
        private Point ?_b = null;

        //do wybierania punktów i poruszania nimi
        private Line _selectedLine;
        private Point _selectedPoint;
        
        //kliknięcie myszką -> rysowanie lini
        public override void MouseDown(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = new Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
            
                if (_a == null)
                {
                    _a = point;
                } else if (_b == null) 
                {
                    _b = point;
                    this.DrawLine(canvas);

                    _a = null;
                    _b = null;
                }
            }
        }

        //rysowanie lini z punktu A do B
        private void DrawLine(System.Windows.Controls.Canvas canvas)
        {
            if (this._a != null && this._b != null)
            {
                var line = new Line
                {
                    Stroke = SystemColors.WindowFrameBrush,
                    X1 = this._a.Value.X,
                    Y1 = this._a.Value.Y,
                    X2 = this._b.Value.X,
                    Y2 = this._b.Value.Y,
                };
                
                canvas.Children.Add(line);
            }
        }
        
        public override void MouseUp(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            
        }

        //przy wciśnięciu prawego przycisku myszki -> wybranie najbliższego punktu z lini
        public override void RMouseDown(object sender, MouseButtonEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            var point = e.GetPosition(canvas);
            var closestDistance = double.MaxValue;

            for (var index = 0; index < canvas.Children.Count; index++)
            {
                var line = canvas.Children[index] as Line;

                if (line == null) continue;
                var distanceA = MathHelper.DistanceFromPointA_B(point, new Point(line.X1, line.Y1));
                var distanceB = MathHelper.DistanceFromPointA_B(point, new Point(line.X2, line.Y2));

                if (distanceA < distanceB && distanceA < closestDistance)
                {
                    _selectedLine = line;
                    _selectedPoint = new Point(line.X1, line.Y1);
                    closestDistance = distanceA;
                }
                else if (distanceB < closestDistance)
                {
                    _selectedLine = line;
                    _selectedPoint = new Point(line.X2, line.Y2);
                    closestDistance = distanceB;
                }
            }
        }

        //poruszanie myszką -> zmiana położenia punktu
        public override void MouseMove(object sender, MouseEventArgs e, System.Windows.Controls.Canvas canvas, ref Point currentPoint)
        {
            if (e.RightButton != MouseButtonState.Pressed) return;

            var point = e.GetPosition(canvas);
            
            if (Math.Abs(_selectedPoint.X - _selectedLine.X1) < 0.01 && Math.Abs(_selectedPoint.Y - _selectedLine.Y1) < 0.01)
            {
                _selectedLine.X1 = point.X;
                _selectedLine.Y1 = point.Y;
                _selectedPoint = point;
            }
            else if (Math.Abs(_selectedPoint.X - _selectedLine.X2) < 0.01 && Math.Abs(_selectedPoint.Y - _selectedLine.Y2) < 0.01)
            {
                _selectedLine.X2 = point.X;
                _selectedLine.Y2 = point.Y;
                _selectedPoint = point;
            }
        }
    }
}
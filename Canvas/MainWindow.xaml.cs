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
using Canvas.Brushes;
using Brush = Canvas.Interfaces.Brush;

namespace Canvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush _brush = new NormalBrush();
        private Point _currentPoint = new Point();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            _brush.MouseDown(sender, e, this.PaintSurface, ref _currentPoint);
        }

        private void Canvas_MouseMove_1(object sender, MouseEventArgs e)
        {
            _brush.MouseMove(sender, e, this.PaintSurface, ref _currentPoint);
        }

        private void Canvas_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            _brush.MouseUp(sender, e, this.PaintSurface, ref _currentPoint);
        }

        private void Canvas_RMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            _brush.RMouseDown(sender, e, this.PaintSurface, ref _currentPoint);
        }

        private void ChangeToNormalBrush(object sender, RoutedEventArgs e)
        {
            this._brush = new NormalBrush();
        }

        private void ChangeToVectorBrush(object sender, RoutedEventArgs e)
        {
            this._brush = new VectorBrush();
        }
    }
}
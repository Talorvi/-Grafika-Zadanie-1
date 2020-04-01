using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
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
using Color = System.Drawing.Color;

namespace Canvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush _brush = new NormalBrush();
        private Point _currentPoint = new Point();
        private enum ConvertStance
        {
            PreInit, Init, CmykToRgb, RgbToCmyk
        }

        private ConvertStance _convertStance = ConvertStance.PreInit;
        
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
            OnColorTextChanged(sender, null);
        }

        private void ChangeToVectorBrush(object sender, RoutedEventArgs e)
        {
            this._brush = new VectorBrush();
            OnColorTextChanged(sender, null);
        }

        private void OnColorTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Red.Text == string.Empty || Green.Text == string.Empty || Blue.Text == string.Empty || this._convertStance == ConvertStance.CmykToRgb)
            {
                if (this._convertStance != ConvertStance.PreInit)
                {
                    this._convertStance = ConvertStance.Init;
                }
                return;
            }

            var red = Convert.ToByte(Red.Text);
            var green =  Convert.ToByte(int.Parse(Green.Text));
            var blue = Convert.ToByte(int.Parse(Blue.Text));

            if (this._convertStance == ConvertStance.Init)
            {
                this._convertStance = ConvertStance.RgbToCmyk;

            }
            
            if (this._convertStance == ConvertStance.RgbToCmyk)
            {
                double redval = red / 255f;
                double greenval = green / 255f;
                double blueval = blue / 255f;

                var black = (1 - Math.Max(Math.Max(redval, greenval), blueval));
                Black.Text = black.ToString();
                var cyan = (1 - redval - double.Parse(Black.Text)) * 100/(1 - black);
                Cyan.Text = Convert.ToInt32(cyan).ToString();
                var magenta = (1 - greenval - double.Parse(Black.Text)) * 100 / (1 - black);
                Magenta.Text = Convert.ToInt32(magenta).ToString();
                var yellow = (1 - blueval - double.Parse(Black.Text)) * 100 / (1 - black);
                Yellow.Text = Convert.ToInt32(yellow).ToString();
            }

            this._brush.CurrentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(red, green, blue));
            this._convertStance = ConvertStance.Init;
        }

        private void OnColorTextChangedCMY(object sender, TextChangedEventArgs e)
        {
            if (Cyan.Text == string.Empty || Magenta.Text == string.Empty || Yellow.Text == string.Empty ||
                Black.Text == string.Empty || this._convertStance == ConvertStance.RgbToCmyk)
            {
                return;
            }

            if (this._convertStance == ConvertStance.Init)
            {
                this._convertStance = ConvertStance.CmykToRgb;
            }

            if (this._convertStance != ConvertStance.CmykToRgb) return;
            
            var red = 255 * (1 - float.Parse(Cyan.Text) / 100) * (1 - float.Parse(Black.Text)/100);
            var green = 255 * (1 - float.Parse(Magenta.Text) / 100) * (1 - float.Parse(Black.Text)/100);
            var blue = 255 * (1 - float.Parse(Yellow.Text) / 100) * (1 - float.Parse(Black.Text)/100);
            
            Red.Text = Convert.ToInt32(red).ToString();
            Green.Text = Convert.ToInt32(green).ToString();
            Blue.Text = Convert.ToInt32(blue).ToString();
            
            var redval = Convert.ToByte(Red.Text);
            var greenval =  Convert.ToByte(int.Parse(Green.Text));
            var blueval = Convert.ToByte(int.Parse(Blue.Text));

            this._brush.CurrentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(redval, greenval, blueval));
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            var isMatch = regex.IsMatch(e.Text);

            if (isMatch)
            {
                e.Handled = regex.IsMatch(e.Text);
                return;
            }

            var sender2 = (TextBox) sender;
            if (sender2.Text == string.Empty) return;
            
            var newValue = int.Parse(sender2.Text + e.Text);
            var handled = newValue > 255;
            e.Handled = handled;
        }
        
        private void NumberValidationTextBoxCmy(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            var isMatch = regex.IsMatch(e.Text);

            if (isMatch)
            {
                e.Handled = regex.IsMatch(e.Text);
                return;
            }

            var sender2 = (TextBox) sender;
            if (sender2.Text == string.Empty) return;
            
            var newValue = int.Parse(sender2.Text + e.Text);
            var handled = newValue > 100;
            e.Handled = handled;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Red.Text = "0";
            this.Green.Text = "0";
            this.Blue.Text = "0";
            
            this.Cyan.Text = "0";
            this.Magenta.Text = "0";
            this.Yellow.Text = "0";
            this.Black.Text = "100";

            this._convertStance = ConvertStance.Init;
        }
    }
}
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
using Canvas.ColorModels;
using Canvas.Helpers;
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

            if (this._convertStance == ConvertStance.Init)
            {
                this._convertStance = ConvertStance.RgbToCmyk;
            }
            
            var red = Convert.ToInt32(Red.Text);
            var green =  Convert.ToInt32(Green.Text);
            var blue = Convert.ToInt32(Blue.Text);
            var rgb = new ModelRgb(red, green, blue);

            if (this._convertStance == ConvertStance.RgbToCmyk)
            {
                var cmyk = ModelCmyk.ConvertFromRgb(rgb);
                
                Black.Text = cmyk.Black.ToString();
                Cyan.Text = cmyk.Cyan.ToString();
                Magenta.Text = cmyk.Magenta.ToString();
                Yellow.Text = cmyk.Yellow.ToString();
            }

            this._brush.CurrentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(Convert.ToByte(rgb.Red), Convert.ToByte(rgb.Green), Convert.ToByte(rgb.Blue)));
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
            
            var cyan = Convert.ToInt32(Cyan.Text);
            var magenta =  Convert.ToInt32(Magenta.Text);
            var yellow = Convert.ToInt32(Yellow.Text);
            var black = Convert.ToInt32(Black.Text);
            
            var cmyk = new ModelCmyk(cyan, magenta, yellow, black);
            var rgb = ModelRgb.ConvertFromCmyk(cmyk);

            Red.Text = rgb.Red.ToString();
            Green.Text = rgb.Green.ToString();
            Blue.Text = rgb.Blue.ToString();

            this._brush.CurrentColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(Convert.ToByte(rgb.Red), Convert.ToByte(rgb.Green), Convert.ToByte(rgb.Blue)));
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            ValidatorHelper.ValidateTextBox(sender, ref e, 255);
        }
        
        private void NumberValidationTextBoxCmy(object sender, TextCompositionEventArgs e)
        {
            ValidatorHelper.ValidateTextBox(sender,ref e, 100);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //init text fields
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
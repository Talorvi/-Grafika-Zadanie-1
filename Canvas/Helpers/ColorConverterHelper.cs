using System;
using Canvas.ColorModels;

namespace Canvas.Helpers
{
    public static class ColorConverterHelper
    {
        public static ModelCmyk ConvertRgbToCmyk(ModelRgb rgb)
        {
            double redVal = rgb.Red / 255f;
            double greenVal = rgb.Green / 255f;
            double blueVal = rgb.Blue / 255f;

            var black = 1 - Math.Max(Math.Max(redVal, greenVal), blueVal);

            if (black == 1) return new ModelCmyk(0, 0, 0, 100);
            
            var cyan = (1 - redVal - black) * 100/(1 - black);
            var magenta = (1 - greenVal - black) * 100 / (1 - black);
            var yellow = (1 - blueVal - black) * 100 / (1 - black);
            
            return new ModelCmyk(Convert.ToInt32(cyan), Convert.ToInt32(magenta), Convert.ToInt32(yellow), Convert.ToInt32(black*100));
        }

        public static ModelRgb ConvertCmykToRgb(ModelCmyk cmyk)
        {
            var red = Convert.ToInt32(255 * (1 - Convert.ToDouble(cmyk.Cyan) / 100) * (1 - cmyk.Black/100));
            var green = Convert.ToInt32(255 * (1 - Convert.ToDouble(cmyk.Magenta) / 100) * (1 - cmyk.Black/100));
            var blue = Convert.ToInt32(255 * (1 - Convert.ToDouble(cmyk.Yellow) / 100) * (1 - cmyk.Black/100));

            // var redVal = Convert.ToByte(Red);
            // var greenVal =  Convert.ToByte(int.Parse(Green));
            // var blueVal = Convert.ToByte(int.Parse(Blue));
            
            return new ModelRgb(red, green, blue);
        }
    }
}
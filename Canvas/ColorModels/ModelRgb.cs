using Canvas.Helpers;

namespace Canvas.ColorModels
{
    public class ModelRgb
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public ModelRgb(int red, int green, int blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public ModelRgb()
        {
            
        }

        public static ModelRgb ConvertFromCmyk(ModelCmyk cmyk)
        {
            return ColorConverterHelper.ConvertCmykToRgb(cmyk);
        }
    }
}
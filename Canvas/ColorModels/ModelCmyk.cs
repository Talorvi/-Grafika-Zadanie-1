using Canvas.Helpers;

namespace Canvas.ColorModels
{
    public class ModelCmyk
    {
        public int Cyan { get; set; }
        public int Magenta { get; set; }
        public int Yellow { get; set; }
        public int Black { get; set; }

        public ModelCmyk(int cyan, int magenta, int yellow, int black)
        {
            this.Cyan = cyan;
            this.Magenta = magenta;
            this.Yellow = yellow;
            this.Black = black;
        }

        public ModelCmyk()
        {
            
        }

        public static ModelCmyk ConvertFromRgb(ModelRgb rgb)
        {
            return ColorConverterHelper.ConvertRgbToCmyk(rgb);
        }
    }
}
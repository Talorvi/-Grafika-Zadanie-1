using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Canvas.Helpers
{
    public static class ValidatorHelper
    {
        public static void ValidateTextBox(object sender, ref TextCompositionEventArgs e, int maxNumber)
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
            var handled = newValue > maxNumber;
            e.Handled = handled;
        }
    }
}
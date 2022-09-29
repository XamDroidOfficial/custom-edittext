using Android.Content;
using Android.Widget;
using Java.Lang;
using System.Linq;
using System;
using Android.Util;

namespace CustomEditText
{
    public class HeightEditText : EditText
    {
        private int _foot;
        private int _inches;
        private string _formattedHeight = string.Empty;

        public HeightEditText(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InputType = Android.Text.InputTypes.ClassNumber;
            AfterTextChanged += HeightEditText_AfterTextChanged;
        }

        private void HeightEditText_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                string userInput = e.Editable.ToString();
                bool isBackPressed = _formattedHeight.Length < userInput.Length;
                if (isBackPressed)
                {
                    string foot = GetFormattedFoot(userInput);
                    string inches = GetFormattedInches(userInput);
                    _formattedHeight = GetFormattedHeight(foot, inches);
                    SetFormattedHeight(_formattedHeight);
                }
                else
                {
                    _formattedHeight = userInput;
                }
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private string GetFormattedFoot(string userInput)
        {
            string values = GetNumbersFromString(userInput);
            _foot = Convert.ToInt32(values.Substring(0, 1));
            return $"{_foot}\'";
        }

        private string GetFormattedInches(string userInput)
        {
            string valuesStr = GetNumbersFromString(userInput);
            if (valuesStr.Length > 1)
            {
                _inches = Convert.ToInt32(valuesStr.Substring(1, valuesStr.Length - 1));
                _inches = _inches < 12 ? _inches : _inches / 10;
                return $"{_inches}\"";
            }
            return string.Empty;
        }

        private string GetFormattedHeight(string foot, string inches)
        {
            return string.IsNullOrEmpty(inches) ? foot : foot + inches;
        }

        private void SetFormattedHeight(string formattedHeight)
        {
            if (Text != formattedHeight)
            {
                Text = formattedHeight;
            }
            SetSelection(Text.Length);
        }

        private string GetNumbersFromString(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
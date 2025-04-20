using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoRoids.UserFormCode;

namespace AutoRoids.UserForm
{
    /// <summary>
    /// Interaction logic for xmlTextBox.xaml
    /// </summary>
    public partial class xmlTextBox : UserControl
    {
        private readonly clsTextInput clsTextInput = new clsTextInput();

        public xmlTextBox()
        {
            InitializeComponent();
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strName = (this).Name;
            if (sender is TextBox textBox)
            {
                string strText = textBox.Text;

                if (strName.Length > 0)
                {
                    if (clsTextInput.IsDouble(strName))
                    {
                        strText = clsTextInput.FilterDecimal(strText);

                        if (strText.Length > 0)
                        {
                            if (!clsTextInput.IsValidNumber(strName, strText))
                            {
                                // Reset previous values
                                int cursorPosition = textBox.CaretIndex;
                                textBox.Text = clsTextInput.GetData(strName);
                                try
                                {
                                    textBox.CaretIndex = cursorPosition - 1;
                                }
                                catch (Exception)
                                {
                                    // ignored
                                }
                            }
                            else
                                clsTextInput.SaveData(strName, strText);
                        }
                        else
                            clsTextInput.SaveData(strName, strText);
                    }
                    else
                        clsTextInput.SaveData(strName, strText);
                }
            }
        }

        private void SelectAddress(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb?.SelectAll();
        }

        private void SelectAddress(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb?.SelectAll();
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

        private void btnNumUp_Click(object sender, RoutedEventArgs e)
        {
            this.txtNum.Text = clsTextInput.Up(this.txtNum.Text);
        }

        private void btnNumDown_Click(object sender, RoutedEventArgs e)
        {
            this.txtNum.Text = clsTextInput.Down(this.txtNum.Text);
        }

        private void txtNum_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Subtract || e.Key == Key.OemMinus)
                e.Handled = true; // Prevent space key from being entered

            string strName = (this).Name;

            if (!clsTextInput.IsDouble(strName))
            {
                if (e.Key == Key.Decimal)
                    e.Handled = true;
            }
        }

        private void txtNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string strName = (this).Name;

            if (!clsTextInput.IsDouble(strName))
            {
                e.Handled = !e.Text.Any(x => Char.IsDigit(x));
            }
        }
    }
}
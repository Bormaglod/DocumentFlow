//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.03.2019
// Time: 18:38
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Syncfusion;
using Syncfusion.Windows.Forms.Tools;

namespace DocumentFlow.Controls
{
    public class DecimalTextBox : NumericTextBox
    {
        /// <summary>
        /// The line of the null value.
        /// </summary>
        private const string DEF_NULL_VALUE = "0";

        /// <summary>
        /// The minimum value.
        /// </summary>
        private decimal minValue = decimal.MinValue;

        /// <summary>
        ///
        /// </summary>
        private string newDecimalValue = string.Empty;

        /// <summary>
        ///
        /// </summary>
        private string oldDecimalValue = string.Empty;

        /// <summary>
        /// The maximum value.
        /// </summary>
        private decimal maxValue = decimal.MaxValue;

        /// <summary>
        /// The initial decimal value set in InitializeComponent.
        /// </summary>
        private decimal initDecimalValue;

        /// <summary>
        /// The decimal value when the control gets the focus. Used when validating.
        /// </summary>
        private decimal enterDecimalValue;

        /// <summary>
        /// The decimal value that is set through the DecimalValue property.
        /// </summary>
        private decimal preservedDecimalValue;

        public override bool AllowNull
        {
            get => base.AllowNull;
            set
            {
                if (AllowNull != value)
                {
                    base.AllowNull = value;
                    OnAllowNullChanged();
                }
            }
        }

        /// <summary>
        /// Overrides the Text property of <see cref="T:System.Windows.Forms.TextBox" />.
        /// </summary>
        /// <remarks>
        /// This property is overriden in order to normalize the data that is set
        /// to the Text property and format it as needed. The method <see cref="!:Syncfusion.Windows.Forms.NumberTextBoxBase.InsertString" />
        /// is used to format the data.
        /// </remarks>
        [RefreshProperties(RefreshProperties.Repaint)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => base.Text;
            set => SetTextProperty(value);
        }

        /// <summary>
        /// Indicates whether the NULLString property will be used.
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        [Category("Behavior")]
        [Description("Specifies if the NULLString will be used when the value is NULL.")]
        [Obsolete("This property will not be used in future.Insead Use AllowNull")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseNullString
        {
            get => base.UseNullString;
            set => base.UseNullString = value;
        }

        /// <summary>
        /// Gets or sets the decimal value of the control. This will be formatted and
        /// displayed.
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Data")]
        [Description("The decimal value of the currency control.")]
        public decimal DecimalValue
        {
            get
            {
                if (GetPreserveData())
                {
                    return preservedDecimalValue;
                }

                return Convert.ToDecimal(GetNumberValue(base.Text, 0));
            }
            set
            {
                if (!Initializing)
                {
                    SetDecimalValue(value);
                }
                else
                {
                    initDecimalValue = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum value that can be set through the DecimalTextBox.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the maximum value that can be set through the DecimalTextBox.")]
        public decimal MaxValue
        {
            get => maxValue;
            set
            {
                if (value < MinValue)
                {
                    maxValue = decimal.MaxValue;
                    throw new ArgumentOutOfRangeException("MaxValue", value, "MaxValue Cannot be lesser than MinValue");
                }

                maxValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum value that can be set through the DecimalTextBox.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the minimum value that can be set through the DecimalTextBox.")]
        public decimal MinValue
        {
            get => minValue;
            set
            {
                if (value > MaxValue)
                {
                    minValue = decimal.MinValue;
                    throw new ArgumentOutOfRangeException("MinValue", value, "MinValue Cannot be greater than MaxValue");
                }

                minValue = value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:Syncfusion.Windows.Forms.Tools.DecimalTextBox.DecimalValue" /> property is changed.
        /// </summary>
        [Category("PropertyChanged")]
        [Description("Occurs when the DecimalValue property is changed.")]
        public event EventHandler DecimalValueChanged;

        /// <summary>
        /// Overloaded. Creates an object of type DecimalTextBox. 
        /// </summary>
        /// <remarks>
        /// The DecimalTextBox object will be initialized with the default values
        /// for the display and data properties. You need to set any specific 
        /// values.
        /// </remarks>
        public DecimalTextBox()
        {
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += SharedBaseAssembly.AssemblyResolver;
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= SharedBaseAssembly.AssemblyResolver;
            }

            SetDefaultValue(0.0);
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        private void InitializeComponent()
        {
            Multiline = false;
            NullString = string.Empty;
        }

        /// <summary>
        /// Overrides <see cref="M:Syncfusion.Windows.Forms.Tools.NumberTextBoxBase.InitializeNumberTextBox" />.
        /// </summary>
        protected override void InitializeNumberTextBox()
        {
            ignoreTextChange = true;
            bool flag = false;
            if (initDecimalValue == 0.0m && AllowNull && Text == NullString)
            {
                flag = true;
            }

            base.InitializeNumberTextBox();
            SetNumberFormatInfoInitValues();
            if (flag)
            {
                SetNullNumberValue();
                SetTextBoxText(NullString);
            }
            else 
            {
                DecimalValue = initDecimalValue;
            }

            ignoreTextChange = false;
        }

        protected override bool CheckIsZero()
        {
            if (!IsNull)
            {
                return DecimalValue.Equals(0.0);
            }

            return false;
        }

        protected override bool IsValidNumberValue(string decimalString)
        {
            bool result = true;
            try
            {
                decimal.Parse(decimalString, NumberFormatInfoObject);
                return result;
            }
            catch
            {
                return false;
            }
        }

        protected int GetNumberPartLength(decimal numberValue)
        {
            return GetNumberPartLength(numberValue.ToString());
        }

        protected override string GetNumberValue(string formattedText, int startPosition)
        {
            decimal num = 0.0m;
            string empty = startPosition >= formattedText.Length ? formattedText : formattedText.Substring(0, startPosition) + formattedText[startPosition..];
            try
            {
                string empty2 = RemoveFormatting(empty);
                if (empty2 == string.Empty)
                {
                    empty2 = "0";
                }

                string text = string.Format(NumberFormatInfoObject, "{0:n}", decimal.MaxValue);
                string text2 = string.Format(NumberFormatInfoObject, "{0:n}", decimal.MinValue);
                if (!text.Equals(formattedText) && !text2.Equals(formattedText))
                {
                    num = decimal.Parse(empty2, NumberStyles.Any, NumberFormatInfoObject);
                }
            }
            catch
            {
                rollBackOperation = true;
            }

            if (IsNegative && num > 0.0m)
            {
                num *= -1.0m;
            }

            return num.ToString();
        }

        protected override bool CheckIfNegative(string rawValue)
        {
            if (rawValue != null && rawValue != string.Empty)
            {
                decimal num = Convert.ToDecimal(rawValue);
                if (num >= 0.0m)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="previousFormat"></param>
        protected override void FormatChanged(string currentText, NumberFormatInfo previousFormat)
        {
            decimal num = 0.0m;
            if (!Initializing)
            {
                try
                {
                    if (GetPreserveData())
                    {
                        num = DecimalValue;
                    }
                    else
                    {
                        currentText = currentText.Trim();
                        if (currentText != string.Empty)
                        {
                            num = decimal.Parse(currentText, NumberStyles.Float, previousFormat);
                        }
                    }
                }
                catch
                {
                    num = Convert.ToDecimal(GetNumberValue(currentText, 0));
                }
                finally
                {
                    ApplyFormattingAndSetText(num.ToString());
                }
            }
        }

        protected override string ToggleNegative(string currentText)
        {
            decimal num = Convert.ToDecimal(currentText);
            num *= -1.0m;
            if (num.Equals(0.0))
            {
                SetZeroNegative(!GetZeroNegative());
            }

            return num.ToString();
        }

        /// <summary>
        /// Formats the given text according to the current setting.
        /// </summary>
        /// <param name="rawValue"></param>
        /// <returns></returns>
        protected override string ApplyFormatting(string rawValue)
        {
            decimal num = 0.0m;
            if (rawValue != null && rawValue != string.Empty)
            {
                num = Convert.ToDecimal(rawValue);
            }
            else if (AllowNull)
            {
                return string.Format(NullFormat, "{0}", NullString);
            }

            return string.Format(NumberFormatInfoObject, "{0:n}", num);
        }

        /// <summary>
        /// Indicates whether to serialize the Text property
        /// if it is null or quals NullString
        /// </summary>
        /// <returns></returns>
        protected bool ShouldSerializeText()
        {
            if (!(NullString != string.Empty))
            {
                return Text != NullString;
            }

            return true;
        }

        /// <summary>
        /// Restores the CurrencyNumberDigits to the MaximumLength.
        /// </summary>
        protected new void ResetText() => Text = DefaultValue.ToString();

        protected override void SetTextProperty(string newText)
        {
            bool flag = true;
            bool flag2 = newText == null || newText == string.Empty;
            if (AllowNull && newText == NullString)
            {
                SetNullNumberValue();
                SetTextBoxText(NullString);
                return;
            }

            if (AllowNull && flag2)
            {
                flag = false;
            }
            else
            {
                try
                {
                    decimal decimalValue = flag2 ? decimal.Parse("0", NumberStyles.Number, NumberFormatInfoObject) : decimal.Parse(newText, NumberStyles.Number, base.NumberFormatInfoObject);
                    SetDecimalValue(decimalValue);
                }
                catch
                {
                    flag = false;
                }
            }

            if (!flag)
            {
                base.SetTextProperty(newText);
            }
        }

        protected void SetDecimalValue(decimal newValue)
        {
            SelectionStart = 0;
            SelectionLength = base.TextLength;

            IsNegative = (newValue < 0.0m);
            newValue = Math.Round(newValue, NumberDecimalDigits);
            preservedDecimalValue = newValue;
            SetPreserveData(preserveData: true);
            ApplyFormattingAndSetText(newValue.ToString());
        }

        /// <summary>
        /// Indicates whether the MaxValue property should be serialized.
        /// </summary>
        /// <returns>True if the value is not equal to <see cref="F:System.Decimal.MaxValue" />.</returns>
        private bool ShouldSerializeMaxValue()
        {
            if (maxValue != decimal.MaxValue)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resets the Max value to the default.
        /// </summary>
        private void ResetMaxValue() => MaxValue = decimal.MaxValue;

        /// <summary>
        /// Indicates whether the MinValue property should be serialized.
        /// </summary>
        /// <returns>True if the value is not equal to <see cref="F:System.Decimal.MaxValue" />.</returns>
        private bool ShouldSerializeMinValue()
        {
            if (minValue != decimal.MinValue)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resets the value to the default.
        /// </summary>
        private void ResetMinValue() => MinValue = decimal.MinValue;

        protected override bool CheckForMinMax(string currentTextValue, bool ignoreLength)
        {
            if (MinMaxValidation == MinMaxValidation.OnLostFocus)
            {
                return true;
            }

            if (!decimal.TryParse(currentTextValue, NumberStyles.Any, NumberFormatInfoObject, out decimal result) && AllowNull)
            {
                return true;
            }

            if (result < MinValue || result > MaxValue)
            {
                return false;
            }

            return true;
        }

        protected override bool CheckNullStringIsInRange(string nullString)
        {
            if (decimal.TryParse(nullString, NumberStyles.Any, NumberFormatInfoObject, out decimal result))
            {
                return CheckForMinMax(result.ToString(), ignoreLength: true);
            }

            return true;
        }

        /// <summary>
        /// Raises the <see cref="E:Syncfusion.Windows.Forms.Tools.DecimalTextBox.DecimalValueChanged" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnDecimalValueChanged(EventArgs e)
        {
            if (!ignoreTextChange && DecimalValueChanged != null)
            {
                DecimalValueChanged(this, e);
            }
        }

        /// <summary>
        /// Overrides OnTextChanged.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            bool preserveData = GetPreserveData();
            SetPreserveData(preserveData: false);
            base.OnTextChanged(e);
            OnDecimalValueChanged(e);
            OnBindableValueChanged(e);
            SetPreserveData(preserveData);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e) => base.OnKeyDown(e);

        /// <summary>
        /// Overrides the <see cref="M:System.Windows.Forms.Control.OnEnter(System.EventArgs)" /> method.
        /// </summary>
        /// <param name="args">The event data.</param>
        /// <remarks>
        /// Saves the current DecimalValue so that it can be compared 
        /// during validation. The DecimalValueChanged and TextChanged event
        /// will only be raised if the value is different during validation.
        /// </remarks>
        protected override void OnEnter(EventArgs args)
        {
            enterDecimalValue = DecimalValue;
            base.OnEnter(args);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            newDecimalValue = Text;
            if ((!AllowNull || !(Text == NullString)) && (DecimalValue < MinValue || DecimalValue > MaxValue))
            {
                string formattedText = FormattedText;
                switch (OnValidationFailed)
                {
                    case OnValidationFailed.SetNullString:
                        if (AllowNull)
                        {
                            SetNullNumberValue();
                            SetTextBoxText(NullString);
                        }
                        else
                        {
                            Focus();
                            SelectionStart = Text.Length;
                        }
                        break;
                    case OnValidationFailed.SetMinOrMax:
                        decimal tempDecimalValue = (DecimalValue < MinValue) ? MinValue : DecimalValue;
                        DecimalValue = (tempDecimalValue > MaxValue) ? MaxValue : tempDecimalValue;
                        break;
                    case OnValidationFailed.KeepFocus:
                        Focus();
                        base.SelectionStart = Text.Length;
                        break;
                }
                RaiseValidationError(formattedText, 0, "Current value does not meet MinValue and MaxValue requirements.");
            }

            oldDecimalValue = Text;
            base.OnValidating(e);
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            OnControlValidated(newDecimalValue, oldDecimalValue);
        }

        protected override void SetNullNumberValue()
        {
            preservedDecimalValue = 0.0m;
            SetPreserveData(preserveData: true);
        }

        protected override bool IsAssignable(object val)
        {
            if (val == null || Convert.IsDBNull(val))
            {
                return false;
            }

            return typeof(decimal).IsAssignableFrom(val.GetType());
        }

        protected override void SetValue(object val) => DecimalValue = (decimal)val;

        protected override object GetValue() => DecimalValue;

        protected override bool HandleBackspaceKey()
        {
            if (CheckIsZero() && AllowNull)
            {
                SetNullNumberValue();
                SetTextBoxText(NullString);
                return true;
            }

            return base.HandleBackspaceKey();
        }

        protected override bool HandleDeleteKey()
        {
            if (CheckIsZero() && AllowNull)
            {
                SetNullNumberValue();
                SetTextBoxText(NullString);
                return true;
            }

            return base.HandleDeleteKey();
        }

        protected override bool HandleDecimalKey()
        {
            string text = base.Text;
            int decimalSeparatorPosition = GetDecimalSeparatorPosition(text);
            if (decimalSeparatorPosition != -1)
            {
                decimalSeparatorPosition++;
                SetEmptySelection(decimalSeparatorPosition);
            }
            else
            {
                if (!IsNull || NumberFormatInfoObject.CurrencyDecimalDigits <= 0)
                {
                    return false;
                }
                m_bDecimalMode = true;
            }

            return true;
        }

        protected override NumberModifyState HandleSubtractKey()
        {
            NumberModifyState result = base.HandleSubtractKey();
            if (DeleteSelectionOnNegative)
            {
                SetEmptySelection(GetDecimalSeparatorPosition(GetTextBoxText()));
            }

            return result;
        }

        protected override AccessibleObject CreateAccessibilityInstance() => new DecimalTextBoxAccessibility(this);

        protected override NumberModifyState PrepareInsertString(string currentText, int startPosition, int selectionLength, string textToBeInserted, bool pasteOperation)
        {
            if (DecimalValue == 0.0m)
            {
                currentText = currentText.Substring(0, startPosition + selectionLength);
            }

            return base.PrepareInsertString(currentText, startPosition, selectionLength, textToBeInserted, pasteOperation);
        }

        private void OnAllowNullChanged()
        {
            if (IsNull)
            {
                if (AllowNull)
                {
                    SetNullNumberValue();
                    SetTextBoxText(NullString);
                }
                else
                {
                    string rawValue = (MinValue < 0.0m) ? "0" : MinValue.ToString();
                    ApplyFormattingAndSetText(rawValue);
                }
            }
        }
    }

}

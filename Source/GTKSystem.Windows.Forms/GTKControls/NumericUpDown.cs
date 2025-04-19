/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

[DesignerCategory("Component")]
public partial class NumericUpDown : Control
{
    public readonly NumericUpDownBase self;
    public override object GtkControl => self;
    public NumericUpDown()
    {
        self = new NumericUpDownBase();
        Minimum = int.MinValue;
        Maximum = int.MaxValue;
        self.ValueChanged += Self_ValueChanged;
    }

    private void Self_ValueChanged(object? sender, EventArgs e)
    {
        if (ValueChanged != null && self.IsVisible)
            OnValueChanged(e);
    }

    public int DecimalPlaces { get => Convert.ToInt32(self.Digits); set => self.Digits = Convert.ToUInt32(value); }
    public decimal Increment
    {
        get => Convert.ToDecimal(self.Adjustment.StepIncrement);
        set => self.Adjustment.StepIncrement = Convert.ToDouble(value);
    }

    public decimal Maximum
    {
        get => Convert.ToDecimal(self.Adjustment.Upper);
        set
        {
            var adjustmentUpper = Convert.ToDouble(value);
            if (Value > (decimal)adjustmentUpper)
            {
                Value = (decimal)adjustmentUpper;
            }

            self.Adjustment.Upper = adjustmentUpper;
        }
    }

    public decimal Minimum
    {
        get => Convert.ToDecimal(self.Adjustment.Lower);
        set
        {
            var adjustmentLower = Convert.ToDouble(value);
            if (Value < (decimal)adjustmentLower)
            {
                Value = (decimal)adjustmentLower;
            }

            self.Adjustment.Lower = adjustmentLower;
        }
    }

    public decimal Value
    {
        get => Convert.ToDecimal(self.Value);
        set
        {
            var selfValue = Convert.ToDouble(value);
            if (value < Minimum || value>Maximum)
            {
                throw new ArgumentOutOfRangeException();
            }
            self.Value = selfValue;
        }
    }

    public override string Text
    {
        get => Value.ToString(CultureInfo.InvariantCulture);
        set
        {
            decimal.TryParse(value, out var v);
            Value = v;
        } }
}
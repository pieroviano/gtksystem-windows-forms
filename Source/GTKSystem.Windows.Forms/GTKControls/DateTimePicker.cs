/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using Gtk;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Calendar = Gtk.Calendar;

namespace System.Windows.Forms;

using Color = Color;
#if NET462_OR_GREATER
using GtkSystemColors = System.Drawing.SystemColors;
#endif

[DesignerCategory("Component")]
public partial class DateTimePicker : MaskedTextBox
{
    private static DateTime initialMinDate = new DateTime(1753, 1, 1);
    private static DateTime initialMaxDate = DateTime.MaxValue;
    private readonly Popover popver;
    private readonly Gtk.Calendar calendar;
    public DateTimePicker() : base("DateTimePicker")
    {
        calendar = new Calendar();
        Format = DateTimePickerFormat.Long;
        Mask = Properties.Resources.DateTimePicker_DateTimePicker_Mask;

        self.SecondaryIconActivatable = true;
        self.SecondaryIconStock = "open-menu";
        self.SecondaryIconPixbuf = new Gdk.Pixbuf(GetType().Assembly, "System.Windows.Forms.Resources.System.MonthCalendar.ico");
        self.IconRelease += DateTimePicker_IconRelease;
        self.Shown += Self_Shown;

        popver = new Popover(self);
        popver.BorderWidth = 5;
        popver.Modal = true;
        popver.Position = PositionType.Bottom;

        calendar.Halign = Align.Fill;
        calendar.Valign = Align.Fill;
        calendar.DaySelected += Calendar_DaySelected;
        calendar.PrevYear += Calendar_PrevYear;
        calendar.NextYear += Calendar_NextYear;
        calendar.PrevMonth += Calendar_PrevMonth;
        calendar.NextMonth += Calendar_NextMonth;

        var popbody = new Box(Gtk.Orientation.Vertical, 6);
        popbody.Add(calendar);
        var todaybtn = new Gtk.Button()
        {
            Label = Properties.Resources.DateTimePicker_DateTimePicker_Choose_Today +
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        };
        todaybtn.Clicked += Todaybtn_Clicked;
        popbody.Add(todaybtn);
        popver.Add(popbody);
        self.Changed += Self_Changed;
    }
    private void Self_Changed(object? sender, EventArgs e)
    {
        if (ValueChanged != null && self.IsMapped)
            OnValueChanged(e);
    }

    private void Calendar_NextMonth(object? sender, EventArgs e)
    {
        if (calendar.GetDate() > MaxDate)
        {
            calendar.Date = MaxDate.Date.AddDays(-1);
        }
    }

    private void Calendar_PrevMonth(object? sender, EventArgs e)
    {
        if (calendar.GetDate() < MinDate)
        {
            calendar.Date = MinDate.Date.AddDays(1);
        }
    }

    private void Calendar_NextYear(object? sender, EventArgs e)
    {
        if (calendar.GetDate() > MaxDate)
        {
            calendar.Date = MaxDate.Date.AddDays(-1);
        }
    }

    private void Calendar_PrevYear(object? sender, EventArgs e)
    {
        if (calendar.GetDate() < MinDate)
        {
            calendar.Date = MinDate.Date.AddDays(1);
        }
    }

    private void Todaybtn_Clicked(object? sender, EventArgs e)
    {
        Value = DateTime.Now;
        popver.Hide();
    }

    private void Self_Shown(object? sender, EventArgs e)
    {
        Value = DateTime.Now;
    }

    private void DateTimePicker_IconRelease(object? o, IconReleaseArgs args)
    {
        var current = Value;
        if (current >= MaxDate)
        {
            calendar.Date = MaxDate.Date.AddDays(-1);
        }
        else if (current <= MinDate)
        {
            calendar.Date = MinDate.AddDays(1);
        }
        else
        {
            calendar.Date = Value;
        }
        popver.ShowAll();
    }

    private void Calendar_DaySelected(object? sender, EventArgs e)
    {
        var calendarValue = sender as Gtk.Calendar;
        var dt = calendarValue?.GetDate() ?? default;
        if (dt > MaxDate || dt < MinDate)
        {
            if (calendarValue != null)
            {
                calendarValue.Date = Value;
            }

            MessageBox.Show(
                string.Format(Properties.Resources.DateTimePicker_Calendar_DaySelected_Choose,
                    MaxDate.ToString("yyyy/MM/dd HH:mm:ss"),
                    MinDate.ToString("yyyy/MM/dd HH:mm:ss")),
                Properties.Resources.DateTimePicker_Calendar_DaySelected_Date_restrictions);
        }
        else
        {
            var current = Value;
            Value = dt.AddHours(current.Hour).AddMinutes(current.Minute).AddSeconds(current.Second);
        }
    }

    public DateTime MaxDate
    {
        get => maxDate;
        set
        {
            if (value > initialMaxDate)
            {
                throw new ArgumentOutOfRangeException(nameof(MaxDate), value, @$"{nameof(MaxDate)} too big: {value}");
            }
            maxDate = value;
        }
    }

    public DateTime MinDate
    {
        get => minDate;
        set
        {
            if (value < initialMinDate)
            {
                throw new ArgumentOutOfRangeException(nameof(MinDate), value, @$"{nameof(MinDate)} too small: {value}");
            }
            minDate = value;
        }
    }

    public DateTime Value
    {
        get
        {
            var result = DateTime.Now;
            var provider = CultureInfo.InvariantCulture;
            if (string.IsNullOrWhiteSpace(Text))
            { }
            else if (Format == DateTimePickerFormat.Custom && DateTime.TryParseExact(Text, CustomFormat, provider, DateTimeStyles.AllowWhiteSpaces, out result))
            { }
            else if (DateTime.TryParse(Text, provider, DateTimeStyles.AllowWhiteSpaces, out result))
            { }
            else
            {
                result = DateTime.Now;
            }
            if (result > MaxDate)
            {
                return MaxDate;
            }
            if (result < MinDate)
            {
                return MinDate;
            }
            return result;
        }
        set
        {
            if (value > MaxDate)
            {
                throw new ArgumentOutOfRangeException(nameof(MaxDate),value, @$"{nameof(MaxDate)} too big: {value}");
            }
            if (value < MinDate)
            {
                throw new ArgumentOutOfRangeException(nameof(MinDate), value, @$"{nameof(MinDate)} too small: {value}");
            }

            _value = value;
            if (Format == DateTimePickerFormat.Long)
                base.Text = value.ToString(Properties.Resources.DateTimePicker_Value_yyyy年MM月dd日);
            else if (Format == DateTimePickerFormat.Short)
                base.Text = value.ToString("yyyy/MM/dd");
            else if (Format == DateTimePickerFormat.Time)
                base.Text = value.ToString("HH:mm:ss");
            else if (Format == DateTimePickerFormat.Custom)
                base.Text = value.ToString(CustomFormat);
            else
                base.Text = value.ToString(Properties.Resources.DateTimePicker_Value_yyyy年MM月dd日);
        }
    }
    private string customFormat = Properties.Resources.DateTimePicker_Value_yyyy年MM月dd日;
    public string CustomFormat
    {
        get => customFormat;
        set
        {
            value ??= string.Empty;
            customFormat = value;
            Mask = Regex.Replace(value, "[ymdhs]", "_", RegexOptions.IgnoreCase);
            Value = _value;
        }
    }
    public DateTimePickerFormat format;
    private DateTime _value;
    private DateTime minDate = initialMinDate;
    private DateTime maxDate = initialMaxDate;

    public DateTimePickerFormat Format
    {
        get => format;
        set
        {
            format = value;
            if (Format == DateTimePickerFormat.Long)
                Mask = Properties.Resources.DateTimePicker_DateTimePicker_Mask;
            else if (Format == DateTimePickerFormat.Short)
                Mask = "____/__/__";
            else if (Format == DateTimePickerFormat.Time)
                Mask = "__:__:__";
            else if (Format == DateTimePickerFormat.Custom)
                Mask = Regex.Replace(CustomFormat, "[ymdhs]", "_", RegexOptions.IgnoreCase);
            else
                Mask = Properties.Resources.DateTimePicker_DateTimePicker_Mask;
        }
    }
    public override string Text
    {
        get => base.Text;
        set
        {
            if (!DateTime.TryParseExact(value ?? string.Empty, CustomFormat, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out _))
            {
                throw new FormatException();
            }
            base.Text = value ?? string.Empty;
        }
    }
    public Font? CalendarFont { get; set; }
    public Color CalendarForeColor { get; set; } = GtkSystemColors.ControlText;
    public Color CalendarMonthBackground { get; set; } = GtkSystemColors.Window;
    public Color CalendarTitleBackColor { get; set; } = GtkSystemColors.ActiveCaption;
    public Color CalendarTitleForeColor { get; set; } = GtkSystemColors.ActiveCaptionText;
    public Color CalendarTrailingForeColor { get; set; } = GtkSystemColors.GrayText;
}
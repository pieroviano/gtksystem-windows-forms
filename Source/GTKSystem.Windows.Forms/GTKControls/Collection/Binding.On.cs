using System.Globalization;
using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class Binding
{
    /// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event. </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
    protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (!_inOnBindingComplete)
            {
                try
                {
                    try
                    {
                        _inOnBindingComplete = true;
                        _bindingComplete?.Invoke(this, e);
                    }
                    catch (Exception exception)
                    {
                        if (ClientUtils.IsSecurityOrCriticalException(exception))
                        {
                            throw;
                        }
                        e.Cancel = true;
                    }
                }
                finally
                {
                    _inOnBindingComplete = false;
                }
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Format" /> event.</summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
    protected virtual void OnFormat(ConvertEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _format?.Invoke(this, e);
            if (!_formattingEnabled && !(e.Value is DBNull) && e.DesiredType != null && !e.DesiredType.IsInstanceOfType(e.Value) && e.Value is IConvertible)
            {
                e.Value = Convert.ChangeType(e.Value, e.DesiredType, CultureInfo.CurrentCulture);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Parse" /> event.</summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
    protected virtual void OnParse(ConvertEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _parse?.Invoke(this, e);
            if (!_formattingEnabled && !(e.Value is DBNull) && e is { Value: not null, DesiredType: not null } && !e.DesiredType.IsInstanceOfType(e.Value) && e.Value is IConvertible)
            {
                e.Value = Convert.ChangeType(e.Value, e.DesiredType, CultureInfo.CurrentCulture);
            }
        }, GtkApplication.UseAsyncLoad);
    }

}


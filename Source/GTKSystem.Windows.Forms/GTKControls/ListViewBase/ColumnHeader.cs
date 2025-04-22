using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms;

[DefaultProperty("Text")]
public partial class ColumnHeader : Component, ICloneable
{
    internal int _index = -1;

    internal string? _text;

    internal string? _name;

    internal int _width = 120;

    [Localizable(true)]
    public int DisplayIndex
    {
        get => displayIndex;
        set
        {
            if (_listView != null && value < 0 && !_listView.IsDisposed)
            {
                throw new ArgumentOutOfRangeException(nameof(DisplayIndex), @$"{value}<0");
            }
            if (_listView != null && value >= _listView.Columns.Count && !_listView.IsDisposed)
            {
                throw new ArgumentOutOfRangeException(nameof(DisplayIndex), @$"{value}>={_listView.Columns.Count}");
            }
            var index = displayIndex;
            if (_listView != null)
            {
                for (var i = 0; i < _listView.Columns.Count; i++)
                {
                    if (value <= displayIndex)
                    {
                        if (_listView.Columns[i] != this &&
                            _listView.Columns[i].displayIndex <= displayIndex)
                        {
                            _listView.Columns[i].displayIndex++;
                        }
                    }
                    else
                    {
                        if (_listView.Columns[i] != this &&
                            _listView.Columns[i].displayIndex <= value && _listView.Columns[i].displayIndex >= displayIndex)
                        {
                            _listView.Columns[i].displayIndex--;
                        }
                    }
                }
            }

            displayIndex = value;
            if (index != value)
            {
                OnDisplayIndexChanged(EventArgs.Empty);
            }
        }
    }

    [Browsable(false)]
    public int Index
    {
        get => _index;
        internal set => _index = value;
    }

    [DefaultValue(-1)]
    public int ImageIndex
    {
        get => imageIndex;
        set
        {
            if (value >= 0)
            {
                imageIndex = value;
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ImageIndex), value, $"Value for {nameof(ImageIndex)} is not valid: {value}");
            }
            imageKey = string.Empty;
        }
    }

    private ImageList? _imageList;
    internal int displayIndex = -1;
    public ListView? _listView;
    private int imageIndex = -1;
    private string? imageKey = string.Empty;
    private string name = string.Empty;
    private object? tag;
    private int width = 100;

    [Browsable(false)]
    public ImageList? ImageList
    {
        get => _imageList;
        internal set => _imageList = value;
    }

    public string? ImageKey
    {
        get => imageKey;
        set
        {
            imageKey = value ?? string.Empty;
            imageIndex = -1;
        }
    }

    [Browsable(false)]
    public ListView? ListView
    {
        get => _listView;
        set => _listView = value;
    }

    [Browsable(false)]
    public string Name
    {
        get => name;
        set => name = value ?? string.Empty;
    }

    [Localizable(true)]
    public string Text
    {
        get;
        set;
    }


    [Localizable(true)]
    [DefaultValue(HorizontalAlignment.Left)]
    public HorizontalAlignment TextAlign
    {
        get;
        set;
    } = HorizontalAlignment.Left;

    [Localizable(false)]
    [Bindable(true)]
    [DefaultValue(null)]

    public object? Tag
    {
        get => tag;
        set => tag = value;
    }

    [Localizable(true)]
    [DefaultValue(60)]
    public int Width
    {
        get => width;
        set
        {
            if (value < 0)
            {
                value = _listView?.ClientRectangle.Width ??
                        0 - _listView?.Columns.Where(i => i != this).Sum(col1 => col1.Width) ?? 0;
            }

            width = value;
        }
    }

    public Gtk.Button Button { get; set; } = null!;

    public ColumnHeader()
    {
        Width = 60;
        Text = @"ColumnHeader";
    }

    public override string ToString()
    {
        return $"ColumnHeader: Text: {Text}";
    }

    public ColumnHeader(int imageIndex) : this()
    {
        ImageIndex = imageIndex;
    }

    public ColumnHeader(string? imageKey) : this()
    {
        ImageKey = imageKey;
    }

    public ColumnHeader(string text, int width)
    {
        Width = 60;
        Text = text;
        Width = width;
    }

    //public void AutoResize(ColumnHeaderAutoResizeStyle headerAutoResize)
    //{
    //	throw new NotImplementedException();
    //}

    public object Clone()
    {
        return null!;
        //string data = System.Text.Json.JsonSerializer.Serialize(this,typeof(ColumnHeader));
        //return System.Text.Json.JsonSerializer.Deserialize<ColumnHeader>(data);
    }
    protected override void Dispose(bool disposing)
    {

    }
}
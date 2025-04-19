using System.Drawing;
using GTKSystemWinFormsApp11.Properties;

namespace GTKSystemWinFormsApp11;

public partial class GtkForm : Form
{
    public GtkForm()
    {
        InitializeComponent();
        InitializeLocalization();
    }

    public ToolStripMenuItem MenuOneToolStripMenuItem => memnuToolStripMenuItem;

    public ToolStripMenuItem MenuToolStripMenuItem => menuToolStripMenuItem;

    public ToolStripMenuItem ToolStripMenuItem1 => toolStripMenuItem1;

    public ToolStripButton ToolStripButton1 => toolStripButton1;

    public ToolStripSplitButton ToolStripSplitButton1 => toolStripSplitButton1;

    public ToolStripMenuItem DdddToolStripMenuItem => ddddToolStripMenuItem;

    public ToolStripMenuItem SsssToolStripMenuItem => ssssToolStripMenuItem;

    public ToolStripMenuItem FffffffToolStripMenuItem => fffffffToolStripMenuItem;

    public ToolStripMenuItem SsssToolStripMenuItem1 => ssssToolStripMenuItem1;

    public ToolStripMenuItem BbMenuToolStripMenuItem => bbMenuToolStripMenuItem;

    public ToolStripMenuItem BbMenu2ToolStripMenuItem => bbMenu2ToolStripMenuItem;

    public ToolStripMenuItem DdddToolStripMenuItem1 => ddddToolStripMenuItem1;

    public ToolStripMenuItem FfffToolStripMenuItem => ffffToolStripMenuItem;

    public ToolStripMenuItem ToolStripMenuItem3 => toolStripMenuItem3;

    public ToolStripMenuItem ToolStripMenuItem4 => toolStripMenuItem4;

    public ToolStripMenuItem ToolStripMenuItem5 => toolStripMenuItem5;

    public ToolStripMenuItem ToolStripMenuItem6 => toolStripMenuItem6;

    public Button Button3 => button3;

    public Button Button2 => button2;

    private void InitializeLocalization()
    {
        SizeChanged -= Form1_SizeChanged;
        SizeChanged += GtkForm_SizeChanged;

        toolStripMenuItem1.Text = $"{Resources.GtkForm_InitializeComponent_Menu} 1";
        ddddToolStripMenuItem.Text = $"a {Resources.GtkForm_InitializeComponent_Menu}";
        ssssToolStripMenuItem.Text = $"b {Resources.GtkForm_InitializeComponent_Menu}";
        bbMenuToolStripMenuItem.Text = $"bb {Resources.GtkForm_InitializeComponent_Menu}";
        bbMenu2ToolStripMenuItem.Text = $"bb {Resources.GtkForm_InitializeComponent_Menu} 2";
        ssssToolStripMenuItem1.Text = $"{Resources.GtkForm_InitializeComponent_Menu} 2";
        toolStripDropDownButton1.Text = Resources.GtkForm_GtkForm_dropdown_list_1;
        memnuToolStripMenuItem.Text = $"{Resources.GtkForm_InitializeComponent_Item} 1";
        fffffffToolStripMenuItem.Text = $"{Resources.GtkForm_InitializeComponent_Item} 2";
        button1.Text = Resources.GtkForm_InitializeComponent_Open_main_window;
        toolStripStatusLabel1.Text = Resources.GtkForm_InitializeComponent_Open_Status_Text;
        toolStripSplitButton2.Text = Resources.GtkForm_InitializeComponent_DropSownMenu;

        MenuOneToolStripMenuItem.Click += OnControlOnClick;
        MenuToolStripMenuItem.Click += OnControlOnClick;
        ToolStripMenuItem1.Click += OnControlOnClick;
        ToolStripButton1.Click += OnControlOnClick;
        ToolStripSplitButton1.Click += OnControlOnClick;
        DdddToolStripMenuItem.Click += OnControlOnClick;
        SsssToolStripMenuItem.Click += OnControlOnClick;
        FffffffToolStripMenuItem.Click += OnControlOnClick;
        SsssToolStripMenuItem1.Click += OnControlOnClick;
        BbMenuToolStripMenuItem.Click += OnControlOnClick;
        BbMenu2ToolStripMenuItem.Click += OnControlOnClick;
        DdddToolStripMenuItem1.Click += OnControlOnClick;
        FfffToolStripMenuItem.Click += OnControlOnClick;
        ToolStripMenuItem3.Click += OnControlOnClick;
        ToolStripMenuItem4.Click += OnControlOnClick;
        ToolStripMenuItem5.Click += OnControlOnClick;
        ToolStripMenuItem6.Click += OnControlOnClick;
        Button3.Click += OnControlOnClick;
        Button2.Click += OnControlOnClick;
    }

    private void OnControlOnClick(object? sender, EventArgs e)
    {
        _ = OnControlOnClickAsync(sender);
    }

    private static async Task OnControlOnClickAsync(object? sender)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(500));
        AutoClosingMessageBox.Instance.MessageBoxTimeout = 2000;
        if (sender is ITextControl textControl)
        {
            AutoClosingMessageBox.Instance.Show(textControl.Text);
        }
    }

    public LinkLabel LinkLabel1 => linkLabel1;

    private void Form1_SizeChanged(object? sender, EventArgs e)
    {
        GtkForm_SizeChanged(sender, e);
    }

    private void GtkForm_SizeChanged(object? sender, EventArgs e)
    {
        Console.WriteLine(@$"{Width},{Height}");
    }

    private void button1_Click(object? sender, EventArgs e)
    {
        Console.WriteLine(button1.Text);
    }

    private void trackBar1_Scroll(object? sender, EventArgs e)
    {
        label1.Text = trackBar1.Value.ToString();
    }

    private void button1_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g!.DrawEllipse(new Pen(new SolidBrush(System.Drawing.Color.Red), 2), 80, 25, 30, 20);
        g.FillEllipse(new SolidBrush(Color.Yellow), 40, 25, 30, 20);
        g.DrawEllipse(new Pen(new SolidBrush(Color.Blue), 0), 40, 25, 40, 40);

    }


    private void LinkLabel1_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        AutoClosingMessageBox.Instance.MessageBoxTimeout = 2000;
        AutoClosingMessageBox.Instance.Show(linkLabel1.Text);
    }
}
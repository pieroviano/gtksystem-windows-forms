namespace System.Windows.Forms;

public partial class Form
{
    private bool _topMost;

    public bool TopMost
    {
        get => _topMost;
        set => _topMost = self.KeepAbove = value;
    }
}
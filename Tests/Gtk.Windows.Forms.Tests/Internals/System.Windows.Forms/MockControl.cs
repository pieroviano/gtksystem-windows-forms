namespace System.Windows.Forms
{
    internal class MockControl : Control
    {
        public override object? GtkControl { get; set; } = new MockGtkControl();
    }
}

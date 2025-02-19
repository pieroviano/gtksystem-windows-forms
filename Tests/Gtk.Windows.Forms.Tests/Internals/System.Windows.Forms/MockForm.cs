namespace System.Windows.Forms
{
    internal class MockForm: Form
    {
        private object gtkControl;

        public MockForm()
        {
            var mockControl = new MockControl();
            gtkControl = mockControl.GtkControl!;
        }

        public override object GtkControl
        {
            get => gtkControl;
            set => gtkControl = value;
        }
    }
}

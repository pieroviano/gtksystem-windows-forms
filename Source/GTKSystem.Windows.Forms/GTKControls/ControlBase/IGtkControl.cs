using System;
using System.Windows.Forms;
using GTKSystem.Windows.Forms.Interfaces;


namespace GTKSystem.Windows.Forms.GTKControls.ControlBase
{
    public interface IGtkControl : IWidget
    {
        IGtkControlOverride Override { get; set; }
    }
}

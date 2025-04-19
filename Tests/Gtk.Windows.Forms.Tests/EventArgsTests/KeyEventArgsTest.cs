//
//
// Author:
//   Rolf Bjarne Kvinge  (RKvinge@novell.com)
//
// (C) 2007 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.EventArgsTests;

[TestFixture]
public class KeyEventArgsTest : TestHelper
{
    [Test]
    public void SuppressKeyPressTest ()
    {
        var kea = new KeyEventArgs (Keys.L);
			
        Assert.IsFalse (kea.SuppressKeyPress);
        Assert.IsFalse (kea.Handled);
			
        kea.SuppressKeyPress = true;
			
        Assert.IsTrue (kea.SuppressKeyPress);
        Assert.IsTrue (kea.Handled);
			
        kea.SuppressKeyPress = false;
			
        Assert.IsFalse (kea.SuppressKeyPress);
        Assert.IsFalse (kea.Handled);
			
    }
}
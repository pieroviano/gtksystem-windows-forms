//
// ApplicationContextTest.cs
//
// Author:
//   Chris Toshok (toshok@ximian.com)
//
// (C) 2006 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.ApplicationTests;

[TestFixture]
public class ApplicationContextTest : TestHelper
{
    private ApplicationContext ctx;
    private int thread_exit_count;
    private bool reached_form_handle_destroyed;

    [TearDown]
    protected override void TearDown()
    {
        ctx?.Dispose();
    }

    private void thread_exit (object? sender, EventArgs e)
    {
        thread_exit_count++;
    }

    private void form_handle_destroyed (object? sender, EventArgs e)
    {
        Assert.That((object?)thread_exit_count, Is.EqualTo(0), "1");
        Assert.That((object?)ctx.MainForm, Is.EqualTo(sender), "2");
        reached_form_handle_destroyed = true;
    }

    private void form_handle_destroyed2 (object? sender, EventArgs e)
    {
        Assert.That((object?)thread_exit_count, Is.EqualTo(1), "1");
        Assert.That((object?)ctx.MainForm, Is.EqualTo(sender), "2");
        reached_form_handle_destroyed = true;
    }

    [Test]
    public void TestEventOrdering ()
    {
        thread_exit_count = 0;
        reached_form_handle_destroyed = false;

        var f1 = new Form ();
        f1.ShowInTaskbar = false;
        f1.HandleDestroyed += form_handle_destroyed;

        ctx = new ApplicationContext (f1);
        ctx.ThreadExit += thread_exit;

        f1.Show ();
        f1.Dispose();

        Assert.That((object?)reached_form_handle_destroyed, Is.EqualTo(true), "3");
        Assert.That((object?)thread_exit_count, Is.EqualTo(1), "4");

        f1.Dispose ();
    }

    [Test]
    public void TestEventOrdering2 ()
    {
        thread_exit_count = 0;
        reached_form_handle_destroyed = false;

        var f1 = new Form ();
        f1.ShowInTaskbar = false;

        ctx = new ApplicationContext (f1);
        ctx.ThreadExit += thread_exit;

        f1.HandleDestroyed += form_handle_destroyed2;

        f1.Show ();
        f1.Dispose();
        Assert.That((object?)reached_form_handle_destroyed, Is.EqualTo(true), "3");
        Assert.That((object?)thread_exit_count, Is.EqualTo(1), "4");
			
        f1.Dispose ();
    }

    [Test]
    public void ThreadExitTest ()
    {
        thread_exit_count = 0;

        var f1 = new Form ();
        f1.ShowInTaskbar = false;
        ctx = new ApplicationContext (f1);
        ctx.ThreadExit += thread_exit;

        Assert.That((object?)ctx.MainForm, Is.EqualTo(f1), "1");
        f1.ShowInTaskbar = false;
        f1.Show ();
        f1.Dispose ();
        Assert.That((object?)ctx.MainForm, Is.EqualTo(f1), "2");
        Assert.That((object?)thread_exit_count, Is.EqualTo(1), "3");

        f1 = new Form ();
        ctx = new ApplicationContext (f1);
        ctx.ThreadExit += thread_exit;
        f1.ShowInTaskbar = false;
        f1.Show ();
        f1.Dispose();
        Assert.That((object?)ctx.MainForm, Is.EqualTo(f1), "4");
        Assert.That((object?)thread_exit_count, Is.EqualTo(2), "5");
        f1.Dispose ();
    }
}
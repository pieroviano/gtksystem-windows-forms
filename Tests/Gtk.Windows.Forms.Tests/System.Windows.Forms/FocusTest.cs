//
// Copyright (c) 2006 Novell, Inc.
//
// Authors:
//      Jackson Harper  (jackson@ximian.com)
//

using GtkTests.Helpers;
using System.Text;
using System.Windows.Forms;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class FocusTest : TestHelper
{

    public class ControlPoker : Button
    {

        internal bool directed_select_called;

        public ControlPoker()
        {
        }

        public ControlPoker(string text)
        {
            Text = text;
        }

        public void _Select(bool directed, bool forward)
        {
            Select(directed, forward);
        }

        protected override void Select(bool directed, bool forward)
        {
            directed_select_called = true;
            base.Select(directed, forward);
        }

    }

    private ControlPoker[] flat_controls;

    public class ContainerPoker : ContainerControl
    {

        public ContainerPoker(string s)
        {
            Text = s;
        }

        public void _Select(bool directed, bool forward)
        {
            Select(directed, forward);
        }

        public override string ToString()
        {
            return string.Concat(GetType(), " ", Text);
        }
    }

    public class GroupBoxPoker : GroupBox
    {

        public GroupBoxPoker(string s)
        {
            Text = s;
        }

        public void _Select(bool directed, bool forward)
        {
            Select(directed, forward);
        }

        public override string ToString()
        {
            return string.Concat(GetType(), " ", Text);
        }
    }

    [SetUp]
    protected override void SetUp()
    {
        flat_controls = null!;

        flat_controls =
        [
            new(), new(), new()
        ];

        for (var i = 0; i < flat_controls.Length; i++)
            flat_controls[i].Text = i.ToString();
        base.SetUp();
    }

    [Test]
    public void ControlSelectNextFlatTest()
    {
        //			if (TestHelper.RunningOnUnix) {
        //				Assert.Ignore ("Relies on form.Show() synchronously generating WM_ACTIVATE");
        //			}

        var form = new Form();
        form.ShowInTaskbar = false;

        form.Controls.AddRange(flat_controls);
        form.Show();

        Assert.IsTrue(flat_controls[0].Focused, "sanity-1");
        Assert.That((object?)flat_controls[0], Is.EqualTo(form.ActiveControl), "sanity-2");

        form.SelectNextControl(flat_controls[0], true, false, false, false);
        Assert.IsFalse(flat_controls[0].Focused);
        Assert.IsTrue(flat_controls[1].Focused);
        Assert.IsFalse(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[1], Is.EqualTo(form.ActiveControl));

        form.SelectNextControl(flat_controls[1], true, false, false, false);
        Assert.IsFalse(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsTrue(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[2], Is.EqualTo(form.ActiveControl));

        // Can't select anymore because we aren't wrapping
        form.SelectNextControl(flat_controls[2], true, false, false, false);
        Assert.IsFalse(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsTrue(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[2], Is.EqualTo(form.ActiveControl));

        form.SelectNextControl(flat_controls[2], true, false, false, true);
        Assert.IsTrue(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsFalse(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[0], Is.EqualTo(form.ActiveControl));
        form.Dispose();
    }

    [Test]
    public void SelectNextControlNullTest()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        form.Show();
        form.Controls.AddRange(flat_controls);

        form.SelectNextControl(null!, true, false, false, false);
        Assert.IsTrue(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsFalse(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[0], Is.EqualTo(form.ActiveControl));

        form.SelectNextControl(null!, true, false, false, false);
        Assert.IsTrue(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsFalse(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[0], Is.EqualTo(form.ActiveControl));
        form.Dispose();
    }

    [Test]
    public void SelectControlTest()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        form.Show();
        form.Controls.AddRange(flat_controls);

        flat_controls[0]._Select(false, false);
        Assert.That((object?)flat_controls[0], Is.EqualTo(form.ActiveControl));

        flat_controls[0]._Select(true, false);
        Assert.That((object?)flat_controls[0], Is.EqualTo(form.ActiveControl));

        flat_controls[0]._Select(true, true);
        Assert.That((object?)flat_controls[0], Is.EqualTo(form.ActiveControl));
        form.Dispose();
    }

    [Test]
    public void EnsureDirectedSelectUsed()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        form.Show();
        form.Controls.AddRange(flat_controls);

        form.SelectNextControl(null!, true, false, false, false);
        Assert.IsTrue(flat_controls[0].directed_select_called);
        form.Dispose();
    }

    [Test]
    public void ContainerSelectDirectedForward()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var cp = new ContainerPoker("container-a");

        form.Show();
        form.Controls.Add(cp);

        cp.Controls.AddRange(flat_controls);

        cp._Select(true, true);
        Assert.IsTrue(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsFalse(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[0], Is.EqualTo(cp.ActiveControl));
        Assert.That((object?)cp, Is.EqualTo(form.ActiveControl));

        // Should select the first one again
        cp._Select(true, true);
        Assert.IsTrue(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsFalse(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[0], Is.EqualTo(cp.ActiveControl));
        Assert.That((object?)cp, Is.EqualTo(form.ActiveControl));
        form.Dispose();
    }

    [Test]
    public void ContainerSelectDirectedBackward()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var cp = new ContainerPoker("container-a");

        form.Show();
        form.Controls.Add(cp);

        cp.Controls.AddRange(flat_controls);

        cp._Select(true, false);
        Assert.IsFalse(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsTrue(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[2], Is.EqualTo(cp.ActiveControl));
        Assert.That((object?)cp, Is.EqualTo(form.ActiveControl));

        // Should select the first one again
        cp._Select(true, false);
        Assert.IsFalse(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsTrue(flat_controls[2].Focused);
        Assert.That((object?)flat_controls[2], Is.EqualTo(cp.ActiveControl));
        Assert.That((object?)cp, Is.EqualTo(form.ActiveControl));
        form.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ContainerSelectUndirectedForward()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var cp = new ContainerPoker("container-a");

        form.Show();
        form.Controls.Add(cp);

        cp.Controls.AddRange(flat_controls);

        Assert.IsFalse(flat_controls[0].Focused);
        cp._Select(false, true);
        Assert.IsFalse(flat_controls[0].Focused);
        Assert.IsFalse(flat_controls[1].Focused);
        Assert.IsFalse(flat_controls[2].Focused);
        Assert.That(cp.ActiveControl, Is.EqualTo(null));
        Assert.That((object?)cp, Is.EqualTo(form.ActiveControl));
        form.Dispose();
    }

    [Test]
    public void GetNextControlFromForm()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var con_a = new ContainerPoker("container-a");
        var con_b = new ContainerPoker("container-b");
        var con_c = new ContainerPoker("container-c");
        ControlPoker[] ctrls_a =
        [
            new(), new(), new()
        ];
        ControlPoker[] ctrls_b =
        [
            new(), new(), new()
        ];
        ControlPoker[] ctrls_c =
        [
            new(), new(), new()
        ];

        con_a.Controls.AddRange(ctrls_a);
        con_b.Controls.AddRange(ctrls_b);
        con_c.Controls.AddRange(ctrls_c);

        form.Controls.Add(con_a);
        form.Controls.Add(con_b);
        form.Controls.Add(con_c);

        form.Show();

        // top level movement, 
        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(con_a), "null-1");
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(con_c), "null-2");

        Assert.That((object?)form.GetNextControl(form, true), Is.EqualTo(con_a), "form-1");
        Assert.That((object?)form.GetNextControl(form, false), Is.EqualTo(con_c), "form-2");

        Assert.That((object?)form.GetNextControl(con_a, true), Is.EqualTo(con_b), "container-1");
        Assert.That((object?)form.GetNextControl(con_a, false), Is.EqualTo(null), "container-2");
        Assert.That((object?)form.GetNextControl(con_b, true), Is.EqualTo(con_c), "container-3");
        Assert.That((object?)form.GetNextControl(con_b, false), Is.EqualTo(con_a), "container-4");
        Assert.That((object?)form.GetNextControl(con_c, true), Is.EqualTo(null), "container-5");
        Assert.That((object?)form.GetNextControl(con_c, false), Is.EqualTo(con_b), "container-6");

        object expected = ctrls_a[1];
        Assert.That((object?)form.GetNextControl(ctrls_a[0], true), Is.EqualTo(expected), "ctrls-a-1");
        Assert.That((object?)form.GetNextControl(ctrls_a[0], false), Is.EqualTo(con_a), "ctrls-a-2");
        object expected1 = ctrls_a[2];
        Assert.That((object?)form.GetNextControl(ctrls_a[1], true), Is.EqualTo(expected1), "ctrls-a-3");
        object expected2 = ctrls_a[0];
        Assert.That((object?)form.GetNextControl(ctrls_a[1], false), Is.EqualTo(expected2), "ctrls-a-4");
        Assert.That((object?)form.GetNextControl(ctrls_a[2], true), Is.EqualTo(con_b), "ctrls-a-5");
        object expected3 = ctrls_a[1];
        Assert.That((object?)form.GetNextControl(ctrls_a[2], false), Is.EqualTo(expected3), "ctrls-a-6");

        object expected4 = ctrls_b[1];
        Assert.That((object?)form.GetNextControl(ctrls_b[0], true), Is.EqualTo(expected4), "ctrls-b-1");
        Assert.That((object?)form.GetNextControl(ctrls_b[0], false), Is.EqualTo(con_b), "ctrls-b-2");
        object expected5 = ctrls_b[2];
        Assert.That((object?)form.GetNextControl(ctrls_b[1], true), Is.EqualTo(expected5), "ctrls-b-3");
        object expected6 = ctrls_b[0];
        Assert.That((object?)form.GetNextControl(ctrls_b[1], false), Is.EqualTo(expected6), "ctrls-b-4");
        Assert.That((object?)form.GetNextControl(ctrls_b[2], true), Is.EqualTo(con_c), "ctrls-b-5");
        object expected7 = ctrls_b[1];
        Assert.That((object?)form.GetNextControl(ctrls_b[2], false), Is.EqualTo(expected7), "ctrls-b-6");

        object expected8 = ctrls_c[1];
        Assert.That((object?)form.GetNextControl(ctrls_c[0], true), Is.EqualTo(expected8), "ctrls-c-1");
        Assert.That((object?)form.GetNextControl(ctrls_c[0], false), Is.EqualTo(con_c), "ctrls-c-2");
        object expected9 = ctrls_c[2];
        Assert.That((object?)form.GetNextControl(ctrls_c[1], true), Is.EqualTo(expected9), "ctrls-c-3");
        object expected10 = ctrls_c[0];
        Assert.That((object?)form.GetNextControl(ctrls_c[1], false), Is.EqualTo(expected10), "ctrls-c-4");
        Assert.That((object?)form.GetNextControl(ctrls_c[2], true), Is.EqualTo(null), "ctrls-c-5");
        object expected11 = ctrls_c[1];
        Assert.That((object?)form.GetNextControl(ctrls_c[2], false), Is.EqualTo(expected11), "ctrls-c-6");
        form.Dispose();
    }

    [Test]
    public void GetNextControlFromContainerA()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var con_a = new ContainerPoker("container-a");
        var con_b = new ContainerPoker("container-b");
        var con_c = new ContainerPoker("container-c");
        ControlPoker[] ctrls_a =
        [
            new("ctrls-a-0"), new("ctrls-a-1"), new("ctrls-a-2")
        ];
        ControlPoker[] ctrls_b =
        [
            new("ctrls-b-0"), new("ctrls-b-1"), new("ctrls-b-2")
        ];
        ControlPoker[] ctrls_c =
        [
            new("ctrls-c-0"), new("ctrls-c-1"), new("ctrls-c-2")
        ];

        con_a.Controls.AddRange(ctrls_a);
        con_b.Controls.AddRange(ctrls_b);
        con_c.Controls.AddRange(ctrls_c);

        form.Controls.Add(con_a);
        form.Controls.Add(con_b);
        form.Controls.Add(con_c);

        form.Show();

        // top level movement, 
        object expected = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(null!, true), Is.EqualTo(expected), "null-1");
        object expected1 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(null!, false), Is.EqualTo(expected1), "null-2");

        object expected2 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(form, true), Is.EqualTo(expected2), "form-1");
        object expected3 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(form, false), Is.EqualTo(expected3), "form-2");

        object expected4 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(con_a, true), Is.EqualTo(expected4), "container-1");
        object expected5 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(con_a, false), Is.EqualTo(expected5), "container-2");
        object expected6 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(con_b, true), Is.EqualTo(expected6), "container-3");
        object expected7 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(con_b, false), Is.EqualTo(expected7), "container-4");
        object expected8 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(con_c, true), Is.EqualTo(expected8), "container-5");
        object expected9 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(con_c, false), Is.EqualTo(expected9), "container-6");

        object expected10 = ctrls_a[1];
        Assert.That((object?)con_a.GetNextControl(ctrls_a[0], true), Is.EqualTo(expected10), "ctrls-a-1");
        Assert.That((object?)con_a.GetNextControl(ctrls_a[0], false), Is.EqualTo(null), "ctrls-a-2");
        object expected11 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(ctrls_a[1], true), Is.EqualTo(expected11), "ctrls-a-3");
        object expected12 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(ctrls_a[1], false), Is.EqualTo(expected12), "ctrls-a-4");
        Assert.That((object?)con_a.GetNextControl(ctrls_a[2], true), Is.EqualTo(null), "ctrls-a-5");
        object expected13 = ctrls_a[1];
        Assert.That((object?)con_a.GetNextControl(ctrls_a[2], false), Is.EqualTo(expected13), "ctrls-a-6");

        object expected14 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(ctrls_b[0], true), Is.EqualTo(expected14), "ctrls-b-1");
        object expected15 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(ctrls_b[0], false), Is.EqualTo(expected15), "ctrls-b-2");
        object expected16 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(ctrls_b[1], true), Is.EqualTo(expected16), "ctrls-b-3");
        object expected17 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(ctrls_b[1], false), Is.EqualTo(expected17), "ctrls-b-4");
        object expected18 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(ctrls_b[2], true), Is.EqualTo(expected18), "ctrls-b-5");
        object expected19 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(ctrls_b[2], false), Is.EqualTo(expected19), "ctrls-b-6");

        object expected20 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(ctrls_c[0], true), Is.EqualTo(expected20), "ctrls-c-1");
        object expected21 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(ctrls_c[0], false), Is.EqualTo(expected21), "ctrls-c-2");
        object expected22 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(ctrls_c[1], true), Is.EqualTo(expected22), "ctrls-c-3");
        object expected23 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(ctrls_c[1], false), Is.EqualTo(expected23), "ctrls-c-4");
        object expected24 = ctrls_a[0];
        Assert.That((object?)con_a.GetNextControl(ctrls_c[2], true), Is.EqualTo(expected24), "ctrls-c-5");
        object expected25 = ctrls_a[2];
        Assert.That((object?)con_a.GetNextControl(ctrls_c[2], false), Is.EqualTo(expected25), "ctrls-c-6");
        form.Dispose();
    }

    [Test]
    public void GetNextControlFromContainerB()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var con_a = new ContainerPoker("container-a");
        var con_b = new ContainerPoker("container-b");
        var con_c = new ContainerPoker("container-c");
        ControlPoker[] ctrls_a =
        [
            new("ctrls-a-0"), new("ctrls-a-1"), new("ctrls-a-2")
        ];
        ControlPoker[] ctrls_b =
        [
            new("ctrls-b-0"), new("ctrls-b-1"), new("ctrls-b-2")
        ];
        ControlPoker[] ctrls_c =
        [
            new("ctrls-c-0"), new("ctrls-c-1"), new("ctrls-c-2")
        ];

        con_a.Controls.AddRange(ctrls_a);
        con_b.Controls.AddRange(ctrls_b);
        con_c.Controls.AddRange(ctrls_c);

        form.Controls.Add(con_a);
        form.Controls.Add(con_b);
        form.Controls.Add(con_c);

        form.Show();

        // top level movement
        object expected = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(null!, true), Is.EqualTo(expected), "null-1");
        object expected1 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(null!, false), Is.EqualTo(expected1), "null-2");

        object expected2 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(form, true), Is.EqualTo(expected2), "form-1");
        object expected3 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(form, false), Is.EqualTo(expected3), "form-2");

        object expected4 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(con_a, true), Is.EqualTo(expected4), "container-1");
        object expected5 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(con_a, false), Is.EqualTo(expected5), "container-2");
        object expected6 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(con_b, true), Is.EqualTo(expected6), "container-3");
        object expected7 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(con_b, false), Is.EqualTo(expected7), "container-4");
        object expected8 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(con_c, true), Is.EqualTo(expected8), "container-5");
        object expected9 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(con_c, false), Is.EqualTo(expected9), "container-6");

        object expected10 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(ctrls_a[0], true), Is.EqualTo(expected10), "ctrls-a-1");
        object expected11 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(ctrls_a[0], false), Is.EqualTo(expected11), "ctrls-a-2");
        object expected12 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(ctrls_a[1], true), Is.EqualTo(expected12), "ctrls-a-3");
        object expected13 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(ctrls_a[1], false), Is.EqualTo(expected13), "ctrls-a-4");
        object expected14 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(ctrls_a[2], true), Is.EqualTo(expected14), "ctrls-a-5");
        object expected15 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(ctrls_a[2], false), Is.EqualTo(expected15), "ctrls-a-6");

        object expected16 = ctrls_b[1];
        Assert.That((object?)con_b.GetNextControl(ctrls_b[0], true), Is.EqualTo(expected16), "ctrls-b-1");
        Assert.That((object?)con_b.GetNextControl(ctrls_b[0], false), Is.EqualTo(null), "ctrls-b-2");
        object expected17 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(ctrls_b[1], true), Is.EqualTo(expected17), "ctrls-b-3");
        object expected18 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(ctrls_b[1], false), Is.EqualTo(expected18), "ctrls-b-4");
        Assert.That((object?)con_b.GetNextControl(ctrls_b[2], true), Is.EqualTo(null), "ctrls-b-5");
        object expected19 = ctrls_b[1];
        Assert.That((object?)con_b.GetNextControl(ctrls_b[2], false), Is.EqualTo(expected19), "ctrls-b-6");

        object expected20 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(ctrls_c[0], true), Is.EqualTo(expected20), "ctrls-c-1");
        object expected21 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(ctrls_c[0], false), Is.EqualTo(expected21), "ctrls-c-2");
        object expected22 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(ctrls_c[1], true), Is.EqualTo(expected22), "ctrls-c-3");
        object expected23 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(ctrls_c[1], false), Is.EqualTo(expected23), "ctrls-c-4");
        object expected24 = ctrls_b[0];
        Assert.That((object?)con_b.GetNextControl(ctrls_c[2], true), Is.EqualTo(expected24), "ctrls-c-5");
        object expected25 = ctrls_b[2];
        Assert.That((object?)con_b.GetNextControl(ctrls_c[2], false), Is.EqualTo(expected25), "ctrls-c-6");
        form.Dispose();
    }

    [Test]
    public void GetNextControlFromContainerC()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var con_a = new ContainerPoker("container-a");
        var con_b = new ContainerPoker("container-b");
        var con_c = new ContainerPoker("container-c");
        ControlPoker[] ctrls_a =
        [
            new("ctrls-a-0"), new("ctrls-a-1"), new("ctrls-a-2")
        ];
        ControlPoker[] ctrls_b =
        [
            new("ctrls-b-0"), new("ctrls-b-1"), new("ctrls-b-2")
        ];
        ControlPoker[] ctrls_c =
        [
            new("ctrls-c-0"), new("ctrls-c-1"), new("ctrls-c-2")
        ];

        con_a.Controls.AddRange(ctrls_a);
        con_b.Controls.AddRange(ctrls_b);
        con_c.Controls.AddRange(ctrls_c);

        form.Controls.Add(con_a);
        form.Controls.Add(con_b);
        form.Controls.Add(con_c);

        form.Show();

        // top level movement, 
        object expected = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(null!, true), Is.EqualTo(expected), "null-1");
        object expected1 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(null!, false), Is.EqualTo(expected1), "null-2");

        object expected2 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(form, true), Is.EqualTo(expected2), "form-1");
        object expected3 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(form, false), Is.EqualTo(expected3), "form-2");

        object expected4 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(con_a, true), Is.EqualTo(expected4), "container-1");
        object expected5 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(con_a, false), Is.EqualTo(expected5), "container-2");
        object expected6 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(con_b, true), Is.EqualTo(expected6), "container-3");
        object expected7 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(con_b, false), Is.EqualTo(expected7), "container-4");
        object expected8 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(con_c, true), Is.EqualTo(expected8), "container-5");
        object expected9 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(con_c, false), Is.EqualTo(expected9), "container-6");

        object expected10 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(ctrls_a[0], true), Is.EqualTo(expected10), "ctrls-a-1");
        object expected11 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(ctrls_a[0], false), Is.EqualTo(expected11), "ctrls-a-2");
        object expected12 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(ctrls_a[1], true), Is.EqualTo(expected12), "ctrls-a-3");
        object expected13 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(ctrls_a[1], false), Is.EqualTo(expected13), "ctrls-a-4");
        object expected14 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(ctrls_a[2], true), Is.EqualTo(expected14), "ctrls-a-5");
        object expected15 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(ctrls_a[2], false), Is.EqualTo(expected15), "ctrls-a-6");

        object expected16 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(ctrls_b[0], true), Is.EqualTo(expected16), "ctrls-b-1");
        object expected17 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(ctrls_b[0], false), Is.EqualTo(expected17), "ctrls-b-2");
        object expected18 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(ctrls_b[1], true), Is.EqualTo(expected18), "ctrls-b-3");
        object expected19 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(ctrls_b[1], false), Is.EqualTo(expected19), "ctrls-b-4");
        object expected20 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(ctrls_b[2], true), Is.EqualTo(expected20), "ctrls-b-5");
        object expected21 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(ctrls_b[2], false), Is.EqualTo(expected21), "ctrls-b-6");

        object expected22 = ctrls_c[1];
        Assert.That((object?)con_c.GetNextControl(ctrls_c[0], true), Is.EqualTo(expected22), "ctrls-c-1");
        Assert.That((object?)con_c.GetNextControl(ctrls_c[0], false), Is.EqualTo(null), "ctrls-c-2");
        object expected23 = ctrls_c[2];
        Assert.That((object?)con_c.GetNextControl(ctrls_c[1], true), Is.EqualTo(expected23), "ctrls-c-3");
        object expected24 = ctrls_c[0];
        Assert.That((object?)con_c.GetNextControl(ctrls_c[1], false), Is.EqualTo(expected24), "ctrls-c-4");
        Assert.That((object?)con_c.GetNextControl(ctrls_c[2], true), Is.EqualTo(null), "ctrls-c-5");
        object expected25 = ctrls_c[1];
        Assert.That((object?)con_c.GetNextControl(ctrls_c[2], false), Is.EqualTo(expected25), "ctrls-c-6");
        form.Dispose();
    }

    [Test]
    public void GetNextControl2FromForm()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var con_a = new ContainerPoker("container-a");
        var con_b = new ContainerPoker("container-b");
        var con_c = new ContainerPoker("container-c");

        RemoveWarning(con_b, con_c);

        ControlPoker[] ctrls_a =
        [
            new("ctrls-a-0"), new("ctrls-a-1"), new("ctrls-a-2")
        ];
        var ctrl_b = new ControlPoker("ctrl-b");

        con_a.Controls.AddRange(ctrls_a);

        form.Controls.Add(con_a);
        form.Controls.Add(ctrl_b);

        form.Show();

        // top level movement, 
        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(con_a), "null-1");
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(ctrl_b), "null-2");

        Assert.That((object?)form.GetNextControl(form, true), Is.EqualTo(con_a), "form-1");
        Assert.That((object?)form.GetNextControl(form, false), Is.EqualTo(ctrl_b), "form-2");

        Assert.That((object?)form.GetNextControl(con_a, true), Is.EqualTo(ctrl_b), "con-a-1");
        Assert.That((object?)form.GetNextControl(con_a, false), Is.EqualTo(null), "con-a-2");

        Assert.That((object?)form.GetNextControl(ctrl_b, true), Is.EqualTo(null), "ctrl-b-1");
        Assert.That((object?)form.GetNextControl(ctrl_b, false), Is.EqualTo(con_a), "ctrl-b-2");

        object expected = ctrls_a[1];
        Assert.That((object?)form.GetNextControl(ctrls_a[0], true), Is.EqualTo(expected), "ctrl-a-1");
        Assert.That((object?)form.GetNextControl(ctrls_a[0], false), Is.EqualTo(con_a), "ctrl-a-2");
        object expected1 = ctrls_a[2];
        Assert.That((object?)form.GetNextControl(ctrls_a[1], true), Is.EqualTo(expected1), "ctrl-a-1");
        object expected2 = ctrls_a[0];
        Assert.That((object?)form.GetNextControl(ctrls_a[1], false), Is.EqualTo(expected2), "ctrl-a-2");
        Assert.That((object?)form.GetNextControl(ctrls_a[2], true), Is.EqualTo(ctrl_b), "ctrl-a-1");
        object expected3 = ctrls_a[1];
        Assert.That((object?)form.GetNextControl(ctrls_a[2], false), Is.EqualTo(expected3), "ctrl-a-2");
        form.Dispose();
    }

    [Test]
    public void GetNextControlFlat()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        form.Controls.AddRange(flat_controls);
        form.Show();

        object expected = flat_controls[0];
        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(expected), "form-1");
        object expected1 = flat_controls[2];
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(expected1), "form-2");
        object expected2 = flat_controls[1];
        Assert.That((object?)form.GetNextControl(flat_controls[0], true), Is.EqualTo(expected2), "form-3");
        Assert.That((object?)form.GetNextControl(flat_controls[0], false), Is.EqualTo(null), "form-4");
        object expected3 = flat_controls[2];
        Assert.That((object?)form.GetNextControl(flat_controls[1], true), Is.EqualTo(expected3), "form-5");
        object expected4 = flat_controls[0];
        Assert.That((object?)form.GetNextControl(flat_controls[1], false), Is.EqualTo(expected4), "form-6");
        Assert.That((object?)form.GetNextControl(flat_controls[2], true), Is.EqualTo(null), "form-7");
        object expected5 = flat_controls[1];
        Assert.That((object?)form.GetNextControl(flat_controls[2], false), Is.EqualTo(expected5), "form-8");


        Assert.That((object?)flat_controls[0].GetNextControl(null!, true), Is.EqualTo(null), "ctrls-0-1");
        Assert.That((object?)flat_controls[0].GetNextControl(null!, false), Is.EqualTo(null), "ctrls-0-2");
        Assert.That((object?)flat_controls[0].GetNextControl(flat_controls[0], true), Is.EqualTo(null), "ctrls-0-3");
        Assert.That((object?)flat_controls[0].GetNextControl(flat_controls[0], false), Is.EqualTo(null), "ctrls-0-4");
        Assert.That((object?)flat_controls[0].GetNextControl(flat_controls[1], true), Is.EqualTo(null), "ctrls-0-5");
        Assert.That((object?)flat_controls[0].GetNextControl(flat_controls[1], false), Is.EqualTo(null), "ctrls-0-6");
        Assert.That((object?)flat_controls[0].GetNextControl(flat_controls[2], true), Is.EqualTo(null), "ctrls-0-7");
        Assert.That((object?)flat_controls[0].GetNextControl(flat_controls[2], false), Is.EqualTo(null), "ctrls-0-8");
        form.Dispose();
    }

    [Test]
    public void GetNextGroupBoxControlFlat()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var gbp = new GroupBoxPoker("group-box");

        gbp.Controls.AddRange(flat_controls);
        form.Controls.Add(gbp);
        form.Show();

        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(gbp), "form-1");
        object expected = flat_controls[2];
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(expected), "form-2");

        object expected1 = flat_controls[0];
        Assert.That((object?)form.GetNextControl(gbp, true), Is.EqualTo(expected1), "gb-1");
        Assert.That((object?)form.GetNextControl(gbp, false), Is.EqualTo(null), "gb-2");

        object expected2 = flat_controls[0];
        Assert.That((object?)gbp.GetNextControl(null!, true), Is.EqualTo(expected2), "gb-3");
        object expected3 = flat_controls[2];
        Assert.That((object?)gbp.GetNextControl(null!, false), Is.EqualTo(expected3), "gb-4");
        object expected4 = flat_controls[0];
        Assert.That((object?)gbp.GetNextControl(gbp, true), Is.EqualTo(expected4), "gb-5");
        object expected5 = flat_controls[2];
        Assert.That((object?)gbp.GetNextControl(gbp, false), Is.EqualTo(expected5), "gb-6");

        object expected6 = flat_controls[1];
        Assert.That((object?)form.GetNextControl(flat_controls[0], true), Is.EqualTo(expected6), "form-ctrls-0-forward");
        Assert.That((object?)form.GetNextControl(flat_controls[0], false), Is.EqualTo(gbp), "form-ctrls-0-backward");
        object expected7 = flat_controls[2];
        Assert.That((object?)form.GetNextControl(flat_controls[1], true), Is.EqualTo(expected7), "form-ctrls-1-forward");
        object expected8 = flat_controls[0];
        Assert.That((object?)form.GetNextControl(flat_controls[1], false), Is.EqualTo(expected8), "form-ctrls-1-backward");
        Assert.That((object?)form.GetNextControl(flat_controls[2], true), Is.EqualTo(null), "form-ctrls-2-forward");
        object expected9 = flat_controls[1];
        Assert.That((object?)form.GetNextControl(flat_controls[2], false), Is.EqualTo(expected9), "form-ctrls-2-backward");

        object expected10 = flat_controls[1];
        Assert.That((object?)gbp.GetNextControl(flat_controls[0], true), Is.EqualTo(expected10), "gbp-ctrls-0-forward");
        Assert.That((object?)gbp.GetNextControl(flat_controls[0], false), Is.EqualTo(null), "gbp-ctrls-0-backward");
        object expected11 = flat_controls[2];
        Assert.That((object?)gbp.GetNextControl(flat_controls[1], true), Is.EqualTo(expected11), "gbp-ctrls-1-forward");
        object expected12 = flat_controls[0];
        Assert.That((object?)gbp.GetNextControl(flat_controls[1], false), Is.EqualTo(expected12), "gbp-ctrls-1-backward");
        Assert.That((object?)gbp.GetNextControl(flat_controls[2], true), Is.EqualTo(null), "gbp-ctrls-2-forward");
        object expected13 = flat_controls[1];
        Assert.That((object?)gbp.GetNextControl(flat_controls[2], false), Is.EqualTo(expected13), "gbp-ctrls-2-backward");
        form.Dispose();
    }

    [Test]
    public void GetNextControlFromTabControl()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var tab = new TabControl();
        var page1 = new TabPage("page one");
        var page2 = new TabPage("page two");

        tab.TabPages.Add(page1);
        tab.TabPages.Add(page2);

        form.Controls.Add(tab);
        form.Show();

        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(tab), "form-1");
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(page2), "form-2");

        Assert.That((object?)form.GetNextControl(tab, true), Is.EqualTo(page1), "tab-1");
        Assert.That((object?)form.GetNextControl(tab, false), Is.EqualTo(null), "tab-2");

        Assert.That((object?)form.GetNextControl(page1, true), Is.EqualTo(page2), "page-one-1");
        Assert.That((object?)form.GetNextControl(page1, false), Is.EqualTo(tab), "page-one-2");

        Assert.That((object?)form.GetNextControl(page2, true), Is.EqualTo(null), "page-two-1");
        Assert.That((object?)form.GetNextControl(page2, false), Is.EqualTo(page1), "page-two-2");
        form.Dispose();
    }

    [Test]
    public void GetNextControlFromTabControl2()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var tab = new TabControl();

        var page1 = new TabPage("page one");
        page1.Controls.AddRange(flat_controls);

        var page2 = new TabPage("page two");

        tab.TabPages.Add(page1);

        tab.TabPages.Add(page2);

        form.Controls.Add(tab);
        form.Show();

        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(tab), "form-1");
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(page2), "form-2");

        Assert.That((object?)form.GetNextControl(tab, true), Is.EqualTo(page1), "tab-1");
        Assert.That((object?)form.GetNextControl(tab, false), Is.EqualTo(null), "tab-2");

        object expected = flat_controls[0];
        Assert.That((object?)form.GetNextControl(page1, true), Is.EqualTo(expected), "page-one-1");
        Assert.That((object?)form.GetNextControl(page1, false), Is.EqualTo(tab), "page-one-2");

        Assert.That((object?)form.GetNextControl(page2, true), Is.EqualTo(null), "page-two-1");
        object expected1 = flat_controls[2];
        Assert.That((object?)form.GetNextControl(page2, false), Is.EqualTo(expected1), "page-two-2");

        Assert.That((object?)form.GetNextControl(flat_controls[0], false), Is.EqualTo(page1), "form-ctrls-0-backward");
        Assert.That((object?)form.GetNextControl(flat_controls[2], true), Is.EqualTo(page2), "form-ctrls-2-forward");

        Assert.That((object?)tab.GetNextControl(null!, true), Is.EqualTo(page1), "tab-null-forward");
        Assert.That((object?)tab.GetNextControl(page1, false), Is.EqualTo(null), "tab-page1-backward");

        Assert.That((object?)tab.GetNextControl(flat_controls[0], false), Is.EqualTo(page1), "tab-ctrls-0-backward");
        Assert.That((object?)tab.GetNextControl(flat_controls[2], true), Is.EqualTo(page2), "tab-ctrls-2-forward");

        object expected2 = flat_controls[1];
        Assert.That((object?)page1.GetNextControl(flat_controls[0], true), Is.EqualTo(expected2), "page1-ctrls-0-forward");
        Assert.That((object?)page1.GetNextControl(flat_controls[0], false), Is.EqualTo(null), "page1-ctrls-0-backward");
        object expected3 = flat_controls[2];
        Assert.That((object?)page1.GetNextControl(flat_controls[1], true), Is.EqualTo(expected3), "page1-ctrls-1-forward");
        object expected4 = flat_controls[0];
        Assert.That((object?)page1.GetNextControl(flat_controls[1], false), Is.EqualTo(expected4), "page1-ctrls-1-backward");
        Assert.That((object?)page1.GetNextControl(flat_controls[2], true), Is.EqualTo(null), "page1-ctrls-2-forward");
        object expected5 = flat_controls[1];
        Assert.That((object?)page1.GetNextControl(flat_controls[2], false), Is.EqualTo(expected5), "page1-ctrls-2-backward");
        form.Dispose();
    }

    [Test]
    public void GetNextControlTabIndex()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        ControlPoker[] ctrls = new ControlPoker[5];

        for (var i = 0; i < 5; i++)
        {
            ctrls[i] = new ControlPoker();
            ctrls[i].TabIndex = i;
            ctrls[i].Text = "ctrl " + i;
        }

        form.Controls.AddRange(ctrls);
        form.Show();

        object expected = ctrls[0];
        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(expected));
        object expected1 = ctrls[4];
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(expected1));

        object expected2 = ctrls[1];
        Assert.That((object?)form.GetNextControl(ctrls[0], true), Is.EqualTo(expected2));
        Assert.That((object?)form.GetNextControl(ctrls[0], false), Is.EqualTo(null));

        object expected3 = ctrls[2];
        Assert.That((object?)form.GetNextControl(ctrls[1], true), Is.EqualTo(expected3));
        object expected4 = ctrls[0];
        Assert.That((object?)form.GetNextControl(ctrls[1], false), Is.EqualTo(expected4));

        object expected5 = ctrls[3];
        Assert.That((object?)form.GetNextControl(ctrls[2], true), Is.EqualTo(expected5));
        object expected6 = ctrls[1];
        Assert.That((object?)form.GetNextControl(ctrls[2], false), Is.EqualTo(expected6));

        object expected7 = ctrls[4];
        Assert.That((object?)form.GetNextControl(ctrls[3], true), Is.EqualTo(expected7));
        object expected8 = ctrls[2];
        Assert.That((object?)form.GetNextControl(ctrls[3], false), Is.EqualTo(expected8));

        Assert.That((object?)form.GetNextControl(ctrls[4], true), Is.EqualTo(null));
        object expected9 = ctrls[3];
        Assert.That((object?)form.GetNextControl(ctrls[4], false), Is.EqualTo(expected9));

        form.Dispose();
    }

    [Test]
    public void GetNextControlDuplicateTabIndex()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        ControlPoker[] ctrls = new ControlPoker[5];

        for (var i = 0; i < 5; i++)
        {
            ctrls[i] = new ControlPoker();
            ctrls[i].TabIndex = i;
            ctrls[i].Text = "ctrl " + i;
        }

        ctrls[3].TabIndex = 2;

        form.Controls.AddRange(ctrls);
        form.Show();

        object expected = ctrls[0];
        Assert.That((object?)form.GetNextControl(null!, true), Is.EqualTo(expected));
        object expected1 = ctrls[4];
        Assert.That((object?)form.GetNextControl(null!, false), Is.EqualTo(expected1));

        object expected2 = ctrls[1];
        Assert.That((object?)form.GetNextControl(ctrls[0], true), Is.EqualTo(expected2));
        Assert.That((object?)form.GetNextControl(ctrls[0], false), Is.EqualTo(null));

        object expected3 = ctrls[2];
        Assert.That((object?)form.GetNextControl(ctrls[1], true), Is.EqualTo(expected3));
        object expected4 = ctrls[0];
        Assert.That((object?)form.GetNextControl(ctrls[1], false), Is.EqualTo(expected4));

        object expected5 = ctrls[3];
        Assert.That((object?)form.GetNextControl(ctrls[2], true), Is.EqualTo(expected5));
        object expected6 = ctrls[1];
        Assert.That((object?)form.GetNextControl(ctrls[2], false), Is.EqualTo(expected6));

        object expected7 = ctrls[4];
        Assert.That((object?)form.GetNextControl(ctrls[3], true), Is.EqualTo(expected7));
        object expected8 = ctrls[2];
        Assert.That((object?)form.GetNextControl(ctrls[3], false), Is.EqualTo(expected8));

        Assert.That((object?)form.GetNextControl(ctrls[4], true), Is.EqualTo(null));
        object expected9 = ctrls[3];
        Assert.That((object?)form.GetNextControl(ctrls[4], false), Is.EqualTo(expected9));

        form.Dispose();
    }

    [Test]
    public void GetNextControlComposite()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var a = new ControlPoker("a");
        var b = new ControlPoker("b");
        var c = new ControlPoker("c");

        form.Controls.Add(a);
        form.Controls.Add(b);
        b.Controls.Add(c);

        form.Show();

        Assert.That((object?)form.GetNextControl(a, true), Is.EqualTo(b), "form-1");
        Assert.That((object?)form.GetNextControl(a, false), Is.EqualTo(null), "form-2");

        form.Dispose();
    }

    [Test]
    public void ActiveControl()
    {
        //			if (TestHelper.RunningOnUnix) {
        //				Assert.Ignore ("Relies on form.Show() synchronously generating WM_ACTIVATE");
        //			}

        var form = new Form();
        form.ShowInTaskbar = false;

        form.Controls.AddRange(flat_controls);
        form.Show();

        object expected = flat_controls[0];
        Assert.That((object?)form.ActiveControl, Is.EqualTo(expected));

        flat_controls[1].Focus();

        object expected1 = flat_controls[1];
        Assert.That((object?)form.ActiveControl, Is.EqualTo(expected1));

        form.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ActiveControl2()
    {
        var cc = new ContainerControl();
        var c1 = new Control();
        cc.Controls.Add(c1);
        var c2 = new Control();
        cc.Controls.Add(c2);
        var c3 = new Control();
        cc.Controls.Add(c3);
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.IsNull(cc.ActiveControl);

        cc.ActiveControl = c1;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        cc.ActiveControl = c2;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c2));

        c1.Focus();
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c2));

        cc.ActiveControl = c2;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c2));

        cc.Controls.Remove(c2);
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        cc.ActiveControl = c3;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c3));

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(cc);
        form.Show();

        Assert.IsTrue(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        cc.ActiveControl = c3;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsTrue(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c3));

        c1.Focus();
        Assert.IsTrue(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsFalse(c3.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        form.Dispose();
    }

    [Test] // bug #80411
    public void ActiveControl_NoChild()
    {
        var cc = new ContainerControl();
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                cc.ActiveControl = new Control();
            }
            catch (ArgumentException ex)
            {
                Assert.That((object?)typeof(ArgumentException), Is.EqualTo(ex.GetType()));
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    private StringBuilder sb;

    private void enter(object? sender, EventArgs e)
    {
        sb.Append($"OnEnter: {((Control)sender!).Name} {sender}");
        sb.Append("\n");
    }

    private void leave(object? sender, EventArgs e)
    {
        sb.Append($"OnLeave: {((Control)sender!).Name} {sender}");
        sb.Append("\n");
    }

    private void gotfocus(object? sender, EventArgs e)
    {
        sb.Append($"OnGotFocus: {((Control)sender!).Name} {sender}");
        sb.Append("\n");
    }

    private void lostfocus(object? sender, EventArgs e)
    {
        sb.Append($"OnLostFocus: {((Control)sender!).Name} {sender}");
        sb.Append("\n");
    }

    private void validating(object? sender, CancelEventArgs e)
    {
        sb.Append($"OnValidating: {((Control)sender!).Name} {sender}");
        sb.Append("\n");
    }

    private void validated(object? sender, EventArgs e)
    {
        sb.Append($"OnValidated: {((Control)sender!).Name} {sender}");
        sb.Append("\n");
    }

    private void connect(Control c)
    {
        c.Enter += enter;
        c.Leave += leave;
        c.GotFocus += gotfocus;
        c.LostFocus += lostfocus;
        c.Validating += validating;
        c.Validated += validated;
    }

    [Test]
    public void EnterLeaveFocusEventTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Name = "Form1";
        var cc0 = new ContainerControl();
        cc0.Name = "ContainerControl 0";
        var cc1 = new ContainerControl();
        cc1.Name = "ContainerControl 1";
        var cc2 = new ContainerControl();
        cc2.Name = "ContainerControl 2";
        var c1 = new Control();
        c1.Name = "Control 1";
        var c2 = new Control();
        c2.Name = "Control 2";

        connect(f);
        connect(cc0);
        connect(cc1);
        connect(cc2);
        connect(c1);
        connect(c2);

        cc0.Controls.Add(cc1);
        cc0.Controls.Add(cc2);
        cc1.Controls.Add(c1);
        cc2.Controls.Add(c2);

        f.Controls.Add(cc0);

        sb = new StringBuilder();
        f.Show();
        c1.Select();

        Assert.That((object?)@"OnEnter: ContainerControl 0 System.Windows.Forms.ContainerControl
OnEnter: ContainerControl 1 System.Windows.Forms.ContainerControl
OnEnter: Control 1 System.Windows.Forms.Control
OnGotFocus: Control 1 System.Windows.Forms.Control
", Is.EqualTo(sb.ToString()), "1");

        sb.Length = 0;
        c2.Select();
        Assert.That((object?)@"OnLeave: Control 1 System.Windows.Forms.Control
OnLeave: ContainerControl 1 System.Windows.Forms.ContainerControl
OnValidating: Control 1 System.Windows.Forms.Control
OnValidated: Control 1 System.Windows.Forms.Control
OnValidating: ContainerControl 1 System.Windows.Forms.ContainerControl
OnValidated: ContainerControl 1 System.Windows.Forms.ContainerControl
OnEnter: ContainerControl 2 System.Windows.Forms.ContainerControl
OnEnter: Control 2 System.Windows.Forms.Control
OnLostFocus: Control 1 System.Windows.Forms.Control
OnGotFocus: Control 2 System.Windows.Forms.Control
", Is.EqualTo(sb.ToString()), "2");

        sb.Length = 0;
        cc1.Select();
        Assert.That((object?)@"OnLeave: Control 2 System.Windows.Forms.Control
OnLeave: ContainerControl 2 System.Windows.Forms.ContainerControl
OnValidating: Control 2 System.Windows.Forms.Control
OnValidated: Control 2 System.Windows.Forms.Control
OnValidating: ContainerControl 2 System.Windows.Forms.ContainerControl
OnValidated: ContainerControl 2 System.Windows.Forms.ContainerControl
OnEnter: ContainerControl 1 System.Windows.Forms.ContainerControl
OnLostFocus: Control 2 System.Windows.Forms.Control
OnGotFocus: ContainerControl 1 System.Windows.Forms.ContainerControl
", Is.EqualTo(sb.ToString()), "3");

        sb.Length = 0;
        cc2.Select();
        Assert.That((object?)@"OnLeave: ContainerControl 1 System.Windows.Forms.ContainerControl
OnValidating: ContainerControl 1 System.Windows.Forms.ContainerControl
OnValidated: ContainerControl 1 System.Windows.Forms.ContainerControl
OnEnter: ContainerControl 2 System.Windows.Forms.ContainerControl
OnLostFocus: ContainerControl 1 System.Windows.Forms.ContainerControl
OnGotFocus: ContainerControl 2 System.Windows.Forms.ContainerControl
", Is.EqualTo(sb.ToString()), "4");

        Assert.IsNull(cc2.ActiveControl, "5");

        sb.Length = 0;
        c2.Select();
        Assert.That((object?)@"OnEnter: Control 2 System.Windows.Forms.Control
OnLostFocus: ContainerControl 2 System.Windows.Forms.ContainerControl
OnGotFocus: Control 2 System.Windows.Forms.Control
", Is.EqualTo(sb.ToString()), "6");

        sb.Length = 0;
        cc1.Select();
        Assert.That((object?)@"OnLeave: Control 2 System.Windows.Forms.Control
OnLeave: ContainerControl 2 System.Windows.Forms.ContainerControl
OnValidating: Control 2 System.Windows.Forms.Control
OnValidated: Control 2 System.Windows.Forms.Control
OnValidating: ContainerControl 2 System.Windows.Forms.ContainerControl
OnValidated: ContainerControl 2 System.Windows.Forms.ContainerControl
OnEnter: ContainerControl 1 System.Windows.Forms.ContainerControl
OnLostFocus: Control 2 System.Windows.Forms.Control
OnGotFocus: ContainerControl 1 System.Windows.Forms.ContainerControl
", Is.EqualTo(sb.ToString()), "7");

        sb.Length = 0;
        f.Select();
        Assert.That((object?)"", Is.EqualTo(sb.ToString()), "8");

        f.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ActiveControl_Invisible()
    {
        var cc = new ContainerControl();
        var c1 = new Control();
        c1.Visible = false;
        cc.Controls.Add(c1);
        var c2 = new Control();
        cc.Controls.Add(c2);
        cc.ActiveControl = c1;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(cc);
        form.Show();

        Assert.IsFalse(c1.Focused);
        Assert.IsTrue(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c2));

        cc.ActiveControl = c1;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        form.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ActiveControl_Disabled()
    {
        var cc = new ContainerControl();
        var c1 = new Control();
        c1.Enabled = false;
        cc.Controls.Add(c1);
        var c2 = new Control();
        cc.Controls.Add(c2);
        cc.ActiveControl = c1;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(cc);
        form.Show();

        Assert.IsFalse(c1.Focused);
        Assert.IsTrue(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c2));

        cc.ActiveControl = c1;
        Assert.IsFalse(c1.Focused);
        Assert.IsTrue(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        form.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ActiveControl_Null()
    {
        var cc = new ContainerControl();
        var c1 = new Control();
        cc.Controls.Add(c1);
        var c2 = new Control();
        cc.Controls.Add(c2);
        cc.ActiveControl = c1;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        cc.ActiveControl = null;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsNull(cc.ActiveControl);

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(cc);
        form.Show();

        Assert.IsTrue(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c1));

        cc.ActiveControl = c2;
        Assert.IsFalse(c1.Focused);
        Assert.IsTrue(c2.Focused);
        Assert.That((object?)cc.ActiveControl, Is.SameAs(c2));

        cc.ActiveControl = null;
        Assert.IsFalse(c1.Focused);
        Assert.IsFalse(c2.Focused);
        Assert.IsNull(cc.ActiveControl);

        form.Dispose();
    }

    // #372616
    [Test]
    public void UserControlFocus()
    {
        var form = new Form();
        var c = new UserControl();
        var t1 = new TextBox();
        var t2 = new TextBox();
        form.Controls.Add(t1);
        c.Controls.Add(t2);
        form.Controls.Add(c);
        form.Show();

        c.Focus();

        Assert.IsTrue(t2.Focused);
        Assert.That((object?)c.ActiveControl, Is.SameAs(t2));

        form.Close();
    }
}
//
// Copyright (c) 2007 Novell, Inc.
//
// Authors:
//	Rolf Bjarne Kvinge  (RKvinge@novell.com)
//

using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using FormsApplication = System.Windows.Forms.Application;
using GtkTests.Helpers;

namespace GtkTests.Controls;

[TestFixture]
public class TimerTest : TestHelper
{
    private bool Ticked;

    [Test]
    public void IntervalException1()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var timer = new Timer();
            timer.Interval = 0;
        });
    }

    [Test]
    public void IntervalException2()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var timer = new Timer();
            timer.Interval = -1;
        });
    }

    [Test]
    public void IntervalException3()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var timer = new Timer();
            timer.Interval = int.MaxValue;
        });
    }

    [Test]
    public void IntervalException4()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var timer = new Timer();
            timer.Interval = int.MinValue;
        });
    }

    [Test]
    [Category("NotWorking")]
    public void StartTest()
    {
        // This test fails about 50% of the time on the buildbots.
        Ticked = false;
        using var timer = new Timer();
        timer.Tick += TickHandler;
        timer.Start();
        Thread.Sleep(500);
        FormsApplication.DoEvents();
        Assert.That((object?)timer.Enabled, Is.EqualTo(true), "1");
        Assert.That((object?)Ticked, Is.EqualTo(true), "2");
    }

    [Test]
    public void StopTest()
    {
        Ticked = false;
        using var timer = new Timer();
        timer.Tick += TickHandler;
        timer.Interval = 200;
        timer.Start();
        Assert.That((object?)timer.Enabled, Is.EqualTo(true), "1");
        Assert.That((object?)Ticked, Is.EqualTo(false), "2");
        timer.Stop();
        Assert.That((object?)Ticked, Is.EqualTo(false), "3"); // This may fail if we are running on a very slow machine...
        Assert.That((object?)timer.Enabled, Is.EqualTo(false), "4");
        Thread.Sleep(500);
        Assert.That((object?)Ticked, Is.EqualTo(false), "5");
    }

    [Test]
    public void TagTest()
    {
        var timer = new Timer();
        timer.Tag = "a";
        Assert.That(timer.Tag, Is.EqualTo("a"), "1");
    }

    /* Application.DoEvents and Sleep are not guarenteed on Linux
    [Test]
    public void EnabledTest ()
    {
        Ticked = false;
        using (Timer timer = new Timer ()) {
            timer.Tick += new EventHandler (TickHandler);
            timer.Enabled = true;
            Sys_Threading.Thread.Sleep (150);
            Application.DoEvents ();
            Assert1.AreEqual(true, timer.Enabled, "1");
            Assert1.AreEqual(true, Ticked, "2");
        }

        Ticked = false;
        using (Timer timer = new Timer ()) {
            timer.Tick += new EventHandler (TickHandler);
            timer.Interval = 1000;
            timer.Enabled = true;
            Assert1.AreEqual(true, timer.Enabled, "3");
            Assert1.AreEqual(false, Ticked, "4");
            timer.Enabled = false;
            Assert1.AreEqual(false, Ticked, "5"); // This may fail if we are running on a very slow machine...
            Assert1.AreEqual(false, timer.Enabled, "6");
        }
    }
    */

    private void TickHandler(object? sender, EventArgs e)
    {
        Ticked = true;
    }

    [Test]
    public void DefaultProperties()
    {
        var timer = new Timer();
        Assert.That((object?)timer.Container, Is.EqualTo(null));
        Assert.That((object?)timer.Enabled, Is.EqualTo(false));
        Assert.That((object?)timer.Interval, Is.EqualTo(100));
        Assert.That((object?)timer.Site, Is.EqualTo(null));
        Assert.That(timer.Tag, Is.EqualTo(null));
    }

}
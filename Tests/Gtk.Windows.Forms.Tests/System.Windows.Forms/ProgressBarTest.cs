//
// ProgressBarTest.cs: Test cases for ProgressBar.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using System.Windows.Forms;
using System.Drawing;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ProgressBarTest : TestHelper
{
    [Test]
    public void ProgressBarPropertyTest()
    {
        var myProgressBar = new ProgressBar();

        // A
        Assert.That((object?)myProgressBar.AllowDrop, Is.EqualTo(false));

        // B
        Assert.That((object?)myProgressBar.BackColor.Name, Is.EqualTo("Control"));
        Assert.That((object?)myProgressBar.BackgroundImage, Is.EqualTo(null));
        var gif = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        myProgressBar.BackgroundImage = Image.FromFile(gif);
        // comparing image objects fails on MS .Net so using Size property
        object expected = Image.FromFile(gif, true).Size;
        Assert.That((object?)myProgressBar.BackgroundImage.Size, Is.EqualTo(expected));

        // F 
        Assert.That((object?)myProgressBar.Font?.Style, Is.EqualTo(FontStyle.Regular));

        // M
        Assert.That((object?)myProgressBar.Maximum, Is.EqualTo(100));
        Assert.That((object?)myProgressBar.Minimum, Is.EqualTo(0));

        // R
        Assert.That((object?)myProgressBar.RightToLeft, Is.EqualTo(RightToLeft.No));

        // S
        Assert.That((object?)myProgressBar.Step, Is.EqualTo(10));

        // T
        Assert.That((object?)myProgressBar.Text, Is.EqualTo(string.Empty));
        myProgressBar.Text = "New ProgressBar";
        Assert.That((object?)myProgressBar.Text, Is.EqualTo("New ProgressBar"));

        // V
        Assert.That((object?)myProgressBar.Value, Is.EqualTo(0));
    }

    [Test]
    public void ForeColorTest()
    {
        var progressBar = new ProgressBar();
        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(SystemColors.Highlight));
        progressBar.ForeColor = Color.Red;
        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(Color.Red));
        progressBar.ForeColor = Color.White;
        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(Color.White));

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(progressBar);
        form.Show();

        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(Color.White));
        progressBar.ForeColor = Color.Red;
        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(Color.Red));
        progressBar.ForeColor = Color.Red;
        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(Color.Red));
        progressBar.ForeColor = Color.Blue;
        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(Color.Blue));

        form.Close();
    }

    [Test]
    public void ResetForeColor()
    {
        var progressBar = new ProgressBar();
        progressBar.ForeColor = Color.Red;
        progressBar.ResetForeColor();
        Assert.That((object?)progressBar.ForeColor, Is.EqualTo(SystemColors.Highlight));
    }

    [Test]
    public void ValueTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var myProgressBar = new ProgressBar();
            myProgressBar.Value = -1;
            myProgressBar.Value = 100;
        });
    }

    [Test]
    public void MinMax()
    {
        var expectedArgExType = typeof(ArgumentOutOfRangeException);
        //
        var c = new ProgressBar();
        Assert.That((object?)c.Minimum, Is.EqualTo(0), "default_min");
        Assert.That((object?)c.Maximum, Is.EqualTo(100), "default_max");
        Assert.That((object?)c.Value, Is.EqualTo(0), "default_value");
        //----
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                c.Minimum = -1;
            }
            catch (ArgumentException ex)
            {
                // MSDN says ArgumentException, but really its *subtype* ArgumentOutOfRangeException.
                // Actually it changed in FX2.
                Assert.That((object?)ex.GetType(), Is.EqualTo(expectedArgExType), "Typeof Min-1");
                Assert.That((object?)ex.ParamName, Is.EqualTo("Minimum"), "ParamName Min-1"); // (culture insensitive).
                throw;
            }
        });
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                c.Maximum = -1;
            }
            catch (ArgumentException ex)
            {
                Assert.That((object?)ex.GetType(), Is.EqualTo(expectedArgExType), "Typeof Max-1");
                Assert.That((object?)ex.ParamName, Is.EqualTo("Maximum"), "ParamName Max-1"); // (culture insensitive).
                throw;
            }
        });
        Assert.That((object?)c.Minimum, Is.EqualTo(0), "after Min/Max-1_min");
        Assert.That((object?)c.Maximum, Is.EqualTo(100), "after Min/Max-1_max");
        Assert.That((object?)c.Value, Is.EqualTo(0), "after Min/Max-1_value");
        //
        // What happens when Min/Max is set respectively above/below the current Value
        // and Max/Min values.
        c.Minimum = 200;
        Assert.That((object?)c.Minimum, Is.EqualTo(200), "200L_min");
        Assert.That((object?)c.Maximum, Is.EqualTo(200), "200L_max");
        Assert.That((object?)c.Value, Is.EqualTo(200), "200L_value");
        //
        c.Minimum = 50;
        Assert.That((object?)c.Minimum, Is.EqualTo(50), "50L_min");
        Assert.That((object?)c.Maximum, Is.EqualTo(200), "50L_max");
        Assert.That((object?)c.Value, Is.EqualTo(200), "50L_value");
        //
        c.Maximum = 30;
        Assert.That((object?)c.Minimum, Is.EqualTo(30), "30T_min");
        Assert.That((object?)c.Maximum, Is.EqualTo(30), "30T_max");
        Assert.That((object?)c.Value, Is.EqualTo(30), "30T_value");
        //
        // What happens when Value is set outside the Min/Max ranges.
        c.Maximum = 50;
        Assert.That((object?)c.Minimum, Is.EqualTo(30), "50T_min");
        Assert.That((object?)c.Maximum, Is.EqualTo(50), "50T_max");
        c.Value = 45;
        Assert.That((object?)c.Value, Is.EqualTo(45), "50T_value");
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                c.Value = 29;
            }
            catch (ArgumentException ex)
            {
                Assert.That((object?)ex.GetType(), Is.EqualTo(expectedArgExType), "Typeof 29");
                Assert.That((object?)ex.ParamName, Is.EqualTo("Value"), "ParamName 29");
                throw;
            }
        });
        Assert.That((object?)c.Value, Is.EqualTo(45), "after 29_value");
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                c.Value = 51;
            }
            catch (ArgumentException ex)
            {
                Assert.That((object?)ex.GetType(), Is.EqualTo(expectedArgExType), "Typeof 51");
                Assert.That((object?)ex.ParamName, Is.EqualTo("Value"), "ParamName 151");
                throw;
            }
        });
        Assert.That((object?)c.Value, Is.EqualTo(45), "after 51_value");
    }

    [Test]
    public void PerformStepAndIncrement()
    {
        var c = new ProgressBar();
        //
        c.Value = 10;
        c.Step = 30;
        Assert.That((object?)c.Value, Is.EqualTo(10), "StepAt30_Init");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(40), "StepAt30_1");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(70), "StepAt30_2");
        //
        c.Value = 0;
        c.Step = 20;
        Assert.That((object?)c.Value, Is.EqualTo(0), "StepAt20_Init");
        //
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(20), "StepAt20_1");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(40), "StepAt20_2");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(60), "StepAt20_3");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(80), "StepAt20_4");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(100), "StepAt20_5");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(100), "StepAt20_6x");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(100), "StepAt20_7x");
        //
        c.Step = -20;
        Assert.That((object?)c.Value, Is.EqualTo(100), "StepAt2Neg0_Init");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(80), "StepAtNeg20_1");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(60), "StepAtNeg20_2");
        //
        c.Step = -40;
        Assert.That((object?)c.Value, Is.EqualTo(60), "StepAt2Neg40_Init");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(20), "StepAtNeg40_1");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(0), "StepAtNeg40_2");
        c.PerformStep();
        Assert.That((object?)c.Value, Is.EqualTo(0), "StepAtNeg40_2");
        //
        c.Increment(30);
        Assert.That((object?)c.Value, Is.EqualTo(30), "Increment30_1");
        c.Increment(30);
        Assert.That((object?)c.Value, Is.EqualTo(60), "Increment30_2");
        c.Increment(30);
        Assert.That((object?)c.Value, Is.EqualTo(90), "Increment30_3");
        c.Increment(30);
        Assert.That((object?)c.Value, Is.EqualTo(100), "Increment30_4x");
    }

    [Test]
    public void Styles()
    {
        var c = new ProgressBar();
        //--
        Assert.That((object?)c.Style, Is.EqualTo(ProgressBarStyle.Blocks), "orig=blocks");
        //--
        c.Style = ProgressBarStyle.Continuous;
        //--
        c.Style = ProgressBarStyle.Marquee;
        // Increment and PerformStep are documented to fail in Marquee style.
        Assert.Throws<InvalidOperationException>(() =>
        {
            c.Increment(5);
        });
        Assert.Throws<InvalidOperationException>(() => c.PerformStep());
        // What about the other value-related properties?  No fail apparently!
        c.Value = 20;
        c.Minimum = 5;
        c.Maximum = 95;
        //--
        // Now undefined style values...
        Assert.Throws<global::System.ComponentModel.InvalidEnumArgumentException>(() =>
        {
            try
            {
                c.Style = (ProgressBarStyle)4;
            }
            catch (global::System.ComponentModel.InvalidEnumArgumentException ex)
            {
                //Console.WriteLine(ex.Message);
                object expected = typeof(global::System.ComponentModel.InvalidEnumArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected), "Typeof bad style4");
                Assert.That((object?)ex.ParamName, Is.EqualTo("value"), "ParamName bad style 4");
                throw;
            }
        });
        Assert.Throws<global::System.ComponentModel.InvalidEnumArgumentException>(() =>
        {
            try
            {
                c.Style = (ProgressBarStyle)99;
            }
            catch (global::System.ComponentModel.InvalidEnumArgumentException ex)
            {
                object expected = typeof(global::System.ComponentModel.InvalidEnumArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected), "Typeof bad style99");
                Assert.That((object?)ex.ParamName, Is.EqualTo("value"), "ParamName bad style 99");
                throw;
            }
        });
    }

    [Test]
    public void ToStringMethodTest()
    {
        var myProgressBar = new ProgressBar();
        myProgressBar.Text = "New ProgressBar";
        Assert.That((object?)myProgressBar.ToString(), Is.EqualTo("System.Windows.Forms.ProgressBar, Minimum: 0, Maximum: 100, Value: 0"));
    }
    // [MonoTODO("Add test for method Increment (Visual Test)")]
    // [MonoTODO("Add test for method PerformStep (Visual Test)")]
}
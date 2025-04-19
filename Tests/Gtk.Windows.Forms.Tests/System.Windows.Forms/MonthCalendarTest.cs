//
// MonthCalendarTest.cs: Test case for MonthCalendar
// 
// Authors:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class MonthCalendarTest : TestHelper
{
    [Test]
    public void MonthCalendarPropertyTest()
    {
        var myfrm = new Form();
        myfrm.ShowInTaskbar = false;
        var myMonthCal1 = new MonthCalendar();
        //MonthCalendar myMonthCal2 = new MonthCalendar ();
        myMonthCal1.Name = "MonthCendar";
        myMonthCal1.TabIndex = 1;
        //DateTime myDateTime = new DateTime ();

        // F
        Assert.That((object?)myMonthCal1.FirstDayOfWeek, Is.EqualTo(Day.Default));
        myMonthCal1.FirstDayOfWeek = Day.Sunday;
        Assert.That((object?)myMonthCal1.FirstDayOfWeek, Is.EqualTo(Day.Sunday));
        Assert.That((object?)myMonthCal1.ForeColor.Name, Is.EqualTo("WindowText"));

        // M 
        object expected = new DateTime(9998, 12, 31);
        Assert.That((object?)myMonthCal1.MaxDate, Is.EqualTo(expected));
        object expected1 = new DateTime(1753, 1, 1);
        Assert.That((object?)myMonthCal1.MinDate, Is.EqualTo(expected1));
        Assert.That((object?)myMonthCal1.ShowToday, Is.EqualTo(true));
        Assert.That((object?)myMonthCal1.ShowTodayCircle, Is.EqualTo(true));
        Assert.That((object?)myMonthCal1.ShowWeekNumbers, Is.EqualTo(false));
        // Font dependent. // Assert1.AreEqual(153, myMonthCal1.SingleMonthSize.Height, "#S8a");
        // Font dependent. // Assert1.AreEqual(176, myMonthCal1.SingleMonthSize.Width, "#S8b");
        Assert.That((object?)myMonthCal1.Site, Is.EqualTo(null));
        Assert.That((object?)myMonthCal1.TodayDate, Is.EqualTo(DateTime.Today));

        myfrm.Dispose();
    }

    [Test]
    public void InitialSizeTest()
    {
        var cal = new MonthCalendar();
        Assert.IsTrue(cal.Size != Size.Empty);
    }

    [Test]
    public void MonthCalMaxDateException()
    {
        var myMonthCal1 = new MonthCalendar();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                myMonthCal1.MaxDate = new DateTime(1752, 1, 1, 0, 0, 0, 0); // value is less than min date (01/01/1753)
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("MaxDate"));
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test]
    public void MonthCalMinDateException()
    {
        var myMonthCal1 = new MonthCalendar();

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                myMonthCal1.MinDate = new DateTime(1752, 1, 1, 0, 0, 0, 0); // Date earlier than 01/01/1753
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("MinDate"));
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
        
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                myMonthCal1.MinDate = new DateTime(9999, 12, 31, 0, 0, 0, 0); // Date greater than max date
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("MinDate"));
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test]
    public void MonthCalSelectRangeException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var myMonthCal1 = new MonthCalendar();
            myMonthCal1.SelectionRange = new SelectionRange(new DateTime(1752, 01, 01), new DateTime(1752, 01, 02));
        });
    }

    [Test]
    public void MonthCalSelectRangeException2()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var myMonthCal1 = new MonthCalendar();
            myMonthCal1.SelectionRange = new SelectionRange(new DateTime(9999, 12, 30), new DateTime(9999, 12, 31));
        });
    }

}

[TestFixture]
public class MonthCalendarPropertiesTest : MonthCalendar
{
    private bool clickRaised;
    private bool doubleClickRaised;

    [SetUp]
    protected void SetUp () {
        clickRaised = false;
        doubleClickRaised = false;
    }

    [Test]
    public void DefaultMarginTest ()
    {
        Assert.That((object?)DefaultMargin.All, Is.EqualTo(9));
    }
}


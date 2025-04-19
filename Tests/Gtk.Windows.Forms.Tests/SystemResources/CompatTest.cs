//
// CompatTest.cs: Compatibility unit tests for ResXResourceReader.
//
// Authors:
//     Robert Jordan <robertj@gmx.net>
//

using GtkTests.Helpers;

namespace GtkTests.SystemResources;

[TestFixture]
public class CompatTest : TestHelper
{
    [Test]
    public void TestReader ()
    {
        CompatTestHelper.TestReader (TestResourceHelper.GetFullPathOfResource ("GtkTests.SystemResources.compat_1_1.resx"));
    }

    [Test]
    public void TestReader_2_0 ()
    {
        CompatTestHelper.TestReader (TestResourceHelper.GetFullPathOfResource ("GtkTests.SystemResources.compat_2_0.resx"));
    }
}
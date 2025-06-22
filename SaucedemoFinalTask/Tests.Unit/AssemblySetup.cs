using FluentAssertions;

namespace Tests.Unit;

[TestClass]
public static class AssemblySetup
{
    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        License.Accepted = true;
    }
}

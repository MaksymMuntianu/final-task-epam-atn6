using FluentAssertions;

namespace Tests.Unit;

[TestClass]
public static class AssemblySetup
{
    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        // Ensure the License of Fluent Assersion is accepted before running tests.
        License.Accepted = true;
    }
}

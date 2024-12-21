using Host.IntegrationTests.Fixtures;

namespace Host.IntegrationTests;

public class HomePageTests : BaseTest, IClassFixture<TestFixture>
{
    public HomePageTests(TestFixture service, ITestOutputHelper output) : base(service, output) { }
    
    [Fact]
    public async Task Home_page_Should_be_ok() =>
        (await Client.GetAsync("/")).EnsureSuccessStatusCode();
}
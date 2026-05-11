using AutoFixture;
using AutoFixture.AutoMoq;

namespace InventoryAPI.Tests.Customizations;

public static class FixtureFactory
{
    public static IFixture Create()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
        fixture.Customize(new InventoryCustomization());
        return fixture;
    }
}
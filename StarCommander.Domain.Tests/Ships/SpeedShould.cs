using StarCommander.Domain.Ships;
using Xunit;

namespace StarCommander.Domain.Tests.Ships;

public class SpeedShould
{
	[Fact]
	public void MultiplyWithTimeToCalculateDistance()
	{
		Assert.Equal(new (100), new Speed(10) * new Time(10));
	}
}
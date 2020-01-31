using StarCommander.Domain.Ships;
using Xunit;

namespace StarCommander.Domain.Tests.Ships
{
	public class TimeShould
	{
		[Fact]
		public void MultiplyWithSpeedToCalculateDistance()
		{
			Assert.Equal(new Distance(100), new Time(10) * new Speed(10));
		}
	}
}
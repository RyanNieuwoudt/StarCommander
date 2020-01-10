using StarCommander.Domain.Ships;
using Xunit;

namespace StarCommander.Domain.Tests.Ships
{
	public class PositionShould
	{
		[Theory]
		[InlineData(0, 10, 0, -10)]
		[InlineData(45, 0, 0, 0)]
		[InlineData(45, 10, 5, 5)]
		[InlineData(90, 10, 10, 0)]
		[InlineData(115, 22, 20, 9)]
		[InlineData(180, 10, 0, 10)]
		[InlineData(270, 10, -10, 0)]
		public void ProduceEndPositionByApplyingHeadingAndDistance(double heading, long distance, long endX, long endY)
		{
			Assert.Equal(new Position(endX, endY),
				new Position(0, 0).Apply(new Heading(heading), new Distance(distance)));
		}
	}
}
using StarCommander.Domain.Ships;
using Xunit;

namespace StarCommander.Domain.Tests.Ships
{
	public class PositionShould
	{
		[Theory]
		[InlineData(0, 10, 0, -10)]
		[InlineData(10, 34, 6, -33)]
		[InlineData(45, 0, 0, 0)]
		[InlineData(45, 10, 7, -7)]
		[InlineData(55, 12, 10, -7)]
		[InlineData(90, 10, 10, 0)]
		[InlineData(115, 22, 20, 9)]
		[InlineData(135, 10, 7, 7)]
		[InlineData(160, 50, 17, 47)]
		[InlineData(180, 10, 0, 10)]
		[InlineData(200, 32, -11, -30)]
		[InlineData(225, 10, -7, -7)]
		[InlineData(250, 6, -6, -2)]
		[InlineData(270, 10, -10, 0)]
		[InlineData(283, 20, -19, 4)]
		[InlineData(315, 10, -7, 7)]
		[InlineData(348, 15, -3, 15)]
		public void ProduceEndPositionByApplyingHeadingAndDistance(double heading, long distance, long endX, long endY)
		{
			var expectedEnd = new Position(endX, endY);
			var end = new Position().Apply(new Heading(heading), new Distance(distance));

			Assert.Equal(expectedEnd.X, end.X);
			Assert.Equal(expectedEnd.Y, end.Y);
		}
	}
}
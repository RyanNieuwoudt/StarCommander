using Xunit;

namespace StarCommander.Shared.Tests
{
	public class StringExtensionsShould
	{
		[Theory]
		[InlineData("OnChange", "ON_CHANGE")]
		[InlineData("OnPlayerNameChanged", "ON_PLAYER_NAME_CHANGED")]
		public void ConvertClassNameToActionType(string className, string actionType)
		{
			Assert.Equal(actionType, className.ToActionType());
		}

		[Theory]
		[InlineData("ON_CHANGE", "OnChange")]
		[InlineData("ON_PLAYER_NAME_CHANGED", "OnPlayerNameChanged")]
		public void ConvertActionTypeToClassName(string actionType, string className)
		{
			Assert.Equal(className, actionType.ToClassName());
		}
	}
}
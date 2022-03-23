namespace StarCommander.Shared.Model.Payload;

public class OnPlayerNameChanged : OnPlayer
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
}
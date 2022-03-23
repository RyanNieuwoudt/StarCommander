using System;

namespace StarCommander.Shared.Model.Payload;

public abstract class OnPlayer
{
	public Guid PlayerId { get; set; }
}
using System;
using System.Collections.Generic;
using NodaTime;

namespace StarCommander.Domain.Ships;

public class NavigationComputer
{
	readonly IClock clock;
	readonly Position startingPosition;

	readonly Dictionary<Instant, KeyValuePair<Heading, Speed>> navigation;

	Heading lastHeading;
	Speed lastSpeed;
	Instant start;

	internal NavigationComputer(IClock clock, Position startingPosition)
	{
		this.clock = clock;
		this.startingPosition = startingPosition;

		start = Instant.MaxValue;
		navigation = new ();
	}

	public void SetHeading(Instant instant, Heading heading)
	{
		if (instant < start)
		{
			start = instant;
		}

		if (navigation.ContainsKey(instant))
		{
			navigation[instant] = new (heading, lastSpeed);
		}
		else
		{
			navigation.Add(instant, new (heading, lastSpeed));
		}

		lastHeading = heading;
	}

	public void SetSpeed(Instant instant, Speed speed)
	{
		if (instant < start)
		{
			start = instant;
		}

		if (navigation.ContainsKey(instant))
		{
			navigation[instant] = new (lastHeading, speed);
		}
		else
		{
			navigation.Add(instant, new (lastHeading, speed));
		}

		lastSpeed = speed;
	}

	public (Instant, Heading, Position, Speed) Locate()
	{
		var currentInstant = start;
		var currentHeading = Heading.Default;
		var currentPosition = startingPosition;
		var currentSpeed = Speed.Default;

		foreach (var (instant, (heading, speed)) in navigation)
		{
			currentPosition = currentPosition.Apply(heading, new ((instant - currentInstant).Seconds * speed));

			currentInstant = instant;
			currentHeading = heading;
			currentSpeed = speed;
		}

		var lastDate = clock.GetCurrentInstant();

		currentPosition = currentPosition.Apply(currentHeading,
			new ((long)(lastDate - currentInstant).TotalSeconds * currentSpeed));

		return (lastDate, currentHeading, currentPosition, currentSpeed);
	}
}
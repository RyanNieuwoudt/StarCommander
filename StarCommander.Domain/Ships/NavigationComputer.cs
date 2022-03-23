using System;
using System.Collections.Generic;

namespace StarCommander.Domain.Ships;

public class NavigationComputer
{
	readonly Dictionary<DateTimeOffset, KeyValuePair<Heading, Speed>> navigation;
	readonly Position startingPosition;

	Heading lastHeading;
	Speed lastSpeed;
	DateTimeOffset startDate;

	internal NavigationComputer(Position startingPosition)
	{
		this.startingPosition = startingPosition;

		startDate = DateTimeOffset.MaxValue;
		navigation = new ();
	}

	public void SetHeading(DateTimeOffset date, Heading heading)
	{
		if (date < startDate)
		{
			startDate = date;
		}

		if (navigation.ContainsKey(date))
		{
			navigation[date] = new (heading, lastSpeed);
		}
		else
		{
			navigation.Add(date, new (heading, lastSpeed));
		}

		lastHeading = heading;
	}

	public void SetSpeed(DateTimeOffset date, Speed speed)
	{
		if (date < startDate)
		{
			startDate = date;
		}

		if (navigation.ContainsKey(date))
		{
			navigation[date] = new (lastHeading, speed);
		}
		else
		{
			navigation.Add(date, new (lastHeading, speed));
		}

		lastSpeed = speed;
	}

	public (DateTimeOffset, Heading, Position, Speed) Locate()
	{
		var currentDate = startDate;
		var currentHeading = Heading.Default;
		var currentPosition = startingPosition;
		var currentSpeed = Speed.Default;

		foreach (var (date, (heading, speed)) in navigation)
		{
			currentPosition = currentPosition.Apply(heading, new ((date - currentDate).Seconds * speed));

			currentDate = date;
			currentHeading = heading;
			currentSpeed = speed;
		}

		var lastDate = DateTimeOffset.Now;

		currentPosition = currentPosition.Apply(currentHeading,
			new ((long)(lastDate - currentDate).TotalSeconds * currentSpeed));

		return (lastDate, currentHeading, currentPosition, currentSpeed);
	}
}
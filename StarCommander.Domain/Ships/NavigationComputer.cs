using System;
using System.Collections.Generic;

namespace StarCommander.Domain.Ships
{
	public class NavigationComputer
	{
		readonly Dictionary<DateTimeOffset, KeyValuePair<Heading, Speed>> navigation;
		readonly Position startingPosition;

		DateTimeOffset latestDate;
		DateTimeOffset startDate;

		internal NavigationComputer(Position startingPosition)
		{
			this.startingPosition = startingPosition;
			latestDate = DateTimeOffset.MinValue;
			startDate = DateTimeOffset.MaxValue;

			navigation = new Dictionary<DateTimeOffset, KeyValuePair<Heading, Speed>>();

			Heading = Heading.Default;
			Speed = Speed.Default;
		}

		public Heading Heading { get; private set; }
		public Speed Speed { get; private set; }

		public void SetHeading(DateTimeOffset date, Heading heading)
		{
			if (date < startDate)
			{
				startDate = date;
			}

			if (navigation.ContainsKey(date))
			{
				navigation[date] = new KeyValuePair<Heading, Speed>(heading, Speed);
			}
			else
			{
				navigation.Add(date, new KeyValuePair<Heading, Speed>(heading, Speed));
			}

			latestDate = date;
			Heading = heading;
		}

		public void SetSpeed(DateTimeOffset date, Speed speed)
		{
			if (date < startDate)
			{
				startDate = date;
			}

			if (navigation.ContainsKey(date))
			{
				navigation[date] = new KeyValuePair<Heading, Speed>(Heading, speed);
			}
			else
			{
				navigation.Add(date, new KeyValuePair<Heading, Speed>(Heading, speed));
			}

			latestDate = date;
			Speed = speed;
		}

		public Position Locate()
		{
			var position = startingPosition;
			var current = startDate;

			foreach (var (date, (heading, speed)) in navigation)
			{
				position = position.Apply(heading, new Distance((date - current).Seconds * speed));
				current = date;
			}

			return position.Apply(Heading, new Distance((DateTimeOffset.Now - current).Seconds * Speed));
		}
	}
}
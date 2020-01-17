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
			if (date <= latestDate)
			{
				throw new Exception("Temporal anomaly detected.");
			}

			if (date < startDate)
			{
				startDate = date;
			}

			navigation.Add(date, new KeyValuePair<Heading, Speed>(heading, Speed));

			latestDate = date;
			Heading = heading;
		}

		public void SetSpeed(DateTimeOffset date, Speed speed)
		{
			if (date <= latestDate)
			{
				throw new Exception("Temporal anomaly detected.");
			}

			if (date < startDate)
			{
				startDate = date;
			}

			navigation.Add(date, new KeyValuePair<Heading, Speed>(Heading, speed));

			latestDate = date;
			Speed = speed;
		}

		public Position Locate()
		{
			var position = startingPosition;
			var now = startDate;

			foreach (var (date, (heading, speed)) in navigation)
			{
				position = position.Apply(heading, new Distance((date - now).Seconds * speed));
				now = date;
			}

			return position;
		}
	}
}
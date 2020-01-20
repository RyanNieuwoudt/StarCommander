using System;
using System.Collections.Generic;

namespace StarCommander.Domain.Ships
{
	public class NavigationComputer
	{
		readonly Dictionary<DateTimeOffset, KeyValuePair<Heading, Speed>> navigation;
		readonly Position startingPosition;

		DateTimeOffset startDate;

		internal NavigationComputer(Position startingPosition)
		{
			this.startingPosition = startingPosition;
			startDate = DateTimeOffset.MaxValue;

			navigation = new Dictionary<DateTimeOffset, KeyValuePair<Heading, Speed>>();

			Heading = Heading.Default;
			Position = startingPosition;
			Speed = Speed.Default;
		}

		public Heading Heading { get; private set; }
		public Position Position { get; private set; }
		public Speed Speed { get; private set; }

		public void Deconstruct(out Heading heading, out Position position, out Speed speed)
		{
			heading = Heading;
			position = Position;
			speed = Speed;
		}

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

			return Position = position.Apply(Heading,
				new Distance((long)(DateTimeOffset.Now - current).TotalSeconds * Speed));
		}
	}
}
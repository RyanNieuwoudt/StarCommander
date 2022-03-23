using System;
using System.Collections.Generic;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using StarCommander.Shared.Model.Payload;

namespace StarCommander.Application;

public static class NotificationRegistry
{
	static readonly Dictionary<Type, Type> P = new ();
	public static readonly IReadOnlyDictionary<Type, Type> Player = P;

	static NotificationRegistry()
	{
		AddPlayer<CaptainBoarded, OnCaptainBoarded>();
		AddPlayer<PlayerNameChanged, OnPlayerNameChanged>();
		AddPlayer<ShipLocated, OnShipLocated>();
	}

	static void AddPlayer<TU, TV>() where TU : INotifyPlayer => P.Add(typeof(TU), typeof(TV));
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StarCommander.Application;

static class Reflection
{
	internal static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType, Assembly assembly) =>
		from x in assembly.GetTypes()
		from z in x.GetInterfaces()
		let y = x.BaseType
		where y is { IsGenericType: true } && openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition()) ||
		      z.IsGenericType && openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition())
		select x;
}
using System.Globalization;
using System.Text.RegularExpressions;

namespace StarCommander.Shared;

static class StringExtensions
{
	internal static string ToActionType(this string s) =>
		Regex.Replace(s, "([a-z])([A-Z])", "$1_$2").Trim().ToUpperInvariant();

	internal static string ToClassName(this string s) => CultureInfo.InvariantCulture.TextInfo
		.ToTitleCase(s.ToLower().Replace("_", " "))
		.Replace(" ", "");
}
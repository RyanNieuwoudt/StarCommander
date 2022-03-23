using System;
using System.Security.Cryptography;
using System.Text;

namespace StarCommander.Application;

static class Password
{
	internal static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHashWithSalt(string password)
	{
		if (password == null)
		{
			throw new ArgumentNullException(nameof(password));
		}

		if (string.IsNullOrWhiteSpace(password))
		{
			throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
		}

		using var hmac = new HMACSHA512();

		return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
	}

	internal static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
	{
		if (password == null)
		{
			throw new ArgumentNullException(nameof(password));
		}

		if (string.IsNullOrWhiteSpace(password))
		{
			throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
		}

		if (storedHash.Length != 64)
		{
			throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
		}

		if (storedSalt.Length != 128)
		{
			throw new ArgumentException("Invalid length of password salt (128 bytes expected).",
				nameof(storedHash));
		}

		using var hmac = new HMACSHA512(storedSalt);

		return IsEqual(storedHash, hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
	}

	static bool IsEqual(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right)
	{
		return left.SequenceEqual(right);
	}
}
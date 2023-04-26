﻿using System.Security.Cryptography;

namespace Core.Services;

public class PasswordService
{
    #region Static Fields and Constants
    private const int Pbkdf2IterCount = 1000;
    private const int Pbkdf2SubkeyLength = 256 / 8;
    private const int SaltSize = 128 / 8;
    #endregion

    #region Private members
    private static bool ByteArraysEqual(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a == null || b == null || a.Count != b.Count)
            return false;

        var areSame = true;
        for (var i = 0; i < a.Count; i++)
        {
            areSame &= a[i] == b[i];
        }
        return areSame;
    }
    #endregion

    #region Public members
    public string Hash(string password)
    {
        if (password == null)
            throw new ArgumentNullException(nameof(password));

        byte[] salt;
        byte[] subkey;
        using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2IterCount))
        {
            salt = deriveBytes.Salt;
            subkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
        }

        var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
        return Convert.ToBase64String(outputBytes);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        if (hashedPassword == null)
            throw new ArgumentNullException(nameof(hashedPassword));
        if (password == null)
            throw new ArgumentNullException(nameof(password));

        var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

        if (hashedPasswordBytes.Length != 1 + SaltSize + Pbkdf2SubkeyLength || hashedPasswordBytes[0] != 0x00)
            return false;

        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
        var storedSubkey = new byte[Pbkdf2SubkeyLength];
        Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, Pbkdf2SubkeyLength);

        byte[] generatedSubkey;
        using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Pbkdf2IterCount))
        {
            generatedSubkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
        }
        return ByteArraysEqual(storedSubkey, generatedSubkey);
    }
    #endregion
}

using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class PlayerPrefsSafe
{
    private const int salt = 678309397;

    public static void SetInt(string key, int value)
    {
        int salted = value ^ salt;
        PlayerPrefs.SetInt(StringHash(key), salted);
        PlayerPrefs.SetInt(StringHash("_" + key), IntHash(value));
    }

    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public static int GetInt(string key, int defaultValue)
    {
        string hashedKey = StringHash(key);
        if (!PlayerPrefs.HasKey(hashedKey)) return defaultValue;

        int salted = PlayerPrefs.GetInt(hashedKey);
        int value = salted ^ salt;

        int loadedHash = PlayerPrefs.GetInt(StringHash("_" + key));
        if (loadedHash != IntHash(value)) return defaultValue;

        return value;
    }

    public static void SetFloat(string key, float value)
    {
        int intValue = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);

        int salted = intValue ^ salt;
        PlayerPrefs.SetInt(StringHash(key), salted);
        PlayerPrefs.SetInt(StringHash("_" + key), IntHash(intValue));
    }

    public static float GetFloat(string key)
    {
        return GetFloat(key, 0);
    }

    public static float GetFloat(string key, float defaultValue)
    {
        string hashedKey = StringHash(key);
        if (!PlayerPrefs.HasKey(hashedKey)) return defaultValue;

        int salted = PlayerPrefs.GetInt(hashedKey);
        int value = salted ^ salt;

        int loadedHash = PlayerPrefs.GetInt(StringHash("_" + key));
        if (loadedHash != IntHash(value)) return defaultValue;

        return BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
    }

    private static int IntHash(int x)
    {
        x = ((x >> 16) ^ x) * 0x45d9f3b;
        x = ((x >> 16) ^ x) * 0x45d9f3b;
        x = (x >> 16) ^ x;
        return x;
    }

    public static string StringHash(string x)
    {
        HashAlgorithm algorithm = SHA256.Create();
        StringBuilder sb = new StringBuilder();

        var bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(x));
        foreach (byte b in bytes) sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(StringHash(key));
        PlayerPrefs.DeleteKey(StringHash("_" + key));
    }

    public static bool HasKey(string key)
    {
        string hashedKey = StringHash(key);
        if (!PlayerPrefs.HasKey(hashedKey)) return false;

        int salted = PlayerPrefs.GetInt(hashedKey);
        int value = salted ^ salt;

        int loadedHash = PlayerPrefs.GetInt(StringHash("_" + key));

        return loadedHash == IntHash(value);
    }
}

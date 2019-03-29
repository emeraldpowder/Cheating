using System;

public struct SafeFloat
{
    private int value;
    private int salt;

    public SafeFloat(float value)
    {
        salt = UnityEngine.Random.Range(int.MinValue/4, int.MaxValue/4);
        int intValue = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        this.value = intValue ^ salt;
    }

    public override bool Equals(object obj)
    {
        return (float)this == (float)obj;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return ((float)this).ToString();
    }

    public static implicit operator float(SafeFloat safeFloat)
    {
        return BitConverter.ToSingle(BitConverter.GetBytes(safeFloat.salt ^ safeFloat.value), 0);
    }

    public static implicit operator SafeFloat(float normalFloat)
    {
        return new SafeFloat(normalFloat);
    }
}

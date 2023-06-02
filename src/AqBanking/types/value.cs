using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class Value : IEquatable<Value>, IComparable<Value>
{
    #region DLL Import

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_new")]
    private static extern IntPtr AB_Value_new();

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_free")]
    private static extern void AB_Value_free(IntPtr v);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_fromString", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Value_fromString([In, MarshalAs(UnmanagedType.LPStr)] string s);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_toString")]
    private static extern void AB_Value_toString(IntPtr v, IntPtr buf);

    // TBD:
    // AB_Value_toHumanReadableString

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_fromDouble")]
    private static extern IntPtr AB_Value_fromDouble(double i);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_fromInt")]
    private static extern IntPtr AB_Value_fromInt(long num, long denom);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_GetValueAsDouble")]
    private static extern double AB_Value_GetValueAsDouble(IntPtr v);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_SetValueFromDouble")]
    private static extern void AB_Value_SetValueFromDouble(IntPtr v, double i);

    // TBD:
    //private static extern int AB_Value_GetNumDenomString(IntPtr v, ...);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_SetZero")]
    private static extern void AB_Value_SetZero(IntPtr v);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_IsZero")]
    private static extern int AB_Value_IsZero(IntPtr v);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_IsNegative")]
    private static extern int AB_Value_IsNegative(IntPtr v);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_IsPositive")]
    private static extern int AB_Value_IsPositive(IntPtr v);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_Compare")]
    private static extern int AB_Value_Compare(IntPtr v1, IntPtr v2);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_Equal")]
    private static extern int AB_Value_Equal(IntPtr v1, IntPtr v2);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_AddValue")]
    private static extern int AB_Value_AddValue(IntPtr v1, IntPtr v2);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_SubValue")]
    private static extern int AB_Value_SubValue(IntPtr v1, IntPtr v2);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_MultValue")]
    private static extern int AB_Value_MultValue(IntPtr v1, IntPtr v2);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_DivValue")]
    private static extern int AB_Value_DivValue(IntPtr v1, IntPtr v2);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_Negate")]
    private static extern int AB_Value_Negate(IntPtr v);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_GetCurrency", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Value_GetCurrency(IntPtr v);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_SetCurrency", CharSet = CharSet.Ansi)]
    private static extern void AB_Value_SetCurrency(IntPtr v, [In, MarshalAs(UnmanagedType.LPStr)] string? s);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_Num")]
    private static extern long AB_Value_Num(IntPtr v);
    [DllImport("libaqbanking.so", EntryPoint = "AB_Value_Denom")]
    private static extern long AB_Value_Denom(IntPtr v);

    // NOTE: implementatino for AB_Value_toHbciString is skipped.

    #endregion

    internal readonly IntPtr _value;

    public Value()
    {
        this._value = AB_Value_new();
    }
    internal Value(IntPtr v)
    {
        this._value = v;
    }

    ~Value()
    {
        AB_Value_free(this._value);
    }

    /// <summary>
    /// This function reads a AB_VALUE from a string. Strings suitable as
    /// arguments are those created by @ref AB_Value_toString or simple
    /// floating point string (as in "123.45" or "-123.45").
    /// </summary>
    public static Value FromString(string s)
    {
        return new Value(AB_Value_fromString(s));
    }

    public static Value FromDouble(double i)
    {
        return new Value(AB_Value_fromDouble(i));
    }

    /// <summary>
    /// Returns a newly allocated rational number, initialized to
    /// num/denom.
    /// </summary>
    public static Value FromInt(long num, long denom)
    {
        return new Value(AB_Value_fromInt(num, denom));
    }

    /// <summary>
    /// This function returns the value as a double.
    /// You should not feed another AB_VALUE from this double, because the
    /// conversion from an AB_VALUE to a double might be lossy!
    /// </summary>
    public double GetValueAsDouble()
    {
        return AB_Value_GetValueAsDouble(this._value);
    }

    /// <summary>
    /// You should not use a double retrieved via <c>Value</c> as an argument to this function, because
    /// the conversion from AB_VALUE to double to AB_VALUE might change the
    /// real value.
    /// </summary>
    public void SetValueFromDouble(double i)
    {
        AB_Value_SetValueFromDouble(this._value, i);
    }

    public void SetZero()
    {
        AB_Value_SetZero(this._value);
    }

    public bool IsZero
    {
        get => AB_Value_IsZero(this._value) != 0;
    }
    public bool IsNegative
    {
        get => AB_Value_IsNegative(this._value) != 0;
    }
    public bool IsPositive
    {
        get => AB_Value_IsPositive(this._value) != 0;
    }

    public static Value operator +(Value a, Value b)
    {
        int returnValue = AB_Value_AddValue(a._value, b._value);
        ErrorHandling.CheckForErrors(returnValue);
        return a;
    }
    public static Value operator -(Value a, Value b)
    {
        int returnValue = AB_Value_SubValue(a._value, b._value);
        ErrorHandling.CheckForErrors(returnValue);
        return a;
    }
    public static Value operator *(Value a, Value b)
    {
        int returnValue = AB_Value_MultValue(a._value, b._value);
        ErrorHandling.CheckForErrors(returnValue);
        return a;
    }
    public static Value operator /(Value a, Value b)
    {
        int returnValue = AB_Value_DivValue(a._value, b._value);
        ErrorHandling.CheckForErrors(returnValue);
        return a;
    }

    public static Value operator -(Value a)
    {
        int returnValue = AB_Value_Negate(a._value);
        ErrorHandling.CheckForErrors(returnValue);
        return a;
    }

    /// <summary>
    /// Returns the numerator of the given rational number.
    /// </summary>
    public long Num
    {
        get => AB_Value_Num(this._value);
    }

    /// <summary>
    /// Returns the denominator of the given rational number.
    /// </summary>
    public long Denom
    {
        get => AB_Value_Denom(this._value);
    }

    public string? Currency
    {
        get => AB_Value_GetCurrency(this._value);
        set => AB_Value_SetCurrency(this._value, value);
    }

    public override string ToString()
    {
        var buf = new GWEN_Buffer(0, 129, 0, true);
        AB_Value_toString(_value, (IntPtr)buf);
        return buf.GetStart() ?? string.Empty;
    }

    public bool Equals(Value? other)
    {
        if (ReferenceEquals(other, null))
        {
            return false;
        }

        return AB_Value_Equal(this._value, other._value) != 0;
    }

    public int CompareTo(Value? other)
    {
        return AB_Value_Compare(this._value, other != null ? _value : (IntPtr)default);
    }
}

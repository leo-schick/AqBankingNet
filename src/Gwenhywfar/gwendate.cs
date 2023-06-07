using System.Runtime.InteropServices;

namespace Gwenhywfar;

/// <summary>
/// A wrapper class for the GWEN_DATE struct from the library gwenhywfar.
///
/// This implementation does not implement all possible functions available for the GWEN_DATE
/// but rather tries to best integrate it with C#. E.g. by making it cast-able to the C# type
/// DateTime and DateOnly.
/// </summary>
public class GwenDate
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libgwenhywfar.so")]
    private static extern IntPtr GWEN_Date_fromGregorian(int y, int m, int d);

    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_Date_fromJulian(int julian);

    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    private static extern void GWEN_Date_free(IntPtr gd);
    
    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_Date_GetString(IntPtr gd);
    
    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_Date_GetYear(IntPtr gd);

    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_Date_GetMonth(IntPtr gd);

    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_Date_GetDay(IntPtr gd);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _date;

    internal GwenDate(IntPtr gd)
    {
        this._date = gd;
    }

    ~GwenDate()
    {
        GWEN_Date_free(this._date);
    }

    /// <summary>
    /// Create a date from the gregorian calender using year, month and day.
    /// </summary>
    /// <param name="year">year (e.g. 2009)</param>
    /// <param name="month">month (1-12)</param>
    /// <param name="day">day of month (1-31)</param>
    public static GwenDate FromGregorian(int year, int month, int day)
    {
        return new GwenDate(GWEN_Date_fromGregorian(year, month, day));
    }

    /// <summary>
    /// Create a date from the julian calender.
    /// </summary>
    /// <param name="julian">julian date in julian calender</param>
    /// <returns></returns>
    public static GwenDate FromJulian(int julian)
    {
        return new GwenDate(GWEN_Date_fromJulian(julian));
    }

    public int Year => GWEN_Date_GetYear(this._date);
    public int Month => GWEN_Date_GetMonth(this._date);
    public int Day => GWEN_Date_GetDay(this._date);

    public static implicit operator DateTime(GwenDate gwenDate)
    {
        return new DateTime(gwenDate.Year, gwenDate.Month, gwenDate.Day);
    }

    public static implicit operator DateOnly(GwenDate gwenDate)
    {
        return new DateOnly(gwenDate.Year, gwenDate.Month, gwenDate.Day);
    }

    public static implicit operator GwenDate(DateTime date)
    {
        return FromGregorian(date.Year, date.Month, date.Day);
    }

    public static implicit operator GwenDate(DateOnly date)
    {
        return FromGregorian(date.Year, date.Month, date.Day);
    }

    public static explicit operator IntPtr(GwenDate gd) => gd._date;
    public static explicit operator GwenDate(IntPtr p) => new GwenDate(p);

    public override string ToString()
    {
        IntPtr ptr = GWEN_Date_GetString(this._date);
        return Marshal.PtrToStringAuto(ptr) ?? string.Empty;
    }
}

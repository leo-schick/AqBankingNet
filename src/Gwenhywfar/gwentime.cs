using System.Runtime.InteropServices;

namespace Gwenhywfar;

/// <summary>
/// A wrapper class for the GWEN_TIME struct from the library gwenhywfar.
///
/// This implementation does not implement all possible functions available for the GWEN_TIME
/// but rather tries to best integrate it with C#. E.g. by making it cast-able to the C# type
/// DateTime and DateOnly.
/// </summary>
public class GwenTime
{
    private static readonly DateTime Epoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libgwenhywfar")]
    private static extern IntPtr GWEN_Time_new(int year, int month, int day, int hour, int min, int sec, int inUtc);

    [DllImport("libgwenhywfar")]
    private static extern IntPtr GWEN_Time_fromSeconds(uint s);

    [DllImport("libgwenhywfar")]
    private static extern void GWEN_Time_free(IntPtr t);
    
    [DllImport("libgwenhywfar")]
    private static extern uint GWEN_Time_Seconds(IntPtr t);
    
    [DllImport("libgwenhywfar")]
    private static extern double GWEN_Time_Milliseconds(IntPtr t);

    // Not necessary:
    //[DllImport("libgwenhywfar")]
    //private static extern int GWEN_Time_Diff(IntPtr t1, IntPtr t0);
    //
    //[DllImport("libgwenhywfar")]
    //private static extern int GWEN_Time_DiffSeconds(IntPtr t1, IntPtr t0);
    //
    //[DllImport("libgwenhywfar")]
    //private static extern int GWEN_Time_Compare(IntPtr t1, IntPtr t0);
    //
    //[DllImport("libgwenhywfar")]
    //private static extern int GWEN_Time_AddSeconds(IntPtr t1, uint secs);
    //
    //[DllImport("libgwenhywfar")]
    //private static extern int GWEN_Time_SubSeconds(IntPtr t1, uint secs);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _time;

    private GwenTime(IntPtr t)
    {
        this._time = t;
    }

    /// <summary>
    /// Create a time using year, month, day, hour, min, sec.
    /// </summary>
    /// <param name="year">year year (e.g. 2009)</param>
    /// <param name="month">month month (0-11)</param>
    /// <param name="day">day day of month (1-31)</param>
    /// <param name="hour">hour</param>
    /// <param name="min">min minute (0-59)</param>
    /// <param name="sec">sec second (0-59)</param>
    /// <param name="inUtc">when true time is given in UTC</param>
    public GwenTime(int year, int month, int day, int hour, int min, int sec, bool inUtc)
    {
        _time = GWEN_Time_new(year, month, day, hour, min, sec, inUtc ? 1 : 0);
    }

    ~GwenTime()
    {
        GWEN_Time_free(this._time);
    }

    /// <summary>
    /// Creates a GwenTime object from the return value of <see cref="Seconds"/>.
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static GwenDate FromSeconds(uint seconds)
    {
        return new GwenDate(GWEN_Time_fromSeconds(seconds));
    }

    /// <summary>
    /// The time in seconds since the epoch (00:00:00 UTC Jan 1, 1970).
    /// </summary>
    public uint Seconds => GWEN_Time_Seconds(this._time);

    /// <summary>
    /// The time in milliseconds
    /// </summary>
    public double Milliseconds => GWEN_Time_Milliseconds(this._time);

    public static explicit operator IntPtr(GwenTime gwenTime) => gwenTime._time;
    public static explicit operator GwenTime(IntPtr p) => new GwenTime(p);
    
    public static implicit operator DateTime(GwenTime gwenTime)
    {
        return Epoc + TimeSpan.FromSeconds(gwenTime.Seconds);
    }

    public static implicit operator GwenTime(DateTime date)
    {
        bool inUtc = date.Kind == DateTimeKind.Utc;

        return new GwenTime(date.Year, date.Month, date.Day, date.Month, date.Minute, date.Second, inUtc);
    }
}

using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class Security
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking")]
    private static extern IntPtr AB_Security_new();
    [DllImport("libaqbanking")]
    private static extern void AB_Security_free(IntPtr p_struct);
    
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Security_GetName(IntPtr p_struct);
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Security_GetUniqueId(IntPtr p_struct);
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Security_GetNameSpace(IntPtr p_struct);
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Security_GetTickerSymbol(IntPtr p_struct);
    [DllImport("libaqbanking")]
    private static extern IntPtr AB_Security_GetUnits(IntPtr p_struct);
    [DllImport("libaqbanking")]
    private static extern IntPtr AB_Security_GetUnitPriceValue(IntPtr p_struct);
    [DllImport("libaqbanking")]
    private static extern IntPtr AB_Security_GetUnitPriceDate(IntPtr p_struct);
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    private static extern void AB_Security_SetName(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    private static extern void AB_Security_SetUniqueId(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    private static extern void AB_Security_SetNameSpace(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    private static extern void AB_Security_SetTickerSymbol(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);
    [DllImport("libaqbanking")]
    private static extern void AB_Security_SetUnits(IntPtr p_struct, IntPtr p_src);
    [DllImport("libaqbanking")]
    private static extern void AB_Security_SetUnitPriceValue(IntPtr p_struct, IntPtr p_src);
    [DllImport("libaqbanking")]
    private static extern void AB_Security_SetUnitPriceDate(IntPtr p_struct, IntPtr p_src);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _security;

    public Security()
    {
        this._security = AB_Security_new();
    }
    
    internal Security(IntPtr ptr)
    {
        this._security = ptr;
    }

    ~Security()
    {
        AB_Security_free(this._security);
    }
    
    public string? Name
    {
        get => AB_Security_GetName(this._security);
        set => AB_Security_SetName(this._security, value);
    }
    public string? UniqueId
    {
        get => AB_Security_GetUniqueId(this._security);
        set => AB_Security_SetUniqueId(this._security, value);
    }
    public string? NameSpace
    {
        get => AB_Security_GetNameSpace(this._security);
        set => AB_Security_SetNameSpace(this._security, value);
    }
    public string? TickerSymbol
    {
        get => AB_Security_GetTickerSymbol(this._security);
        set => AB_Security_SetTickerSymbol(this._security, value);
    }
    public Value Units
    {
        get => new Value(AB_Security_GetUnits(this._security));
        set => AB_Security_SetUnits(this._security, (IntPtr)value);
    }
    public Value UnitPriceValue
    {
        get => new Value(AB_Security_GetUnitPriceValue(this._security));
        set => AB_Security_SetUnitPriceValue(this._security, (IntPtr)value);
    }
    public GwenTime UnitPriceDate
    {
        get => (GwenTime)AB_Security_GetUnitPriceDate(this._security);
        set => AB_Security_SetUnitPriceDate(this._security, (IntPtr)value);
    }
    
    public static explicit operator IntPtr(Security security) => security._security;
}
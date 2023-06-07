using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class BankInfo
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_BankInfo_new();

    [DllImport("libaqbanking.so")]
    private static extern void AB_BankInfo_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetCountry(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBranchId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBankId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBic(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBankName(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetLocation(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetStreet(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetZipcode(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetCity(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetRegion(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetPhone(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetFax(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetEmail(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetWebsite(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_BankInfo_GetServices(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetCountry(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBranchId(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBankId(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBic(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBankName(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetLocation(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetStreet(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetZipcode(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetCity(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetRegion(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetPhone(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetFax(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetEmail(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetWebsite(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_BankInfo_SetServices(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_BankInfo_ReadDb(IntPtr p_struct, IntPtr p_db);
    
    [DllImport("libaqbanking.so")]
    private static extern int AB_BankInfo_WriteDb(IntPtr p_struct, IntPtr p_db);
    
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_BankInfo_fromDb(IntPtr p_db);
    
    [DllImport("libaqbanking.so")]
    private static extern int AB_BankInfo_toDb(IntPtr p_struct, IntPtr p_db);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _bankInfo;

    private BankInfo(IntPtr bankInfo)
    {
        this._bankInfo = bankInfo;
    }
    
    public BankInfo()
    {
        this._bankInfo = AB_BankInfo_new();
    }

    ~BankInfo()
    {
        AB_BankInfo_free(this._bankInfo);
    }

    public string? Country
    {
        get => AB_BankInfo_GetCountry(this._bankInfo);
        set => AB_BankInfo_SetCountry(this._bankInfo, value);
    }

    public string? BranchId
    {
        get => AB_BankInfo_GetBranchId(this._bankInfo);
        set => AB_BankInfo_SetBranchId(this._bankInfo, value);
    }

    public string? BankId
    {
        get => AB_BankInfo_GetBankId(this._bankInfo);
        set => AB_BankInfo_SetBankId(this._bankInfo, value);
    }

    public string? Bic
    {
        get => AB_BankInfo_GetBic(this._bankInfo);
        set => AB_BankInfo_SetBic(this._bankInfo, value);
    }

    public string? BankName
    {
        get => AB_BankInfo_GetBankName(this._bankInfo);
        set => AB_BankInfo_SetBankName(this._bankInfo, value);
    }

    public string? Location
    {
        get => AB_BankInfo_GetLocation(this._bankInfo);
        set => AB_BankInfo_SetLocation(this._bankInfo, value);
    }

    public string? Street
    {
        get => AB_BankInfo_GetStreet(this._bankInfo);
        set => AB_BankInfo_SetStreet(this._bankInfo, value);
    }

    public string? Zipcode
    {
        get => AB_BankInfo_GetZipcode(this._bankInfo);
        set => AB_BankInfo_SetZipcode(this._bankInfo, value);
    }

    public string? City
    {
        get => AB_BankInfo_GetCity(this._bankInfo);
        set => AB_BankInfo_SetCity(this._bankInfo, value);
    }

    public string? Region
    {
        get => AB_BankInfo_GetRegion(this._bankInfo);
        set => AB_BankInfo_SetRegion(this._bankInfo, value);
    }

    public string? Phone
    {
        get => AB_BankInfo_GetPhone(this._bankInfo);
        set => AB_BankInfo_SetPhone(this._bankInfo, value);
    }

    public string? Fax
    {
        get => AB_BankInfo_GetFax(this._bankInfo);
        set => AB_BankInfo_SetFax(this._bankInfo, value);
    }

    public string? Email
    {
        get => AB_BankInfo_GetEmail(this._bankInfo);
        set => AB_BankInfo_SetEmail(this._bankInfo, value);
    }

    public string? Website
    {
        get => AB_BankInfo_GetWebsite(this._bankInfo);
        set => AB_BankInfo_SetWebsite(this._bankInfo, value);
    }

    public BankInfoServiceList Services
    {
        get => new BankInfoServiceList(AB_BankInfo_GetServices(this._bankInfo));
        set => AB_BankInfo_SetServices(this._bankInfo, (IntPtr)value);
    }
    
    public void ReadDb(GwenDbNode db)
    {
        AB_BankInfo_ReadDb(_bankInfo, (IntPtr)db);
    }

    public void WriteDb(GwenDbNode db)
    {
        int returnValue = AB_BankInfo_WriteDb(_bankInfo, (IntPtr)db);
        ErrorHandling.CheckForErrors(returnValue);
    }

    public void ToDb(GwenDbNode db)
    {
        int returnValue = AB_BankInfo_toDb(_bankInfo, (IntPtr)db);
        ErrorHandling.CheckForErrors(returnValue);
    }
    
    public static BankInfo FromDb(GwenDbNode db)
    {
        return new BankInfo(AB_BankInfo_fromDb((IntPtr)db));
    }
    
    public static explicit operator IntPtr(BankInfo bankInfo) => bankInfo._bankInfo;
}
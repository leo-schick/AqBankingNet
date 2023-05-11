using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class BankInfo
{
    #region DLL Imports

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_new")]
    private static extern IntPtr AB_BankInfo_new();

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_free")]
    private static extern void AB_BankInfo_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetCountry")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetCountry(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetBranchId")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBranchId(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetBankId")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBankId(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetBic")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBic(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetBankName")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetBankName(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetLocation")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetLocation(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetStreet")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetStreet(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetZipcode")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetZipcode(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetCity")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetCity(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetRegion")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetRegion(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetPhone")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetPhone(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetFax")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetFax(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetEmail")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetEmail(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetWebsite")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfo_GetWebsite(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_GetServices")]
    private static extern IntPtr AB_BankInfo_GetServices(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetCountry", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetCountry(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetBranchId", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBranchId(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetBankId", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBankId(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetBic", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBic(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetBankName", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetBankName(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetLocation", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetLocation(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetStreet", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetStreet(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetZipcode", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetZipcode(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetCity", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetCity(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetRegion", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetRegion(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetPhone", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetPhone(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetFax", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetFax(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetEmail", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetEmail(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetWebsite", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfo_SetWebsite(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfo_SetServices")]
    private static extern void AB_BankInfo_SetServices(IntPtr p_struct, IntPtr p_src);

    #endregion

    internal readonly IntPtr _bankinfo;

    public BankInfo()
    {
        this._bankinfo = AB_BankInfo_new();
    }

    ~BankInfo()
    {
        AB_BankInfo_free(this._bankinfo);
    }

    public string? Country
    {
        get => AB_BankInfo_GetCountry(this._bankinfo);
        set => AB_BankInfo_SetCountry(this._bankinfo, value);
    }

    public string? BranchId
    {
        get => AB_BankInfo_GetBranchId(this._bankinfo);
        set => AB_BankInfo_SetBranchId(this._bankinfo, value);
    }

    public string? BankId
    {
        get => AB_BankInfo_GetBankId(this._bankinfo);
        set => AB_BankInfo_SetBankId(this._bankinfo, value);
    }

    public string? Bic
    {
        get => AB_BankInfo_GetBic(this._bankinfo);
        set => AB_BankInfo_SetBic(this._bankinfo, value);
    }

    public string? BankName
    {
        get => AB_BankInfo_GetBankName(this._bankinfo);
        set => AB_BankInfo_SetBankName(this._bankinfo, value);
    }

    public string? Location
    {
        get => AB_BankInfo_GetLocation(this._bankinfo);
        set => AB_BankInfo_SetLocation(this._bankinfo, value);
    }

    public string? Street
    {
        get => AB_BankInfo_GetStreet(this._bankinfo);
        set => AB_BankInfo_SetStreet(this._bankinfo, value);
    }

    public string? Zipcode
    {
        get => AB_BankInfo_GetZipcode(this._bankinfo);
        set => AB_BankInfo_SetZipcode(this._bankinfo, value);
    }

    public string? City
    {
        get => AB_BankInfo_GetCity(this._bankinfo);
        set => AB_BankInfo_SetCity(this._bankinfo, value);
    }

    public string? Region
    {
        get => AB_BankInfo_GetRegion(this._bankinfo);
        set => AB_BankInfo_SetRegion(this._bankinfo, value);
    }

    public string? Phone
    {
        get => AB_BankInfo_GetPhone(this._bankinfo);
        set => AB_BankInfo_SetPhone(this._bankinfo, value);
    }

    public string? Fax
    {
        get => AB_BankInfo_GetFax(this._bankinfo);
        set => AB_BankInfo_SetFax(this._bankinfo, value);
    }

    public string? Email
    {
        get => AB_BankInfo_GetEmail(this._bankinfo);
        set => AB_BankInfo_SetEmail(this._bankinfo, value);
    }

    public string? Website
    {
        get => AB_BankInfo_GetWebsite(this._bankinfo);
        set => AB_BankInfo_SetWebsite(this._bankinfo, value);
    }

    public BankInfoServiceList Services
    {
        get => new BankInfoServiceList(AB_BankInfo_GetServices(this._bankinfo));
        set => AB_BankInfo_SetServices(this._bankinfo, value._bankInfoServiceList);
    }
}
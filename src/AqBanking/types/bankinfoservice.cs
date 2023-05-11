using System.Collections;
using System.Runtime.InteropServices;

namespace AqBanking;

public class BankInfoService
{
    #region DLL Imports

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_new")]
    private static extern IntPtr AB_BankInfoService_new();

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_free")]
    private static extern void AB_BankInfoService_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetType", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetType(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetAddress", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetAddress(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetSuffix", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetSuffix(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetPversion", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetPversion(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetHversion", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetHversion(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetMode", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetMode(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetAux1", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetAux1(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetAux2", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetAux2(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetAux3", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetAux3(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetAux4", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_BankInfoService_GetAux4(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_GetUserFlags")]
    private static extern UInt32 AB_BankInfoService_GetUserFlags(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetType", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetType(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetAddress", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetAddress(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetSuffix", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetSuffix(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetPversion", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetPversion(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetHversion", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetHversion(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetMode", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetMode(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetAux1", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetAux1(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetAux2", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetAux2(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetAux3", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetAux3(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetAux4", CharSet = CharSet.Ansi)]
    private static extern void AB_BankInfoService_SetAux4(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SetUserFlags")]
    private static extern void AB_BankInfoService_SetUserFlags(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_AddUserFlags")]
    private static extern void AB_BankInfoService_AddUserFlags(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_BankInfoService_SubUserFlags")]
    private static extern void AB_BankInfoService_SubUserFlags(IntPtr p_struct, UInt32 p_src);

    #endregion

    internal readonly IntPtr _bankInfoService;

    public BankInfoService()
    {
        this._bankInfoService = AB_BankInfoService_new();
    }
    
    ~BankInfoService()
    {
        AB_BankInfoService_free(this._bankInfoService);
    }

    public string? Type
    {
        get => AB_BankInfoService_GetType(this._bankInfoService);
        set => AB_BankInfoService_SetType(this._bankInfoService, value);
    }

    public string? Address
    {
        get => AB_BankInfoService_GetAddress(this._bankInfoService);
        set => AB_BankInfoService_SetAddress(this._bankInfoService, value);
    }

    public string? Suffix
    {
        get => AB_BankInfoService_GetSuffix(this._bankInfoService);
        set => AB_BankInfoService_SetSuffix(this._bankInfoService, value);
    }

    public string? Pversion
    {
        get => AB_BankInfoService_GetPversion(this._bankInfoService);
        set => AB_BankInfoService_SetPversion(this._bankInfoService, value);
    }

    public string? Hversion
    {
        get => AB_BankInfoService_GetHversion(this._bankInfoService);
        set => AB_BankInfoService_SetHversion(this._bankInfoService, value);
    }

    public string? Mode
    {
        get => AB_BankInfoService_GetMode(this._bankInfoService);
        set => AB_BankInfoService_SetMode(this._bankInfoService, value);
    }

    public string? Aux1
    {
        get => AB_BankInfoService_GetAux1(this._bankInfoService);
        set => AB_BankInfoService_SetAux1(this._bankInfoService, value);
    }

    public string? Aux2
    {
        get => AB_BankInfoService_GetAux2(this._bankInfoService);
        set => AB_BankInfoService_SetAux2(this._bankInfoService, value);
    }

    public string? Aux3
    {
        get => AB_BankInfoService_GetAux3(this._bankInfoService);
        set => AB_BankInfoService_SetAux3(this._bankInfoService, value);
    }

    public string? Aux4
    {
        get => AB_BankInfoService_GetAux4(this._bankInfoService);
        set => AB_BankInfoService_SetAux4(this._bankInfoService, value);
    }

    public UInt32 UserFlags
    {
        get => AB_BankInfoService_GetUserFlags(this._bankInfoService);
        set => AB_BankInfoService_SetUserFlags(this._bankInfoService, value);
    }

    public void AddUserFlags(UInt32 flags)
    {
        AB_BankInfoService_AddUserFlags(this._bankInfoService, flags);
    }

    public void SubUserFlags(UInt32 flags)
    {
        AB_BankInfoService_SubUserFlags(this._bankInfoService, flags);
    }
}

public class BankInfoServiceList : IEnumerable<BankInfoService>
{
    internal readonly IntPtr _bankInfoServiceList;

    internal BankInfoServiceList(IntPtr bankInfoServiceList)
    {
        _bankInfoServiceList = bankInfoServiceList;
    }
    
    public IEnumerator<BankInfoService> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

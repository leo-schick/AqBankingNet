using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public partial class Banking
{
    #region DLL Imports

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_new", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Banking_new([In, MarshalAs(UnmanagedType.LPStr)] string appName, [In, MarshalAs(UnmanagedType.LPStr)] string? dname, UInt32 extensions = 0);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_free", CharSet = CharSet.Ansi)]
    private static extern void AB_Banking_free(IntPtr ab);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_GetVersion", CharSet = CharSet.Ansi)]
    private static extern void AB_Banking_GetVersion(ref int major, ref int minor, ref int patchlevel, ref int build);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_Init", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_Init(IntPtr ab);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_Fini", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_Fini(IntPtr ab);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_GetAppName", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Banking_GetAppName(IntPtr ab);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_GetEscapedAppName", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Banking_GetEscapedAppName(IntPtr ab);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_GetUserDataDir", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_GetUserDataDir(IntPtr ab, [Out] IntPtr buf);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Banking_GetSharedDataDir", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_GetSharedDataDir(IntPtr ab, [In, MarshalAs(UnmanagedType.LPStr)] string? name, [Out] IntPtr buf);

    #endregion

    private readonly IntPtr _banking;

    /// <summary>
    /// 
    /// </summary>
    public Banking(string appName, string? dname = null)
    {
        this._banking = AB_Banking_new(appName, dname);
    }

    ~Banking()
    {
        AB_Banking_free(this._banking);
    }

    public static Version GetVersion()
    {
        int major = 0;
        int minor = 0;
        int patchlevel = 0;
        int build = 0;
        AB_Banking_GetVersion(ref major, ref minor, ref patchlevel, ref build);
        var version = new Version(major, minor, patchlevel, build);
        return version;
    }

    /// <summary>
    /// Initializes AqBanking.
    /// This sets up the plugins, plugin managers and path managers.
    /// </summary>
    public void Init()
    {
        int returnValue = AB_Banking_Init(this._banking);
        ErrorHandling.CheckForErrors(returnValue);
    }

    /// <summary>
    /// Deinitializes AqBanking thus allowing it to save its data and to unload
    /// backends.
    /// </summary>
    public void Fini()
    {
        int returnValue = AB_Banking_Fini(this._banking);
        ErrorHandling.CheckForErrors(returnValue);
    }

    public string? AppName
    {
        get => AB_Banking_GetAppName(this._banking);
    }

    public string? EscapedAppName
    {
        get => AB_Banking_GetEscapedAppName(this._banking);
    }

    public string? UserDataDir
    {
        get
        {
            var buf = new GWEN_Buffer(0, 256, 0, true);
            int returnValue = AB_Banking_GetUserDataDir(this._banking, buf);
            ErrorHandling.CheckForErrors(returnValue);
            return buf.GetStart();
        }
    }

    public string? GetSharedDataDir(string name)
    {
        var buf = new GWEN_Buffer(0, 256, 0, true);
        int returnValue = AB_Banking_GetSharedDataDir(this._banking, name, buf);
        ErrorHandling.CheckForErrors(returnValue);
        return buf.GetStart();
    }

    public static explicit operator IntPtr(Banking banking) => banking._banking;
}

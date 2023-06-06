using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public partial class Banking : IDisposable
{
    #region DLL Imports

    // Constructor, Destructor, Init, Fini

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Banking_new([In, MarshalAs(UnmanagedType.LPStr)] string appName, [In, MarshalAs(UnmanagedType.LPStr)] string? dname, UInt32 extensions = 0);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Banking_free(IntPtr ab);

    [DllImport("libaqbanking.so")]
    private static extern int AB_Banking_Init(IntPtr ab);

    [DllImport("libaqbanking.so")]
    private static extern int AB_Banking_Fini(IntPtr ab);
    
    // Application Information, Shared Data

    [DllImport("libaqbanking.so")]
    private static extern void AB_Banking_GetVersion(ref int major, ref int minor, ref int patchlevel, ref int build);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Banking_GetAppName(IntPtr ab);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Banking_GetEscapedAppName(IntPtr ab);

    [DllImport("libaqbanking.so")]
    private static extern int AB_Banking_GetUserDataDir(IntPtr ab, [Out] IntPtr buf);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_GetSharedDataDir(IntPtr ab, [In, MarshalAs(UnmanagedType.LPStr)] string? name, [Out] IntPtr buf);
    
    // Missing functions:
    //  - AB_Banking_LoadSharedConfig
    //  - AB_Banking_SaveSharedConfig
    //  - AB_Banking_LockSharedConfig
    //  - AB_Banking_UnlockSharedConfig
    
    // Runtime Configuration
    
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Banking_RuntimeConfig_SetCharValue(IntPtr ab, [MarshalAs(UnmanagedType.LPStr)] string? varName, [MarshalAs(UnmanagedType.LPStr)] string? value);
    
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Banking_RuntimeConfig_GetCharValue(IntPtr ab, [MarshalAs(UnmanagedType.LPStr)] string? varName, [MarshalAs(UnmanagedType.LPStr)] string? defaultValue);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Banking_RuntimeConfig_SetIntValue(IntPtr ab, [MarshalAs(UnmanagedType.LPStr)] string? varName, int value);
    
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_RuntimeConfig_GetIntValue(IntPtr ab, [MarshalAs(UnmanagedType.LPStr)] string? varName, int defaultValue);

    #endregion

    private readonly IntPtr _banking;

    #region Constructor, Destructor, Init, Fini

    /// <summary>
    /// <p>
    /// Creates an instance of AqBanking. Though AqBanking is quite object
    /// oriented (and thus allows multiple instances of <see cref="Banking"/> to co-exist)
    /// you should avoid having multiple <see cref="Banking"/> objects in parallel.
    /// </p>
    /// <p>
    /// This is just because the backends are loaded dynamically and might not like
    /// to be used with multiple instances of <see cref="Banking"/> in parallel.
    /// </p>
    /// <p>
    /// This function does not actually load the configuration file or setup
    /// AqBanking, that is performed by <see cref="Init"/>.
    /// </p>
    /// </summary>
    /// <param name="appName">
    /// name of the application which wants to use AqBanking.
    /// This allows AqBanking to separate settings and data for multiple
    /// applications.
    /// </param>
    /// <param name="dname">
    /// Path for the directory containing the user data of
    /// AqBanking. You should in most cases present a <c>null</c> for this
    /// parameter, which means AqBanking will choose the default user
    /// data folder which is "$HOME/.aqbanking".
    /// The configuration itself is handled using GWEN's GWEN_ConfigMgr
    /// module (see @ref GWEN_ConfigMgr_Factory). That module stores the
    /// configuration in AqBanking's subfolder "settings" (i.e. the
    /// full path to the user/account configuration is "$HOME/.aqbanking/settings").
    /// </param>
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
        int patchLevel = 0;
        int build = 0;
        AB_Banking_GetVersion(ref major, ref minor, ref patchLevel, ref build);
        var version = new Version(major, minor, patchLevel, build);
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
    
    #endregion

    #region IDisposable

    /// <summary>
    /// Makes sure that <see cref="Fini"/> is called.
    /// </summary>
    public void Dispose()
    {
        Fini();
    }

    #endregion
    
    #region Application Information, Shared Data 
    
    /// <summary>
    /// Returns the application name as given to <see cref="Banking(string, string)"/>
    /// </summary>
    public string? AppName => AB_Banking_GetAppName(this._banking);

    /// <summary>
    /// Returns the escaped version of the application name. This name can
    /// safely be used to create file paths since all special characters (like
    /// * '/', '.' etc) are escaped.
    /// </summary>
    public string? EscapedAppName => AB_Banking_GetEscapedAppName(this._banking);

    /// <summary>
    /// Returns the name of the user folder for AqBanking's data.
    /// Normally this is something like "/home/me/.aqbanking".
    /// </summary>
    public string? UserDataDir
    {
        get
        {
            var buf = new GwenBuffer(0, 256, 0, true);
            int returnValue = AB_Banking_GetUserDataDir(this._banking, buf);
            ErrorHandling.CheckForErrors(returnValue);
            return buf.GetStart();
        }
    }

    /// <summary>
    /// Returns the path to a folder to which shared data can be stored.
    /// This might be used by multiple applications if they wish to share some
    /// of their data, e.g. QBankManager and AqMoney3 share their transaction
    /// storage so that both may work with it.
    /// Please note that this folder does not necessarily exist, but you are free
    /// to create it.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string? GetSharedDataDir(string name)
    {
        var buf = new GwenBuffer(0, 256, 0, true);
        int returnValue = AB_Banking_GetSharedDataDir(this._banking, name, buf);
        ErrorHandling.CheckForErrors(returnValue);
        return buf.GetStart();
    }
    
    #endregion

    #region Runtime Configuration

    /// <summary>
    /// Set runtime char variable. Overwrites the currently set value if any.
    /// </summary>
    /// <param name="varName">varName name of the variable to set</param>
    /// <param name="value">value new value to set</param>
    public void SetRuntimeConfig(string varName, string? value)
    {
        AB_Banking_RuntimeConfig_SetCharValue(this._banking, varName, value);
    }

    /// <summary>
    /// Set runtime int variable. Overwrites the currently set value if any.
    /// </summary>
    /// <param name="varName">varName name of the variable to set</param>
    /// <param name="value">value new value to set</param>
    public void SetRuntimeConfig(string varName, int value)
    {
        AB_Banking_RuntimeConfig_SetIntValue(this._banking, varName, value);
    }
    
    /// <summary>
    /// Get runtime char value (or default value if not set).
    /// </summary>
    /// <param name="varName">varName name of the variable to set</param>
    /// <param name="defaultValue">defaultValue default value to return if there is no value set</param>
    /// <returns></returns>
    public string? GetRuntimeConfigAsString(string varName, string? defaultValue = null)
    {
        return AB_Banking_RuntimeConfig_GetCharValue(this._banking, varName, defaultValue);
    }

    /// <summary>
    /// Get runtime int value (or default value if not set).
    /// </summary>
    /// <param name="varName">varName name of the variable to set</param>
    /// <param name="defaultValue">defaultValue default value to return if there is no value set</param>
    /// <returns></returns>
    public int GetRuntimeConfigAsInt(string varName, int defaultValue = default)
    {
        return AB_Banking_RuntimeConfig_GetIntValue(this._banking, varName, defaultValue);
    }

    #endregion
    
    public static explicit operator IntPtr(Banking banking) => banking._banking;
}

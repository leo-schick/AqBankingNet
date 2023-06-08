using System.Runtime.InteropServices;

namespace Gwenhywfar;

public static class GWEN
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libgwenhywfar")]
    private static extern int GWEN_Init();

    [DllImport("libgwenhywfar")]
    private static extern void GWEN_Fini();

    [DllImport("libgwenhywfar")]
    private static extern void GWEN_Fini_Forced();

    [DllImport("libgwenhywfar")]
    private static extern void GWEN_Version(ref int major, ref int minor, ref int patchlevel, ref int build);
    // ReSharper restore InconsistentNaming

    #endregion

    public static void Init()
    {
        int returnValue = GWEN_Init();
        if (returnValue != 0)
            throw new Exception("Could not initialize GWEN module");
    }

    public static void Fini()
    {
        GWEN_Fini();
    }

    public static void Fini_Forced()
    {
        GWEN_Fini_Forced();
    }

    public static Version GetVersion()
    {
        int major = 0;
        int minor = 0;
        int patchLevel = 0;
        int build = 0;
        GWEN_Version(ref major, ref minor, ref patchLevel, ref build);
        var version = new Version(major, minor, patchLevel, build);
        return version;
    }
}

using System.Runtime.InteropServices;

namespace Gwenhywfar;

public static class GWEN
{
    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Init", CharSet = CharSet.Ansi)]
    private static extern int GWEN_Init();

    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Fini", CharSet = CharSet.Ansi)]
    private static extern void GWEN_Fini();

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
}

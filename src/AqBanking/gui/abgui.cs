using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public static class AqBankingGUI
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Gui_new(IntPtr banking);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Gui_Extend(IntPtr gui, IntPtr banking);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Gui_Unextend(IntPtr gui);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern int AB_Gui_SetCliCallbackForOpticalTan(IntPtr gui, [MarshalAs(UnmanagedType.LPStr)] string? tool);
    // ReSharper restore InconsistentNaming

    #endregion

    public static GwenGUI New(Banking banking)
    {
        return (GwenGUI)AB_Gui_new((IntPtr)banking);
    }

    public static void Extend(GwenGUI gui, Banking banking)
    {
        AB_Gui_Extend((IntPtr)gui, (IntPtr)banking);
    }

    public static void Unextend(GwenGUI gui)
    {
        AB_Gui_Unextend((IntPtr)gui);
    }

    public static void SetCliCallbackForOpticalTan(this GwenGUI gui, string? tool)
    {
        int returnValue = AB_Gui_SetCliCallbackForOpticalTan((IntPtr)gui, tool);
        ErrorHandling.CheckForErrors(returnValue);
    }
}

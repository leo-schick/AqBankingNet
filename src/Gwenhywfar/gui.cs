using System.Runtime.InteropServices;

namespace Gwenhywfar;

public class GwenGUI
{
    #region DLL Imports

    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Gui_new", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_Gui_new();

    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Gui_free", CharSet = CharSet.Ansi)]
    private static extern void GWEN_Gui_free(IntPtr n);

    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Gui_SetGui", CharSet = CharSet.Ansi)]
    private static extern void GWEN_Gui_SetGui(IntPtr gui);

    #endregion

    private IntPtr _gui;

    public GwenGUI()
    {
        this._gui = GWEN_Gui_new();
    }

    private GwenGUI(IntPtr gui)
    {
        this._gui = gui;
    }

    ~GwenGUI()
    {
        GWEN_Gui_free(this._gui);
    }

    /// <summary>
    /// Set the global Gui instance.
    /// </summary>
    public static void SetGui(GwenGUI gui)
    {
        GWEN_Gui_SetGui(gui._gui);
    }

    public static explicit operator GwenGUI(IntPtr p) => new GwenGUI(p);
    public static explicit operator IntPtr(GwenGUI gui) => gui._gui;
}

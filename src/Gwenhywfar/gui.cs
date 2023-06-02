using System.Runtime.InteropServices;

namespace Gwenhywfar;

/// <summary>
/// Password/pin/tan entry method.
/// The method id uses the higher 16 bits of a 32 bit word to define the method.
/// The lower 16 bits are used to define the version of the method. E.g. the method for
/// input using optical data (i.e. flicker code) knows multiple versions, currently 1.3 and 1.4.
/// So the complete method id for optical input method HHD version 1.4 would be 0x20104.
/// Use @ref GWEN_Gui_PasswordMethod_Mask to get the basic method id.
/// </summary>
[Flags]
public enum GwenGUIPasswordMethod : uint
{
    Unknown    = 0,
    Mask       = 0xffff0000,
    
    /// <summary>
    /// This method doesn't need any parameters except those which are already defined by the function
    /// prototype (i.e. min/max length etc.)
    /// </summary>
    Text       = 0x10000,
    
    /// <summary>
    /// Optical Input (i.e. flicker code)
    ///
    /// This method uses a so-called "challenge" which is chiefly a row of digits which are encoded in a
    /// flicker graphic which is then read by a specific card reader device which is held to the monitor to read
    /// that code.
    /// The possible contents of the methodParams are:
    /// <ul>
    ///   <li>char challenge: Hex code in ASCII form with a row of bytes to be converted to a flicker code (e.g. "123456789ABC" </li>
    ///   <li>char methodName: Name of the method (optional)</li>
    ///   <li>char methodDescription: Description of the method (optional)</li>
    /// </ul>
    /// </summary>
    OpticalHHD = 0x20000,
}

public class GwenGUI
{
    #region DLL Imports

    [DllImport("libgwenhywfar.so")]
    private static extern IntPtr GWEN_Gui_new();

    [DllImport("libgwenhywfar.so")]
    private static extern void GWEN_Gui_free(IntPtr n);

    [DllImport("libgwenhywfar.so")]
    private static extern void GWEN_Gui_SetGui(IntPtr gui);

    
    // Password Cache
    
    [DllImport("libgwenhywfar.so")]
    private static extern void GWEN_Gui_SetPasswordDb(IntPtr gui, IntPtr dbPasswords, int persistent);
    
    [DllImport("libgwenhywfar.so")]
    private static extern IntPtr GWEN_Gui_GetPasswordDb(IntPtr gui);

    #endregion

    private readonly IntPtr _gui;

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

    #region Password Cache

    /// <summary>
    /// Set the password DB. Takes over the given DB.
    /// </summary>
    /// <param name="dbPasswords">dbPasswords password cache</param>
    /// <param name="persistent">
    /// persistent if <c>true</c >then the passwords come from a password file
    /// and a request to clear the password cache will be ignored.
    /// </param>
    public void SetPasswordDb(GwenDbNode dbPasswords, bool persistent)
    {
        GWEN_Gui_SetPasswordDb(_gui, (IntPtr)dbPasswords, persistent ? 1 : 0);
    }

    /// <summary>
    /// Returns a pointer to the internally used password cache. The GUI
    /// object remains the owner of the object returned (if any).
    /// </summary>
    /// <returns></returns>
    public GwenDbNode GetPasswordDb()
    {
        return (GwenDbNode)GWEN_Gui_GetPasswordDb(_gui);
    }

    #endregion
}

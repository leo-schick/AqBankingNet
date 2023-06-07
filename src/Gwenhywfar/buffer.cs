using System.Runtime.InteropServices;

namespace Gwenhywfar;

public class GwenBuffer
{
    #region DLL Imports
    
    // ReSharper disable InconsistentNaming
    [DllImport("libgwenhywfar.so")]
    private static extern IntPtr GWEN_Buffer_new(int buffer, UInt32 size, UInt32 used, int take_ownership);

    [DllImport("libgwenhywfar.so")]
    private static extern void GWEN_Buffer_free([In] IntPtr bf);

    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? GWEN_Buffer_GetStart([In] IntPtr bf);
    // ReSharper restore InconsistentNaming
    
    #endregion

    private readonly IntPtr _buffer;

    public GwenBuffer(int buffer, UInt32 size, UInt32 used, bool takeOwnership)
    {
        this._buffer = GWEN_Buffer_new(buffer, size, used, takeOwnership ? 1 : 0);
    }

    ~GwenBuffer()
    {
        GWEN_Buffer_free(this._buffer);
    }

    public string? GetStart()
    {
        return GWEN_Buffer_GetStart(this._buffer);
    }

    public static implicit operator IntPtr(GwenBuffer buf) => buf._buffer;

    public override string? ToString()
    {
        return this.GetStart();
    }
}

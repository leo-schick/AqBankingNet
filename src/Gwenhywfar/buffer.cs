using System.Runtime.InteropServices;

namespace Gwenhywfar;

public class GWEN_Buffer
{
    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Buffer_new", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_Buffer_new(int buffer, UInt32 size, UInt32 used, int take_ownership);

    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Buffer_free", CharSet = CharSet.Ansi)]
    private static extern void GWEN_Buffer_free([In] IntPtr bf);

    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_Buffer_GetStart", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? GWEN_Buffer_GetStart([In] IntPtr bf);

    private IntPtr _buffer;

    public GWEN_Buffer(int buffer, UInt32 size, UInt32 used, bool takeOwnership)
    {
        this._buffer = GWEN_Buffer_new(buffer, size, used, takeOwnership ? 1 : 0);
    }

    ~GWEN_Buffer()
    {
        GWEN_Buffer_free(this._buffer);
    }

    public string? GetStart()
    {
        return GWEN_Buffer_GetStart(this._buffer);
    }

    public static implicit operator IntPtr(GWEN_Buffer buf) => buf._buffer;

    public override string? ToString()
    {
        return this.GetStart();
    }
}

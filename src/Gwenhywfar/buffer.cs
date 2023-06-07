using System.Runtime.InteropServices;

namespace Gwenhywfar;

/// <summary>
/// GwenBuffer is a dynamically resizeable text buffer.
/// </summary>
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
    
    [DllImport("libgwenhywfar.so")]
    private static extern uint GWEN_Buffer_GetPos([In] IntPtr bf);

    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_Buffer_SetPos([In] IntPtr bf, uint i);
    // ReSharper restore InconsistentNaming
    
    #endregion

    private readonly IntPtr _buffer;

    /// <summary>
    /// Creates a new GWEN_BUFFER, which is a dynamically resizeable
    /// text buffer.
    /// </summary>
    /// <param name="buffer">
    /// If non-NULL, then this buffer will be used as
    /// actual storage space. Otherwise a new buffer will be allocated
    /// (with <paramref name="size" /> bytes)
    /// </param>
    /// <param name="size">
    /// If <paramref name="buffer" /> was non-NULL, then this argument
    /// <i>must</i> specifiy the size of that buffer. If <paramref name="buffer" /> was
    /// NULL, then this argument specifies the number of bytes that
    /// will be allocated.
    /// </param>
    /// <param name="used">
    /// Number of bytes of the buffer actually used. This is
    /// interesting when reading from a buffer.
    /// </param>
    /// <param name="takeOwnership">
    /// If <paramref name="buffer" /> is <c>true</c>, then the new <see cref="GwenBuffer"/> object
    /// takes over the ownership of the given <paramref name="buffer" /> so that it will be freed.
    /// If this argument is <c>false</c>, the given <paramref name="buffer" /> will not be freed.
    /// If <paramref name="buffer" /> was <c>0</c>, this argument has no effect.
    /// </param>
    public GwenBuffer(int buffer, UInt32 size, UInt32 used, bool takeOwnership)
    {
        this._buffer = GWEN_Buffer_new(buffer, size, used, takeOwnership ? 1 : 0);
    }

    ~GwenBuffer()
    {
        GWEN_Buffer_free(this._buffer);
    }

    /// <summary>
    /// Returns the start of the buffer. You can use <see cref="Pos"/> to navigate within the buffer.
    /// </summary>
    /// <returns></returns>
    public string? GetStart()
    {
        return GWEN_Buffer_GetStart(this._buffer);
    }

    /// <summary>
    /// Returns or sets the current position within the buffer. This pointer is adjusted
    /// by the various read and write functions.
    /// </summary>
    /// <returns></returns>
    public uint Pos
    {
        get => GWEN_Buffer_GetPos(_buffer);
        set
        {
            int returnValue = GWEN_Buffer_SetPos(_buffer, value);
            if (returnValue != 0)
                throw new Exception($"Could not set the Position within the buffer. Error code: {returnValue}");
        }
    }

    public static implicit operator IntPtr(GwenBuffer buf) => buf._buffer;

    public override string? ToString()
    {
        return this.GetStart();
    }
}

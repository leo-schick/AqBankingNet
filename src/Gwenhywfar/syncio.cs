using System.Runtime.InteropServices;

namespace Gwenhywfar;

public enum SyncIOStatus
{
    Unknown = -1,
    Unconnected = 0,
    Disconnected,
    Connected,
    Disabled
}

[Flags]
public enum SyncIOFlags : uint
{
    Transparent = 0x80000000,
    DontClose   = 0x40000000,
    Passive     = 0x20000000,
    PacketEnd   = 0x10000000,
    DosMode     = 0x08000000
}

public class SyncIO : IDisposable
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libgwenhywfar", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_SyncIo_new([In, MarshalAs(UnmanagedType.LPStr)] string? typeName, IntPtr baseIo);

    [DllImport("libgwenhywfar")]
    private static extern void GWEN_SyncIo_free(IntPtr sio);

    [DllImport("libgwenhywfar")]
    private static extern int GWEN_SyncIo_Connect(IntPtr sio);

    [DllImport("libgwenhywfar")]
    private static extern int GWEN_SyncIo_Disconnect(IntPtr sio);

    [DllImport("libgwenhywfar")]
    private static extern int GWEN_SyncIo_Flush(IntPtr sio);

    [DllImport("libgwenhywfar")]
    private static extern int GWEN_SyncIo_Read(IntPtr sio, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint size);

    [DllImport("libgwenhywfar")]
    private static extern uint GWEN_SyncIo_GetFlags(IntPtr sio);
    [DllImport("libgwenhywfar")]
    private static extern void GWEN_SyncIo_SetFlags(IntPtr sio, uint fl);
    [DllImport("libgwenhywfar")]
    private static extern void GWEN_SyncIo_AddFlags(IntPtr sio, uint fl);
    [DllImport("libgwenhywfar")]
    private static extern void GWEN_SyncIo_SubFlags(IntPtr sio, uint fl);
    
    [DllImport("libgwenhywfar")]
    private static extern SyncIOStatus GWEN_SyncIo_GetStatus(IntPtr sio);
    [DllImport("libgwenhywfar")]
    private static extern void GWEN_SyncIo_SetStatus(IntPtr sio, SyncIOStatus st);

    [DllImport("libgwenhywfar", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? GWEN_SyncIo_GetTypeName(IntPtr sio);
    
    [DllImport("libgwenhywfar")]
    private static extern IntPtr GWEN_SyncIo_GetBaseIo(IntPtr sio);
    // ReSharper restore InconsistentNaming
    
    #endregion
    
    protected readonly IntPtr _syncIO;
    // basis TODO functions not implemented

    internal SyncIO(IntPtr syncIo)
    {
        this._syncIO = syncIo;
    }

    public SyncIO(string typeName, SyncIO baseIo)
    {
        this._syncIO = GWEN_SyncIo_new(typeName, baseIo._syncIO);
    }

    ~SyncIO()
    {
        ReleaseUnmanagedResources();

        GWEN_SyncIo_free(this._syncIO);
    }

    public void Connect()
    {
        int returnValue = GWEN_SyncIo_Connect(this._syncIO);
        if (returnValue != 0)
            throw new IOException($"Failed to connect. Code: {returnValue}");
    }

    public void Disconnect()
    {
        int returnValue = GWEN_SyncIo_Disconnect(this._syncIO);
        if (returnValue != 0)
            throw new IOException($"Failed to disconnect. Code: {returnValue}");
    }

    public void Flush()
    {
        int returnValue = GWEN_SyncIo_Flush(this._syncIO);
        if (returnValue != 0)
            throw new IOException($"Failed to flush. Code: {returnValue}");
    }

    public void Read(byte[] buffer, uint size)
    {
        int returnValue = GWEN_SyncIo_Read(this._syncIO, buffer, size);
        if (returnValue != 0)
            throw new IOException($"Failed read stream IO. Code: {returnValue}");
    }

    public uint Flags
    {
        get => GWEN_SyncIo_GetFlags(this._syncIO);
        set => GWEN_SyncIo_SetFlags(this._syncIO, value);
    }

    public void AddFlags(uint flags)
    {
        GWEN_SyncIo_AddFlags(this._syncIO, flags);
    }

    public void SubFlags(uint flags)
    {
        GWEN_SyncIo_SubFlags(this._syncIO, flags);
    }

    public SyncIOStatus Status
    {
        get => GWEN_SyncIo_GetStatus(this._syncIO);
        set => GWEN_SyncIo_SetStatus(this._syncIO, value);
    }

    public string? TypeName => GWEN_SyncIo_GetTypeName(this._syncIO);

    public SyncIO BaseIO => new SyncIO(GWEN_SyncIo_GetBaseIo(this._syncIO));

    private void ReleaseUnmanagedResources()
    {
        if (Status == SyncIOStatus.Connected)
        {
            this.Disconnect();            
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
    
    public static explicit operator IntPtr(SyncIO sio) => sio._syncIO;
    public static explicit operator SyncIO(IntPtr p) => new SyncIO(p);
}

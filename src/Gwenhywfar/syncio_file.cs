using System.Runtime.InteropServices;

namespace Gwenhywfar;

public enum SyncIOFileCreationMode
{
    Unknown = -1,
    OpenExisting = 0,
    CreateNew,
    CreateAlways,
    OpenAlways,
    TruncateExisting
}

public enum SyncIOFileWhence
{
    Set = 0,
    Current,
    End
}

[Flags]
public enum SyncIOFileFlags : uint
{
    Read   = 0x00000001,
    Write  = 0x00000002,
    Append = 0x00000008,
    Random = 0x00000010,

    UserRead  = 0x00000100,
    UserWrite = 0x00000200,
    UserExec  = 0x00000400,

    GroupRead  = 0x00001000,
    GroupWrite = 0x00002000,
    GroupExec  = 0x00004000,

    OtherRead  = 0x00010000,
    OtherWrite = 0x00020000,
    OtherExec  = 0x00040000
}

public class SyncIOFile : SyncIO
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_SyncIo_File_new([MarshalAs(UnmanagedType.LPStr)] string path, SyncIOFileCreationMode creationMode);

    [DllImport("libgwenhywfar.so")]
    private static extern IntPtr GWEN_SyncIo_File_fromStdin();

    [DllImport("libgwenhywfar.so")]
    private static extern IntPtr GWEN_SyncIo_File_fromStdout();

    [DllImport("libgwenhywfar.so")]
    private static extern IntPtr GWEN_SyncIo_File_fromStderr();

    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? GWEN_SyncIo_File_GetPath(IntPtr sio);

    [DllImport("libgwenhywfar.so")]
    private static extern long GWEN_SyncIo_File_Seek(IntPtr sio, long pos, SyncIOFileWhence whence);
    // ReSharper restore InconsistentNaming

    #endregion

    public SyncIOFile(string path, SyncIOFileCreationMode creationMode)
        : base(GWEN_SyncIo_File_new(path, creationMode))
    {
    }

    private SyncIOFile(IntPtr syncIOFile)
        : base(syncIOFile)
    {
    }

    public static SyncIOFile FromStdin()
    {
        return new SyncIOFile(GWEN_SyncIo_File_fromStdin());
    }

    public static SyncIOFile FromStdout()
    {
        return new SyncIOFile(GWEN_SyncIo_File_fromStdout());
    }

    public static SyncIOFile FromStderr()
    {
        return new SyncIOFile(GWEN_SyncIo_File_fromStderr());
    }

    public string? Path => GWEN_SyncIo_File_GetPath(this._syncIO);

    public long Seek(long pos, SyncIOFileWhence whence)
    {
        return GWEN_SyncIo_File_Seek(this._syncIO, pos, whence);
    }
}

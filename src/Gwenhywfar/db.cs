using System.Runtime.InteropServices;

namespace Gwenhywfar;

/// <summary>
/// DB Flags
///
/// Please note that the setter functions also take the flags from
/// the module @ref MOD_PATH (e.g. @ref GWEN_PATH_FLAGS_PATHMUSTEXIST)
/// into account. So you most likely need to specify
/// them, too.
///
/// However, for your conveniance there is a default flag value which suffices
/// in most cases (@ref GWEN_DB_FLAGS_DEFAULT).
/// </summary>
[Flags]
public enum GwenDbFlags : uint
{
    /// <summary>
    /// when reading a DB allow for empty streams (e.g. empty file)
    /// </summary>
    ALLOW_EMPTY_STREAM     = 0x00008000,
    /// <summary>
    /// Overwrite existing values when assigning a new value to a variable
    /// </summary>
    OVERWRITE_VARS         = 0x00010000,
    /// <summary>
    /// Overwrite existing groups when calling @ref GWEN_DB_GetGroup()
    /// </summary>
    OVERWRITE_GROUPS       = 0x00020000,
    /// <summary>
    /// quote variable names when writing them to a stream
    /// </summary>
    QUOTE_VARNAMES         = 0x00040000,
    /// <summary>
    /// quote values when writing them to a stream
    /// </summary>
    QUOTE_VALUES           = 0x00080000,
    /// <summary>
    /// allows writing of subgroups when writing to a stream
    /// </summary>
    WRITE_SUBGROUPS        = 0x00100000,
    /// <summary>
    /// adds some comments when writing to a stream
    /// </summary>
    DETAILED_GROUPS        = 0x00200000,
    /// <summary>
    /// indents text according to the current path depth when writing to a
    /// stream to improve the readability of the created file
    /// </summary>
    INDEND                 = 0x00400000,
    /// <summary>
    /// writes a newline to the stream after writing a group to improve
    /// the readability of the created file
    /// </summary>
    ADD_GROUP_NEWLINES     = 0x00800000,
    /// <summary>
    /// uses a colon (":") instead of an equation mark when reading/writing
    /// variable definitions
    /// </summary>
    USE_COLON              = 0x01000000,
    /// <summary>
    /// stops reading from a stream at empty lines
    /// </summary>
    UNTIL_EMPTY_LINE       = 0x02000000,
    /// <summary>
    /// normally the type of a variable is written to the stream, too.
    /// This flag changes this behaviour
    /// </summary>
    OMIT_TYPES             = 0x04000000,
    /// <summary>
    /// appends data to an existing file instead of overwriting it
    /// </summary>
    APPEND_FILE            = 0x08000000,
    /// <summary>
    /// Char values are escaped when writing them to a file.
    /// </summary>
    ESCAPE_CHARVALUES      = 0x10000000,
    /// <summary>
    /// Char values are unescaped when reading them from a file (uses the same
    /// bit @ref GWEN_DB_FLAGS_ESCAPE_CHARVALUES uses)
    /// </summary>
    UNESCAPE_CHARVALUES    = 0x10000000,
    /// <summary>
    /// locks a file before reading from or writing to it
    /// This is used by @ref GWEN_DB_ReadFile and @ref GWEN_DB_WriteFile
    /// </summary>
    LOCKFILE               = 0x20000000,

    /// <summary>
    /// When setting a value or getting a group insert newly created
    /// values/groups rather than appending them
    /// </summary>
    INSERT                 = 0x40000000,

    /// <summary>
    /// When writing a DB use DOS line termination (e.g. CR/LF) instead if unix mode (LF only)
    /// </summary>
    DosMode                = 0x80000000,
    
    /// <summary>
    /// These are the default flags which you use in most cases
    /// </summary>
    Default  = QUOTE_VALUES | WRITE_SUBGROUPS | DETAILED_GROUPS | INDEND | ADD_GROUP_NEWLINES | ESCAPE_CHARVALUES | UNESCAPE_CHARVALUES,
    
    /// <summary>
    /// same like <see cref="Default"/> except that the produced file
    /// (when writing to a stream) is more compact (but less readable)
    /// </summary>
    Compact  = QUOTE_VALUES | WRITE_SUBGROUPS | ESCAPE_CHARVALUES | UNESCAPE_CHARVALUES,
    
    /// <summary>
    /// These flags can be used to read a DB from a HTTP header. It uses a
    /// colon instead of the equation mark with variable definitions and stops
    /// when encountering an empty line.
    /// </summary>
    Http = USE_COLON | UNTIL_EMPTY_LINE | OMIT_TYPES | DosMode
}

[Flags]
public enum GwenDbNodeFlags : uint
{
    /** is set then this node has been altered */
    DIRTY                   = 0x00000001,
    /** variable is volatile (will not be written) */
    VOLATILE                = 0x00000002,
    /** this is only valid for groups. It determines whether subgroups will
     *  inherit the hash mechanism set in the root node. */
    INHERIT_HASH_MECHANISM  = 0x00000004,
    /** node has to be disposed of safely (i.e. it will be overridden before freeing it) */
    SAFE                    = 0x00000008
}

/// <summary>
/// This specifies the type of a value stored in the DB.
/// </summary>
public enum GwenDbNodeType
{
    /** type unknown */
    Unknown=-1,
    /** group */
    Group=0,
    /** variable */
    Var,
    /** simple, null terminated C-string */
    ValueChar,
    /** integer */
    ValueInt,
    /** binary, user defined data */
    ValueBin,
    /** pointer , will not be stored or read to/from files */
    ValuePtr,
    /** last value type */
    ValueLast
}

public class GwenDbNode
{
    #region DLL Imports

    // Variable Getter and Setter
    [DllImport("libgwenhywfar.so")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? GWEN_DB_GetCharValue(IntPtr n, [MarshalAs(UnmanagedType.LPStr)] string? path, int idx, [MarshalAs(UnmanagedType.LPStr)] string? defVal);
    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_DB_SetCharValue(IntPtr n, uint flags, [MarshalAs(UnmanagedType.LPStr)] string? path, [MarshalAs(UnmanagedType.LPStr)] string? val);

    // Reading and Writing From/To IO Layers
    
    // Not implemented:
    //  - GWEN_DB_ReadFromFastBuffer
    
    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_DB_ReadFromIo(IntPtr n, IntPtr sio, uint dbflags);

    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    private static extern int GWEN_DB_ReadFile(IntPtr n, [MarshalAs(UnmanagedType.LPStr)] string? fname, uint dbflags);
    
    // Not implemented:
    //  - GWEN_DB_ReadFromString
    //  - GWEN_DB_WriteToFastBuffer
    
    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_DB_WriteToIo(IntPtr node, IntPtr sio, uint dbflags);
    
    [DllImport("libgwenhywfar.so")]
    private static extern int GWEN_DB_WriteFile(IntPtr node, [MarshalAs(UnmanagedType.LPStr)] string? fname, uint dbflags);
    
    // Not implemented:
    //  - GWEN_DB_WriteToBuffer

    #endregion
    
    protected GwenDbNode(IntPtr dbNode)
    {
        this._dbNode = dbNode;
    }

    /// <summary>
    /// Returns the variable's retrieved value.
    /// </summary>
    /// <param name="path">path path and name of the variable</param>
    /// <param name="idx">idx index number of the value to return</param>
    /// <param name="defVal">defVal default value to return in case there is no real value</param>
    public string? GetCharValue(string path, int idx = 0, string? defVal = null)
    {
        return GWEN_DB_GetCharValue(this._dbNode, path, idx, defVal);
    }

    protected readonly IntPtr _dbNode;
    
    public static explicit operator IntPtr(GwenDbNode n) => n._dbNode;
    public static explicit operator GwenDbNode(IntPtr p) => new GwenDbNode(p);

    #region Reading and Writing From/To IO Layers
    
    public void ReadFromIo(SyncIO syncIo, GwenDbFlags dbFlags)
    {
        ReadFromIo(syncIo, (uint)dbFlags);
    }
    public void ReadFromIo(SyncIO syncIo, uint dbFlags)
    {
        int returnValue = GWEN_DB_ReadFromIo(this._dbNode, (IntPtr)syncIo, dbFlags);
        if (returnValue != 0)
            throw new IOException($"Error reading from IO: {returnValue}");
    }
    
    public void ReadFile(string fileName, GwenDbFlags dbFlags)
    {
        ReadFile(fileName, (uint)dbFlags);
    }
    public void ReadFile(string fileName, uint dbFlags)
    {
        int returnValue = GWEN_DB_ReadFile(this._dbNode, fileName, dbFlags);
        if (returnValue != 0)
            throw new IOException($"Error reading from file: {returnValue}");
    }
    
    public void WriteToIo(SyncIO syncIo, GwenDbFlags dbFlags)
    {
        WriteToIo(syncIo, (uint)dbFlags);
    }
    public void WriteToIo(SyncIO syncIo, uint dbFlags)
    {
        int returnValue = GWEN_DB_WriteToIo(this._dbNode, (IntPtr)syncIo, dbFlags);
        if (returnValue != 0)
            throw new IOException($"Error writing to IO: {returnValue}");
    }

    public void WriteFile(string fileName, GwenDbFlags dbFlags)
    {
        WriteFile(fileName, (uint)dbFlags);
    }
    public void WriteFile(string fileName, uint dbFlags)
    {
        int returnValue = GWEN_DB_WriteFile(this._dbNode, fileName, dbFlags);
        if (returnValue != 0)
            throw new IOException($"Error writing to file: {returnValue}");
    }
    
    #endregion
}

public class GwenDbGroup : GwenDbNode
{
    [DllImport("libgwenhywfar.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_DB_Group_new([In, MarshalAs(UnmanagedType.LPStr)] string name);

    [DllImport("libgwenhywfar.so")]
    private static extern void GWEN_DB_Group_free(IntPtr n);

    protected GwenDbGroup(GwenDbNode dbNode)
        : base((IntPtr)dbNode)
    {
    }
    
    public GwenDbGroup(string name)
        : base(GWEN_DB_Group_new(name))
    {
    }

    ~GwenDbGroup()
    {
        GWEN_DB_Group_free(this._dbNode);
    }
}

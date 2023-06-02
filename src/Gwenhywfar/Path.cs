namespace Gwenhywfar;

/// <summary>
/// The path flags only use the lower word of the integer. The high word
/// may be used/interpreted by the called function.
/// </summary>
[Flags]
public enum GwenPathFlags : uint
{
    /// <summary>
    /// if this is set then all elements of the path must exist.
    /// </summary>
    PathMustExist          = 0x00000001,
    /// <summary>
    /// if this is set then none of the elements of the path must exist.
    /// </summary>
    PathMustNotExist       = 0x00000002,

    /**
     * if this bit is set then the whole path (at any depth!) will be created.
     * This may lead to double entries at any part of the path.
     * You need this in very rare cases, most likely you want
     * @ref GWEN_PATH_FLAGS_NAMEMUSTEXIST.
     */
    PATHCREATE             = 0x00000004,

    /**
     * if this bit is set then the last element of the path MUST exist.
     * This implies @ref GWEN_PATH_FLAGS_PATHMUSTEXIST
     */
    NAMEMUSTEXIST          = 0x00000008,

    /**
     * if this bit is set then the last element of the path MUST NOT exist.
     */
    NAMEMUSTNOTEXIST       = 0x00000010,

    /// <summary>
    /// if this bit is set then the last element of the path is created in any
    /// case (this is for groups).
    /// This may lead to double entries of the last element.
    /// </summary>
    CreateGroup            = 0x00000020,

    /**
     * if this bit is set then the last element of the path is created in any
     * case (this is for variables).
     * This may lead to double entries of the last element.
     */
    CREATE_VAR              = 0x00000040,

    /**
     * a variable is wanted (if this bit is 0 then a group is wanted).
     * This flag is used internally, too. When the path handler function
     * is called by @ref GWEN_Path_Handle then this flag is modified
     * to reflect the type of the current path element.
     */
    VARIABLE                = 0x00000080,


    /**
     * all elements of the path are to be escaped.
     * This is usefull when used with file names etc. It makes sure that the
     * path elements presented to the path element handler function only
     * consist of alphanumeric characters. All other characters are escaped
     * using @ref GWEN_Text_Escape.
     */
    ESCAPE                  = 0x00000100,

    /** use the same flag for both escape and unescape */
    UNESCAPE                = 0x00000100,

    /* be more tolerant, don't escape common characters such as '.' */
    TOLERANT_ESCAPE         = 0x00000200,

    /**
     * Allow to also escape/unescape the last path element (otherwise it will
     * not be escaped/unescaped).
     */
    CONVERT_LAST            = 0x00000400,

    /**
     * Allows checking for root. If the path begins with a slash ('/') and this
     * flags is set the slash will be included in the first path element
     * passed to the element handler function. Additionally the flag
     * @ref GWEN_PATH_FLAGS_ROOT will be set. Otherwise there will be no check
     * and special treatment of root entries.
     */
    CHECKROOT               = 0x00000800,

    /**
     * This flag is only used with @ref GWEN_Path_HandleWithIdx to disable
     * index handling.
     */
    NO_IDX                  = 0x00001000,

    /**
     *
     */
    RFU1                    = 0x00002000,


    /**
     * @internal
     */
    INTERNAL                = 0x0000c000,

    /**
     * @internal
     * this is flagged for the path function. If this is set then the
     * element given is the last one, otherwise it is not.
     */
    LAST                    = 0x00004000,

    /**
     * @internal
     * this is flagged for the path function. If this is set then the
     * element given is within root (in this case the element passed to the
     * element handler funcion will start with a slash), otherwise it is not.
     */
    ROOT                    = 0x00008000

    /*@}*/
}
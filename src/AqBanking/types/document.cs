using System.Runtime.InteropServices;

namespace AqBanking;

public class Document
{
    #region DLL Imports

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_new")]
    private static extern IntPtr AB_Document_new();

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_free")]
    private static extern void AB_Document_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_GetId", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static extern string? AB_Document_GetId(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_GetOwnerId")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static extern UInt32 AB_Document_GetOwnerId(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_GetId", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static extern string? AB_Document_GetMimeType(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_SetId", CharSet = CharSet.Ansi)]
    public static extern void AB_Document_SetId(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_SetOwnerId")]
    public static extern void AB_Document_SetOwnerId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_SetMimeType", CharSet = CharSet.Ansi)]
    public static extern void AB_Document_SetMimeType(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_SetData")]
    public static extern void AB_Document_SetData(IntPtr p_struct, IntPtr p, UInt32 len);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_SetData")]
    public static extern void AB_Document_GetDataPtr(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Document_SetData")]
    public static extern UInt32 AB_Document_GetDataLen(IntPtr p_struct);

    #endregion

    internal readonly IntPtr _document;

    public Document()
    {
        this._document = AB_Document_new();
    }
    
    ~Document()
    {
        AB_Document_free(this._document);
    }

    public string? Id
    {
        get => AB_Document_GetId(this._document);
        set => AB_Document_SetId(this._document, value);
    }

    public UInt32 OwnerId
    {
        get => AB_Document_GetOwnerId(this._document);
        set => AB_Document_SetOwnerId(this._document, value);
    }

    public string? MimeType
    {
        get => AB_Document_GetMimeType(this._document);
        set => AB_Document_SetMimeType(this._document, value);
    }

    public byte[] Data
    {
        get
        {
            // TBD use AB_Document_GetDataPtr and AB_Document_GetDataLen
            throw new NotImplementedException();
        }

        set
        {
            // TBD use AB_Document_SetData
            throw new NotImplementedException();
        }
    }

}

public class DocumentList
{
    internal readonly IntPtr _documentList;

    public DocumentList(IntPtr documentList)
    {
        this._documentList = documentList;
    }

    // TODO: furhter implementation missing here
}
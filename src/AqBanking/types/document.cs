using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class Document
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Document_new();

    [DllImport("libaqbanking.so")]
    private static extern void AB_Document_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static extern string? AB_Document_GetId(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    public static extern UInt32 AB_Document_GetOwnerId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static extern string? AB_Document_GetMimeType(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    public static extern void AB_Document_SetId(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so")]
    public static extern void AB_Document_SetOwnerId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    public static extern void AB_Document_SetMimeType(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so")]
    public static extern void AB_Document_SetData(IntPtr p_struct, IntPtr p, UInt32 len);

    [DllImport("libaqbanking.so")]
    public static extern void AB_Document_GetDataPtr(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    public static extern void AB_Document_SetAcknowledgeCode(IntPtr st, byte p, UInt32 len);

    [DllImport("libaqbanking.so")]
    public static extern byte AB_Document_GetAcknowledgeCodePtr(IntPtr st);

    [DllImport("libaqbanking.so")]
    public static extern UInt32 AB_Document_GetAcknowledgeCodeLen(IntPtr st);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Document_ReadDb(IntPtr p_struct, IntPtr p_db);
    
    [DllImport("libaqbanking.so")]
    private static extern int AB_Document_WriteDb(IntPtr p_struct, IntPtr p_db);
    
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Document_fromDb(IntPtr p_db);
    
    [DllImport("libaqbanking.so")]
    private static extern int AB_Document_toDb(IntPtr p_struct, IntPtr p_db);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _document;

    private Document(IntPtr document)
    {
        this._document = document;
    }
    
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
    
    public void ReadDb(GwenDbNode db)
    {
        AB_Document_ReadDb(_document, (IntPtr)db);
    }

    public void WriteDb(GwenDbNode db)
    {
        int returnValue = AB_Document_WriteDb(_document, (IntPtr)db);
        ErrorHandling.CheckForErrors(returnValue);
    }

    public void ToDb(GwenDbNode db)
    {
        int returnValue = AB_Document_toDb(_document, (IntPtr)db);
        ErrorHandling.CheckForErrors(returnValue);
    }
    
    public static Document FromDb(GwenDbNode db)
    {
        return new Document(AB_Document_fromDb((IntPtr)db));
    }

    public static explicit operator IntPtr(Document document) => document._document;
}

public class DocumentList
{
    private readonly IntPtr _documentList;

    public DocumentList(IntPtr documentList)
    {
        this._documentList = documentList;
    }

    // TODO: further implementation missing here

    public static explicit operator IntPtr(DocumentList list) => list._documentList;
}
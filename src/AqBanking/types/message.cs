using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public enum MessageSource
{
    Unknown = -1,
    None = 0,
    System,
    Bank
}

public class Message
{
    #region DLL Imports

    [DllImport("libaqbanking.so")]
    private static extern MessageSource AB_Message_Source_fromString([MarshalAs(UnmanagedType.LPStr)] string? p_s);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern string? AB_Message_Source_toString(MessageSource p_i);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Message_new();
    [DllImport("libaqbanking.so")]
    private static extern void AB_Message_free(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern MessageSource AB_Message_GetSource(IntPtr p_struct);
    [DllImport("libaqbanking.so")]
    private static extern uint AB_Message_GetUserId(IntPtr p_struct);
    [DllImport("libaqbanking.so")]
    private static extern uint AB_Message_GetAccountId(IntPtr p_struct);
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern string? AB_Message_GetSubject(IntPtr p_struct);
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern string? AB_Message_GetText(IntPtr p_struct);
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Message_GetDateReceived(IntPtr p_struct);
    [DllImport("libaqbanking.so")]
    private static extern void AB_Message_SetSource(IntPtr p_struct, MessageSource  p_src);
    [DllImport("libaqbanking.so")]
    private static extern void AB_Message_SetUserId(IntPtr p_struct, uint p_src);
    [DllImport("libaqbanking.so")]
    private static extern void AB_Message_SetAccountId(IntPtr p_struct, uint p_src);
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Message_SetSubject(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Message_SetText(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);
    [DllImport("libaqbanking.so")]
    private static extern void AB_Message_SetDateReceived(IntPtr p_struct, IntPtr p_src);

    #endregion

    private readonly IntPtr _message;

    public Message()
    {
        this._message = AB_Message_new();
    }

    internal Message(IntPtr ptr)
    {
        this._message = ptr;
    }
    
    ~Message()
    {
        AB_Message_free(this._message);
    }

    public MessageSource Source
    {
        get => AB_Message_GetSource(this._message);
        set => AB_Message_SetSource(this._message, value);
    }

    public uint UserId
    {
        get => AB_Message_GetUserId(this._message);
        set => AB_Message_SetUserId(this._message, value);
    }

    public uint AccountId
    {
        get => AB_Message_GetAccountId(this._message);
        set => AB_Message_SetAccountId(this._message, value);
    }

    public string? Subject
    {
        get => AB_Message_GetSubject(this._message);
        set => AB_Message_SetSubject(this._message, value);
    }

    public string? Text
    {
        get => AB_Message_GetText(this._message);
        set => AB_Message_SetText(this._message, value);
    }

    public GwenTime DateReceived
    {
        get => (GwenTime)AB_Message_GetDateReceived(this._message);
        set => AB_Message_SetDateReceived(this._message, (IntPtr)value);
    }
    
    public static explicit operator IntPtr(Message message) => message._message;
}

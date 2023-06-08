using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class ImExporterContext
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking")]
    private static extern IntPtr AB_ImExporterContext_new();

    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_free(IntPtr p_struct);

    [DllImport("libaqbanking")]
    private static extern IntPtr AB_ImExporterContext_GetAccountInfoList(IntPtr p_struct);

    [DllImport("libaqbanking")]
    private static extern IntPtr AB_ImExporterContext_GetSecurityList(IntPtr p_struct);
    
    [DllImport("libaqbanking")]
    private static extern IntPtr AB_ImExporterContext_GetMessageList(IntPtr p_struct);
    
    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_SetAccountInfoList(IntPtr p_struct, IntPtr p_src);
    
    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_SetSecurityList(IntPtr p_struct, IntPtr p_src);
    
    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_SetMessageList(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking")]
    private static extern IntPtr AB_ImExporterContext_fromDb(IntPtr p_db);
    
    [DllImport("libaqbanking")]
    private static extern int AB_ImExporterContext_toDb(IntPtr p_struct, IntPtr p_db);
    
    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_Clear(IntPtr str);

    // TODO multiple ImExporterContext functions not implemented here 
    
    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_AddSecurity(IntPtr st, IntPtr sec);

    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_AddMessage(IntPtr st, IntPtr msg);
    
    [DllImport("libaqbanking")]
    private static extern void AB_ImExporterContext_AddTransaction(IntPtr st, IntPtr t);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _context;

    public ImExporterContext()
    {
        this._context = AB_ImExporterContext_new();
    }

    private ImExporterContext(IntPtr context)
    {
        this._context = context;
    }

    ~ImExporterContext()
    {
        AB_ImExporterContext_free(this._context);
    }

    /// <summary>
    /// This function clears the context (e.g. removes all transactions etc).
    /// </summary>
    public void Clear()
    {
        AB_ImExporterContext_Clear(this._context);
    }

    public ImExporterList<ImExporterAccountInfo> AccountInfoList
    {
        get => new(AB_ImExporterContext_GetAccountInfoList(_context));
        set => AB_ImExporterContext_SetAccountInfoList(_context, (IntPtr)value);
    }
    
    public ImExporterList<Security> SecurityList
    {
        get => new(AB_ImExporterContext_GetSecurityList(_context));
        set => AB_ImExporterContext_SetSecurityList(_context, (IntPtr)value);
    }

    public ImExporterList<Message> MessageList
    {
        get => new(AB_ImExporterContext_GetMessageList(_context));
        set => AB_ImExporterContext_SetMessageList(_context, (IntPtr)value);
    }

    public void AddSecurity(Security security)
    {
        AB_ImExporterContext_AddSecurity(this._context, (IntPtr)security);
    }

    public void AddMessage(Message message)
    {
        AB_ImExporterContext_AddMessage(_context, (IntPtr)message);
    }

    public void AddTransaction(Transaction transaction)
    {
        AB_ImExporterContext_AddTransaction(this._context, (IntPtr)transaction);
    }

    public static ImExporterContext FromDb(GwenDbNode node)
    {
        return new ImExporterContext(AB_ImExporterContext_fromDb((IntPtr)node));
    }

    public void ToDb(GwenDbNode node)
    {
        int returnValue = AB_ImExporterContext_toDb(this._context, (IntPtr)node);
        ErrorHandling.CheckForErrors(returnValue);
    }
    
    public static explicit operator IntPtr(ImExporterContext context) => context._context;
}

internal static class ImExporterList
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking")]
    public static extern IntPtr AB_ImExporterAccountInfo_List_new();
    
    [DllImport("libaqbanking")]
    public static extern IntPtr AB_ImExporterAccountInfo_List_First(IntPtr p_struct);
    
    [DllImport("libaqbanking")]
    public static extern IntPtr AB_ImExporterAccountInfo_List_Next(IntPtr p_struct);

    [DllImport("libaqbanking")]
    public static extern IntPtr AB_ImExporterAccountInfo_List_Add(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking")]
    public static extern int AB_ImExporterAccountInfo_List_GetCount(IntPtr p_struct);

    [DllImport("libaqbanking")]
    public static extern int AB_ImExporterAccountInfo_List_Clear(IntPtr p_struct);
    // ReSharper restore InconsistentNaming

    #endregion
}

public class ImExporterList<T> : GwenList<T>
    where T : class
{
    public ImExporterList()
        : base(ImExporterList.AB_ImExporterAccountInfo_List_new())
    {
    }

    internal ImExporterList(IntPtr listPtr) : base(listPtr)
    {
    }

    internal static IntPtr GenericTypeToIntPtr(T item)
    {
        if (item is ImExporterAccountInfo accountInfo)
        {
            return (IntPtr)accountInfo;
        }

        if (item is Message message)
        {
            return (IntPtr)message;
        }

        if (item is Security security)
        {
            return (IntPtr)security;
        }

        throw new NotSupportedException(
            "Unexpected use of generic type. Only Security, Message and ImExporterAccountInfo is supported.");
    }

    public void Add(T item)
    {
        ImExporterList.AB_ImExporterAccountInfo_List_Add(this.ListPtr, GenericTypeToIntPtr(item));
    }

    public int Count => ImExporterList.AB_ImExporterAccountInfo_List_GetCount(ListPtr);

    public void Clear()
    {
        ImExporterList.AB_ImExporterAccountInfo_List_Clear(ListPtr);
    }

    public override IEnumerator<T> GetEnumerator()
    {
        return new ImExporterEnumerator<T>(ListPtr);
    }
}

internal class ImExporterEnumerator<T> : GwenListEnumerator<T>
    where T : class
{
    public ImExporterEnumerator(IntPtr listPtr) : base(listPtr)
    {
    }

    protected override IntPtr FirstInternal()
    {
        return ImExporterList.AB_ImExporterAccountInfo_List_First(ListPtr);
    }

    protected override IntPtr NextInternal(IntPtr last)
    {
        return ImExporterList.AB_ImExporterAccountInfo_List_Next(last);
    }

    protected override T NewInternal(IntPtr ptr)
    {
        if (typeof(T) == typeof(ImExporterAccountInfo))
        {
#pragma warning disable CS8603
            return new ImExporterAccountInfo(ptr) as T;
#pragma warning restore CS8603
        }
        
        if (typeof(T) == typeof(Message))
        {
#pragma warning disable CS8603
            return new Message(ptr) as T;
#pragma warning restore CS8603
        }
        
        if (typeof(T) == typeof(Security))
        {
#pragma warning disable CS8603
            return new Security(ptr) as T;
#pragma warning restore CS8603
        }
        
        throw new NotSupportedException(
            "Unexpected use of generic type in ImExporterEnumerator<>. Only Security, Message and ImExporterAccountInfo is supported.");
    }
}
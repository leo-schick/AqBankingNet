using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class ImExporterContext
{
    #region DLL Imports

    [DllImport("libaqbanking.so", EntryPoint = "AB_ImExporterContext_new")]
    private static extern IntPtr AB_ImExporterContext_new();

    [DllImport("libaqbanking.so", EntryPoint = "AB_ImExporterContext_free")]
    private static extern void AB_ImExporterContext_free(IntPtr p_struct);

    // TODO multiple ImExporterContext functions not implemented here
    
    [DllImport("libaqbanking.so", EntryPoint = "AB_ImExporterContext_Clear")]
    private static extern void AB_ImExporterContext_Clear(IntPtr str);

    // TODO multiple ImExporterContext functions not implemented here 
    
    [DllImport("libaqbanking.so", EntryPoint = "AB_ImExporterContext_AddTransaction")]
    private static extern void AB_ImExporterContext_AddTransaction(IntPtr st, IntPtr t);

    #endregion

    internal readonly IntPtr _context;

    public ImExporterContext()
    {
        this._context = AB_ImExporterContext_new();
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

    public void AddTransaction(Transaction transaction)
    {
        AB_ImExporterContext_AddTransaction(this._context, transaction._transaction);
    }
}

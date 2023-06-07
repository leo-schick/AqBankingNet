using System.Runtime.InteropServices;

namespace AqBanking;

public partial class Banking
{
    #region DLL Imports
    
    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so")]
    private static extern int AB_Banking_GetAccountSpecList(IntPtr ab, ref IntPtr pAccountSpecList);

    [DllImport("libaqbanking.so")]
    private static extern int AB_Banking_GetAccountSpecByUniqueId(IntPtr ab, UInt32 uniqueAccountId, ref IntPtr pAccountSpec);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_Banking_ReserveJobId(IntPtr ab);

    [DllImport("libaqbanking.so")]
    private static extern int AB_Banking_SendCommands(IntPtr ab, IntPtr commandList, IntPtr ctx);
    // ReSharper restore InconsistentNaming

    #endregion
    
    
    /// <summary>
    /// Returns a list of all known accounts.
    /// </summary>
    public AccountList GetAccountSpecList()
    {
        IntPtr pAccountSpecList = default;
        int returnValue = AB_Banking_GetAccountSpecList(this._banking, ref pAccountSpecList);
        ErrorHandling.CheckForErrors(returnValue);
        return new AccountList(pAccountSpecList);
    }

    public Account? GetAccountSpecByUniqueId(UInt32 uniqueAccountId)
    {
        IntPtr pAccountSpec = default;
        int returnValue = AB_Banking_GetAccountSpecByUniqueId(_banking, uniqueAccountId, ref pAccountSpec);
        ErrorHandling.CheckForErrors(returnValue);
        if (pAccountSpec == default)
            return null;
        return new Account(pAccountSpec);
    }
    
    #region Sending Banking Commands

    /// <summary>
    /// Ask AqBanking for a new job id which can be used with <see cref="Transaction"/>.Id.
    ///
    /// When sending jobs via <see cref="SendCommands"/> AqBanking assigns a unique job id for every
    /// job in the list. However, applications can assign such an id beforehand to work with it.
    /// </summary>
    /// <returns></returns>
    public UInt32 ReserveJobId()
    {
        return AB_Banking_ReserveJobId(this._banking);
    }

    /// <summary>
    /// This function sends all jobs from the given list to their
    /// respective backend. The results will be stored in the given im-/exporter
    /// context.
    ///
    /// This function does <bold>not</bold> take over or free the commands.
    /// </summary>
    /// 
    public void SendCommands(TransactionList commandList, ImExporterContext context)
    {
        int returnValue = AB_Banking_SendCommands(this._banking, (IntPtr)commandList, (IntPtr)context);
        ErrorHandling.CheckForErrors(returnValue, "Error on executeQueue ({0})");
    }
    
    #endregion
}

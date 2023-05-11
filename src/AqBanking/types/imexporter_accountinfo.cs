using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class ImExporterAccountInfo
{
    #region DLL Imports
    
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_ImExporterAccountInfo_new();

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetCountry(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetBankCode(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetBankName(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetAccountNumber(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetSubAccountId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetAccountName(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetIban(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetBic(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetOwner(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetCurrency(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetDescription(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_ImExporterAccountInfo_GetAccountType(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_ImExporterAccountInfo_GetAccountId(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_ImExporterAccountInfo_GetBalanceList(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_ImExporterAccountInfo_GetTransactionList(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_ImExporterAccountInfo_GetEStatementList(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetCountry(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetBankCode(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetBankName(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetAccountNumber(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetSubAccountId(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetAccountName(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetIban(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetBic(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetOwner(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetCurrency(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetDescription(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_ImExporterAccountInfo_SetAccountType(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? _src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_SetAccountId(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] UInt32 _src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_SetBalanceList(IntPtr p_struct, IntPtr _src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_SetTransactionList(IntPtr p_struct, IntPtr _src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_SetEStatementList(IntPtr p_struct, IntPtr _src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_Clear(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_FillFromTransaction(IntPtr p_struct, IntPtr t);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_AddTransaction(IntPtr p_struct, IntPtr t);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_AddBalance(IntPtr p_struct, IntPtr b);

    [DllImport("libaqbanking.so")]
    private static extern void AB_ImExporterAccountInfo_AddEStatement(IntPtr p_struct, IntPtr d);

    // TODO: there are multiple functions which could be added here ...
    
    #endregion

    private readonly IntPtr _imExporterAccountInfo;
    
    public ImExporterAccountInfo()
    {
        _imExporterAccountInfo = AB_ImExporterAccountInfo_new();
    }

    ~ImExporterAccountInfo()
    {
        AB_ImExporterAccountInfo_free(_imExporterAccountInfo);
    }

    public string? Country
    {
        get => AB_ImExporterAccountInfo_GetCountry(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetCountry(this._imExporterAccountInfo, value);
    }

    public string? BankCode
    {
        get => AB_ImExporterAccountInfo_GetBankCode(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetBankCode(this._imExporterAccountInfo, value);
    }

    public string? BankName
    {
        get => AB_ImExporterAccountInfo_GetBankName(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetBankName(this._imExporterAccountInfo, value);
    }

    public string? AccountNumber
    {
        get => AB_ImExporterAccountInfo_GetAccountNumber(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetAccountNumber(this._imExporterAccountInfo, value);
    }

    public string? SubAccountId
    {
        get => AB_ImExporterAccountInfo_GetSubAccountId(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetSubAccountId(this._imExporterAccountInfo, value);
    }

    public string? AccountName
    {
        get => AB_ImExporterAccountInfo_GetAccountName(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetAccountName(this._imExporterAccountInfo, value);
    }

    public string? Iban
    {
        get => AB_ImExporterAccountInfo_GetIban(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetIban(this._imExporterAccountInfo, value);
    }

    public string? Bic
    {
        get => AB_ImExporterAccountInfo_GetBic(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetBic(this._imExporterAccountInfo, value);
    }

    public string? Owner
    {
        get => AB_ImExporterAccountInfo_GetOwner(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetOwner(this._imExporterAccountInfo, value);
    }

    public string? Currency
    {
        get => AB_ImExporterAccountInfo_GetCurrency(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetCurrency(this._imExporterAccountInfo, value);
    }

    public string? Description
    {
        get => AB_ImExporterAccountInfo_GetDescription(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetDescription(this._imExporterAccountInfo, value);
    }

    public string? AccountType
    {
        get => AB_ImExporterAccountInfo_GetAccountType(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetAccountType(this._imExporterAccountInfo, value);
    }

    public UInt32 AccountId
    {
        get => AB_ImExporterAccountInfo_GetAccountId(this._imExporterAccountInfo);
        set => AB_ImExporterAccountInfo_SetAccountId(this._imExporterAccountInfo, value);
    }

    public BalanceList BalanceList
    {
        get => new BalanceList(AB_ImExporterAccountInfo_GetBalanceList(this._imExporterAccountInfo));
        set => AB_ImExporterAccountInfo_SetBalanceList(this._imExporterAccountInfo, value._balanceList);
    }

    public TransactionList TransactionList
    {
        get => new TransactionList(AB_ImExporterAccountInfo_GetTransactionList(this._imExporterAccountInfo));
        set => AB_ImExporterAccountInfo_SetTransactionList(this._imExporterAccountInfo, value._transactionList);
    }

    public DocumentList EStatementList
    {
        get => new DocumentList(AB_ImExporterAccountInfo_GetEStatementList(this._imExporterAccountInfo));
        set => AB_ImExporterAccountInfo_SetEStatementList(this._imExporterAccountInfo, value._documentList);
    }

    public void AddTransaction(Transaction transaction)
    {
        AB_ImExporterAccountInfo_AddTransaction(this._imExporterAccountInfo, transaction._transaction);
    }

    public void AddBalance(Balance balance)
    {
        AB_ImExporterAccountInfo_AddBalance(this._imExporterAccountInfo, balance._balance);
    }

    public void AddEStatement(Document document)
    {
        AB_ImExporterAccountInfo_AddEStatement(this._imExporterAccountInfo, document._document);
    }

    /// <summary>
    /// This function clears the account info (e.g. removes all transactions etc).
    /// </summary>
    public void Clear()
    {
        AB_ImExporterAccountInfo_Clear(this._imExporterAccountInfo);
    }

    public void FillFromTransaction(Transaction transaction)
    {
        AB_ImExporterAccountInfo_FillFromTransaction(this._imExporterAccountInfo, transaction._transaction);
    }
}
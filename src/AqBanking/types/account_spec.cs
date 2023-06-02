using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public class Account
{
    #region DLL Imports
    
    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_new")]
    private static extern IntPtr AB_AccountSpec_new();

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_new")]
    private static extern void AB_AccountSpec_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetType")]
    private static extern int AB_AccountSpec_GetType(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetUniqueId")]
    private static extern UInt32 AB_AccountSpec_GetUniqueId(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetBackendName", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetBackendName(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetOwnerName", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetOwnerName(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetAccountName", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetAccountName(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetCurrency", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetCurrency(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetMemo", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetMemo(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetIban", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetIban(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetBic", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetBic(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetCountry", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetCountry(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetBankCode", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetBankCode(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetBankName", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetBankName(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetBranchId", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetBranchId(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetAccountNumber", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetAccountNumber(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetSubAccountNumber", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_AccountSpec_GetSubAccountNumber(IntPtr p_struct);

    //[DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_GetTransactionLimitsList", CharSet = CharSet.Ansi)]
    //private static extern TBD AB_AccountSpec_GetTransactionLimitsList(IntPtr p_struct);


    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetType")]
    private static extern void AB_AccountSpec_SetType(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetUniqueId")]
    private static extern void AB_AccountSpec_SetUniqueId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetBackendName")]
    private static extern void AB_AccountSpec_SetBackendName(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetOwnerName")]
    private static extern void AB_AccountSpec_SetOwnerName(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetAccountName")]
    private static extern void AB_AccountSpec_SetAccountName(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetCurrency")]
    private static extern void AB_AccountSpec_SetCurrency(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetMemo")]
    private static extern void AB_AccountSpec_SetMemo(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetIban")]
    private static extern void AB_AccountSpec_SetIban(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetBic")]
    private static extern void AB_AccountSpec_SetBic(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetCountry")]
    private static extern void AB_AccountSpec_SetCountry(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetBankCode")]
    private static extern void AB_AccountSpec_SetBankCode(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetBankName")]
    private static extern void AB_AccountSpec_SetBankName(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetBranchId")]
    private static extern void AB_AccountSpec_SetBranchId(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetAccountNumber")]
    private static extern void AB_AccountSpec_SetAccountNumber(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetSubAccountNumber")]
    private static extern void AB_AccountSpec_SetSubAccountNumber(IntPtr p_struct, [In, MarshalAs(UnmanagedType.LPStr)] string? p_src);

    //[DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_SetTransactionLimitsList")]
    //private static extern void AB_AccountSpec_SetTransactionLimitsList(IntPtr p_struct, [In] string? p_src);
    
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_AccountSpec_GetTransactionLimitsForCommand(IntPtr st, TransactionCommand cmd);

    [DllImport("libaqbanking.so")]
    private static extern void AB_AccountSpec_AddTransactionLimits(IntPtr st, IntPtr l);

    #endregion
    
    private readonly IntPtr _account;

    internal Account(IntPtr account)
    {
        this._account = account;
    }

    public Account()
    {
        this._account = AB_AccountSpec_new();
    }

    ~Account()
    {
        AB_AccountSpec_free(this._account);
    }

    public int Type
    {
        get => AB_AccountSpec_GetType(this._account);
        set => AB_AccountSpec_SetType(this._account, value);
    }

    public UInt32 UniqueId
    {
        get => AB_AccountSpec_GetUniqueId(this._account);
        set => AB_AccountSpec_SetUniqueId(this._account, value);
    }

    public string? BackendName
    {
        get => AB_AccountSpec_GetBackendName(this._account);
        set => AB_AccountSpec_SetBackendName(this._account, value);
    }

    public string? OwnerName
    {
        get => AB_AccountSpec_GetOwnerName(this._account);
        set => AB_AccountSpec_SetOwnerName(this._account, value);
    }

    public string? AccountName
    {
        get => AB_AccountSpec_GetAccountName(this._account);
        set => AB_AccountSpec_SetAccountName(this._account, value);
    }

    public string? Currency
    {
        get => AB_AccountSpec_GetCurrency(this._account);
        set => AB_AccountSpec_SetCurrency(this._account, value);
    }

    public string? Memo
    {
        get => AB_AccountSpec_GetMemo(this._account);
        set => AB_AccountSpec_SetMemo(this._account, value);
    }

    public string? Iban
    {
        get => AB_AccountSpec_GetIban(this._account);
        set => AB_AccountSpec_SetIban(this._account, value);
    }

    public string? Bic
    {
        get => AB_AccountSpec_GetBic(this._account);
        set => AB_AccountSpec_SetBic(this._account, value);
    }

    public string? Country
    {
        get => AB_AccountSpec_GetCountry(this._account);
        set => AB_AccountSpec_SetCountry(this._account, value);
    }

    public string? BankCode
    {
        get => AB_AccountSpec_GetBankCode(this._account);
        set => AB_AccountSpec_SetBankCode(this._account, value);
    }

    public string? BankName
    {
        get => AB_AccountSpec_GetBankName(this._account);
        set => AB_AccountSpec_SetBankName(this._account, value);
    }

    public string? BranchId
    {
        get => AB_AccountSpec_GetBranchId(this._account);
        set => AB_AccountSpec_SetBranchId(this._account, value);
    }

    public string? AccountNumber
    {
        get => AB_AccountSpec_GetAccountNumber(this._account);
        set => AB_AccountSpec_SetAccountNumber(this._account, value);
    }

    public string? SubAccountNumber
    {
        get => AB_AccountSpec_GetSubAccountNumber(this._account);
        set => AB_AccountSpec_SetSubAccountNumber(this._account, value);
    }

    //public TBD TransactionLimitsList
    //{
    //    get => ...;
    //    set => ...;
    //}

    public TransactionLimits GetTransactionLimitsForCommand(TransactionCommand command)
    {
        return new TransactionLimits(AB_AccountSpec_GetTransactionLimitsForCommand(this._account, command));
    }

    public void AddTransactionLimits(TransactionLimits limits)
    {
        AB_AccountSpec_AddTransactionLimits(this._account, (IntPtr)limits);
    }
    
    public static explicit operator IntPtr(Account account) => account._account;
}

public class AccountList : GwenList<Account>
{
    #region DLL Imports

    [DllImport("libaqbanking.so", EntryPoint = "AB_AccountSpec_List_free")]
    private static extern IntPtr AB_AccountSpec_List_free(IntPtr accs);

    #endregion

    internal AccountList(IntPtr accountSpecList)
        : base(accountSpecList)
    {
    }

    ~AccountList()
    {
        AB_AccountSpec_List_free(ListPtr);
    }

    public override IEnumerator<Account> GetEnumerator()
    {
        return new AccountListEnumerator(ListPtr);
    }
}

internal class AccountListEnumerator : GwenListEnumerator<Account>
{
    #region DLL Imports

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_AccountSpec_List_First(IntPtr accs);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_AccountSpec_List_Next(IntPtr accs);

    #endregion

    internal AccountListEnumerator(IntPtr accountSpecList)
        : base(accountSpecList)
    {
    }

    protected override IntPtr FirstInternal()
    {
        return AB_AccountSpec_List_First(ListPtr);
    }

    protected override IntPtr NextInternal(IntPtr last)
    {
        return AB_AccountSpec_List_Next(last);
    }

    protected override Account NewInternal(IntPtr ptr)
    {
        return new Account(ptr);
    }
}

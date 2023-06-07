using System.Collections;
using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public enum TransactionType
{
    Unknown = -1,
    None = 0,
    Statement = 128,
    NotedStatement,
    Transfer = 256,
    DebitNote,
    StandingOrder,
    InternalTransfer,
    Brokerage,
    Transaction = 2048,
    Split
}

public enum TransactionSubType
{
    SubTypeUnknown = -1,
    SubTypeNone = 0,
    SubTypeStandard,
    SubTypeCheck,
    SubTypeBookedDebitNote,
    SubTypeDrawnDebitNote,
    SubTypeStandingOrder,
    SubTypeLoan,
    SubTypeEuStandard,
    // ReSharper disable once InconsistentNaming
    SubTypeEuASAP,
    SubTypeBuy,
    SubTypeSell,
    SubTypeReinvest,
    SubTypeDividend
}

public enum TransactionCommand
{
    Unknown = -1,
    None = 0,
    GetBalance,
    GetTransactions,
    GetStandingOrders,
    GetDatedTransfers,
    SepaGetStandingOrders,
    LoadCellPhone,
    GetEStatements,
    Transfer = 512,
    DebitNote,
    CreateStandingOrder,
    ModifyStandingOrder,
    DeleteStandingOrder,
    CreateDatedTransfer,
    ModifyDatedTransfer,
    DeleteDatedTransfer,
    InternalTransfer,
    GetDepot,
    SepaTransfer = 1536,
    SepaDebitNote,
    SepaFlashDebitNote,
    SepaCreateStandingOrder,
    SepaModifyStandingOrder,
    SepaDeleteStandingOrder,
    SepaCreateDatedTransfer,
    SepaModifyDatedTransfer,
    SepaDeleteDatedTransfer,
    SepaInternalTransfer
}

public enum TransactionStatus
{
    Unknown = -1,
    None = 0,
    Enqueued,
    Sending,
    Sent,
    Accepted,
    Rejected,
    Pending,
    AutoReconciled,
    ManuallyReconciled,
    Revoked,
    Aborted,
    Error
}

public enum TransactionPeriod
{
    Unknown = -1,
    None = 0,
    Monthly,
    Weekly
}

public enum TransactionCharge
{
    Unknown = -1,
    Nobody = 0,
    Local,
    Remote,
    Share
}

public enum TransactionSequence
{
    Unknown = -1,
    Once = 0,
    First,
    Following,
    Final
}


public class Transaction
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_new();

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_free(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern TransactionType AB_Transaction_GetType(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern TransactionSubType AB_Transaction_GetSubType(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern TransactionCommand AB_Transaction_GetCommand(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern TransactionStatus AB_Transaction_GetStatus(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_Transaction_GetUniqueAccountId(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_Transaction_GetUniqueId(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_Transaction_GetRefUniqueId(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_Transaction_GetIdForApplication(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetStringIdForApplication(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern UInt32 AB_Transaction_GetSessionId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern UInt32 AB_Transaction_GetGroupId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetFiId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalIban(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalBic(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalCountry(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalBankCode(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalBranchId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalAccountNumber(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalSuffix(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetLocalName(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteCountry(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteBankCode(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteBranchId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteAccountNumber(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteSuffix(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteIban(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteBic(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteName(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Transaction_GetDate(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Transaction_GetValutaDate(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Transaction_GetValue(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Transaction_GetFees(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern int AB_Transaction_GetTransactionCode(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetTransactionText(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetTransactionKey(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern int AB_Transaction_GetTextKey(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetPrimanota(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetPurpose(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetCategory(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetCustomerReference(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetBankReference(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetEndToEndReference(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetUltimateCreditor(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetUltimateDebtor(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetCreditorSchemeId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetOriginatorId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetMandateId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_Transaction_GetMandateDate(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetMandateDebitorName(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetOriginalCreditorSchemeId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetOriginalMandateId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetOriginalCreditorName(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern TransactionSequence AB_Transaction_GetSequence(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern TransactionCharge AB_Transaction_GetCharge(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteAddrStreet(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteAddrZipcode(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteAddrCity(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetRemoteAddrPhone(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern TransactionPeriod AB_Transaction_GetPeriod(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_Transaction_GetCycle(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern UInt32 AB_Transaction_GetExecutionDay(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_GetFirstDate(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_GetLastDate(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_GetNextDate(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetUnitId(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetUnitIdNameSpace(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetTickerSymbol(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_GetUnits(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_GetUnitPriceValue(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_GetUnitPriceDate(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_GetCommissionValue(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetMemo(IntPtr p_struct);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string? AB_Transaction_GetHash(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetType(IntPtr p_struct, TransactionType p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetSubType(IntPtr p_struct, TransactionSubType p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetCommand(IntPtr p_struct, TransactionCommand p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetStatus(IntPtr p_struct, TransactionStatus p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetUniqueAccountId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetUniqueId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetRefUniqueId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetIdForApplication(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetStringIdForApplication(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetSessionId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetGroupId(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetFiId(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalIban(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalBic(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalCountry(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalBankCode(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalBranchId(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalAccountNumber(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalSuffix(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetLocalName(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteCountry(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteBankCode(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteBranchId(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteAccountNumber(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteSuffix(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteIban(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteBic(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteName(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetValutaDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetValue(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetFees(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetTransactionCode(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetTransactionText(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetTransactionKey(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetTextKey(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetPrimanota(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetPurpose(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetCategory(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetCustomerReference(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetBankReference(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetEndToEndReference(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetUltimateCreditor(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetUltimateDebtor(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetCreditorSchemeId(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetOriginatorId(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetMandateId(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetMandateDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetMandateDebitorName(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetOriginalCreditorSchemeId(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetOriginalMandateId(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetOriginalCreditorName(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetSequence(IntPtr p_struct, TransactionSequence p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetCharge(IntPtr p_struct, TransactionCharge p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteAddrStreet(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteAddrZipcode(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteAddrCity(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetRemoteAddrPhone(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetPeriod(IntPtr p_struct, TransactionPeriod p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetCycle(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetExecutionDay(IntPtr p_struct, UInt32 p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetFirstDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetLastDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetNextDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void
        AB_Transaction_SetUnitId(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetUnitIdNameSpace(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetTickerSymbol(IntPtr p_struct,
        [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetUnits(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetUnitPriceValue(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetUnitPriceDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_SetCommissionValue(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetMemo(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern void AB_Transaction_SetHash(IntPtr p_struct, [MarshalAs(UnmanagedType.LPStr)] string? p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_ReadDb(IntPtr p_struct, IntPtr p_db);

    [DllImport("libaqbanking.so")]
    private static extern int AB_Transaction_WriteDb(IntPtr p_struct, IntPtr p_db);
    
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_fromDb(IntPtr p_db);

    [DllImport("libaqbanking.so")]
    private static extern int AB_Transaction_toDb(IntPtr p_struct, IntPtr p_db);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _transaction;

    public Transaction()
    {
        this._transaction = AB_Transaction_new();
    }

    internal Transaction(IntPtr ptr)
    {
        this._transaction = ptr;
    }

    ~Transaction()
    {
        AB_Transaction_free(this._transaction);
    }

    public TransactionType Type
    {
        get => AB_Transaction_GetType(this._transaction);
        set => AB_Transaction_SetType(this._transaction, value);
    }

    public TransactionSubType SubType
    {
        get => AB_Transaction_GetSubType(this._transaction);
        set => AB_Transaction_SetSubType(this._transaction, value);
    }

    public TransactionCommand Command
    {
        get => AB_Transaction_GetCommand(this._transaction);
        set => AB_Transaction_SetCommand(this._transaction, value);
    }

    public TransactionStatus Status
    {
        get => AB_Transaction_GetStatus(this._transaction);
        set => AB_Transaction_SetStatus(this._transaction, value);
    }

    public UInt32 UniqueAccountId
    {
        get => AB_Transaction_GetUniqueAccountId(this._transaction);
        set => AB_Transaction_SetUniqueAccountId(this._transaction, value);
    }

    public UInt32 UniqueId
    {
        get => AB_Transaction_GetUniqueId(this._transaction);
        set => AB_Transaction_SetUniqueId(this._transaction, value);
    }

    public UInt32 RefUniqueId
    {
        get => AB_Transaction_GetRefUniqueId(this._transaction);
        set => AB_Transaction_SetRefUniqueId(this._transaction, value);
    }

    public UInt32 IdForApplication
    {
        get => AB_Transaction_GetIdForApplication(this._transaction);
        set => AB_Transaction_SetIdForApplication(this._transaction, value);
    }

    public string? StringIdForApplication
    {
        get => AB_Transaction_GetStringIdForApplication(this._transaction);
        set => AB_Transaction_SetStringIdForApplication(this._transaction, value);
    }

    public UInt32 SessionId
    {
        get => AB_Transaction_GetSessionId(this._transaction);
        set => AB_Transaction_SetSessionId(this._transaction, value);
    }

    public UInt32 GroupId
    {
        get => AB_Transaction_GetGroupId(this._transaction);
        set => AB_Transaction_SetGroupId(this._transaction, value);
    }

    public string? FiId
    {
        get => AB_Transaction_GetFiId(this._transaction);
        set => AB_Transaction_SetFiId(this._transaction, value);
    }

    public string? LocalIban
    {
        get => AB_Transaction_GetLocalIban(this._transaction);
        set => AB_Transaction_SetLocalIban(this._transaction, value);
    }

    public string? LocalBic
    {
        get => AB_Transaction_GetLocalBic(this._transaction);
        set => AB_Transaction_SetLocalBic(this._transaction, value);
    }

    public string? LocalCountry
    {
        get => AB_Transaction_GetLocalCountry(this._transaction);
        set => AB_Transaction_SetLocalCountry(this._transaction, value);
    }

    public string? LocalBankCode
    {
        get => AB_Transaction_GetLocalBankCode(this._transaction);
        set => AB_Transaction_SetLocalBankCode(this._transaction, value);
    }

    public string? LocalBranchId
    {
        get => AB_Transaction_GetLocalBranchId(this._transaction);
        set => AB_Transaction_SetLocalBranchId(this._transaction, value);
    }

    public string? LocalAccountNumber
    {
        get => AB_Transaction_GetLocalAccountNumber(this._transaction);
        set => AB_Transaction_SetLocalAccountNumber(this._transaction, value);
    }

    public string? LocalSuffix
    {
        get => AB_Transaction_GetLocalSuffix(this._transaction);
        set => AB_Transaction_SetLocalSuffix(this._transaction, value);
    }

    public string? LocalName
    {
        get => AB_Transaction_GetLocalName(this._transaction);
        set => AB_Transaction_SetLocalName(this._transaction, value);
    }

    public string? RemoteCountry
    {
        get => AB_Transaction_GetRemoteCountry(this._transaction);
        set => AB_Transaction_SetRemoteCountry(this._transaction, value);
    }

    public string? RemoteBankCode
    {
        get => AB_Transaction_GetRemoteBankCode(this._transaction);
        set => AB_Transaction_SetRemoteBankCode(this._transaction, value);
    }

    public string? RemoteBranchId
    {
        get => AB_Transaction_GetRemoteBranchId(this._transaction);
        set => AB_Transaction_SetRemoteBranchId(this._transaction, value);
    }

    public string? RemoteAccountNumber
    {
        get => AB_Transaction_GetRemoteAccountNumber(this._transaction);
        set => AB_Transaction_SetRemoteAccountNumber(this._transaction, value);
    }

    public string? RemoteSuffix
    {
        get => AB_Transaction_GetRemoteSuffix(this._transaction);
        set => AB_Transaction_SetRemoteSuffix(this._transaction, value);
    }

    public string? RemoteIban
    {
        get => AB_Transaction_GetRemoteIban(this._transaction);
        set => AB_Transaction_SetRemoteIban(this._transaction, value);
    }

    public string? RemoteBic
    {
        get => AB_Transaction_GetRemoteBic(this._transaction);
        set => AB_Transaction_SetRemoteBic(this._transaction, value);
    }

    public string? RemoteName
    {
        get => AB_Transaction_GetRemoteName(this._transaction);
        set => AB_Transaction_SetRemoteName(this._transaction, value);
    }

    public GwenDate Date
    {
        get => (GwenDate)AB_Transaction_GetDate(this._transaction);
        set => AB_Transaction_SetDate(this._transaction, (IntPtr)value);
    }

    public GwenDate ValutaDate
    {
        get => (GwenDate)AB_Transaction_GetValutaDate(this._transaction);
        set => AB_Transaction_SetValutaDate(this._transaction, (IntPtr)value);
    }

    public Value Value
    {
        get => new Value(AB_Transaction_GetValue(this._transaction));
        set => AB_Transaction_SetValue(this._transaction, (IntPtr)value);
    }

    public Value Fees
    {
        get => new Value(AB_Transaction_GetFees(this._transaction));
        set => AB_Transaction_SetFees(this._transaction, (IntPtr)value);
    }

    public int TransactionCode
    {
        get => AB_Transaction_GetTransactionCode(this._transaction);
        set => AB_Transaction_SetTransactionCode(this._transaction, value);
    }

    public string? TransactionText
    {
        get => AB_Transaction_GetTransactionText(this._transaction);
        set => AB_Transaction_SetTransactionText(this._transaction, value);
    }

    public string? TransactionKey
    {
        get => AB_Transaction_GetTransactionKey(this._transaction);
        set => AB_Transaction_SetTransactionKey(this._transaction, value);
    }

    public int TextKey
    {
        get => AB_Transaction_GetTextKey(this._transaction);
        set => AB_Transaction_SetTextKey(this._transaction, value);
    }

    public string? Primanota
    {
        get => AB_Transaction_GetPrimanota(this._transaction);
        set => AB_Transaction_SetPrimanota(this._transaction, value);
    }

    public string? Purpose
    {
        get => AB_Transaction_GetPurpose(this._transaction);
        set => AB_Transaction_SetPurpose(this._transaction, value);
    }

    public string? Category
    {
        get => AB_Transaction_GetCategory(this._transaction);
        set => AB_Transaction_SetCategory(this._transaction, value);
    }

    public string? CustomerReference
    {
        get => AB_Transaction_GetCustomerReference(this._transaction);
        set => AB_Transaction_SetCustomerReference(this._transaction, value);
    }

    public string? BankReference
    {
        get => AB_Transaction_GetBankReference(this._transaction);
        set => AB_Transaction_SetBankReference(this._transaction, value);
    }

    public string? EndToEndReference
    {
        get => AB_Transaction_GetEndToEndReference(this._transaction);
        set => AB_Transaction_SetEndToEndReference(this._transaction, value);
    }

    public string? UltimateCreditor
    {
        get => AB_Transaction_GetUltimateCreditor(this._transaction);
        set => AB_Transaction_SetUltimateCreditor(this._transaction, value);
    }

    public string? UltimateDebtor
    {
        get => AB_Transaction_GetUltimateDebtor(this._transaction);
        set => AB_Transaction_SetUltimateDebtor(this._transaction, value);
    }

    public string? CreditorSchemeId
    {
        get => AB_Transaction_GetCreditorSchemeId(this._transaction);
        set => AB_Transaction_SetCreditorSchemeId(this._transaction, value);
    }

    public string? OriginatorId
    {
        get => AB_Transaction_GetOriginatorId(this._transaction);
        set => AB_Transaction_SetOriginatorId(this._transaction, value);
    }

    public string? MandateId
    {
        get => AB_Transaction_GetMandateId(this._transaction);
        set => AB_Transaction_SetMandateId(this._transaction, value);
    }

    public GwenDate MandateDate
    {
        get => (GwenDate)AB_Transaction_GetMandateDate(this._transaction);
        set => AB_Transaction_SetMandateDate(this._transaction, (IntPtr)value);
    }

    public string? MandateDebitorName
    {
        get => AB_Transaction_GetMandateDebitorName(this._transaction);
        set => AB_Transaction_SetMandateDebitorName(this._transaction, value);
    }

    public string? OriginalCreditorSchemeId
    {
        get => AB_Transaction_GetOriginalCreditorSchemeId(this._transaction);
        set => AB_Transaction_SetOriginalCreditorSchemeId(this._transaction, value);
    }

    public string? OriginalMandateId
    {
        get => AB_Transaction_GetOriginalMandateId(this._transaction);
        set => AB_Transaction_SetOriginalMandateId(this._transaction, value);
    }

    public string? OriginalCreditorName
    {
        get => AB_Transaction_GetOriginalCreditorName(this._transaction);
        set => AB_Transaction_SetOriginalCreditorName(this._transaction, value);
    }

    public TransactionSequence Sequence
    {
        get => AB_Transaction_GetSequence(this._transaction);
        set => AB_Transaction_SetSequence(this._transaction, value);
    }

    public TransactionCharge Charge
    {
        get => AB_Transaction_GetCharge(this._transaction);
        set => AB_Transaction_SetCharge(this._transaction, value);
    }

    public string? RemoteAddrStreet
    {
        get => AB_Transaction_GetRemoteAddrStreet(this._transaction);
        set => AB_Transaction_SetRemoteAddrStreet(this._transaction, value);
    }

    public string? RemoteAddrZipcode
    {
        get => AB_Transaction_GetRemoteAddrZipcode(this._transaction);
        set => AB_Transaction_SetRemoteAddrZipcode(this._transaction, value);
    }

    public string? RemoteAddrCity
    {
        get => AB_Transaction_GetRemoteAddrCity(this._transaction);
        set => AB_Transaction_SetRemoteAddrCity(this._transaction, value);
    }

    public string? RemoteAddrPhone
    {
        get => AB_Transaction_GetRemoteAddrPhone(this._transaction);
        set => AB_Transaction_SetRemoteAddrPhone(this._transaction, value);
    }

    public TransactionPeriod Period
    {
        get => AB_Transaction_GetPeriod(this._transaction);
        set => AB_Transaction_SetPeriod(this._transaction, value);
    }

    public UInt32 Cycle
    {
        get => AB_Transaction_GetCycle(this._transaction);
        set => AB_Transaction_SetCycle(this._transaction, value);
    }

    public UInt32 ExecutionDay
    {
        get => AB_Transaction_GetExecutionDay(this._transaction);
        set => AB_Transaction_SetExecutionDay(this._transaction, value);
    }

    public GwenDate FirstDate
    {
        get => (GwenDate)AB_Transaction_GetFirstDate(this._transaction);
        set => AB_Transaction_SetFirstDate(this._transaction, (IntPtr)value);
    }

    public GwenDate LastDate
    {
        get => (GwenDate)AB_Transaction_GetLastDate(this._transaction);
        set => AB_Transaction_SetLastDate(this._transaction, (IntPtr)value);
    }

    public GwenDate NextDate
    {
        get => (GwenDate)AB_Transaction_GetNextDate(this._transaction);
        set => AB_Transaction_SetNextDate(this._transaction, (IntPtr)value);
    }

    public string? UnitId
    {
        get => AB_Transaction_GetUnitId(this._transaction);
        set => AB_Transaction_SetUnitId(this._transaction, value);
    }

    public string? UnitIdNameSpace
    {
        get => AB_Transaction_GetUnitIdNameSpace(this._transaction);
        set => AB_Transaction_SetUnitIdNameSpace(this._transaction, value);
    }

    public string? TickerSymbol
    {
        get => AB_Transaction_GetTickerSymbol(this._transaction);
        set => AB_Transaction_SetTickerSymbol(this._transaction, value);
    }

    public Value Units
    {
        get => new Value(AB_Transaction_GetUnits(this._transaction));
        set => AB_Transaction_SetUnits(this._transaction, (IntPtr)value);
    }

    public Value UnitPriceValue
    {
        get => new Value(AB_Transaction_GetUnitPriceValue(this._transaction));
        set => AB_Transaction_SetUnitPriceValue(this._transaction, (IntPtr)value);
    }

    public GwenDate UnitPriceDate
    {
        get => (GwenDate)AB_Transaction_GetUnitPriceDate(this._transaction);
        set => AB_Transaction_SetUnitPriceDate(this._transaction, (IntPtr)value);
    }

    public Value CommissionValue
    {
        get => new Value(AB_Transaction_GetCommissionValue(this._transaction));
        set => AB_Transaction_SetCommissionValue(this._transaction, (IntPtr)value);
    }

    public string? Memo
    {
        get => AB_Transaction_GetMemo(this._transaction);
        set => AB_Transaction_SetMemo(this._transaction, value);
    }

    public string? Hash
    {
        get => AB_Transaction_GetHash(this._transaction);
        set => AB_Transaction_SetHash(this._transaction, value);
    }

    public void ReadDb(GwenDbNode db)
    {
        AB_Transaction_ReadDb(_transaction, (IntPtr)db);
    }

    public void WriteDb(GwenDbNode db)
    {
        int returnValue = AB_Transaction_WriteDb(_transaction, (IntPtr)db);
        ErrorHandling.CheckForErrors(returnValue);
    }

    public void ToDb(GwenDbNode db)
    {
        int returnValue = AB_Transaction_toDb(_transaction, (IntPtr)db);
        ErrorHandling.CheckForErrors(returnValue);
    }

    public static Transaction FromDb(GwenDbNode db)
    {
        return new Transaction(AB_Transaction_fromDb((IntPtr)db));
    }

    public static explicit operator IntPtr(Transaction transaction) => transaction._transaction;
}

public class TransactionList : IEnumerable<Transaction>
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_List2_new();

    // not used in aqbanking tools code, so it might not exist  
    //[DllImport("libaqbanking.so")]
    //private static extern void AB_Transaction_List2_free(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_List2_PushBack(IntPtr p_struct, IntPtr t);

    [DllImport("libaqbanking.so")]
    private static extern uint AB_Transaction_List2_GetSize(IntPtr p_struct);
    // ReSharper restore InconsistentNaming
    
    #endregion

    private readonly IntPtr _listPtr;
    
    public TransactionList()
    {
        _listPtr = AB_Transaction_List2_new();
    }

    internal TransactionList(IntPtr transactionList)
    {
        _listPtr = transactionList;
    }

    public void PushBack(Transaction transaction)
    {
        AB_Transaction_List2_PushBack(_listPtr, (IntPtr)transaction);
    }

    public uint Size => AB_Transaction_List2_GetSize(_listPtr);

    public IEnumerator<Transaction> GetEnumerator()
    {
        return new TransactionListEnumerator(_listPtr);
    }

    #region IEnumerator

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
    
    public static explicit operator IntPtr(TransactionList list) => list._listPtr;
}

internal class TransactionListEnumerator : GwenList2Enumerator<Transaction>
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_List2_First(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_List2Iterator_Data(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Transaction_List2Iterator_Next(IntPtr p_struct);
    
    [DllImport("libaqbanking.so")]
    private static extern void AB_Transaction_List2Iterator_free(IntPtr p_struct);
    // ReSharper restore InconsistentNaming

    #endregion

    public TransactionListEnumerator(IntPtr listPtr) : base(listPtr)
    {
    }

    protected override IntPtr FirstInternal()
    {
        return AB_Transaction_List2_First(ListPtr);
    }

    protected override IntPtr NextInternal(IntPtr iterator)
    {
        return AB_Transaction_List2Iterator_Next(iterator);
    }

    protected override Transaction NewInternal(IntPtr ptr)
    {
        return new Transaction(ptr);
    }

    protected override IntPtr GetCurrentInternal(IntPtr iterator)
    {
        return AB_Transaction_List2Iterator_Data(iterator);
    }

    protected override void FreeIteratorInternal(IntPtr ptr)
    {
        AB_Transaction_List2Iterator_free(ptr);
    }
}

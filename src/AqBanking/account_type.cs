using System.Runtime.InteropServices;

namespace AqBanking;

public enum AccountType
{
    Invalid = -1,
    Unknown = 0,
    Bank,
    CreditCard,
    Checking,
    Savings,
    Investment,
    Cash,
    MoneyMarket,
    Credit,
    Unspecified=100,
    Last
}

public static class AccountTypeExtension
{
    #region DLL Imports
    
    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern IntPtr AB_AccountType_toChar(AccountType ty);

    [DllImport("libaqbanking.so", CharSet = CharSet.Ansi)]
    private static extern Int32 AB_AccountType_fromChar([MarshalAs(UnmanagedType.LPStr)] string s);
    // ReSharper restore InconsistentNaming

    #endregion

    public static string? ToString(this AccountType accountType)
    {
        return Marshal.PtrToStringAnsi(AB_AccountType_toChar(accountType));
    }

    public static AccountType AccountTypeFromString(string accountType)
    {
        return (AccountType)AB_AccountType_fromChar(accountType);
    }
}

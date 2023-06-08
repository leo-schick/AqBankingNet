using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public enum BankInfoCheckResult
{
    Ok=0,
    NotOk,
    UnknownBank,
    UnknownResult
}

public partial class Banking
{
    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_CheckIban([In, MarshalAs(UnmanagedType.LPStr)] string iban);

    public static int CheckIban(string iban)
    {
        return AB_Banking_CheckIban(iban);
    }

    [DllImport("libaqbanking", CharSet = CharSet.Ansi)]
    private static extern int AB_Banking_MakeGermanIban([In, MarshalAs(UnmanagedType.LPStr)] string bankCode, [In, MarshalAs(UnmanagedType.LPStr)] string accountNumber, IntPtr ibanBuf);

    public static string? MakeGermanIban(string bankCode, string accountNumber)
    {
        var ibanBuf = new GwenBuffer(0, 35, 0, true);
        int returnValue = AB_Banking_MakeGermanIban(bankCode, accountNumber, ibanBuf);
        ErrorHandling.CheckForErrors(returnValue);
        return ibanBuf.GetStart();
    }
}

using System.Runtime.InteropServices;
using Gwenhywfar;

namespace AqBanking;

public enum BalanceType
{
    Unknown = -1,
    None = 0,
    Noted,
    Booked,
    BankLine,
    Disposable,
    Temporary,
    DayStart,
    DayEnd
}

public class Balance
{
    #region DLL Imports

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_new")]
    private static extern IntPtr AB_Balance_new();

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_free")]
    private static extern void AB_Balance_free(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_GetDate")]
    private static extern IntPtr AB_Balance_GetDate(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_GetValue")]
    private static extern IntPtr AB_Balance_GetValue(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_GetType")]
    private static extern BalanceType AB_Balance_GetType(IntPtr p_struct);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_SetDate")]
    private static extern void AB_Balance_SetDate(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_SetValue")]
    private static extern void AB_Balance_SetValue(IntPtr p_struct, IntPtr p_src);

    [DllImport("libaqbanking.so", EntryPoint = "AB_Balance_SetType")]
    private static extern void AB_Balance_SetType(IntPtr p_struct, BalanceType p_src);

    #endregion

    internal readonly IntPtr _balance;

    public Balance()
    {
        this._balance = AB_Balance_new();
    }

    ~Balance()
    {
        AB_Balance_free(this._balance);
    }

    public GwenDate Date
    {
        get => (GwenDate)AB_Balance_GetDate(this._balance);
        set => AB_Balance_SetDate(this._balance, (IntPtr)value);
    }

    public Value Value
    {
        get => new Value(AB_Balance_GetValue(this._balance));
        set => AB_Balance_SetValue(this._balance, value._value);
    }

    public BalanceType Type
    {
        get => AB_Balance_GetType(this._balance);
        set => AB_Balance_SetType(this._balance, value);
    }
}

public class BalanceList
{
    internal readonly IntPtr _balanceList;

    public BalanceList(IntPtr balanceList)
    {
        this._balanceList = balanceList;
    }

    // TODO: furhter implementation missing here
}
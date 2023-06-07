using System.Runtime.InteropServices;

namespace AqBanking;

public class TransactionLimits
{
    #region DLL Imports

    // ReSharper disable InconsistentNaming
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_TransactionLimits_new();

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_free(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern TransactionCommand AB_TransactionLimits_GetCommand(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxLenLocalName(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinLenLocalName(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxLenRemoteName(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinLenRemoteName(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxLenCustomerReference(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinLenCustomerReference(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxLenBankReference(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinLenBankReference(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxLenPurpose(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinLenPurpose(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxLinesPurpose(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinLinesPurpose(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetNeedDate(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinValueSetupTime(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxValueSetupTime(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinValueSetupTimeFirst(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxValueSetupTimeFirst(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinValueSetupTimeOnce(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxValueSetupTimeOnce(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinValueSetupTimeRecurring(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxValueSetupTimeRecurring(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMinValueSetupTimeFinal(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetMaxValueSetupTimeFinal(IntPtr p_struct);

    //[DllImport("libaqbanking.so")]
    //private static extern uint8_t *AB_TransactionLimits_GetValuesCycleWeek(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetValuesCycleWeekUsed(IntPtr p_struct);

    //[DllImport("libaqbanking.so")]
    //private static extern uint8_t *AB_TransactionLimits_GetValuesCycleMonth(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetValuesCycleMonthUsed(IntPtr p_struct);

    //[DllImport("libaqbanking.so")]
    //private static extern uint8_t *B_TransactionLimits_GetValuesExecutionDayWeek(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetValuesExecutionDayWeekUsed(IntPtr p_struct);

    //[DllImport("libaqbanking.so")]
    //private static extern uint8_t *AB_TransactionLimits_GetValuesExecutionDayMonth(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetValuesExecutionDayMonthUsed(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowMonthly(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowWeekly(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeRecipientAccount(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeRecipientName(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeValue(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeTextKey(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangePurpose(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeFirstExecutionDate(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeLastExecutionDate(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeCycle(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangePeriod(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern int AB_TransactionLimits_GetAllowChangeExecutionDay(IntPtr p_struct);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetCommand(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxLenLocalName(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinLenLocalName(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxLenRemoteName(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinLenRemoteName(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxLenCustomerReference(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinLenCustomerReference(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxLenBankReference(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinLenBankReference(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxLenPurpose(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinLenPurpose(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxLinesPurpose(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinLinesPurpose(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetNeedDate(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinValueSetupTime(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxValueSetupTime(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinValueSetupTimeFirst(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxValueSetupTimeFirst(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinValueSetupTimeOnce(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxValueSetupTimeOnce(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinValueSetupTimeRecurring(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxValueSetupTimeRecurring(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMinValueSetupTimeFinal(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetMaxValueSetupTimeFinal(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetValuesCycleWeekUsed(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetValuesCycleMonthUsed(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetValuesExecutionDayWeekUsed(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetValuesExecutionDayMonthUsed(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowMonthly(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowWeekly(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeRecipientAccount(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeRecipientName(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeValue(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeTextKey(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangePurpose(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeFirstExecutionDate(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeLastExecutionDate(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeCycle(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangePeriod(IntPtr p_struct, int p_src);

    [DllImport("libaqbanking.so")]
    private static extern void AB_TransactionLimits_SetAllowChangeExecutionDay(IntPtr p_struct, int p_src);
    // ReSharper restore InconsistentNaming

    #endregion

    private readonly IntPtr _transactionLimits;

    public TransactionLimits()
    {
        this._transactionLimits = AB_TransactionLimits_new();
    }

    internal TransactionLimits(IntPtr transactionLimits)
    {
        this._transactionLimits = transactionLimits;
    }

    ~TransactionLimits()
    {
        AB_TransactionLimits_free(this._transactionLimits);
    }

    public TransactionCommand Command
    {
        get => AB_TransactionLimits_GetCommand(this._transactionLimits);
        set => AB_TransactionLimits_SetCommand(this._transactionLimits, (int)value);
    }

    public int MaxLenLocalName
    {
        get => AB_TransactionLimits_GetMaxLenLocalName(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxLenLocalName(this._transactionLimits, value);
    }

    public int MinLenLocalName
    {
        get => AB_TransactionLimits_GetMinLenLocalName(this._transactionLimits);
        set => AB_TransactionLimits_SetMinLenLocalName(this._transactionLimits, value);
    }

    public int MaxLenRemoteName
    {
        get => AB_TransactionLimits_GetMaxLenRemoteName(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxLenRemoteName(this._transactionLimits, value);
    }

    public int MinLenRemoteName
    {
        get => AB_TransactionLimits_GetMinLenRemoteName(this._transactionLimits);
        set => AB_TransactionLimits_SetMinLenRemoteName(this._transactionLimits, value);
    }

    public int MaxLenCustomerReference
    {
        get => AB_TransactionLimits_GetMaxLenCustomerReference(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxLenCustomerReference(this._transactionLimits, value);
    }

    public int MinLenCustomerReference
    {
        get => AB_TransactionLimits_GetMinLenCustomerReference(this._transactionLimits);
        set => AB_TransactionLimits_SetMinLenCustomerReference(this._transactionLimits, value);
    }

    public int MaxLenBankReference
    {
        get => AB_TransactionLimits_GetMaxLenBankReference(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxLenBankReference(this._transactionLimits, value);
    }

    public int MinLenBankReference
    {
        get => AB_TransactionLimits_GetMinLenBankReference(this._transactionLimits);
        set => AB_TransactionLimits_SetMinLenBankReference(this._transactionLimits, value);
    }

    public int MaxLenPurpose
    {
        get => AB_TransactionLimits_GetMaxLenPurpose(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxLenPurpose(this._transactionLimits, value);
    }

    public int MinLenPurpose
    {
        get => AB_TransactionLimits_GetMinLenPurpose(this._transactionLimits);
        set => AB_TransactionLimits_SetMinLenPurpose(this._transactionLimits, value);
    }

    public int MaxLinesPurpose
    {
        get => AB_TransactionLimits_GetMaxLinesPurpose(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxLinesPurpose(this._transactionLimits, value);
    }

    public int MinLinesPurpose
    {
        get => AB_TransactionLimits_GetMinLinesPurpose(this._transactionLimits);
        set => AB_TransactionLimits_SetMinLinesPurpose(this._transactionLimits, value);
    }

    public bool NeedDate
    {
        get => AB_TransactionLimits_GetNeedDate(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetNeedDate(this._transactionLimits, value ? 1 : 0);
    }

    public int MinValueSetupTime
    {
        get => AB_TransactionLimits_GetMinValueSetupTime(this._transactionLimits);
        set => AB_TransactionLimits_SetMinValueSetupTime(this._transactionLimits, value);
    }

    public int MaxValueSetupTime
    {
        get => AB_TransactionLimits_GetMaxValueSetupTime(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxValueSetupTime(this._transactionLimits, value);
    }

    public int MinValueSetupTimeFirst
    {
        get => AB_TransactionLimits_GetMinValueSetupTimeFirst(this._transactionLimits);
        set => AB_TransactionLimits_SetMinValueSetupTimeFirst(this._transactionLimits, value);
    }

    public int MaxValueSetupTimeFirst
    {
        get => AB_TransactionLimits_GetMaxValueSetupTimeFirst(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxValueSetupTimeFirst(this._transactionLimits, value);
    }

    public int MinValueSetupTimeOnce
    {
        get => AB_TransactionLimits_GetMinValueSetupTimeOnce(this._transactionLimits);
        set => AB_TransactionLimits_SetMinValueSetupTimeOnce(this._transactionLimits, value);
    }

    public int MaxValueSetupTimeOnce
    {
        get => AB_TransactionLimits_GetMaxValueSetupTimeOnce(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxValueSetupTimeOnce(this._transactionLimits, value);
    }

    public int MinValueSetupTimeRecurring
    {
        get => AB_TransactionLimits_GetMinValueSetupTimeRecurring(this._transactionLimits);
        set => AB_TransactionLimits_SetMinValueSetupTimeRecurring(this._transactionLimits, value);
    }

    public int MaxValueSetupTimeRecurring
    {
        get => AB_TransactionLimits_GetMaxValueSetupTimeRecurring(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxValueSetupTimeRecurring(this._transactionLimits, value);
    }

    public int MinValueSetupTimeFinal
    {
        get => AB_TransactionLimits_GetMinValueSetupTimeFinal(this._transactionLimits);
        set => AB_TransactionLimits_SetMinValueSetupTimeFinal(this._transactionLimits, value);
    }

    public int MaxValueSetupTimeFinal
    {
        get => AB_TransactionLimits_GetMaxValueSetupTimeFinal(this._transactionLimits);
        set => AB_TransactionLimits_SetMaxValueSetupTimeFinal(this._transactionLimits, value);
    }

    //public int ValuesCycleWeek
    //{
    //    get => AB_TransactionLimits_GetValuesCycleWeek(this._transactionLimits);
    //    set => AB_TransactionLimits_SetValuesCycleWeek(this._transactionLimits, value);
    //}

    public int ValuesCycleWeekUsed
    {
        get => AB_TransactionLimits_GetValuesCycleWeekUsed(this._transactionLimits);
        set => AB_TransactionLimits_SetValuesCycleWeekUsed(this._transactionLimits, value);
    }

    //public int ValuesCycleMonth
    //{
    //    get => AB_TransactionLimits_GetValuesCycleMonth(this._transactionLimits);
    //    set => AB_TransactionLimits_SetValuesCycleMonth(this._transactionLimits, value);
    //}

    public int ValuesCycleMonthUsed
    {
        get => AB_TransactionLimits_GetValuesCycleMonthUsed(this._transactionLimits);
        set => AB_TransactionLimits_SetValuesCycleMonthUsed(this._transactionLimits, value);
    }

    //public int ValuesExecutionDayWeek
    //{
    //    get => AB_TransactionLimits_GetValuesExecutionDayWeek(this._transactionLimits);
    //    set => AB_TransactionLimits_SetValuesExecutionDayWeek(this._transactionLimits, value);
    //}

    public int ValuesExecutionDayWeekUsed
    {
        get => AB_TransactionLimits_GetValuesExecutionDayWeekUsed(this._transactionLimits);
        set => AB_TransactionLimits_SetValuesExecutionDayWeekUsed(this._transactionLimits, value);
    }

    //public int ValuesExecutionDayMonth
    //{
    //    get => AB_TransactionLimits_GetValuesExecutionDayMonth(this._transactionLimits);
    //    set => AB_TransactionLimits_SetValuesExecutionDayMonth(this._transactionLimits, value);
    //}

    public int ValuesExecutionDayMonthUsed
    {
        get => AB_TransactionLimits_GetValuesExecutionDayMonthUsed(this._transactionLimits);
        set => AB_TransactionLimits_SetValuesExecutionDayMonthUsed(this._transactionLimits, value);
    }

    public bool AllowMonthly
    {
        get => AB_TransactionLimits_GetAllowMonthly(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowMonthly(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowWeekly
    {
        get => AB_TransactionLimits_GetAllowWeekly(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowWeekly(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeRecipientAccount
    {
        get => AB_TransactionLimits_GetAllowChangeRecipientAccount(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeRecipientAccount(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeRecipientName
    {
        get => AB_TransactionLimits_GetAllowChangeRecipientName(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeRecipientName(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeValue
    {
        get => AB_TransactionLimits_GetAllowChangeValue(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeValue(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeTextKey
    {
        get => AB_TransactionLimits_GetAllowChangeTextKey(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeTextKey(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangePurpose
    {
        get => AB_TransactionLimits_GetAllowChangePurpose(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangePurpose(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeFistExecutionDate
    {
        get => AB_TransactionLimits_GetAllowChangeFirstExecutionDate(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeFirstExecutionDate(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeLastExecutionDate
    {
        get => AB_TransactionLimits_GetAllowChangeLastExecutionDate(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeLastExecutionDate(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeCycle
    {
        get => AB_TransactionLimits_GetAllowChangeCycle(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeCycle(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangePeriod
    {
        get => AB_TransactionLimits_GetAllowChangePeriod(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangePeriod(this._transactionLimits, value ? 1 : 0);
    }

    public bool AllowChangeExecutionDay
    {
        get => AB_TransactionLimits_GetAllowChangeExecutionDay(this._transactionLimits) != 0;
        set => AB_TransactionLimits_SetAllowChangeExecutionDay(this._transactionLimits, value ? 1 : 0);
    }
    
    public static explicit operator IntPtr(TransactionLimits transactionLimits) => transactionLimits._transactionLimits;
}

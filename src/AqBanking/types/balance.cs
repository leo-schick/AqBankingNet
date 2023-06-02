using System.Collections;
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

    private readonly IntPtr _balance;

    public Balance()
    {
        this._balance = AB_Balance_new();
    }

    internal Balance(IntPtr balance)
    {
        this._balance = balance;
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
        get => new(AB_Balance_GetValue(this._balance));
        set => AB_Balance_SetValue(this._balance, value._value);
    }

    public BalanceType Type
    {
        get => AB_Balance_GetType(this._balance);
        set => AB_Balance_SetType(this._balance, value);
    }
    
    public static explicit operator IntPtr(Balance balance) => balance._balance;
}

public class BalanceList
{
    #region DLL Imports

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Balance_List_GetByType(IntPtr p_list, BalanceType p_cmp);

    #endregion
    
    internal readonly IntPtr _balanceList;

    public BalanceList(IntPtr balanceList)
    {
        this._balanceList = balanceList;
    }

    public Balance GetByType(BalanceType type)
    {
        return new Balance(AB_Balance_List_GetByType(this._balanceList, type));
    }
    
    internal class TypedEnumerable : IEnumerable<Balance>
    {
        private readonly IntPtr _balanceList;
        private readonly BalanceType _balanceType;

        internal TypedEnumerable(IntPtr balanceList, BalanceType balanceType)
        {
            _balanceList = balanceList;
            _balanceType = balanceType;
        }
        
        public IEnumerator<Balance> GetEnumerator()
        {
            return new BalanceListEnumerator(_balanceList, _balanceType);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public IEnumerable<Balance> AsEnumerable(BalanceType balanceType)
    {
        return new TypedEnumerable(_balanceList, balanceType);
    }

    // TODO: further implementation missing here
}

internal class BalanceListEnumerator : IEnumerator<Balance>
{
    #region DLL Imports
    
    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Balance_List_FindFirstByType(IntPtr bl, BalanceType ty);

    [DllImport("libaqbanking.so")]
    private static extern IntPtr AB_Balance_List_FindNextByType(IntPtr bl, BalanceType ty);

    //[DllImport("libaqbanking.so")]
    //private static extern IntPtr AB_Balance_List_GetLatestByType(IntPtr bl, BalanceType ty);

    #endregion
    
    private readonly IntPtr _balanceList;
    private readonly BalanceType _balanceType;
    private Balance? _current;
    
    internal BalanceListEnumerator(IntPtr balanceList, BalanceType balanceType)
    {
        _balanceList = balanceList;
        _balanceType = balanceType;
    }
    
    public bool MoveNext()
    {
        IntPtr newAccount = default;
        if (_current == null)
        {
            newAccount = AB_Balance_List_FindFirstByType(this._balanceList, this._balanceType);
        }
        else
        {
            newAccount = AB_Balance_List_FindNextByType((IntPtr)this._current, this._balanceType);
        }

        if (newAccount == default)
            return false;
        
        _current = new Balance(newAccount);
        return true;
    }

    public void Reset()
    {
        _current = null;
    }

    public Balance Current
    {
        get
        {
            if (_current == null)
                throw new InvalidOperationException();
            return _current;
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}
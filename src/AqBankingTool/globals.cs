namespace AqBanking.Tool;

public enum AqBankingToolMultiSepaType
{
    SepaTransfers,
    SepaDepitNotes
}


[Flags]
public enum AqBankingToolLimitFlags : uint
{
    Purpose    = 0x0001,
    Names      = 0x0002,
    Sequence   = 0x0004,
    Date       = 0x0008,
    Sepa       = 0x0010,
}


[Flags]
public enum AqBankingToolRequest : uint
{
    Balance       = 0x0001,
    Statements    = 0x0002,
    SepaSTO       = 0x0004,
    EStatements   = 0x0008,
    Depot         = 0x0010,

    /// <summary>
    /// ignore unsupported
    /// </summary>
    IgnoreUnsup   = 0x8000,
}

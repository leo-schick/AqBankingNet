using System.Runtime.InteropServices;
using AqBanking;
using Gwenhywfar;
using AqBanking.Tool;

string[] ibans = new []
{
    "DE75512108001245126199",
    "DE75 5121 0800 1245 1261 99",
    "fale",
    "12345"
};

foreach(var iban in ibans)
{
    Console.WriteLine($"IBAN: {iban} Valid: {Banking.CheckIban(iban)}");
}

using var banking = new Banking("AqBankingNet.TestApp");

Console.WriteLine($"AqBanking Version: {Banking.GetVersion()}");
Console.WriteLine($"Gwenhywfar Version: {GWEN.GetVersion()}");

// TODO: Enter your FinTS registration key here ...
banking.SetRuntimeConfig("fintsRegistrationKey", "<TBD>");
banking.SetRuntimeConfig("fintsApplicationVersionString", "6.2");

GWEN.Init();

var gui = new GwenGUI();

var dbPins = new GwenDbGroup("pins");
// TODO: Define here a pinfile or manipulate the dbPins class (not implemented/testet yet)
//dbPins.ReadFile("pinfile", (uint)GwenDbFlags.Default | (uint)GwenPathFlags.CreateGroup);

gui.SetPasswordDb(dbPins, true);

GwenGUI.SetGui(gui);

var accounts = banking.GetAccountSpecList();

Console.WriteLine("List all accounts:");
foreach(var account in accounts)
{
    Console.WriteLine($"  {account}");
    Console.WriteLine($"  Type={account.Type}");
    Console.WriteLine($"  UniqueId={account.UniqueId}");
    Console.WriteLine($"  BackendName={account.BackendName}");
    Console.WriteLine($"  OwnerName={account.OwnerName}");
    Console.WriteLine($"  AccountName={account.AccountName}");
    Console.WriteLine($"  Currency={account.Currency}");
    Console.WriteLine($"  Memo={account.Memo}");
    Console.WriteLine($"  Iban={account.Iban}");
    Console.WriteLine($"  Bic={account.Bic}");
    Console.WriteLine($"  Country={account.Country}");
    Console.WriteLine($"  BankCode={account.BankCode}");
    Console.WriteLine($"  BankName={account.BankName}");
    Console.WriteLine($"  BranchId={account.BranchId}");
    Console.WriteLine($"  AccountNumber={account.AccountNumber}");
    Console.WriteLine($"  SubAccountNumber={account.SubAccountNumber}");
    Console.WriteLine("--");
}

GwenDate fromDate = GwenDate.FromGregorian(2023, 5, 1);
GwenDate toDate = GwenDate.FromGregorian(2023, 6, 1);

var transaction = new Transaction();
transaction.Command = TransactionCommand.GetTransactions;
transaction.UniqueAccountId = 2; // TBD get account.UniqueId from first HBCI module account in banking.GetAccountSpecList(), by using AB_AccountSpec_List_FindFirst
if (transaction.Command == TransactionCommand.GetTransactions)
{
    transaction.FirstDate = fromDate;
    transaction.LastDate = toDate;
}

var cmdList = new TransactionList();
cmdList.PushBack(transaction);

var context = new ImExporterContext();

banking.Init();
banking.SendCommands(cmdList, context);

var db = new GwenDbGroup("context");
context.ToDb(db);

// print the context foratted as to the console
var buffer = new GwenBuffer(0, 256, 0, true);
db.WriteToBuffer(buffer, (uint)GwenDbFlags.Default);
Console.WriteLine(buffer.GetStart());

// ... alternatively, iterate through the context data.
// Note: This does not work for the transaction list (which is of type LIST2).
//       Somehow I was not able to make this work, so below
//       I show an anternative way to access the transaction list.
/*Console.WriteLine("AccountInfo {");
Console.WriteLine(context.AccountInfoList.Count);
foreach (var accountInfo in context.AccountInfoList)
{
    Console.WriteLine(accountInfo.BankCode);
    Console.WriteLine(accountInfo.AccountNumber);
    Console.WriteLine(accountInfo.Iban);
    Console.WriteLine(accountInfo.AccountType);
    Console.WriteLine(accountInfo.AccountId);

    Console.WriteLine("Balance {");
    foreach (var balance in accountInfo.BalanceList.AsEnumerable(BalanceType.Noted))
    {
        Console.WriteLine(balance.Type);
        Console.WriteLine(balance.Date);
        Console.WriteLine(balance.Value);
        Console.WriteLine("----");
    }
    Console.WriteLine("}");
    
    Console.WriteLine("Transaction {");

    IntPtr tList = (IntPtr)accountInfo.TransactionList;
    Console.WriteLine(tList);


    //var size = GwenList.GWEN_List_GetSize(tList);
    //Console.WriteLine(size);
    //
    //var jit = GwenList.GWEN_List_First(tList);
    //Console.WriteLine($"iterator: {jit}");
    //
    //var t = GwenListEnumerator.GWEN_ListIterator_DataRefPtr(jit);
    //Console.WriteLine($"data: {t}");

    //while (t != default)
    //{
    //    Console.WriteLine($"n: {t}");
    //    t = TransactionListEnumerator.AB_Transaction_List2Iterator_Next(jit);
    //}

    //GwenListEnumerator.GWEN_ListIterator_free(jit);

    Console.WriteLine(accountInfo.TransactionList.Size);


    foreach (var accountTransaction in accountInfo.TransactionList)
    {
        Console.WriteLine(accountTransaction.Type);
        Console.WriteLine(accountTransaction.SubType);
        Console.WriteLine(accountTransaction.UniqueAccountId);
        Console.WriteLine(accountTransaction.UniqueId);
        
        Console.WriteLine(accountTransaction.LocalBankCode);
        Console.WriteLine(accountTransaction.LocalAccountNumber);
        Console.WriteLine(accountTransaction.RemoteBankCode);
        Console.WriteLine(accountTransaction.RemoteAccountNumber);
        Console.WriteLine(accountTransaction.RemoteIban);
        Console.WriteLine(accountTransaction.RemoteName);
        
        Console.WriteLine(accountTransaction.Date);
        Console.WriteLine(accountTransaction.ValutaDate);
        Console.WriteLine(accountTransaction.Fees);
        Console.WriteLine(accountTransaction.TransactionCode);
        Console.WriteLine(accountTransaction.TransactionText);
        Console.WriteLine(accountTransaction.TransactionKey);
        Console.WriteLine(accountTransaction.Primanota);
        Console.WriteLine("----");
    }
    Console.WriteLine("}");
    Console.WriteLine("----");
}
Console.WriteLine("}");

Console.WriteLine("Message {");
Console.WriteLine(context.MessageList.Count);
foreach (var message in context.MessageList)
{
    Console.WriteLine(message.UserId);
    Console.WriteLine(message.AccountId);
    Console.WriteLine(message.Subject);
    Console.WriteLine(message.Text);
    Console.WriteLine("----");
}
Console.WriteLine("}");

Console.WriteLine("Security {");
Console.WriteLine(context.SecurityList.Count);
foreach (var security in context.SecurityList)
{
    Console.WriteLine(security.UniqueId);
    Console.WriteLine(security.Name);
    Console.WriteLine(security.Units);
    Console.WriteLine("----");
}
Console.WriteLine("}");*/

//context.MessageList


// Here we get the transaction list from the db, iterate through the transaction list sub groups
// and map them into a Transaction instance. Then we just print out the Transaction information.
var transactionList = db
    .FindFirstGroup("accountInfoList")?
    .FindFirstGroup("accountInfo")?
    .FindFirstGroup("transactionList");

if (transactionList != null)
{
    Console.WriteLine(transactionList.GroupName);

    foreach (var t in transactionList.Groups)
    {
        Transaction accountTransaction = Transaction.FromDb(t);

        Console.WriteLine(accountTransaction.Type);
        Console.WriteLine(accountTransaction.SubType);
        Console.WriteLine(accountTransaction.UniqueAccountId);
        Console.WriteLine(accountTransaction.UniqueId);

        Console.WriteLine(accountTransaction.LocalBankCode);
        Console.WriteLine(accountTransaction.LocalAccountNumber);
        Console.WriteLine(accountTransaction.RemoteBankCode);
        Console.WriteLine(accountTransaction.RemoteAccountNumber);
        Console.WriteLine(accountTransaction.RemoteIban);
        Console.WriteLine(accountTransaction.RemoteName);

        Console.WriteLine(accountTransaction.Date);
        Console.WriteLine(accountTransaction.ValutaDate);
        Console.WriteLine(accountTransaction.Fees);
        Console.WriteLine(accountTransaction.TransactionCode);
        Console.WriteLine(accountTransaction.TransactionText);
        Console.WriteLine(accountTransaction.TransactionKey);
        Console.WriteLine(accountTransaction.Primanota);
        Console.WriteLine("----");
    }    
}

// Above I showed a way to manually call the settlements from the bank.
// There is the way the original aqbanking-cli implements this logic
// internally. Not so straight forward but more flexible:

//var t2 = t.GetNextGroup();

//var db = new GwenDbGroup("arguments");
//rv=GWEN_Args_Check(argc, argv, 1,
//                     GWEN_ARGS_MODE_ALLOW_FREEPARAM |
//                     GWEN_ARGS_MODE_STOP_AT_FREEPARAM,
//                     args,
//                     db);

//string? ctxfile = db.GetCharValue("ctxfile");

// Was always empty for me...
//Console.WriteLine("ctxFile = " + ctxfile);

// ... so I defined the file here. Just to know: if you do not provide the ctxfile, the app will return the data to StdOut
//ctxfile = "context.db";

// create requests for every account spec and send them
//Request._createAndAndSendRequests(banking, accounts, fromDate: new DateTime(2023, 5, 1), toDate: new DateTime(2023, 6, 1), AqBankingToolRequest.Statements, ctxfile);

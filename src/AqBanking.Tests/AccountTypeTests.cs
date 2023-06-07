using Gwenhywfar;

namespace AqBanking.Tests;

public class AccountTypeTests
{
    private const char NewLine = '\n';
    private readonly string _sampleAccountDb = "int  type=\"0\"" + NewLine +
                                              "int  uniqueId=\"2\"" + NewLine +
                                              "char accountName=\"My Account\"" + NewLine +
                                              "char iban=\"DE75512108001245126199\"" + NewLine +
                                              "char country=\"DE\"" + NewLine +
                                              "" + NewLine +
                                              "transactionLimitsList {" + NewLine +
                                              "} #transactionLimitsList" + NewLine +
                                              "" + NewLine +
                                              "refAccountList {" + NewLine +
                                              "} #refAccountList" + NewLine;

    [Fact]
    public void AccountToDbStringTest()
    {
        // create Account
        var account = new Account
        {
            UniqueId = 2,
            AccountName = "My Account",
            Country = "DE",
            Iban = "DE75512108001245126199" // IBAN example
        };

        // map data into a db group
        var db = new GwenDbGroup("account");
        account.ToDb(db);

        // transform db into a C# string through a buffer
        var buffer = new GwenBuffer(0, 256, 0, true);
        db.WriteToBuffer(buffer, (uint)GwenDbFlags.Default);

        var dbAsString = buffer.GetStart();
        
        Assert.Equal(_sampleAccountDb, dbAsString);
    }

    [Fact]
    public void AccountFromDbStringTest()
    {
        var db = new GwenDbGroup("account");
        db.ReadFromString(_sampleAccountDb);

        var account = Account.FromDb(db);
        Assert.Equal((uint)2, account.UniqueId);
        Assert.Equal("My Account", account.AccountName);
        Assert.Equal("DE", account.Country);
        Assert.Equal("DE75512108001245126199", account.Iban);
    }
}
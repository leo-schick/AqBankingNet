namespace AqBanking.Tool;

public static class Request
{
    public static void _createAndAndSendRequests(Banking banking, IEnumerable<Account> accountList, DateTime fromDate,
        DateTime toDate, AqBankingToolRequest requestFlags, string? ctxFile = null)
    {
        var list = new TransactionList();

        foreach (var account in accountList)
        {
            Util.createAndAddRequests(banking, list, account, fromDate, toDate, requestFlags);
        }

        // send jobs
        if (list.Size > 0)
        {
            Util.execBankingJobs(banking, list, ctxFile);
        }
    }
}
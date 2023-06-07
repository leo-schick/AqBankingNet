using AqBanking;
using Gwenhywfar;

namespace AqBanking.Tool;

public static class Util
{
    private static void readContext(string? ctxFile, ref ImExporterContext ctx, bool mustExist)
    {
        SyncIO sio;
        if (ctxFile == null)
        {
            sio = SyncIOFile.FromStdin();
            sio.AddFlags((uint)SyncIOFlags.DontClose | (uint)SyncIOFileFlags.Read);
        }
        else
        {
            sio = new SyncIOFile(ctxFile, SyncIOFileCreationMode.OpenExisting);
            sio.AddFlags((uint)SyncIOFileFlags.Read);
            try
            {
                sio.Connect();
            }
            catch (IOException)
            {
                if (!mustExist)
                {
                    ctx = new ImExporterContext();
                    return;
                }
            }
            finally
            {
                sio.Dispose();
            }
        }

        GwenDbNode dbCtx;
        using (sio)
        {
            dbCtx = new GwenDbGroup("context");
            dbCtx.ReadFromIo(sio, (uint)GwenDbFlags.Default | (uint)GwenPathFlags.CreateGroup);
        }

        ctx = ImExporterContext.FromDb(dbCtx);
    }
    
    private static void writeContext(string? ctxFile, ImExporterContext ctx)
    {
        SyncIO sio;
        
        if (ctxFile == null)
        {
            sio = SyncIOFile.FromStdout();
            sio.AddFlags((uint)SyncIOFlags.DontClose | (uint)SyncIOFileFlags.Write);
        }
        else
        {
            sio = new SyncIOFile(ctxFile, SyncIOFileCreationMode.CreateAlways);
            sio.AddFlags((uint)(
                SyncIOFileFlags.Read |
                SyncIOFileFlags.Write |
                SyncIOFileFlags.UserRead |
                SyncIOFileFlags.UserWrite |
                SyncIOFileFlags.GroupRead |
                SyncIOFileFlags.GroupWrite
                ));
            try
            {
                sio.Connect();
            }
            catch (Exception e)
            {
                throw new Exception("Error selecting output file", e);
            }
        }

        GwenDbNode dbCtx;
        using (sio)
        {
            dbCtx = new GwenDbGroup("context");

            try
            {
                ctx.ToDb(dbCtx);
            }
            catch (Exception e)
            {
                throw new Exception("Error writing context to db", e);
            }

            try
            {
                dbCtx.WriteToIo(sio, GwenDbFlags.Default);
            }
            catch (IOException e)
            {
                throw new Exception("Error writing context", e);
            }
            
        }
    }

    public static void execBankingJobs(Banking banking, TransactionList list, string? ctxFile)
    {
        var ctx = new ImExporterContext();

        try
        {
            banking.SendCommands(list, ctx);
        }
        catch (Exception e)
        {
            throw new Exception("Error on executeQueue", e);
        }

        try
        {
            writeContext(ctxFile, ctx);
        }
        catch (Exception e)
        {
            throw new Exception("Error writing context file", e);
        }
    }

    private static Transaction? createAndCheckRequest(Banking banking, Account @as, TransactionCommand cmd)
    {
        /// AB_AccountSpec_GetTransactionLimitsForCommand
        var transactionLimits = @as.GetTransactionLimitsForCommand(cmd);
        if (transactionLimits != null)
        {
            Transaction j = new Transaction();
            j.UniqueAccountId = @as.UniqueId;
            j.Command = cmd;
            return j;
        }
        else
        {
            return null;
        }
    }

    public static void createAndAddRequest(Banking banking, TransactionList list, Account @as,
                        TransactionCommand cmd, DateTime? fromDate = null, DateTime? toDate = null,
                        bool ignoreUnsupported = true)
    {
        if (@as == null)
            throw new ArgumentNullException(nameof(@as));

        var aid = @as.UniqueId;

        var j = createAndCheckRequest(banking, @as, cmd);
        if (j != null)
        {
            if (cmd == TransactionCommand.GetTransactions)
            {
                if (fromDate.HasValue)
                    j.FirstDate = fromDate.Value;
                if (toDate.HasValue)
                    j.LastDate = toDate.Value;
            }
            list.PushBack(j);
            return;
        }
        else
        {
            if (ignoreUnsupported)
            {
                Console.WriteLine($"Warning: Ignoring request \"{cmd}\" for {aid}, not supported.");
            }
            else
            {
                // TODO define custom Exception class
                throw new Exception($"Error: Request \"{cmd}\" for {aid} not supported.");
            }
        }
    }

    public static void createAndAddRequests(Banking banking, TransactionList list, Account @as,
        DateTime? fromDate, DateTime? toDate, AqBankingToolRequest requestFlags)
    {
        bool ignoreUnsupported = requestFlags.HasFlag(AqBankingToolRequest.IgnoreUnsup);

        if (banking == null)
            throw new ArgumentNullException(nameof(banking));
        if (list == null)
            throw new ArgumentNullException(nameof(list));
        if (@as == null)
            throw new ArgumentNullException(nameof(@as));
        
        /* create and add requests */
        if (requestFlags.HasFlag(AqBankingToolRequest.Balance))
        {
            createAndAddRequest(banking, list, @as, TransactionCommand.GetBalance, fromDate, toDate, ignoreUnsupported);
        }

        if (requestFlags.HasFlag(AqBankingToolRequest.Statements))
        {
            createAndAddRequest(banking, list, @as, TransactionCommand.GetTransactions, fromDate, toDate, ignoreUnsupported);
        }

        if (requestFlags.HasFlag(AqBankingToolRequest.SepaSTO))
        {
            createAndAddRequest(banking, list, @as, TransactionCommand.SepaGetStandingOrders, fromDate, toDate, ignoreUnsupported);
        }

        if (requestFlags.HasFlag(AqBankingToolRequest.EStatements))
        {
            createAndAddRequest(banking, list, @as, TransactionCommand.GetEStatements, fromDate, toDate, ignoreUnsupported);
        }

        if (requestFlags.HasFlag(AqBankingToolRequest.Depot))
        {
            createAndAddRequest(banking, list, @as, TransactionCommand.GetDepot, fromDate, toDate, ignoreUnsupported);
        }
    }
}

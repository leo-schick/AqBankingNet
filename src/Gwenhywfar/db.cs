using System.Runtime.InteropServices;

namespace Gwenhywfar;

public abstract class DbNode
{
}

public class DbGroup : DbNode
{
    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_DB_Group_new", CharSet = CharSet.Ansi)]
    private static extern IntPtr GWEN_DB_Group_new([In, MarshalAs(UnmanagedType.LPStr)] string name);

    [DllImport("libgwenhywfar.so", EntryPoint = "GWEN_DB_Group_free", CharSet = CharSet.Ansi)]
    private static extern void GWEN_DB_Group_free(IntPtr n);

    private IntPtr _dbNode;

    public DbGroup(string name)
    {
        this._dbNode = GWEN_DB_Group_new(name);
    }

    ~DbGroup()
    {
        GWEN_DB_Group_free(this._dbNode);
    }
}

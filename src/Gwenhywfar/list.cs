using System.Runtime.InteropServices;
using System.Collections;

namespace Gwenhywfar;

internal static class GwenList
{
    #region DLL Imports

    /// <summary>
    /// Returns the size of this list, i.e. the number of elements in this
    /// list.
    ///
    /// This number is counted in the list metadata, so this is a cheap
    /// operation.
    /// </summary>
    [DllImport("libgwenhywfar.so")]
    public static extern uint GWEN_List_GetSize(IntPtr l);

    /// <summary>
    /// Return an iterator pointing to the first element in the list.
    /// </summary>
    [DllImport("libgwenhywfar.so")]
    public static extern IntPtr GWEN_List_First(IntPtr l);

    /// <summary>
    /// Returns an iterator pointing to the last element in the list.
    /// </summary>
    [DllImport("libgwenhywfar.so")]
    public static extern void GWEN_List_Last(IntPtr li);

    #endregion
}

internal static class GwenListEnumerator
{
    #region DLL Imports

    [DllImport("libgwenhywfar.so")]
    public static extern IntPtr GWEN_ListIterator_new(IntPtr l);

    [DllImport("libgwenhywfar.so")]
    public static extern void GWEN_ListIterator_free(IntPtr li);

    [DllImport("libgwenhywfar.so")]
    public static extern IntPtr GWEN_ListIterator_Data(IntPtr li);

    [DllImport("libgwenhywfar.so")]
    public static extern IntPtr GWEN_ListIterator_DataRefPtr(IntPtr li);

    #endregion
}

public abstract class GwenList<T> : IEnumerable<T>
{
    protected readonly IntPtr ListPtr;

    protected GwenList(IntPtr listPtr)
    {
        ListPtr = listPtr;
    }

    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    
    public static explicit operator IntPtr(GwenList<T> list) => list.ListPtr;
}

public abstract class GwenListEnumerator<T> : IEnumerator<T>
    where T : class
{
    protected readonly IntPtr ListPtr;
    private IntPtr _currentPtr;
    private T? _current;

    protected GwenListEnumerator(IntPtr listPtr)
    {
        ListPtr = listPtr;
    }

    public T Current
    {
        get
        {
            if (_currentPtr == default)
                throw new InvalidOperationException();
            return _current ??= NewInternal(_currentPtr) ?? throw new InvalidOperationException();
        }
    }

    object? IEnumerator.Current => _currentPtr == default ? null : _current ??= NewInternal(_currentPtr);

    public void Dispose()
    {
    }

    protected abstract IntPtr FirstInternal();
    protected abstract IntPtr NextInternal(IntPtr last);
    protected abstract T? NewInternal(IntPtr ptr);

    public bool MoveNext()
    {
        _currentPtr = _currentPtr == default
            ? FirstInternal()
            : NextInternal(_currentPtr);

        _current = null;
        return _currentPtr != default;
    }

    public void Reset()
    {
        _currentPtr = default;
        _current = null;
    }
}

using System.Collections;

namespace Gwenhywfar;

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

public abstract class GwenList2Enumerator<T> : IEnumerator<T>
    where T : class
{
    // ReSharper disable once NotAccessedField.Global
    protected readonly IntPtr ListPtr;
    private IntPtr? _iteratorPtr;
    private IntPtr? _currentPtr;
    private T? _current;

    protected GwenList2Enumerator(IntPtr listPtr)
    {
        ListPtr = listPtr;
    }

    ~GwenList2Enumerator()
    {
        FreeIterator();
    }

    public T Current
    {
        get
        {
            if (!_currentPtr.HasValue)
                throw new InvalidOperationException();
            return _current ??= NewInternal(_currentPtr.Value) ?? throw new InvalidOperationException();
        }
    }

    object? IEnumerator.Current => _currentPtr.HasValue ? _current ??= NewInternal(_currentPtr.Value) : null;

    public void Dispose()
    {
        FreeIterator();
    }

    private void FreeIterator()
    {
        if (_iteratorPtr != null)
        {
            FreeIteratorInternal(_iteratorPtr.Value);
            _iteratorPtr = null;
        }
    }

    protected abstract IntPtr FirstInternal();
    protected abstract IntPtr NextInternal(IntPtr last);
    protected abstract IntPtr GetCurrentInternal(IntPtr iterator);
    protected abstract T? NewInternal(IntPtr ptr);
    protected abstract void FreeIteratorInternal(IntPtr ptr);

    public bool MoveNext()
    {
        if (_iteratorPtr == null)
        {
            _iteratorPtr = FirstInternal();
            _currentPtr = GetCurrentInternal(_iteratorPtr.Value);
        }
        else
        {
            _currentPtr = NextInternal(_iteratorPtr.Value);
        }

        _current = null;
        return _currentPtr.HasValue && _currentPtr.Value != default;
    }

    public void Reset()
    {
        FreeIterator();
        _currentPtr = null;
        _current = null;
    }
}
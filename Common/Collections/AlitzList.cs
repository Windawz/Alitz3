using System;
using System.Collections;

namespace Alitz3.Collections;
public class AlitzList<T> : IList<T>, IReadOnlyList<T> {
    private const int DefaultCapacity = 4;

    public AlitzList() : this(DefaultCapacity) { }

    public AlitzList(int capacity) {
        _array = new T[capacity];
    }

    public ref T this[int index] =>
        ref GetSpanOverActualElements()[index];

    T IList<T>.this[int index] {
        get => this[index];
        set => this[index] = value;
    }

    T IReadOnlyList<T>.this[int index] =>
        this[index];

    private T[] _array;
    private int _count;

    public int Count => _count;
    public bool IsReadOnly => false;
    public int Capacity => _array.Length;

    public void Add(T item) {
        if (_count == _array.Length) {
            Grow();
        }
        _array[_count++] = item;
    }

    public void Clear() {
        _count = 0;
    }

    public bool Contains(T item) =>
        EnumerateActualElements().Contains(item);

    public void CopyTo(T[] array, int arrayIndex) {
        Array.Copy(
            sourceArray: _array,
            sourceIndex: 0,
            destinationArray: array,
            destinationIndex: arrayIndex,
            length: _count);
    }

    public IEnumerator<T> GetEnumerator() =>
        EnumerateActualElements().GetEnumerator();

    public int IndexOf(T item) =>
        Array.IndexOf(_array, item);

    public void Insert(int index, T item) {
        if (index < 0 || index > _count) {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        if (index == _count) {
            Add(item);
        }
        ShiftElementsRight(index, 1);
        _array[index] = item;
        _count++;
    }

    public bool Remove(T item) {
        int index = Array.IndexOf(_array, item);
        if (index == -1) {
            return false;
        }
        RemoveAt(index);
        return true;
    }

    public void RemoveAt(int index) {
        if (index >= _count || index < 0) {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        if (index < _count - 1) {
            ShiftElementsLeft(index + 1, 1);
        }
        _count--;
    }

    public Span<T> AsSpan() =>
        GetSpanOverActualElements();

    public void EnsureCapacity(int capacity) {
        if (capacity <= _array.Length) {
            return;
        }

        Array.Resize(ref _array, capacity);
    }

    public void Resize(int count, T item) {
        int oldCount = _count;
        ResizeNoFill(count);
        if (_count > oldCount) {
            Array.Fill(_array, item, oldCount, _count - oldCount);
        }
    }

    public void Resize(int count, Func<T> generator) {
        int oldCount = _count;
        ResizeNoFill(count);
        if (_count > oldCount) {
            foreach (ref T element in GetSpanOverActualElements()) {
                element = generator();
            }
        }
    }

    public void ShrinkToFit() {
        if (_array.Length > _count) {
            Array.Resize(ref _array, _count);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private Span<T> GetSpanOverActualElements() =>
        _array.AsSpan(0, _count);

    private IEnumerable<T> EnumerateActualElements() =>
        _array.Take(_count);

    private void Grow() {
        Array.Resize(ref _array, _array.Length * 2);
    }

    private void ShiftElementsRight(int start, int step) {
        if (_count + step > _array.Length) {
            Grow();
        }
        for (int i = _count - 1; i >= start; i--) {
            _array[i + step] = _array[i];
        }
    }

    private void ShiftElementsLeft(int start, int step) {
        for (int i = start; i < _count; i++) {
            _array[i - step] = _array[i];
        }
    }

    private void ResizeNoFill(int count) {
        if (count == _count) {
            return;
        } else if (count < _count) {
            _count = count;
        } else {
            if (count > _array.Length) {
                Array.Resize(ref _array, count);
            }
            _count = count;
        }
    }
}

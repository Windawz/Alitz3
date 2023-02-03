using System;
using System.Collections;
using System.Collections.Generic;

namespace Alitz3.Collections;
public class Matrix<T> : IEnumerable<T> {
    public Matrix(int width, int height) {
        Resize(width, height);
    }
    public Matrix(int width, int height, T value) : this(width, height) {
        Array.Fill(_elems, value);
    }

    public int Count => _elems.Length;
    public int Width => _width;
    public int Height => GetHeight();
    public IEnumerable<IEnumerable<T>> Rows {
        get {
            for (var i = 0; i < GetHeight(); i++) {
                yield return EnumerateRow(i);
            }
        }
    }
    public IEnumerable<IEnumerable<T>> Columns {
        get {
            for (var i = 0; i < _width; i++) {
                yield return EnumerateColumn(i);
            }
        }
    }

    public T this[int x, int y] {
        get => _elems[GetElementIndex(x, y)];
        set => _elems[GetElementIndex(x, y)] = value;
    }

    public IEnumerator<T> GetEnumerator() {
        return ((IEnumerable<T>)_elems).GetEnumerator();
    }
    public void Resize(int width, int height) {
        var length = width * height;
        _elems = new T[length];
        _width = width;
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return _elems.GetEnumerator();
    }
    private int GetElementIndex(int x, int y) {
        return y * _width + x;
    }
    private int GetHeight() {
        return _elems.Length / _width;
    }
    private IEnumerable<T> EnumerateRow(int index) {
        for (var i = 0; i < _width; i++) {
            yield return _elems[GetElementIndex(i, index)];
        }
    }
    private IEnumerable<T> EnumerateColumn(int index) {
        for (var i = 0; i < GetHeight(); i++) {
            yield return _elems[GetElementIndex(index, i)];
        }
    }

    private T[] _elems = null!;
    private int _width;
}

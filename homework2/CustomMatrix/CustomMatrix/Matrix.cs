using System.Text;
using System.Text.RegularExpressions;

namespace CustomMatrix;

public class Matrix
{
    private readonly int _rows;
    private readonly int _cols;
    private readonly double[][] _data = Array.Empty<double[]>();
    
    public Matrix(int nRows, int nCols)
    {
        if (nRows <= 0 || nCols <= 0) 
            return;
        _rows = nRows;
        _cols = nCols;
        _data = new double[_rows][];
        for (int i = 0; i < _rows; i++)
            _data[i] = new double[_cols];
    }

    public Matrix(double[,] initData)
    {
        _rows = initData.GetLength(0);
        _cols = initData.GetLength(1);
        _data = new double[_rows][];
        for (int i = 0; i < _rows; i++)
        {
            _data[i] = new double[_cols];
            for (int j = 0; j < _cols; j++)
                _data[i][j] = initData[i, j];
        }
    }

    public Matrix(double[][] initData)
    {
        _rows = initData.Length;
        _cols = initData[0].Length;
        _data = new double[_rows][];
        for (int i = 0; i < _rows; i++)
        {
            _data[i] = new double[_cols];
            for (int j = 0; j < _cols; j++) 
                _data[i][j] = initData[i][j];
        }
    }
    
    public double? this[int i, int j]
    {
        get
        {
            if (i >= 0 && i < _rows 
                       && j >= 0 && j < _cols)
                return _data[i][j];
            return null;
        }
        set
        {
            if (i >= 0 && i < _rows 
                       && j >= 0 && j < _cols 
                       && value.HasValue)
                _data[i][j] = (double) value;
        }
    }

    public int Rows => _rows;

    public int Cols => _cols;
    
    public bool IsSquared() => _rows == _cols && _rows != 0;

    public bool IsEmpty()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                if (_data[i][j] != 0d)
                    return false;
            }
        }
        return true;
    }

    public bool IsUnity()
    {
        if (_rows != _cols)
            return false;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                if (i == j && Math.Abs(_data[i][j] - 1d) > 0.000001)
                    return false;
                if (i != j && _data[i][j] != 0d)
                    return false;
            }
        }
        return true;
    }

    public bool IsSymmetric()
    {
        if (_rows != _cols)
            return false;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = i; j < _cols; j++)
            {
                if (Math.Abs(_data[i][j] - _data[j][i]) > 0.000001)
                    return false;
            }
        }
        return true;
    }
    
    public static Matrix operator *(Matrix m1, Matrix m2)
    {
        if (m1._rows != m2._rows || m1._cols != m2._cols)
            return m1;
        Matrix result = new Matrix(m1._rows, m1._cols);
        for (int i = 0; i < result._rows; i++)
        {
            for (int j = 0; j < result._cols; j++)
                result[i, j] = m1[i, j] * m2[i, j];
        }
        return result;
    }

    public static explicit operator Matrix(double[,] arr)
    {
        Matrix result = new Matrix(arr.GetLength(0), arr.GetLength(1));
        for (int i = 0; i < result._rows; i++)
        {
            for (int j = 0; j < result._cols; j++)
                result[i, j] = arr[i, j];
        }
        return result;
    }

    public Matrix Transpose()
    {
        Matrix transpose = new Matrix(_cols, _rows);
        for (int i = 0; i < transpose._rows; i++)
        {
            for (int j = 0; j < transpose._cols; j++)
                transpose[i, j] = this[j, i];
        }
        return transpose;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
                sb.Append($"{_data[i][j]} ");
            sb.Append("\n");
        }
        return sb.ToString();
    }

    public static Matrix GetUnity(int size)
    {
        if (size <= 0)
            return new Matrix(0, 0);
        Matrix unityMatrix = new Matrix(size, size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == j)
                    unityMatrix[i, j] = 1d;
                else
                    unityMatrix[i, j] = 0d;
            }
        }
        return unityMatrix;
    }
    
    public static Matrix GetEmpty(int size)
    {
        if (size <= 0)
            return new Matrix(0, 0);
        Matrix emptyMatrix = new Matrix(size, size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                emptyMatrix[i, j] = 0d;
        }
        return emptyMatrix;
    }

    public static bool TryParse(string s, out Matrix m)
    {
        string sFormatted = Regex.Replace(s, @"\s+", " ");
        string[] sRows = sFormatted.Split(',');
        int rows = sRows.Length;
        int cols = sRows[0].Trim().Split(' ').Length;
        m = new Matrix(rows, cols);
        for (int i = 0; i < rows; i++)
        {
            string[] sCols = sRows[i].Trim().Split(' ');
            if (i > 0 && cols != sCols.Length)
                throw new FormatException();
            for (int j = 0; j < cols; j++)
                m[i, j] = Convert.ToDouble(sCols[j]);
        }
        return true;
    }
}
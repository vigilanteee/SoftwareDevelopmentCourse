namespace CustomMatrix.Tests;

[TestClass]
public class MatrixTests
{
    private readonly string _s1 = "1 2, 3 4, 5 6";
    private readonly string _s2 = "  1    3 5,  2   4   6   ";
    private readonly string _s3 = " 1 2  3  4, 1 2, 1 2 3"; 
    
    private readonly Matrix _emptyMatrix5X5 = Matrix.GetEmpty(5);
    private readonly Matrix _unityMatrix5X5 = Matrix.GetUnity(5);

    private readonly double[,] _randArr1 = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
    private readonly double[][] _randArr2 = {
        new double[] { 1, 3, 5 },
        new double[] { 2, 4, 6 }
    };
    private readonly double[,] _randArr3 = { { 1, -1 }, { 2, -2 }, { -3, 0 } };

    private readonly double[,] _symmetricArr = { { 1, 3, 0 }, { 3, 2, 6 }, { 0, 6, 5 } };
    private readonly double[,] _emptyArr = { { 0, 0 }, { 0, 0 }, { 0, 0 } };

    [TestMethod]
    public void ConstructorsTest()
    {
        Assert.AreEqual(_emptyMatrix5X5.ToString(), new Matrix(5, 5).ToString());
        Assert.AreEqual(new Matrix(3, 2).ToString(), new Matrix(_emptyArr).ToString());
        Assert.AreEqual(new Matrix(0, 0).ToString(), String.Empty);
        Assert.AreEqual(new Matrix(-2, 3).ToString(), String.Empty);
        Assert.AreEqual(new Matrix(_randArr1).ToString(), "1 2 \n3 4 \n5 6 \n");
        Assert.AreEqual(new Matrix(_randArr2).ToString(), "1 3 5 \n2 4 6 \n");
    }
    
    [TestMethod]
    public void PropertiesTest()
    {
        Assert.AreEqual(new Matrix(0, 0).Rows, 0);
        Assert.AreEqual(new Matrix(0, 0).Cols, 0);
        Assert.AreEqual(new Matrix(-2, 3).Rows, 0);
        Assert.AreEqual(new Matrix(-3, 2).Cols, 0);
        Assert.AreEqual(new Matrix(_randArr1).Rows, _randArr1.GetLength(0));
        Assert.AreEqual(new Matrix(_randArr1).Cols, _randArr1.GetLength(1));
        Assert.AreEqual(new Matrix(_randArr2).Rows, _randArr2.Length);
        Assert.AreEqual(new Matrix(_randArr2).Cols, _randArr2[0].Length);
    }

    [TestMethod]
    public void BooleanPropertiesTest()
    {
        Assert.IsTrue(_emptyMatrix5X5.IsSquared());
        Assert.IsFalse(new Matrix(_randArr1).IsSquared());
        Assert.IsTrue(_emptyMatrix5X5.IsEmpty());
        Assert.IsFalse(new Matrix(_randArr2).IsEmpty());
        Assert.IsTrue(_unityMatrix5X5.IsUnity());
        Assert.IsFalse(new Matrix(_emptyArr).IsUnity());
        Assert.IsTrue(new Matrix(_symmetricArr).IsSymmetric());
        Assert.IsFalse(new Matrix(_randArr1).IsSymmetric());
    }

    [TestMethod]
    public void OperatorsTest()
    {
        double[,] arr = new double[_randArr1.GetLength(0), _randArr1.GetLength(1)];
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
                arr[i, j] = _randArr1[i, j] * _randArr3[i, j];
        }

        Matrix m1 = new Matrix(_randArr1);
        Matrix m2 = new Matrix(_randArr3);
        Matrix m3 = m1 * m2;
        Assert.AreEqual(m3.ToString(), new Matrix(arr).ToString());
        Assert.AreEqual(m3[0, 1], new Matrix(arr)[0, 1]);
        Assert.AreEqual(m3[2, 2], new Matrix(arr)[2, 2]);
        
        Matrix m4 = new Matrix(_randArr2);
        m3 = m1 * m4;
        Assert.AreNotEqual(m3.ToString(), new Matrix(arr).ToString());

        m3 = (Matrix) _randArr1;
        Assert.IsInstanceOfType(m3, typeof(Matrix));
        Assert.AreEqual(m3.ToString(), "1 2 \n3 4 \n5 6 \n");
    }

    [TestMethod]
    public void MethodsTest()
    {
        Assert.AreEqual(new Matrix(_randArr1).Transpose().ToString(), new Matrix(_randArr2).ToString());
        Assert.AreEqual(new Matrix(_randArr2).Transpose().ToString(), new Matrix(_randArr1).ToString());
        Assert.AreEqual(_emptyMatrix5X5.Transpose().ToString(), _emptyMatrix5X5.ToString());
        Assert.AreEqual(_unityMatrix5X5.Transpose().ToString(), _unityMatrix5X5.ToString());
        Assert.AreNotEqual(new Matrix(_randArr2).Transpose().ToString(), new Matrix(_randArr3).ToString());
    }

    [TestMethod]
    public void StaticMethodsTest()
    {
        Assert.AreEqual(Matrix.GetEmpty(5).ToString(), _emptyMatrix5X5.ToString());
        Assert.AreEqual(Matrix.GetUnity(5).ToString(), _unityMatrix5X5.ToString());
        Assert.AreEqual(Matrix.GetEmpty(0).ToString(), String.Empty);

        Matrix temp = new Matrix(0, 0);
        Assert.IsTrue(Matrix.TryParse(_s1, out temp));
        Assert.IsTrue(Matrix.TryParse(_s2, out temp));
        Assert.ThrowsException<FormatException>(() => Matrix.TryParse(_s3, out temp));
    }
}
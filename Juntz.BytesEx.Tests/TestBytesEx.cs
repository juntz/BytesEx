namespace Juntz.BytesEx.Tests;

public class TestBytesEx
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void Not_GiveRandomBytes_SameAsBitArrayResult(
        [Values(0, 1, 2)] int length)
    {
        AssertBinaryOperationUsingBitArrayAndRandomBytes(
            length,
            (a, _) => a.Not(),
            (a, _) => a.Not()
            );
    }

    [Test]
    public void And_GiveRandomBytes_SameAsBitArrayResult(
        [Values(0, 1, 2)] int length)
    {
        AssertBinaryOperationUsingBitArrayAndRandomBytes(
            length,
            (a, b) => a.And(b),
            (a, b) => a.And(b)
            );
    }
    
    [Test]
    public void Or_GiveRandomBytes_SameAsBitArrayResult(
        [Values(0, 1, 2)] int length)
    {
        AssertBinaryOperationUsingBitArrayAndRandomBytes(
            length,
            (a, b) => a.Or(b),
            (a, b) => a.Or(b)
            );
    }
    
    [Test]
    public void Xor_GiveRandomBytes_SameAsBitArrayResult(
        [Values(0, 1, 2)] int length)
    {
        AssertBinaryOperationUsingBitArrayAndRandomBytes(
            length,
            (a, b) => a.Xor(b),
            (a, b) => a.Xor(b)
            );
    }

    [Test]
    public void RightShift_GiveRandomBytes_SameAsBitArrayResult(
        [Values(0, 1, 2)] int length, [Values(0, 1, 8, 9)] int count)
    {
        AssertBinaryOperationUsingBitArrayAndRandomBytes(
            length,
            (a, _) => a.RightShift(count),
            (a, _) => a.RightShift(count)
            );
    }

    [Test]
    public void LeftShift_GiveRandomBytes_SameAsBitArrayResult(
        [Values(0, 1, 2)] int length, [Values(0, 1, 8, 9)] int count)
    {
        AssertBinaryOperationUsingBitArrayAndRandomBytes(
            length,
            (a, _) => a.LeftShift(count),
            (a, _) => a.LeftShift(count)
            );
    }

    [Test]
    public void ExtractByte_GiveSample_SameAsExpected()
    {
        var actual = BytesEx.ExtractByte([7, 1], 1);
        var expected = 131;
        Assert.That(actual, Is.EqualTo(expected));
    }

    private static void AssertBinaryOperationUsingBitArrayAndRandomBytes(
        int bytesLength,
        Action<byte[], byte[]> byteOper,
        Action<BitArray, BitArray> bitsOper)
    {
        var a = GetRandomBytes(bytesLength);
        var b = GetRandomBytes(bytesLength);

        var actual = (byte[])a.Clone();
        byteOper(actual, b);

        var expected = BitArrayOperation(bitsOper, a, b);
        Assert.That(actual, Is.EqualTo(expected));
    }

    private static byte[] GetRandomBytes(int length)
    {
        var bytes = new byte[length];

        Random.Shared.NextBytes(bytes);
        return bytes;
    }

    private static byte[] BitArrayOperation(
        Action<BitArray, BitArray> oper, byte[] a, byte[] b)
    {
        var length = Math.Max(a.Length, b.Length);
        var result = new byte[length];

        var aA = new BitArray(a);
        var bA = new BitArray(b);

        oper(aA, bA);

        aA.CopyTo(result, 0);
        return result;
    }
}
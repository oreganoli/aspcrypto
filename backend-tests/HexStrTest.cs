using Xunit;
using backend.Common;

public class HexStrTest
{
    [Fact]
    public void WellFormedHexStr()
    {
        var input = "FF-00-0A";

        var output = HexStr.ToBytes(input);

        Assert.Equal(255, output[0]);
        Assert.Equal(0, output[1]);
        Assert.Equal(10, output[2]);
    }
    [Fact]
    public void NastyHexStr()
    {
        var input = "FE -FE-fe  _ąćpo";
        var output = HexStr.ToBytes(input);

        Assert.Equal(254, output[0]);
        Assert.Equal(254, output[1]);
        Assert.Equal(254, output[2]);
    }
}
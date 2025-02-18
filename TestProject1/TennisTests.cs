using ConsoleApp1;

namespace TestProject1;

public class TennisTests
{
    private TennisBox _tennisBox;
    
    [SetUp]
    public void Setup()
    {
        _tennisBox = new TennisBox("Alex", "Bert");
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
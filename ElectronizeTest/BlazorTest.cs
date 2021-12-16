using Bunit;
using NUnit.Framework;
using Electronize.Pages;

namespace ElectronizeTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        using var ctx = new Bunit.TestContext();
        var cut = ctx.RenderComponent<Counter>();

        var button = cut.Find("button");
        var counter = cut.Find("p");
        counter.MarkupMatches("<p>Current count: 0</p>");
        
        button.Click();
        counter.MarkupMatches("<p>Current count: 42</p>");
    }
}
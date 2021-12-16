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
    public void TestIncrease()
    {
        using var ctx = new Bunit.TestContext();
        var cut = ctx.RenderComponent<Counter>();

        var button = cut.Find("button#incBtn");
        var counter = cut.Find("p");
        counter.MarkupMatches("<p>Current count: 0</p>");
        
        button.Click();
        counter.MarkupMatches("<p>Current count: 42</p>");
        button.Click();
        counter.MarkupMatches("<p>Current count: 84</p>");
    }

    [Test]
    public void TestDecrease()
    {
        using var ctx = new Bunit.TestContext();
        var cut = ctx.RenderComponent<Counter>();
        
        var button = cut.Find("button#decBtn");
        var counter = cut.Find("p");
        counter.MarkupMatches("<p>Current count: 0</p>");
        
        button.Click();
        counter.MarkupMatches("<p>Current count: -1</p>");
        button.Click();
        counter.MarkupMatches("<p>Current count: -4</p>");
    }
}
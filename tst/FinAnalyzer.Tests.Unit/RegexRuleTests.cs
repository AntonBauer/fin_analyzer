using System.Text.RegularExpressions;
using FinAnalyzer.Domain.Models;
using FinAnalyzer.Domain.Rules;

namespace FinAnalyzer.Tests.Unit;

[TestFixture]
internal sealed partial class RegexRuleTests
{
    [TestCaseSource(nameof(TestCases))]
    public void TestRegexRule(RegexRule rule, Transaction transaction, Category? expectedCategory)
    {
        // Act
        var suggestion = rule.ApplyTo(transaction);

        // Assert
        if (expectedCategory is null)
            Assert.That(suggestion, Is.Null);
        else
            Assert.That(suggestion!.SuggestedCategory, Is.EqualTo(expectedCategory));
    }

    private static IEnumerable<TestCaseData> TestCases()
    {
        var transaction_01 = new Transaction
        {
            Id = Guid.NewGuid(),
            RawTransaction = new RawTransaction
            {
                Id = Guid.NewGuid(),
                Booking = DateOnly.FromDateTime(DateTime.Now),
                ValueDate = DateOnly.FromDateTime(DateTime.Now),
                PayerOrPayee = "Test Payer",
                BookingText = "Test Booking Text",
                Purpose = "Test Purpose",
                Balance = new Currency(10, CurrencyName.Euro),
                Amount = new Currency(20, CurrencyName.Euro),
            },
        };

        var expectedCategory = new Category
        {
            Id = 1,
            Name = "Test category"
        };

        var rule = new RegexRule
        {
            Id = 1,
            Name = "Test rule",
            PropertyToCheck = TransactionProperty.BookingText,
            Expression = TestRegex(),
            SuggestedCategory = expectedCategory
        };

        yield return new TestCaseData(rule, transaction_01, expectedCategory)
            .SetName("Test matching booking text");

        var transaction_02 = new Transaction
        {
            Id = Guid.NewGuid(),
            RawTransaction = new RawTransaction
            {
                Id = Guid.NewGuid(),
                Booking = DateOnly.FromDateTime(DateTime.Now),
                ValueDate = DateOnly.FromDateTime(DateTime.Now),
                PayerOrPayee = "Test Payer",
                BookingText = "Nothing to look for in Booking Text",
                Purpose = "Test Purpose",
                Balance = new Currency(10, CurrencyName.Euro),
                Amount = new Currency(20, CurrencyName.Euro),
            },
        };

        yield return new TestCaseData(rule, transaction_02, null)
            .SetName("Test not matching booking text");
    }

    [GeneratedRegex(@"\bTest\b")]
    private static partial Regex TestRegex();
}

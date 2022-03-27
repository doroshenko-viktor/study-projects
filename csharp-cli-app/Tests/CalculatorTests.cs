using Xunit;
using Business;
using FluentAssertions;

namespace Tests;

public class CalculatorTests
{
    [Theory]
    [InlineData(2, 1, 3)]
    public void ShouldAddTwoValues__WhenNoOverflow(int x, int y, int expectedResult)
    {
        // arrange
        var calculator = new Calculator();
        // act
        var result = calculator.Add(x, y);
        // assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(2, 1, 1)]
    public void ShouldSubtractTwoValues__WhenNoOverflow(int x, int y, int expectedResult)
    {
        // arrange
        var calculator = new Calculator();
        // act
        var result = calculator.Subtract(x, y);
        // assert
        result.Should().Be(expectedResult);
    }
}
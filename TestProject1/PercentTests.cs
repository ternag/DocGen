using ConsoleApp1;
using FluentAssertions;

namespace TestProject1
{
    public class PercentTests
    {
        [Fact]
        public void ImplicitCastIntegerToPercent()
        {
            Percent p = 10;
            Assert.True(p==10);
        }

        [Fact]
        public void ImplicitCastPercentToInteger()
        {
            Percent p = 10;
            int q = p;
            Assert.True(q==10);
        }

        [Fact]
        public void ImplicitCastPercentToInteger2()
        {
            Percent p = 25;
            double p1 = p / 100.0;
            p1.Should().Be(0.25);
        }

        [Fact]
        public void PercentIsEquatable()
        {
            Percent p1 = 10;
            Percent p2 = 20;
            Percent p3 = 20;

            Assert.True(p1 < p2);
            Assert.True(p2 == p3);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void GivenInvalidValues_ThrowArgumentException(int invalidValue)
        {
            Assert.Throws<OverflowException>(() =>
            {
                Percent p = invalidValue;
            });
        }
    }
}
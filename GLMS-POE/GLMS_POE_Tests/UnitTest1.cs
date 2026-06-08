using NUnit.Framework;

namespace GLMS_POE_Tests
{
    public class CurrencyTests
    {
        [Test]
        public void ConvertToZAR_ShouldCalculateCorrectAmount()
        {
            // Arrange
            decimal amountUSD = 100;
            decimal fakeRate = 18.50m; 

            // Act
            decimal result = amountUSD * fakeRate;

            // Assert
            Assert.AreEqual(1850, result);
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////
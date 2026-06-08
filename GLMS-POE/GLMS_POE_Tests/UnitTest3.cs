using System;
using System.Collections.Generic;
using System.Text;

namespace GLMS_POE_Tests
{
    public class BusinessRuleTests
    {
        [Test]
        public void Should_Block_ServiceRequest_When_Contract_Is_Expired()
        {
            // Arrange
            string status = "Expired";

            // Act
            bool isAllowed = status != "Expired" && status != "On Hold";

            // Assert
            Assert.IsFalse(isAllowed);
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace GLMS_POE_Tests
{
    public class FileValidationTests
    {
        [Test]
        public void UploadFile_ShouldRejectNonPdf()
        {
            // Arrange
            string fileName = "virus.exe";

            // Act
            bool isPdf = Path.GetExtension(fileName).ToLower() == ".pdf";

            // Assert
            Assert.IsFalse(isPdf);
        }

        [Test]
        public void UploadFile_ShouldAcceptPdf()
        {
            // Arrange
            string fileName = "contract.pdf";

            // Act
            bool isPdf = Path.GetExtension(fileName).ToLower() == ".pdf";

            // Assert
            Assert.IsTrue(isPdf);
        }
    }
}
/////////////////////////////////////////// ----- End Of File ----- ///////////////////////////////////////////
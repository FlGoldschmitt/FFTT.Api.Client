using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FG.FFTT.Api.Client.UnitTests
{
    [TestClass]
    public class FfttCredentialsTests
    {
        FfttCredentials _credentials = new FfttCredentials("ID", "FFTT");

        [TestMethod]
        public void GetHashKeyFromPassword()
        {
            var hashPassword = _credentials.GetHashKeyFromPassword();

            Assert.AreEqual(hashPassword, "fbcbfc973284726ab5a53ac296927cc6");
        }

        [TestMethod]
        public void EncodeTimestampToSha1()
        {
            string timestampEncoded = _credentials.EncodeTimestampToSha1("20150611140022081");

            Assert.AreEqual(timestampEncoded, "517b27013dd619db47f2bf4c50ae504acbb33980");
        }

        [TestMethod]
        public void GenerateSerialNumber()
        {
            string serialNumber = _credentials.GenerateSerialNumber(15);

            Assert.AreEqual(serialNumber.Length, 15);
        }
    }
}
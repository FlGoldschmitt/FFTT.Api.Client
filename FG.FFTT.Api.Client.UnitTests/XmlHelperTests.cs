using FG.FFTT.Api.Client.Models;
using FG.FFTT.Api.Client.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FG.FFTT.Api.Client.UnitTests
{
    [TestClass]
    public class XmlHelperTests
    {
        [TestMethod]
        public void FromXml()
        {
            string xml =
            "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
            "<liste xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
              "<club>" +
                "<idclub>12781094</idclub>" +
                "<numero>08781094</numero>" +
                "<nom>MONTFORT TT US</nom>" +
                "<validation>01/07/2021</validation>" +
                "<typeclub>L</typeclub>" +
              "</club>" +
            "</liste>";

            var clubResponse = xml.FromXml<ClubsResponse>();

            Assert.AreEqual(clubResponse.Clubs[0].Id, 12781094);
            Assert.AreEqual(clubResponse.Clubs[0].Number, "08781094");
            Assert.AreEqual(clubResponse.Clubs[0].Name, "MONTFORT TT US");
        }

        [TestMethod]
        public void ToXml()
        {
            // Create an club response object model
            ClubsResponse response = new ClubsResponse();
            response.Clubs = new Club[] { 
                new Club()
                {
                    Id = 12781094,
                    Name = "MONTFORT TT US",
                    Number = "08781094",
                    ValidationDate = new System.DateTime(2021, 07, 01).ToString("dd/MM/yyyy"),
                    Type = "L"
                } 
            };

            Assert.IsNotNull(XmlHelper.ToXml(response));
        }
    }
}
using addressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace JSONServerTest
{
    public class Contact
    {
        public int ID
        { get; set; }
        public string Name
        { get; set; }

        public string Address
        { get; set; }

        public string City
        { get; set; }

        public string State
        { get; set; }

        public int Zip
        { get; set; }

        public string Phone
        { get; set; }

        public string Email
        { get; set; }

    }
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse getContactList()
        {
            RestRequest request = new RestRequest("/contacts", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void OnClaiingGETApi_ReturnContactList()
        {
            IRestResponse response = getContactList();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Contact> dataresponse = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(3, dataresponse.Count);
            foreach (var item in dataresponse)
                System.Console.WriteLine("ID: "+item.ID+" Name: "+item.Name+" Address: "+item.Address+" City: "+item.City+" State: "+item.State+" Zip: "+item.Zip+" Phone: "+item.Phone+" Email: "+item.Email );

        }
    }
}

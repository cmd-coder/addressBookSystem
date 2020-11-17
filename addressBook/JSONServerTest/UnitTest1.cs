using addressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [TestMethod]
        public void GivenMultipleContacts_OnPost_ShouldReturnCountOfContacts()
        {
            RestRequest request = new RestRequest("/contacts", Method.POST);
            JObject[] jObjectbody = new JObject[2];
            JObject obj = new JObject();
            obj.Add("Name", "Name new");
            obj.Add("Address", "Address new");
            obj.Add("City","City new");
            obj.Add("State","State new");
            obj.Add("Zip",455612);
            obj.Add("Phone","7889564512");
            obj.Add("Email","newemail@email.com");
            jObjectbody[0] = obj;
            obj = new JObject();
            obj.Add("Name", "Name new 2");
            obj.Add("Address", "Address new 2");
            obj.Add("City", "City new 2");
            obj.Add("State", "State new 2");
            obj.Add("Zip", 455615);
            obj.Add("Phone", "7889564512");
            obj.Add("Email", "newemail2@email.com");
            jObjectbody[1] = obj;
            for (int i = 0; i < 2; i++)
            {
                request.AddParameter("application/json", jObjectbody[i], ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
                Contact dataResponse = JsonConvert.DeserializeObject<Contact>(response.Content);
                if (i == 0)
                {
                    Assert.AreEqual("Name new", dataResponse.Name);
                    Assert.AreEqual("Address new", dataResponse.Address);
                }
            }
        }

        [TestMethod]
        public void GivenEmployee_OnPatch_ShouldReturnUpdatedEmployee()
        {
            RestRequest request = new RestRequest("/contacts/2", Method.PATCH);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("Name", "Coder Bhai");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Contact dataResponse = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual("Coder Bhai", dataResponse.Name);
        }
    }
}

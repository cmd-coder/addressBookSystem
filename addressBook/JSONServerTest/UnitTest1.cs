using addressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace JSONServerTest
{
    /// <summary>
    /// The class is written to store the contacts of a person
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// A property to store the id of a contact
        /// </summary>
        public int ID
        { get; set; }
        /// <summary>
        /// A property to store the name of a contact
        /// </summary>
        public string Name
        { get; set; }
        /// <summary>
        /// A property to store the address of a contact
        /// </summary>
        public string Address
        { get; set; }
        /// <summary>
        /// A property to store the city of a contact
        /// </summary>
        public string City
        { get; set; }
        /// <summary>
        /// A property to store the state of a contact
        /// </summary>
        public string State
        { get; set; }
        /// <summary>
        /// A property to store the zip of a contact
        /// </summary>
        public int Zip
        { get; set; }
        /// <summary>
        /// A property to store the phone of a contact
        /// </summary>
        public string Phone
        { get; set; }
        /// <summary>
        /// A property to store the email of a contact
        /// </summary>
        public string Email
        { get; set; }

    }

    /// <summary>
    /// A class to test the address book json server
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        /// <summary>
        /// A method to initialize the restclient object with the json server address
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }

        /// <summary>
        /// This function is used to request the json server to send the data stored
        /// </summary>
        /// <returns>An IRestResponse object that contains the response sent by the json server</returns>
        private IRestResponse getContactList()
        {
            RestRequest request = new RestRequest("/contacts", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// This test method gets the data stored in the server and prints the output
        /// </summary>
        [TestMethod]
        public void OnClaiingGETApi_ReturnContactList()
        {
            IRestResponse response = getContactList();
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Contact> dataresponse = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(5, dataresponse.Count);
            foreach (var item in dataresponse)
                System.Console.WriteLine("ID: " + item.ID + " Name: " + item.Name + " Address: " + item.Address + " City: " + item.City + " State: " + item.State + " Zip: " + item.Zip + " Phone: " + item.Phone + " Email: " + item.Email);
        }

        /// <summary>
        /// This test method "POST's" the data into the json server
        /// </summary>
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

        /// <summary>
        /// This test method updates a contact's name based on the id
        /// </summary>
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPB1Test.RestGetEndPoint
{
    [TestClass]
    public class TestGetEndpointSAP
    {
        private string endpoint = "https://srvsapb1w.cainco.org.bo:50000/b1s/v1/Login";

        [TestMethod]
        public void TestSAPConnector()
        {
            //RestClient client = new RestClient();
            //RestRequest request = new RestRequest();

            var client = new RestClient("https://srvsapb1w.cainco.org.bo:50000/b1s/v1/Login");
            //client..Timeout = -1;
            var request = new RestRequest();
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Cookie", "B1SESSION=1a8f00a8-9533-11ed-8000-30e171523d78; ROUTEID=.node1");
            var body = @"{""UserName"":""manager1"", ""Password"":""Pruebas88"",""CompanyDB"":""CRM_PRUEBAS_ABRIL""}";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

        }
    }
}

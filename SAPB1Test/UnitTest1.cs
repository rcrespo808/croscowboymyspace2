using RestSharp;

namespace SAPB1Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);

            var client2 = new RestClient("https://srvsapb1w.cainco.org.bo:50000/b1s/v1/Login");
            //client..Timeout = -1;
            var request = new RestRequest();
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Cookie", "B1SESSION=1a8f00a8-9533-11ed-8000-30e171523d78; ROUTEID=.node1");
            var body = @"{""UserName"":""manager1"", ""Password"":""Pruebas88"",""CompanyDB"":""CRM_PRUEBAS_ABRIL""}";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            RestResponse response = client2.Execute(request, Method.Post);
            Console.WriteLine(response.Content);
        }
    }
}
using B1SLayer;
using SAPB1.Sap_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPB1.Integrations
{
    public class Integration
    {

        public async Task<SLConnection> LoginAsync()
        {
            var serviceLayer = new SLConnection("https://srvsapb1w.cainco.org.bo:50000/b1s/v1/", "CRM_PRUEBAS_ABRIL", "manager1", "Pruebas88");
            var resutl =  await serviceLayer.LoginAsync();
            return serviceLayer;
        }

        public async Task<BusinessPartners> GetBusinessParthner(string cardCode)
        {
            SLConnection serviceLayer = await this.LoginAsync();
            var bp = await serviceLayer.Request("BusinessPartners", cardCode).GetAsync<BusinessPartners>();
            return bp;
        }

        public async Task<List<BusinessPartners>> GetBusinessParthnerList()
        {
            SLConnection serviceLayer = await this.LoginAsync();
            var bpList = await serviceLayer.Request("BusinessPartners")
            .Select("CardCode, CardName, CardType, ContactPerson, EmailAddress, Fax, Cellular, Phone1, Website, Notes")
            .WithPageSize(100)
            .GetAsync<List<BusinessPartners>>();

            return bpList;  
        }

        public async void UpdateBusinessParthner(BusinessPartners bp)
        {
            SLConnection serviceLayer = await this.LoginAsync();
            // Performs a PATCH on /BusinessPartners('C00001'), updating the CardName of the Business Partner
            if (!string.IsNullOrEmpty(bp.CardCode))
            {
                await serviceLayer.Request("BusinessPartners", bp.CardCode).PatchAsync(new { CardName = bp.CardName, EmailAddress = bp.EmailAddress, Fax = bp.Fax, Cellular = bp.Cellular, Phone1 = bp.Phone1, Website = bp.Website, Notes = bp.Notes });
                //await serviceLayer.Request("BusinessPartners", bp.CardCode).PatchAsync(new { CardName = bp.CardName, ContactPerson = bp.ContactPerson, EmailAddress = bp.EmailAddress, Fax = bp.Fax, Cellular = bp.Cellular, Phone1 = bp.Phone1, Website = bp.Website, Notes = bp.Notes });
            }
        }


        public async Task<BusinessPartners> CreateBusinessParthner(BusinessPartners bp)
        {
            SLConnection serviceLayer = await this.LoginAsync();

            // creating a new order and deserializing the created order in a custom model class
            var businessPartners = await serviceLayer.Request("BusinessPartners").PostAsync<BusinessPartners>(new { CardName = bp.CardName, ContactPerson = bp.ContactPerson, EmailAddress = bp.EmailAddress, Fax = bp.Fax, Cellular = bp.Cellular, Phone1 = bp.Phone1, Website = bp.Website, Notes = bp.Notes });

            return businessPartners;

            // Performs a POST on /Orders with the provided object as the JSON body, 
            // creating a new order and deserializing the created order in a custom model class
            //string cardCode, supplierName, cardType, contactPerson, email, fax, mobileNo, phoneNo, website, description;

            //var bp = new BusinessPartners();
            //bp.CardName = supplierName;
            //bp.CardType = cardType;
            //bp.ContactPerson = contactPerson;
            //bp.MailAddress = email;
            //bp.Phone1 = phoneNo;
            //bp.Fax = fax;
            //bp.Cellular = mobileNo;
            //bp.Website = website;
            //bp.Notes = description;
            //await serviceLayer.Request("BusinessPartners").PostAsync<BusinessPartners>(bp);

            // Performs a PATCH on /BusinessPartners('C00001'), updating the CardName of the Business Partner

            // Batch requests! Performs multiple operations in SAP in a single HTTP request
            //var req1 = new SLBatchRequest(
            //    HttpMethod.Post, // HTTP method
            //    "BusinessPartners", // resource
            //    new { CardCode = "C00001", CardName = "I'm a new BP" }); // object to be sent as the JSON body
            //    //.WithReturnNoContent(); // Adds the header "Prefer: return-no-content" to the request

            //var req2 = new SLBatchRequest(HttpMethod.Patch,
            //    "BusinessPartners('C00001')",
            //    new { CardName = "This is my updated name" });

            ////var req3 = new SLBatchRequest(HttpMethod.Delete, "BusinessPartners('C00001')");

            //HttpResponseMessage[] batchResult = await serviceLayer.PostBatchAsync(req1, req2);

            //return bp;

        }













    }
}

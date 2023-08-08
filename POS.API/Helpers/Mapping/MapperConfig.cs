using AutoMapper;
using Microsoft.Extensions.Configuration;
using POS.Helper;

namespace POS.API.Helpers.Mapping
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs(PathHelper pathHelper)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ActionProfile());
                mc.AddProfile(new PageProfile());
                mc.AddProfile(new RoleProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new NLogProfile());
                mc.AddProfile(new EmailTemplateProfile());
                mc.AddProfile(new EmailProfile());
                mc.AddProfile(new CountryProfile());
                mc.AddProfile(new CustomerProfile());
                mc.AddProfile(new TestimonialsProfile());
                mc.AddProfile(new NewsletterSubscriberProfile());
                mc.AddProfile(new CityProfile());
                mc.AddProfile(new SupplierProfile(pathHelper));
                mc.AddProfile(new ContactUsMapping());

                mc.AddProfile(new ReminderProfile());
                mc.AddProfile(new PurchaseOrderProfile());
                mc.AddProfile(new SalesOrderProfile());

                mc.AddProfile(new CompanyProfileProfile());
                mc.AddProfile(new ExpenseProfile());
                mc.AddProfile(new CurrencyProfile());
                mc.AddProfile(new UnitProfile());
                mc.AddProfile(new TaxProfile());
                mc.AddProfile(new WarehouseProfile());


                mc.AddProfile(new InquiryNoteProfile());
                mc.AddProfile(new InquiryActivityProfile());
                mc.AddProfile(new InquiryAttachmentProfile());
                mc.AddProfile(new InquiryProfile());
                mc.AddProfile(new InquiryStatusProfile());
                mc.AddProfile(new InquirySourceProfile());

                mc.AddProfile(new ProductCategoryProfile(pathHelper));
                mc.AddProfile(new ProductProfile(pathHelper));

                mc.AddProfile(new BrandProfile());

                mc.AddProfile(new InventoryProfle());

                mc.AddProfile(new NosotrosProfile());
                mc.AddProfile(new InterestInformationProfile(pathHelper));
                mc.AddProfile(new EventosProfile(pathHelper));

                mc.AddProfile(new AsistenciaProfile(pathHelper));
                mc.AddProfile(new BeneficiosProfile());
                mc.AddProfile(new BeneficioCategoriaProfile());

                mc.AddProfile(new ServiciosProfile());
                mc.AddProfile(new ServicioCategoriaProfile());

                mc.AddProfile(new PublicidadProfile(pathHelper));
                mc.AddProfile(new EmpresasProfile(pathHelper));
                mc.AddProfile(new CertificadosProfile());

                mc.AddProfile(new BuzonSugerenciasProfile());
                mc.AddProfile(new EstadoCuentaProfile());
                mc.AddProfile(new FacturasProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}

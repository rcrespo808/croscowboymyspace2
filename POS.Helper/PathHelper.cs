using Microsoft.Extensions.Configuration;
using System.IO;

namespace POS.Helper
{
    public class PathHelper
    {
        public IConfiguration _configuration;

        public PathHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string UserProfilePath
        {
            get
            {
                return _configuration["UserProfilePath"];
            }
        }

        public string BrandImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:BrandImages"];
            }
        }

        public string ProductImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:ProductImages"];
            }
        }

        public string ProductThumbnailImagePath
        {
            get
            {
                return Path.Combine(ProductImagePath, "Thumbnail");
            }
        }

        public string NoImageFound
        {
            get
            {
                return _configuration["ImagePathSettings:NoImageFound"];
            }
        }

        public string CompanyLogo
        {
            get
            {
                return _configuration["ImagePathSettings:CompanyLogo"];
            }
        }

        public string SupplierImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:SupplierImages"];
            }
        }

        public string ArticleBannerImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:ArticleBannerImagePath"];
            }
        }

        public string CustomerImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:CustomerImages"];
            }
        }

        public string TestimonialsImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:TestimonialsImagePath"];
            }
        }

        public string Attachments
        {
            get
            {
                return _configuration["ImagePathSettings:Attachments"];
            }
        }

        public string SiteMapPath
        {
            get
            {
                return _configuration["SiteMapPath"];
            }
        }

        public string DocumentPath
        {
            get
            {
                return _configuration["DocumentPath"];
            }
        }
        public string AesEncryptionKey
        {
            get
            {
                return _configuration["AesEncryptionKey"];
            }
        }

        public string ReminderFromEmail
        {
            get
            {
                return _configuration["ReminderFromEmail"];
            }
        }

        public string InterestInformationLogos
        {
            get
            {
                return _configuration["ImagePathSettings:InterestInformation:Logos"];
            }
        }

        public string InterestInformationDocuments
        {
            get
            {
                return _configuration["ImagePathSettings:InterestInformation:Documents"];
            }
        }

        public string NosotrosImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:NosotrosImagePath"];
            }
        }

        public string EventosBanners
        {
            get
            {
                return _configuration["ImagePathSettings:Eventos:Banners"];
            }
        }

        public string EventosDocuments
        {
            get
            {
                return _configuration["ImagePathSettings:Eventos:Documents"];
            }
        }

        public string ProductCategoryImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:ProductsCategoryImages"];
            }
        }

        public string BeneficioCategoriaImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:BeneficioCategoriaImages"];
            }
        }

        public string ServicioCategoriaImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:ServicioCategoriaImages"];
            }
        }

        public string ServiciosImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:ServiciosImages"];
            }
        }

        public string PublicidadImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:PublicidadImages"];
            }
        }

        public string EmpresasImagePath
        {
            get
            {
                return _configuration["ImagePathSettings:EmpresasImages"];
            }
        }
    }
}

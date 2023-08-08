﻿using Microsoft.Extensions.DependencyInjection;
using POS.Common.UnitOfWork;
using POS.Repository;
using POS.Repository.BuzonSugerencias;
using POS.Repository.Certificados;
using POS.Repository.Empresas;
using POS.Repository.EstadoCuenta;
using POS.Repository.Facturas;

namespace POS.API.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
            services.AddScoped<INLogRepository, NLogRepository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<INewsletterSubscriberRepository, NewsletterSubscriberRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITestimonialsRepository, TestimonialsRepository>();
            services.AddScoped<IReminderNotificationRepository, ReminderNotificationRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IReminderUserRepository, ReminderUserRepository>();
            services.AddScoped<IReminderSchedulerRepository, ReminderSchedulerRepository>();
            services.AddScoped<IDailyReminderRepository, DailyReminderRepository>();
            services.AddScoped<IQuarterlyReminderRepository, QuarterlyReminderRepository>();
            services.AddScoped<IHalfYearlyReminderRepository, HalfYearlyReminderRepository>();
            services.AddScoped<ISendEmailRepository, SendEmailRepository>();
            // PO
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderItemRepository, PurchaseOrderItemRepository>();
            services.AddScoped<IPurchaseOrderPaymentRepository, PurchaseOrderPaymentRepository>();
            //SO
            services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();
            services.AddScoped<ISalesOrderItemRepository, SalesOrderItemRepository>();
            services.AddScoped<ISalesOrderItemTaxRepository, SalesOrderItemTaxRepository>();
            services.AddScoped<ISalesOrderPaymentRepository, SalesOrderPaymentRepository>();

            services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();
            services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTaxRepository, ProductTaxRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<IInquiryProductRepository, InquiryProductRepository>();
            services.AddScoped<IInquiryStatusRepository, InquiryStatusRepository>();
            services.AddScoped<IInquiryNoteRepository, InquiryNoteRepository>();
            services.AddScoped<IInquiryAttachmentRepository, InquiryAttachmentRepository>();
            services.AddScoped<IInquiryActivityRepository, InquiryActivityRepository>();
            services.AddScoped<IInquirySourceRepository, InquirySourceRepository>();

            services.AddScoped<IBrandRepository, BrandRepository>();

            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryHistoryRepository, InventoryHistoryRepository>();

            services.AddScoped<INosotrosRepository, NosotrosRepository>();
            services.AddScoped<IInterestInformationRepository, InterestInformationRepository>();
            services.AddScoped<IEventosRepository, EventosRepository>();
            services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();

            services.AddScoped<IBeneficiosCategoriasRepository, BeneficiosCategoriasRepository>();
            services.AddScoped<IBeneficiosRepository, BeneficiosRepository>();

            services.AddScoped<IServiciosCategoriasRepository, ServiciosCategoriasRepository>();
            services.AddScoped<IServiciosRepository, ServiciosRepository>();
            services.AddScoped<IPublicidadRepository, PublicidadRepository>();
            
            services.AddScoped<IEmpresasRepository, EmpresasRepository>();
            services.AddScoped<ICertificadosRepository, CertificadosRepository>();
            services.AddScoped<IBuzonSugerenciasRepository, BuzonSugerenciasRepository>();
            
            services.AddScoped<IEstadoCuentaRepository, EstadoCuentaRepository>();
            services.AddScoped<IFacturasRepository, FacturasRepository>();
        }
    }
}

using POS.Data;
using POS.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using POS.Data.Entities.Lookups;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using POS.Helper.Enum;
using POS.Data.Entities.Empresas;
using POS.Helper.Enum.BuzonSugerencias;
using POS.Helper.Enum.EstadoCuenta;

namespace POS.Domain
{
    public class POSDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public POSDbContext(DbContextOptions options) : base(options)
        {
        }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Data.Action> Actions { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<NLog> NLog { get; set; }
        public DbSet<LoginAudit> LoginAudits { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSMTPSetting> EmailSMTPSettings { get; set; }
        public DbSet<Country> Countries { get; set; }


        /*******Implementacion miguel********/
        public DbSet<Eventos> Eventos { get; set; }
        public DbSet<Expone> Expone { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Socios> Socios { get; set; }
        public DbSet<Asistencia> Asistencia { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<ServiciosCategorias> ServiciosCategorias { get; set; }
        public DbSet<BeneficiosCategorias> BeneficiosCategorias { get; set; }
        public DbSet<Beneficios> Beneficios { get; set; }
        public DbSet<Informacioninteres> Informacioninteres { get; set; }

        /*-------------- VIEWS --------*/
        public DbSet<Expositoriesview> Expositoriesview { get; set; }

        /**************/

        public DbSet<City> Cities { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierAddress> SupplierAddresses { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ReminderNotification> ReminderNotifications { get; set; }
        public DbSet<ReminderUser> ReminderUsers { get; set; }
        public DbSet<ReminderScheduler> ReminderSchedulers { get; set; }
        public DbSet<HalfYearlyReminder> HalfYearlyReminders { get; set; }
        public DbSet<QuarterlyReminder> QuarterlyReminders { get; set; }
        public DbSet<DailyReminder> DailyReminders { get; set; }
        public DbSet<SendEmail> SendEmails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<PurchaseOrderItemTax> PurchaseOrderItemTaxes { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public DbSet<SalesOrderItemTax> SalesOrderItemTaxes { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTax> ProductTaxes { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<InquiryActivity> InquiryActivities { get; set; }
        public DbSet<InquiryAttachment> InquiryAttachments { get; set; }
        public DbSet<InquiryNote> InquiryNotes { get; set; }
        public DbSet<InquirySource> InquirySources { get; set; }
        public DbSet<InquiryProduct> InquiryProducts { get; set; }
        public DbSet<InquiryStatus> InquiryStatuses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<InventoryHistory> InventoryHistories { get; set; }
        public DbSet<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public DbSet<SalesOrderPayment> SalesOrderPayments { get; set; }

        public DbSet<Nosotros> Nosotros { get; set; }
        
        public DbSet<Publicidad> Publicidad { get; set; }
        
        public DbSet<Empresas> Empresas { get; set; }

        public DbSet<Certificados> Certificados { get; set; }

        public DbSet<BuzonSugerencias> BuzonSugerencias { get; set; }

        public DbSet<EstadoCuenta> EstadoCuenta { get; set; }

        public DbSet<Facturas> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<ReminderUser>(b =>
            {
                b.HasKey(e => new { e.ReminderId, e.UserId });
                b.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(ur => ur.UserId)
                  .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Data.Action>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Page>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<EmailSMTPSetting>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Customer>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Supplier>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.Property(e => e.Sector)
                .HasConversion(
                    v => v.ToString(),
                    v => (Sector)Enum.Parse(typeof(Sector), v));
                //b.HasOne(e => e.ModifiedByUser)
                //    .WithMany()
                //    .HasForeignKey(rc => rc.ModifiedBy)
                //    .OnDelete(DeleteBehavior.Restrict);

                //b.HasOne(e => e.DeletedByUser)
                //    .WithMany()
                //    .HasForeignKey(rc => rc.DeletedBy)
                //    .OnDelete(DeleteBehavior.Restrict);

                //b.HasOne(e => e.SupplierAddress)
                //  .WithMany()
                //  .HasForeignKey(rc => rc.SupplierAddressId)
                //  .OnDelete(DeleteBehavior.Restrict);

                //b.HasOne(e => e.BillingAddress)
                //  .WithMany()
                //  .HasForeignKey(rc => rc.BillingAddressId)
                //  .OnDelete(DeleteBehavior.Restrict);

                //b.HasOne(e => e.ShippingAddress)
                //  .WithMany()
                //  .HasForeignKey(rc => rc.ShippingAddressId)
                //  .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Testimonials>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ProductCategory>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<EmailTemplate>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Reminder>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Expense>(b =>
            {
                b.HasOne(e => e.ExpenseBy)
                    .WithMany()
                    .HasForeignKey(rc => rc.ExpenseById)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ExpenseCategory>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ProductTax>(b =>
            {
                b.HasKey(c => new { c.ProductId, c.TaxId });
            });

            builder.Entity<City>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Inventory>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Tax>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Unit>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Warehouse>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Product>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

                //b.HasOne(e => e.Supplier)
                //    .WithMany(e => e.Products)
                //    .HasForeignKey(e => e.SupplierId)
                //    .IsRequired();

                b.HasOne(e => e.Brand)
                    .WithMany()
                    .HasForeignKey(e => e.BrandId);
            });

            builder.Entity<ProductTax>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InquirySource>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InquiryStatus>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InquiryProduct>(b =>
            {
                b.HasKey(c => new { c.ProductId, c.InquiryId });
            });

            builder.Entity<InquiryActivity>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InquiryAttachment>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InquiryNote>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Brand>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PurchaseOrderPayment>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<SalesOrderPayment>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Data.Page>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Actions)
                    .WithOne(e => e.Page)
                    .HasForeignKey(uc => uc.PageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            builder.Entity<Nosotros>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Expone>(b =>
            {
                b.HasKey(e => new { e.Id });

                b.HasOne(e => e.Evento)
                    .WithMany(e => e.Panelistas)
                    .HasForeignKey(e => e.EventoId);
            });

            builder.Entity<Asistencia>(b =>
            {
                b.HasKey(e => new { e.Id });

                b.HasOne(e => e.User)
                    .WithMany(e => e.Asistencia)
                    .HasForeignKey(e => e.UsersId);
            });

            builder.Entity<Servicios>(b =>
            {
                b.Property(e => e.TipoPago)
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoPago)Enum.Parse(typeof(TipoPago), v));

                b.Property(e => e.TipoServicio)
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoServicio)Enum.Parse(typeof(TipoServicio), v));
            });

            builder.Entity<Empresas>(b =>
            {
                b.Property(e => e.Sector)
                .HasConversion(
                    v => v.ToString(),
                    v => (Sector)Enum.Parse(typeof(Sector), v));
            });

            builder.Entity<BuzonSugerencias>(b =>
            {
                b.Property(e => e.Tipo)
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoSugerencia)Enum.Parse(typeof(TipoSugerencia), v));

                b.Property(e => e.Estado)
                .HasConversion(
                    v => v.ToString(),
                    v => (EstadoSugerencia)Enum.Parse(typeof(EstadoSugerencia), v));
            });

            builder.Entity<EstadoCuenta>(b =>
            {
                b.HasOne(e => e.Factura)
                 .WithOne(e => e.EstadoCuenta)
                 .HasForeignKey<EstadoCuenta>(e => e.IdFactura);

                b.Property(e => e.Estado)
                .HasConversion(
                    v => v.ToString(),
                    v => (TipoEstado)Enum.Parse(typeof(TipoEstado), v));
            });

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.DefalutMappingValue();
            builder.DefalutDeleteValueFilter();
        }
    }
}

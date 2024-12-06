using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.DBModels
{
    public partial class ECommerceShopDBContext : DbContext
    {
        public ECommerceShopDBContext()
        {
        }

        public ECommerceShopDBContext(DbContextOptions<ECommerceShopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountTransAttachment> AccountTransAttachments { get; set; } = null!;
        public virtual DbSet<AddressType> AddressTypes { get; set; } = null!;
        public virtual DbSet<AdminPanelNotification> AdminPanelNotifications { get; set; } = null!;
        public virtual DbSet<Apiconfiguration> Apiconfigurations { get; set; } = null!;
        public virtual DbSet<AppConfig> AppConfigs { get; set; } = null!;
        public virtual DbSet<AppModule> AppModules { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<AttachmentType> AttachmentTypes { get; set; } = null!;
        public virtual DbSet<AttrFlavor> AttrFlavors { get; set; } = null!;
        public virtual DbSet<AttrHdd> AttrHdds { get; set; } = null!;
        public virtual DbSet<AttrProcessor> AttrProcessors { get; set; } = null!;
        public virtual DbSet<AttrRam> AttrRams { get; set; } = null!;
        public virtual DbSet<AttrWeight> AttrWeights { get; set; } = null!;
        public virtual DbSet<BankAccount> BankAccounts { get; set; } = null!;
        public virtual DbSet<BankAccountAttachment> BankAccountAttachments { get; set; } = null!;
        public virtual DbSet<BankAccountTran> BankAccountTrans { get; set; } = null!;
        public virtual DbSet<BankAccountType> BankAccountTypes { get; set; } = null!;
        public virtual DbSet<BankIndustryType> BankIndustryTypes { get; set; } = null!;
        public virtual DbSet<BankMaster> BankMasters { get; set; } = null!;
        public virtual DbSet<BankStatus> BankStatuses { get; set; } = null!;
        public virtual DbSet<BankTransEvent> BankTransEvents { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<ContactU> ContactUs { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<DiscountCategoriesMapping> DiscountCategoriesMappings { get; set; } = null!;
        public virtual DbSet<DiscountCitiesMapping> DiscountCitiesMappings { get; set; } = null!;
        public virtual DbSet<DiscountManufacturersMapping> DiscountManufacturersMappings { get; set; } = null!;
        public virtual DbSet<DiscountProductsMapping> DiscountProductsMappings { get; set; } = null!;
        public virtual DbSet<DiscountType> DiscountTypes { get; set; } = null!;
        public virtual DbSet<DiscountUsageHistory> DiscountUsageHistories { get; set; } = null!;
        public virtual DbSet<DiscountsCampaign> DiscountsCampaigns { get; set; } = null!;
        public virtual DbSet<Entity> Entities { get; set; } = null!;
        public virtual DbSet<EntityType> EntityTypes { get; set; } = null!;
        public virtual DbSet<FieldType> FieldTypes { get; set; } = null!;
        public virtual DbSet<HomeScreenBanner> HomeScreenBanners { get; set; } = null!;
        public virtual DbSet<InventoryMethod> InventoryMethods { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<LocalizationCommonJson> LocalizationCommonJsons { get; set; } = null!;
        public virtual DbSet<LocalizationTable> LocalizationTables { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<MenuNavigation> MenuNavigations { get; set; } = null!;
        public virtual DbSet<NotificationType> NotificationTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<OrderNote> OrderNotes { get; set; } = null!;
        public virtual DbSet<OrderProductAttributeMapping> OrderProductAttributeMappings { get; set; } = null!;
        public virtual DbSet<OrderRefundReasonType> OrderRefundReasonTypes { get; set; } = null!;
        public virtual DbSet<OrderRefundRequest> OrderRefundRequests { get; set; } = null!;
        public virtual DbSet<OrderShippingDetail> OrderShippingDetails { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<OrderStatusesMapping> OrderStatusesMappings { get; set; } = null!;
        public virtual DbSet<OrdersPayment> OrdersPayments { get; set; } = null!;
        public virtual DbSet<Otplog> Otplogs { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; } = null!;
        public virtual DbSet<ProductAttributeColumn> ProductAttributeColumns { get; set; } = null!;
        public virtual DbSet<ProductDigitalFileMapping> ProductDigitalFileMappings { get; set; } = null!;
        public virtual DbSet<ProductPicturesMapping> ProductPicturesMappings { get; set; } = null!;
        public virtual DbSet<ProductProductAttributeMapping> ProductProductAttributeMappings { get; set; } = null!;
        public virtual DbSet<ProductReview> ProductReviews { get; set; } = null!;
        public virtual DbSet<ProductShippingMethodsMapping> ProductShippingMethodsMappings { get; set; } = null!;
        public virtual DbSet<ProductsCategoriesMapping> ProductsCategoriesMappings { get; set; } = null!;
        public virtual DbSet<ProductsTagsMapping> ProductsTagsMappings { get; set; } = null!;
        public virtual DbSet<RawOrder> RawOrders { get; set; } = null!;
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; } = null!;
        public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
        public virtual DbSet<RequestsQueue> RequestsQueues { get; set; } = null!;
        public virtual DbSet<Right> Rights { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleRight> RoleRights { get; set; } = null!;
        public virtual DbSet<RunTimeException> RunTimeExceptions { get; set; } = null!;
        public virtual DbSet<ScrnsLocalization> ScrnsLocalizations { get; set; } = null!;
        public virtual DbSet<ShippingMethod> ShippingMethods { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<StateProvince> StateProvinces { get; set; } = null!;
        public virtual DbSet<Subscriber> Subscribers { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAddress> UserAddresses { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<VendorsAccountRequest> VendorsAccountRequests { get; set; } = null!;
        public virtual DbSet<VendorsCommissionSetup> VendorsCommissionSetups { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;
        public virtual DbSet<WorldRegion> WorldRegions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAP-WFH171\\SQLEXPRESS;Database=ECommerceShopDB; User ID=sa; Password=%N00r/2022;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountTransAttachment>(entity =>
            {
                entity.HasKey(e => e.AcountTransAttachId);

                entity.ToTable("AccountTransAttachment");

                entity.Property(e => e.AcountTransAttachId).HasColumnName("AcountTransAttachID");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.TransId).HasColumnName("TransID");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.AccountTransAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountTr__Attac__5AA469F6");

                entity.HasOne(d => d.Trans)
                    .WithMany(p => p.AccountTransAttachments)
                    .HasForeignKey(d => d.TransId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTrans_TransId");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.AddressTypeName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AdminPanelNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.ClickUrl).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasMaxLength(500);

                entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");

                entity.Property(e => e.ReadByDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.AdminPanelNotifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AdminPane__Notif__636EBA21");
            });

            modelBuilder.Entity<Apiconfiguration>(entity =>
            {
                entity.ToTable("APIConfigurations");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DefaultParams).HasMaxLength(2048);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ErrorResponse).HasMaxLength(500);

                entity.Property(e => e.MethodType).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.Ormtype)
                    .HasMaxLength(20)
                    .HasColumnName("ORMType");

                entity.Property(e => e.SampleCall).HasMaxLength(500);

                entity.Property(e => e.SuccessResponse).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .HasColumnName("URL");

                entity.Property(e => e.UrlName).HasMaxLength(50);

                entity.Property(e => e.UrlParams).HasMaxLength(500);
            });

            modelBuilder.Entity<AppConfig>(entity =>
            {
                entity.Property(e => e.AppConfigId).HasColumnName("AppConfigID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AppModule>(entity =>
            {
                entity.Property(e => e.AppModuleId)
                    .ValueGeneratedNever()
                    .HasColumnName("AppModuleID");

                entity.Property(e => e.AppModuleName).HasMaxLength(20);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.AltAttribute).HasMaxLength(200);

                entity.Property(e => e.AttachmentName).HasMaxLength(500);

                entity.Property(e => e.AttachmentTypeId).HasColumnName("AttachmentTypeID");

                entity.Property(e => e.AttachmentUrl).HasColumnName("AttachmentURL");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MimeType).HasMaxLength(100);

                entity.Property(e => e.SeoName).HasMaxLength(200);

                entity.Property(e => e.TitleAttribute).HasMaxLength(200);

                entity.HasOne(d => d.AttachmentType)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.AttachmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attachmen__Attac__1F98B2C1");
            });

            modelBuilder.Entity<AttachmentType>(entity =>
            {
                entity.Property(e => e.AttachmentTypeId).HasColumnName("AttachmentTypeID");

                entity.Property(e => e.AttachmentTypeName).HasMaxLength(150);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AttrFlavor>(entity =>
            {
                entity.HasKey(e => e.FlavorId);

                entity.ToTable("AttrFlavor");

                entity.Property(e => e.FlavorId).HasColumnName("FlavorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FlavorName).HasMaxLength(300);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AttrHdd>(entity =>
            {
                entity.HasKey(e => e.HddId);

                entity.ToTable("AttrHDD");

                entity.Property(e => e.HddId).HasColumnName("HddID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HddName).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AttrProcessor>(entity =>
            {
                entity.HasKey(e => e.ProcessorId);

                entity.ToTable("AttrProcessor");

                entity.Property(e => e.ProcessorId).HasColumnName("ProcessorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProcessorName).HasMaxLength(200);
            });

            modelBuilder.Entity<AttrRam>(entity =>
            {
                entity.HasKey(e => e.RamId);

                entity.ToTable("AttrRAM");

                entity.Property(e => e.RamId).HasColumnName("RamID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RamName).HasMaxLength(200);
            });

            modelBuilder.Entity<AttrWeight>(entity =>
            {
                entity.HasKey(e => e.WeightId);

                entity.ToTable("AttrWeight");

                entity.Property(e => e.WeightId).HasColumnName("WeightID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.WeightValue).HasMaxLength(200);
            });

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.Property(e => e.AccountNo).HasMaxLength(80);

                entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

                entity.Property(e => e.AcountTitle).HasMaxLength(200);

                entity.Property(e => e.BankBranchCode).HasMaxLength(10);

                entity.Property(e => e.BankBranchName).HasMaxLength(200);

                entity.Property(e => e.BankMasterId).HasColumnName("BankMasterID");

                entity.Property(e => e.BranchAddress).HasMaxLength(500);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Iban)
                    .HasMaxLength(100)
                    .HasColumnName("IBAN");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateProvinceId).HasColumnName("StateProvinceID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<BankAccountAttachment>(entity =>
            {
                entity.HasKey(e => e.BankAccountAttachId);

                entity.ToTable("BankAccountAttachment");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.BankAccountAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankAccou__Attac__469D7149");
            });

            modelBuilder.Entity<BankAccountTran>(entity =>
            {
                entity.HasKey(e => e.TransId);

                entity.Property(e => e.TransId).HasColumnName("TransID");

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.Property(e => e.ProcessingDate).HasColumnType("datetime");

                entity.Property(e => e.TransAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransCurrencyId).HasColumnName("TransCurrencyID");

                entity.Property(e => e.TransTitle).HasMaxLength(200);

                entity.Property(e => e.TransType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.BankAccountTrans)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK__BankAccou__Payme__57C7FD4B");
            });

            modelBuilder.Entity<BankAccountType>(entity =>
            {
                entity.HasKey(e => e.AccountTypeId);

                entity.ToTable("BankAccountType");

                entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

                entity.Property(e => e.AccountTypeName).HasMaxLength(70);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<BankIndustryType>(entity =>
            {
                entity.HasKey(e => e.IndustryTypeId);

                entity.ToTable("BankIndustryType");

                entity.Property(e => e.IndustryTypeId).HasColumnName("IndustryTypeID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IndustryName).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<BankMaster>(entity =>
            {
                entity.ToTable("BankMaster");

                entity.Property(e => e.BankMasterId).HasColumnName("BankMasterID");

                entity.Property(e => e.BankCode).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(250);

                entity.Property(e => e.BankRegistNo).HasMaxLength(50);

                entity.Property(e => e.BankStatusId).HasColumnName("BankStatusID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IndustryTypeId).HasColumnName("IndustryTypeID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RegistDate).HasColumnType("datetime");

                entity.Property(e => e.SwiftCode).HasMaxLength(50);

                entity.Property(e => e.WebUrl).HasMaxLength(300);

                entity.HasOne(d => d.BankStatus)
                    .WithMany(p => p.BankMasters)
                    .HasForeignKey(d => d.BankStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankMaste__BankS__42CCE065");

                entity.HasOne(d => d.IndustryType)
                    .WithMany(p => p.BankMasters)
                    .HasForeignKey(d => d.IndustryTypeId)
                    .HasConstraintName("FK__BankMaste__Indus__43C1049E");
            });

            modelBuilder.Entity<BankStatus>(entity =>
            {
                entity.Property(e => e.BankStatusId).HasColumnName("BankStatusID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StatusName).HasMaxLength(100);
            });

            modelBuilder.Entity<BankTransEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("BankTransEvent");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EventName).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MetaDescription).HasMaxLength(500);

                entity.Property(e => e.MetaKeywords).HasMaxLength(500);

                entity.Property(e => e.MetaTitle).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.AttachmentId)
                    .HasConstraintName("FK__Categorie__Attac__119F9925");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName).HasMaxLength(250);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude).HasMaxLength(150);

                entity.Property(e => e.Longitude).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateProvinceId).HasColumnName("StateProvinceID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cities__CountryI__22751F6C");

                entity.HasOne(d => d.StateProvince)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateProvinceId)
                    .HasConstraintName("FK__Cities__StatePro__236943A5");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.ColorName).HasMaxLength(150);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.HexCode).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Subject).HasMaxLength(200);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.CountryName, "UQ__Countrie__E056F201D5B702B8")
                    .IsUnique();

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountryCode).HasMaxLength(60);

                entity.Property(e => e.CountryName).HasMaxLength(150);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FlagUrlid).HasColumnName("FlagURLID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MetaDescription).HasMaxLength(500);

                entity.Property(e => e.MetaKeywords).HasMaxLength(500);

                entity.Property(e => e.MetaTitle).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.WorldRegionId).HasColumnName("WorldRegionID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK__Countries__Curre__25518C17");

                entity.HasOne(d => d.FlagUrl)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.FlagUrlid)
                    .HasConstraintName("FK__Countries__FlagU__245D67DE");

                entity.HasOne(d => d.WorldRegion)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.WorldRegionId)
                    .HasConstraintName("FK__Countries__World__2645B050");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyCode).HasMaxLength(60);

                entity.Property(e => e.CurrencyName).HasMaxLength(150);

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.CouponCode).HasMaxLength(200);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1500);

                entity.Property(e => e.DiscountTypeId).HasColumnName("DiscountTypeID");

                entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsCouponCodeRequired)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.DiscountType)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.DiscountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Discounts__Disco__78D3EB5B");
            });

            modelBuilder.Entity<DiscountCategoriesMapping>(entity =>
            {
                entity.HasKey(e => e.DiscountCategoryMappingId);

                entity.ToTable("DiscountCategoriesMapping");

                entity.Property(e => e.DiscountCategoryMappingId).HasColumnName("DiscountCategoryMappingID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.DiscountCategoriesMappings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountC__Categ__7226EDCC");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountCategoriesMappings)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountC__Disco__7132C993");
            });

            modelBuilder.Entity<DiscountCitiesMapping>(entity =>
            {
                entity.HasKey(e => e.DiscountCityMappingId);

                entity.ToTable("DiscountCitiesMapping");

                entity.Property(e => e.DiscountCityMappingId).HasColumnName("DiscountCityMappingID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.DiscountCitiesMappings)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountC__CityI__731B1205");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountCitiesMappings)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountC__Disco__740F363E");
            });

            modelBuilder.Entity<DiscountManufacturersMapping>(entity =>
            {
                entity.HasKey(e => e.DiscountManufacturerMappingId);

                entity.ToTable("DiscountManufacturersMapping");

                entity.Property(e => e.DiscountManufacturerMappingId).HasColumnName("DiscountManufacturerMappingID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountManufacturersMappings)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountM__Disco__75035A77");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.DiscountManufacturersMappings)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountM__Manuf__75F77EB0");
            });

            modelBuilder.Entity<DiscountProductsMapping>(entity =>
            {
                entity.HasKey(e => e.DiscountProductMappingId);

                entity.ToTable("DiscountProductsMapping");

                entity.Property(e => e.DiscountProductMappingId).HasColumnName("DiscountProductMappingID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountProductsMappings)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountP__Disco__76EBA2E9");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DiscountProductsMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountP__Produ__77DFC722");
            });

            modelBuilder.Entity<DiscountType>(entity =>
            {
                entity.Property(e => e.DiscountTypeId).HasColumnName("DiscountTypeID");

                entity.Property(e => e.DiscountTypeName).HasMaxLength(150);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DiscountUsageHistory>(entity =>
            {
                entity.HasKey(e => e.UsageId);

                entity.ToTable("DiscountUsageHistory");

                entity.Property(e => e.UsageId).HasColumnName("UsageID");

                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.UsageDate).HasColumnType("datetime");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.DiscountUsageHistories)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__DiscountU__Campa__0F4D3C5F");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.DiscountUsageHistories)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiscountU__Disco__0D64F3ED");

                entity.HasOne(d => d.UsedByNavigation)
                    .WithMany(p => p.DiscountUsageHistories)
                    .HasForeignKey(d => d.UsedBy)
                    .HasConstraintName("FK__DiscountU__UsedB__0E591826");
            });

            modelBuilder.Entity<DiscountsCampaign>(entity =>
            {
                entity.HasKey(e => e.CampaignId);

                entity.ToTable("DiscountsCampaign");

                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");

                entity.Property(e => e.Body).HasMaxLength(1000);

                entity.Property(e => e.CoverPictureId).HasColumnName("CoverPictureID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DiscountTitle).HasMaxLength(100);

                entity.Property(e => e.DisplayEndDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayStartDate).HasColumnType("datetime");

                entity.Property(e => e.MainTitle).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.Property(e => e.EntityId).HasColumnName("EntityID");

                entity.Property(e => e.AppModuleId).HasColumnName("AppModuleID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DataVersion).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.EntityName).HasMaxLength(150);

                entity.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.AppModule)
                    .WithMany(p => p.Entities)
                    .HasForeignKey(d => d.AppModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppModulesEntities");

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.Entities)
                    .HasForeignKey(d => d.EntityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entities__Entity__48BAC3E5");
            });

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.Property(e => e.EntityTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EntityTypeID");

                entity.Property(e => e.EntityTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<FieldType>(entity =>
            {
                entity.HasIndex(e => e.FieldTypeName, "UQ__FieldTyp__1FB4D9A11700D958")
                    .IsUnique();

                entity.Property(e => e.FieldTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("FieldTypeID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FieldTypeName).HasMaxLength(150);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<HomeScreenBanner>(entity =>
            {
                entity.HasKey(e => e.BannerId);

                entity.Property(e => e.BannerId).HasColumnName("BannerID");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.BottomTitle).HasMaxLength(300);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.LeftButtonText).HasMaxLength(30);

                entity.Property(e => e.MainTitle).HasMaxLength(300);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RightButtonText).HasMaxLength(30);

                entity.Property(e => e.TopTitle).HasMaxLength(300);

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.HomeScreenBanners)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HomeScree__Attac__247D636F");
            });

            modelBuilder.Entity<InventoryMethod>(entity =>
            {
                entity.Property(e => e.InventoryMethodId).HasColumnName("InventoryMethodID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.InventoryMethodName).HasMaxLength(250);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.LanguageId)
                    .ValueGeneratedNever()
                    .HasColumnName("LanguageID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FlagUrl).HasMaxLength(500);

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.LanguageName).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<LocalizationCommonJson>(entity =>
            {
                entity.HasKey(e => e.LocalCommonDataId)
                    .HasName("PK_LocalizationCommonData");

                entity.ToTable("LocalizationCommonJson");

                entity.HasIndex(e => e.PrimaryKeyId, "NonClusteredIndex-20230210-175229");

                entity.Property(e => e.LocalCommonDataId).HasColumnName("LocalCommonDataID");

                entity.Property(e => e.LocalizationTableId).HasColumnName("LocalizationTableID");

                entity.Property(e => e.PrimaryKeyId).HasColumnName("PrimaryKeyID");

                entity.HasOne(d => d.LocalizationTable)
                    .WithMany(p => p.LocalizationCommonJsons)
                    .HasForeignKey(d => d.LocalizationTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocalizationTablesLocalizationCommonData");
            });

            modelBuilder.Entity<LocalizationTable>(entity =>
            {
                entity.Property(e => e.LocalizationTableId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocalizationTableID");

                entity.Property(e => e.TableName).HasMaxLength(100);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<MenuNavigation>(entity =>
            {
                entity.Property(e => e.MenuNavigationId).HasColumnName("MenuNavigationID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityId).HasColumnName("EntityID");

                entity.Property(e => e.IconClass).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LinkUrl).HasMaxLength(250);

                entity.Property(e => e.MenuNavigationName).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ParentMenuNavigationId).HasColumnName("ParentMenuNavigationID");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");

                entity.Property(e => e.NotificationTypeName).HasMaxLength(150);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LatestStatusId).HasColumnName("LatestStatusID");

                entity.Property(e => e.OrderDateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("OrderDateUTC")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderNumber).HasMaxLength(600);

                entity.Property(e => e.OrderSubtotalExclTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderSubtotalInclTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderTotalAttributeCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderTotalDiscountAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderTotalShippingCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Customer__2B0A656D");

                entity.HasOne(d => d.LatestStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LatestStatusId)
                    .HasConstraintName("FK__Orders__LatestSt__52442E1F");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.ItemPriceTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderItemAttributeChargesTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderItemDiscountTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderItemGuid).HasMaxLength(600);

                entity.Property(e => e.OrderItemShippingChargesTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderItemTaxTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderItemTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.VendorCommissionId).HasColumnName("VendorCommissionID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__2739D489");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Produ__282DF8C2");
                entity.HasOne(d => d.VendorCommission)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.VendorCommissionId)
                    .HasConstraintName("FK_VendorsCommissionSetup_OrderItems");
            });

            modelBuilder.Entity<OrderNote>(entity =>
            {
                entity.Property(e => e.OrderNoteId).HasColumnName("OrderNoteID");
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Message).HasMaxLength(1000);
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.ParentOrderNoteId).HasColumnName("ParentOrderNoteID");
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderNotes)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderNote__Order__59E54FE7");
            });

            modelBuilder.Entity<OrderProductAttributeMapping>(entity =>
            {
                entity.HasKey(e => e.OrderAttributeMappingId);
                entity.ToTable("OrderProductAttributeMapping");
                entity.Property(e => e.OrderAttributeMappingId).HasColumnName("OrderAttributeMappingID");
                entity.Property(e => e.AttrAdditionalPrice).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
                entity.Property(e => e.ProductAttributeId).HasColumnName("ProductAttributeID");
                entity.HasOne(d => d.OrderItem)
                    .WithMany(p => p.OrderProductAttributeMappings)
                    .HasForeignKey(d => d.OrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProductAttributeMapping_OrderItems");
                entity.HasOne(d => d.ProductAttribute)
                    .WithMany(p => p.OrderProductAttributeMappings)
                    .HasForeignKey(d => d.ProductAttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProductAttributeMapping_ProductAttributes");
            });

            modelBuilder.Entity<OrderRefundReasonType>(entity =>
            {
                entity.HasKey(e => e.RefundReasonTypeId);
                entity.ToTable("OrderRefundReasonType");
                entity.Property(e => e.RefundReasonTypeId).HasColumnName("RefundReasonTypeID");
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.ReasonName).HasMaxLength(200);
            });

            modelBuilder.Entity<OrderRefundRequest>(entity =>
            {
                entity.HasKey(e => e.RefundRequestId);
                entity.ToTable("OrderRefundRequest");
                entity.Property(e => e.RefundRequestId).HasColumnName("RefundRequestID");
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.RefundAmount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.RefundReasonDesc).HasMaxLength(1000);
                entity.Property(e => e.RefundReasonTypeId).HasColumnName("RefundReasonTypeID");
                entity.Property(e => e.TaskId).HasColumnName("TaskID");
                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.OrderRefundRequests)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonOrderRefundRequest");
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.OrderRefundRequests)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_RequestsQueueOrderRefundRequest");
            });

            modelBuilder.Entity<OrderShippingDetail>(entity =>
            {
                entity.HasKey(e => e.ShippingDetailId);
                entity.ToTable("OrderShippingDetail");
                entity.Property(e => e.ShippingDetailId).HasColumnName("ShippingDetailID");
                entity.Property(e => e.DepartureDate).HasColumnType("datetime");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
                entity.Property(e => e.ReceiverIdentityNo).HasMaxLength(30);
                entity.Property(e => e.ReceiverMobile).HasMaxLength(20);
                entity.Property(e => e.ReceiverName).HasMaxLength(150);
                entity.Property(e => e.ShipperComment).HasMaxLength(1000);
                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
                entity.Property(e => e.ShippingMethodId).HasColumnName("ShippingMethodID");
                entity.Property(e => e.ShippingStatusId).HasColumnName("ShippingStatusID");
                entity.Property(e => e.TrackingNo).HasMaxLength(250);
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderShippingDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderShip__Order__5AD97420");
                entity.HasOne(d => d.OrderItem)
                    .WithMany(p => p.OrderShippingDetails)
                    .HasForeignKey(d => d.OrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderShip__Order__5BCD9859");
                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.OrderShippingDetails)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK__OrderShip__Shipp__5CC1BC92");
                entity.HasOne(d => d.ShippingMethod)
                    .WithMany(p => p.OrderShippingDetails)
                    .HasForeignKey(d => d.ShippingMethodId)
                    .HasConstraintName("FK__OrderShip__Shipp__5DB5E0CB");
                entity.HasOne(d => d.ShippingStatus)
                    .WithMany(p => p.OrderShippingDetails)
                    .HasForeignKey(d => d.ShippingStatusId)
                    .HasConstraintName("FK__OrderShip__Shipp__5EAA0504");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK_order_statuses");
                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.StatusName).HasMaxLength(150);
            });

            modelBuilder.Entity<OrderStatusesMapping>(entity =>
            {
                entity.HasKey(e => e.OrderStatusMappingId)
                    .HasName("PK_order_statuses_mapping");

                entity.ToTable("OrderStatusesMapping");
                entity.Property(e => e.OrderStatusMappingId).HasColumnName("OrderStatusMappingID");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStatusesMappings)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderStat__Order__30C33EC3");
                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OrderStatusesMappings)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderStat__Statu__31B762FC");
            });

            modelBuilder.Entity<OrdersPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK_OrdersPayment");
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");
                entity.Property(e => e.Guid).HasMaxLength(600);
                entity.Property(e => e.MilestoneName).HasMaxLength(150);
                entity.Property(e => e.MilestoneValue).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
                entity.Property(e => e.StripeBalanceTransactionId).HasMaxLength(500);
                entity.Property(e => e.StripeChargeId).HasMaxLength(500);
                entity.Property(e => e.TransactionNo).HasMaxLength(600);
                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.OrdersPayments)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersPay__Curre__2EDAF651");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersPayments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersPay__Order__2DE6D218");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.OrdersPayments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersPay__Payme__2FCF1A8A");
            });

            modelBuilder.Entity<Otplog>(entity =>
            {
                entity.ToTable("OTPLogs");
                entity.Property(e => e.OtplogId).HasColumnName("OTPLogID");
                entity.Property(e => e.CustomMsg).HasMaxLength(500);
                entity.Property(e => e.EmailAddress).HasMaxLength(150);
                entity.Property(e => e.LastAttmptCreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Otp).HasColumnName("OTP");
                entity.Property(e => e.OtpcreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPCreatedOn");
                entity.Property(e => e.PhoneNo).HasMaxLength(20);
                entity.Property(e => e.StatusMsg).HasMaxLength(200);
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Otplogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__OTPLogs__UserID__1E8F7FEF");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasIndex(e => e.PaymentMethodName, "UQ__PaymentM__612080ED1E7B49F4")
                    .IsUnique();
                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.PaymentMethodName).HasMaxLength(150);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.AllowCustomerReviews).HasDefaultValueSql("((1))");
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.DisplayStockQuantity).HasDefaultValueSql("((0))");
                entity.Property(e => e.InventoryMethodId).HasColumnName("InventoryMethodID");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.IsBoundToStockQuantity).HasDefaultValueSql("((0))");
                entity.Property(e => e.IsDigitalProduct).HasDefaultValueSql("((0))");
                entity.Property(e => e.IsDiscountAllowed).HasDefaultValueSql("((0))");
                entity.Property(e => e.IsReturnAble).HasDefaultValueSql("((0))");
                entity.Property(e => e.IsShippingFree).HasDefaultValueSql("((0))");
                entity.Property(e => e.IsTaxExempt).HasDefaultValueSql("((0))");
                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
                entity.Property(e => e.MarkAsNew).HasDefaultValueSql("((0))");
                entity.Property(e => e.MetaDescription).HasMaxLength(500);
                entity.Property(e => e.MetaKeywords).HasMaxLength(500);
                entity.Property(e => e.MetaTitle).HasMaxLength(200);
                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.OldPrice).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ProductName).HasMaxLength(700);
                entity.Property(e => e.SellEndDatetimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("SellEndDatetimeUTC");

                entity.Property(e => e.SellStartDatetimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("SellStartDatetimeUTC");

                entity.Property(e => e.ShippingCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShortDescription).HasMaxLength(1500);

                entity.Property(e => e.ShowOnHomePage).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sku).HasMaxLength(200);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK__Products__Manufa__3493CFA7");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Vendor__339FAB6E");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.Property(e => e.ProductAttributeId).HasColumnName("ProductAttributeID");

                entity.Property(e => e.AttributeName).HasMaxLength(100);

                entity.Property(e => e.AttributeSqlTableName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.DisplayName).HasMaxLength(100);
            });

            modelBuilder.Entity<ProductAttributeColumn>(entity =>
            {
                entity.Property(e => e.ProductAttributeColumnId).HasColumnName("ProductAttributeColumnID");

                entity.Property(e => e.ColumnName).HasMaxLength(80);

                entity.Property(e => e.ColumnType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductAttributeId).HasColumnName("ProductAttributeID");
            });

            modelBuilder.Entity<ProductDigitalFileMapping>(entity =>
            {
                entity.ToTable("ProductDigitalFileMapping");

                entity.Property(e => e.ProductDigitalFileMappingId).HasColumnName("ProductDigitalFileMappingID");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.ProductDigitalFileMappings)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttachmentsProductDigitalFileMapping");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDigitalFileMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsProductDigitalFileMapping");
            });

            modelBuilder.Entity<ProductPicturesMapping>(entity =>
            {
                entity.HasKey(e => e.ProductPictureMappingId);

                entity.ToTable("ProductPicturesMapping");

                entity.Property(e => e.ProductPictureMappingId).HasColumnName("ProductPictureMappingID");

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.PictureId).HasColumnName("PictureID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductPicturesMappings)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_ColorsProductPicturesMapping");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.ProductPicturesMappings)
                    .HasForeignKey(d => d.PictureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductPi__Pictu__56B3DD81");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPicturesMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductPi__Produ__55BFB948");
            });

            modelBuilder.Entity<ProductProductAttributeMapping>(entity =>
            {
                entity.HasKey(e => e.ProductAttributeMappingId);

                entity.ToTable("Product_ProductAttribute_Mapping");

                entity.Property(e => e.ProductAttributeMappingId).HasColumnName("ProductAttributeMappingID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PriceAdjustment).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductAttributeId).HasColumnName("ProductAttributeID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.ProductAttribute)
                    .WithMany(p => p.ProductProductAttributeMappings)
                    .HasForeignKey(d => d.ProductAttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product_P__Produ__662B2B3B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProductAttributeMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product_P__Produ__671F4F74");
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.Body).HasMaxLength(100);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Rating).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewerEmail).HasMaxLength(150);

                entity.Property(e => e.ReviewerName).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductRe__Produ__4CC05EF3");
            });

            modelBuilder.Entity<ProductShippingMethodsMapping>(entity =>
            {
                entity.HasKey(e => e.ProductShippingMethodMappingId);

                entity.ToTable("ProductShippingMethodsMapping");

                entity.Property(e => e.ProductShippingMethodMappingId).HasColumnName("ProductShippingMethodMappingID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ShippingMethodId).HasColumnName("ShippingMethodID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductShippingMethodsMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSh__Produ__178D7CA5");

                entity.HasOne(d => d.ShippingMethod)
                    .WithMany(p => p.ProductShippingMethodsMappings)
                    .HasForeignKey(d => d.ShippingMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSh__Shipp__1699586C");
            });

            modelBuilder.Entity<ProductsCategoriesMapping>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryMappingId)
                    .HasName("PK_products_categories_mapping");

                entity.ToTable("ProductsCategoriesMapping");

                entity.Property(e => e.ProductCategoryMappingId).HasColumnName("ProductCategoryMappingID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductsCategoriesMappings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductsC__Categ__367C1819");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsCategoriesMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductsC__Produ__3587F3E0");
            });

            modelBuilder.Entity<ProductsTagsMapping>(entity =>
            {
                entity.HasKey(e => e.ProductTagMappingId)
                    .HasName("PK_products_tags_mapping");

                entity.ToTable("ProductsTagsMapping");

                entity.Property(e => e.ProductTagMappingId).HasColumnName("ProductTagMappingID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsTagsMappings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductsT__Produ__3B40CD36");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ProductsTagsMappings)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductsT__TagID__3C34F16F");
            });

            modelBuilder.Entity<RawOrder>(entity =>
            {
                entity.ToTable("RawOrder");

                entity.Property(e => e.RawOrderId).HasColumnName("RawOrderID");

                entity.Property(e => e.CartJsonData).HasColumnName("cartJsonData");

                entity.Property(e => e.CouponCode).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.OrderTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");

                entity.Property(e => e.ShippingSubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.ToTable("RequestStatus");

                entity.Property(e => e.RequestStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("RequestStatusID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StatusKey).HasMaxLength(150);
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.ToTable("RequestType");

                entity.Property(e => e.RequestTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("RequestTypeID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestTypeName).HasMaxLength(150);
            });

            modelBuilder.Entity<RequestsQueue>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToTable("RequestsQueue");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");

                entity.Property(e => e.RequestStatusId).HasColumnName("RequestStatusID");

                entity.Property(e => e.RequestTypeId).HasColumnName("RequestTypeID");

                entity.Property(e => e.StatusChangeDate).HasColumnType("datetime");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.RequestsQueues)
                    .HasForeignKey(d => d.RequestStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestStatusRequestsQueue");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.RequestsQueues)
                    .HasForeignKey(d => d.RequestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestTypeRequestsQueue");
            });

            modelBuilder.Entity<Right>(entity =>
            {
                entity.Property(e => e.RightId)
                    .ValueGeneratedNever()
                    .HasColumnName("RightID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RightName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleRight>(entity =>
            {
                entity.Property(e => e.RoleRightId).HasColumnName("RoleRightID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EntityId).HasColumnName("EntityID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RightId).HasColumnName("RightID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.RoleRights)
                    .HasForeignKey(d => d.RightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoleRight__Right__3D491139");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleRights)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoleRight__RoleI__3C54ED00");
            });

            modelBuilder.Entity<RunTimeException>(entity =>
            {
                entity.HasKey(e => e.ExceptionId);

                entity.Property(e => e.ExceptionId).HasColumnName("ExceptionID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExceptionMessage).HasMaxLength(3000);

                entity.Property(e => e.StatusCode).HasMaxLength(20);
            });

            modelBuilder.Entity<ScrnsLocalization>(entity =>
            {
                entity.HasKey(e => e.ScrnLocalizationId);

                entity.ToTable("ScrnsLocalization");

                entity.Property(e => e.ScrnLocalizationId).HasColumnName("ScrnLocalizationID");

                entity.Property(e => e.AppModuleId).HasColumnName("AppModuleID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedOn)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

                entity.HasOne(d => d.AppModule)
                    .WithMany(p => p.ScrnsLocalizations)
                    .HasForeignKey(d => d.AppModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppModulesScrnsLocalization");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ScrnsLocalizations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LanguageLocalization");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.ScrnsLocalizations)
                    .HasForeignKey(d => d.ScreenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntitiesLocalization");
            });

            modelBuilder.Entity<ShippingMethod>(entity =>
            {
                entity.Property(e => e.ShippingMethodId).HasColumnName("ShippingMethodID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MethodName).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.Centimeters).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Inches).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.ShortName).HasMaxLength(20);
            });

            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.Property(e => e.StateProvinceId).HasColumnName("StateProvinceID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude).HasMaxLength(150);

                entity.Property(e => e.Longitude).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(250);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StateProvinces)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StateProv__Count__3D2915A8");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.TagName).HasMaxLength(200);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.ProfilePictureId).HasColumnName("ProfilePictureID");

                entity.Property(e => e.UserName).HasMaxLength(150);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.Property(e => e.VerificationCode).HasMaxLength(600);

                entity.HasOne(d => d.ProfilePicture)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProfilePictureId)
                    .HasConstraintName("FK__Users__ProfilePi__5E1FF51F");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__UserTypeI__40058253");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK_addresses");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressLineOne).HasMaxLength(500);

                entity.Property(e => e.AddressLineTwo).HasMaxLength(500);

                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PostalCode).HasMaxLength(100);

                entity.Property(e => e.StateProvinceId).HasColumnName("StateProvinceID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Addresses__Addre__18EBB532");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Addresses__CityI__1BC821DD");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Addresses__Count__19DFD96B");

                entity.HasOne(d => d.StateProvince)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.StateProvinceId)
                    .HasConstraintName("FK__Addresses__State__1AD3FDA4");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRoles__RoleI__420DC656");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRoles__UserI__4119A21D");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasIndex(e => e.UserTypeName, "UQ__UserType__9262CB715B5A78E2")
                    .IsUnique();

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UserTypeName).HasMaxLength(150);
            });

            modelBuilder.Entity<VendorsAccountRequest>(entity =>
            {
                entity.HasKey(e => e.ReqtVendorId);

                entity.ToTable("VendorsAccountRequest");

                entity.Property(e => e.ReqtVendorId).HasColumnName("ReqtVendorID");

                entity.Property(e => e.AddressOne).HasMaxLength(500);

                entity.Property(e => e.AddressOneCityId).HasColumnName("AddressOneCityID");

                entity.Property(e => e.AddressOneCountryId).HasColumnName("AddressOneCountryID");

                entity.Property(e => e.AddressOnePostalCode).HasMaxLength(50);

                entity.Property(e => e.AddressOneStateId).HasColumnName("AddressOneStateID");

                entity.Property(e => e.AddressOneTypeId).HasColumnName("AddressOneTypeID");

                entity.Property(e => e.AddressTwo).HasMaxLength(500);

                entity.Property(e => e.AddressTwoCityId).HasColumnName("AddressTwoCityID");

                entity.Property(e => e.AddressTwoCountryId).HasColumnName("AddressTwoCountryID");

                entity.Property(e => e.AddressTwoPostalCode).HasMaxLength(50);

                entity.Property(e => e.AddressTwoStateId).HasColumnName("AddressTwoStateID");

                entity.Property(e => e.AddressTwoTypeId).HasColumnName("AddressTwoTypeID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(15);

                entity.Property(e => e.HomeCountryId).HasColumnName("HomeCountryID");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PhoneNo).HasMaxLength(20);

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.HasOne(d => d.AddressOneCity)
                    .WithMany(p => p.VendorsAccountRequestAddressOneCities)
                    .HasForeignKey(d => d.AddressOneCityId)
                    .HasConstraintName("FK_AddressOneCityIDCitiesVendorsAccountRequest");

                entity.HasOne(d => d.AddressOneCountry)
                    .WithMany(p => p.VendorsAccountRequestAddressOneCountries)
                    .HasForeignKey(d => d.AddressOneCountryId)
                    .HasConstraintName("FK_AddressOneCountryIDCountriesVendorsAccountRequest");

                entity.HasOne(d => d.AddressOneState)
                    .WithMany(p => p.VendorsAccountRequestAddressOneStates)
                    .HasForeignKey(d => d.AddressOneStateId)
                    .HasConstraintName("FK_AddressOneStateIDStateProvincesVendorsAccountRequest");

                entity.HasOne(d => d.AddressOneType)
                    .WithMany(p => p.VendorsAccountRequestAddressOneTypes)
                    .HasForeignKey(d => d.AddressOneTypeId)
                    .HasConstraintName("FK_AddressOneTypeIDAddressTypesVendorsAccountRequest");

                entity.HasOne(d => d.AddressTwoCity)
                    .WithMany(p => p.VendorsAccountRequestAddressTwoCities)
                    .HasForeignKey(d => d.AddressTwoCityId)
                    .HasConstraintName("FK_AddressTwoCityIDStateCitiesVendorsAccountRequest");

                entity.HasOne(d => d.AddressTwoCountry)
                    .WithMany(p => p.VendorsAccountRequestAddressTwoCountries)
                    .HasForeignKey(d => d.AddressTwoCountryId)
                    .HasConstraintName("FK_AddressTwoCountryIDCountriesVendorsAccountRequest");

                entity.HasOne(d => d.AddressTwoState)
                    .WithMany(p => p.VendorsAccountRequestAddressTwoStates)
                    .HasForeignKey(d => d.AddressTwoStateId)
                    .HasConstraintName("FK_AddressTwoStateIDStateProvincesVendorsAccountRequest");

                entity.HasOne(d => d.AddressTwoType)
                    .WithMany(p => p.VendorsAccountRequestAddressTwoTypes)
                    .HasForeignKey(d => d.AddressTwoTypeId)
                    .HasConstraintName("FK_AddressTwoTypeIDAddressTypesVendorsAccountRequest");

                entity.HasOne(d => d.HomeCountry)
                    .WithMany(p => p.VendorsAccountRequestHomeCountries)
                    .HasForeignKey(d => d.HomeCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountriesVendorsAccountRequest");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.VendorsAccountRequests)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_RequestsQueueVendorsAccountRequest");
            });

            modelBuilder.Entity<VendorsCommissionSetup>(entity =>
            {
                entity.HasKey(e => e.VendorCommissionId);

                entity.ToTable("VendorsCommissionSetup");

                entity.Property(e => e.VendorCommissionId).HasColumnName("VendorCommissionID");

                entity.Property(e => e.ApplicableFrom).HasColumnType("datetime");

                entity.Property(e => e.ApplicableTo).HasColumnType("datetime");

                entity.Property(e => e.CommissionType).HasMaxLength(20);

                entity.Property(e => e.CommissionValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VendorsCommissionSetups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VendorsCo__UserI__4A6E022D");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.WarehouseName).HasMaxLength(250);
            });

            modelBuilder.Entity<WorldRegion>(entity =>
            {
                entity.Property(e => e.WorldRegionId).HasColumnName("WorldRegionID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplaySeqNo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RegionName).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

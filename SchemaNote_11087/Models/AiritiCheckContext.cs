using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SchemaNote_A11087.Models
{
    public partial class AiritiCheckContext : DbContext
    {
        public AiritiCheckContext()
        {
        }

        public AiritiCheckContext(DbContextOptions<AiritiCheckContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountEtd> AccountEtds { get; set; }
        public virtual DbSet<AccountRefuse> AccountRefuses { get; set; }
        public virtual DbSet<AccountSchInfo> AccountSchInfos { get; set; }
        public virtual DbSet<AccountSchInfoRefuse> AccountSchInfoRefuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-13I52L2\\SQLEXPRESS;Initial Catalog=AiritiCheck;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasComment("使用者帳號主表");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(100)
                    .HasColumnName("AccountID")
                    .HasComment("帳號");

                entity.Property(e => e.AccountEdate)
                    .HasMaxLength(8)
                    .HasColumnName("AccountEDate")
                    .IsFixedLength(true)
                    .HasComment("帳號有效日期(訖)");

                entity.Property(e => e.AccountPassword)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("密碼");

                entity.Property(e => e.AccountSdate)
                    .HasMaxLength(8)
                    .HasColumnName("AccountSDate")
                    .IsFixedLength(true)
                    .HasComment("帳號有效日期(起)");

                entity.Property(e => e.AccountStatus)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("帳號狀態");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("帳號類型");

                entity.Property(e => e.AppsyncStatus)
                    .HasMaxLength(1)
                    .HasColumnName("APPSyncStatus")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true)
                    .HasComment("同步狀態");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(100)
                    .HasComment("Email");

                entity.Property(e => e.CustomerFrom)
                    .HasMaxLength(12)
                    .IsFixedLength(true)
                    .HasComment("原系統");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(12)
                    .HasColumnName("CustomerID")
                    .IsFixedLength(true)
                    .HasComment("所屬客戶ID");

                entity.Property(e => e.GracePeriod)
                    .HasDefaultValueSql("((30))")
                    .HasComment("檔案可以保留天數");

                entity.Property(e => e.RegCustomerId)
                    .HasMaxLength(12)
                    .HasColumnName("RegCustomerID")
                    .IsFixedLength(true)
                    .HasComment("新註冊CustomerID");

                entity.Property(e => e.RegisterDate)
                    .HasMaxLength(8)
                    .HasDefaultValueSql("(CONVERT([varchar](10),getdate(),(112)))")
                    .IsFixedLength(true)
                    .HasComment("註冊日期");

                entity.Property(e => e.Sort)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('U1')")
                    .HasComment("排序欄位與冪次");

                entity.Property(e => e.VerifyId)
                    .HasMaxLength(10)
                    .HasColumnName("VerifyID")
                    .IsFixedLength(true)
                    .HasComment("驗證碼");
            });

            modelBuilder.Entity<AccountEtd>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.Pid });

                entity.ToTable("Account_ETDS");

                entity.HasComment("使用者帳號主表");

                entity.Property(e => e.SchoolId)
                    .HasMaxLength(5)
                    .HasColumnName("schoolID")
                    .IsFixedLength(true)
                    .HasComment("ETDS學校編號");

                entity.Property(e => e.Pid)
                    .HasMaxLength(150)
                    .HasColumnName("pid")
                    .HasComment("ETDS帳密整合帳號");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("AccountID")
                    .HasComment("帳號");
            });

            modelBuilder.Entity<AccountRefuse>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AccountRefuse");

                entity.HasComment("垃圾帳號回收表");

                entity.Property(e => e.AccountEdate)
                    .HasMaxLength(8)
                    .HasColumnName("AccountEDate")
                    .IsFixedLength(true)
                    .HasComment("帳號有效日期(訖)");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("AccountID")
                    .HasComment("帳號");

                entity.Property(e => e.AccountPassword)
                    .HasMaxLength(20)
                    .HasComment("密碼");

                entity.Property(e => e.AccountSdate)
                    .HasMaxLength(8)
                    .HasColumnName("AccountSDate")
                    .IsFixedLength(true)
                    .HasComment("帳號有效日期(起)");

                entity.Property(e => e.AccountStatus)
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("帳號狀態");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("帳號類型");

                entity.Property(e => e.AppsyncStatus)
                    .HasMaxLength(1)
                    .HasColumnName("APPSyncStatus")
                    .IsFixedLength(true)
                    .HasComment("同步狀態");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(100)
                    .HasComment("Email");

                entity.Property(e => e.CustomerFrom)
                    .HasMaxLength(12)
                    .IsFixedLength(true)
                    .HasComment("原系統");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(12)
                    .HasColumnName("CustomerID")
                    .IsFixedLength(true)
                    .HasComment("所屬客戶ID");

                entity.Property(e => e.GracePeriod).HasComment("檔案可以保留天數");

                entity.Property(e => e.RefuseDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')")
                    .HasComment("回收理由");

                entity.Property(e => e.RefuseTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("回收時間");

                entity.Property(e => e.RegCustomerId)
                    .HasMaxLength(12)
                    .HasColumnName("RegCustomerID")
                    .IsFixedLength(true)
                    .HasComment("新註冊CustomerID");

                entity.Property(e => e.RegisterDate)
                    .HasMaxLength(8)
                    .IsFixedLength(true)
                    .HasComment("註冊日期");

                entity.Property(e => e.Sort)
                    .HasMaxLength(10)
                    .HasComment("排序欄位與冪次");

                entity.Property(e => e.VerifyId)
                    .HasMaxLength(10)
                    .HasColumnName("VerifyID")
                    .IsFixedLength(true)
                    .HasComment("驗證碼");
            });

            modelBuilder.Entity<AccountSchInfo>(entity =>
            {
                entity.ToTable("Account_SchInfo");

                entity.HasComment("帳號資訊");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("ID")
                    .HasComment("帳號");

                entity.Property(e => e.ColId)
                    .HasMaxLength(12)
                    .HasColumnName("Col_ID")
                    .HasComment("學院代碼");

                entity.Property(e => e.DepId)
                    .HasMaxLength(12)
                    .HasColumnName("Dep_ID")
                    .HasComment("系所代碼");

                entity.Property(e => e.SchId)
                    .HasMaxLength(12)
                    .HasColumnName("Sch_ID")
                    .HasComment("學校代碼");

                entity.Property(e => e.StudentShip)
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("身份代碼");
            });

            modelBuilder.Entity<AccountSchInfoRefuse>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Account_SchInfoRefuse");

                entity.HasComment("垃圾帳號資訊回收表");

                entity.Property(e => e.ColId)
                    .HasMaxLength(12)
                    .HasColumnName("Col_ID")
                    .HasComment("學院代碼");

                entity.Property(e => e.DepId)
                    .HasMaxLength(12)
                    .HasColumnName("Dep_ID")
                    .HasComment("系所代碼");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ID")
                    .HasComment("帳號");

                entity.Property(e => e.RefuseDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("回收理由");

                entity.Property(e => e.RefuseTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("回收時間");

                entity.Property(e => e.SchId)
                    .HasMaxLength(12)
                    .HasColumnName("Sch_ID")
                    .HasComment("學校代碼");

                entity.Property(e => e.StudentShip)
                    .HasMaxLength(1)
                    .IsFixedLength(true)
                    .HasComment("身分代碼");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

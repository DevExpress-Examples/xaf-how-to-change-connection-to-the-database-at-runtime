using System;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.ExpressApp.EFCore.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RuntimeDbChooser.Services;

namespace RuntimeDbChooser.Module.BusinessObjects;
public class DbContextInitializer : DbContextTypesInfoInitializerBase {
    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
    protected override DbContext CreateDbContext() {
        var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>()
            .UseSqlServer(@";");
        return new DemoDbContext(optionsBuilder.Options, null);
    }
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class EFCoreDemoDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext> {
    public DemoDbContext CreateDbContext(string[] args) {
        throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
        //var optionsBuilder = new DbContextOptionsBuilder<EFCoreDemoDbContext>();
        //optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;MultipleActiveResultSets=true;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EFCoreDemo_v22.1;ConnectRetryCount=0;");
        //return new EFCoreDemoDbContext(optionsBuilder.Options);
    }
}
[TypesInfoInitializer(typeof(DbContextInitializer))]
public class DemoDbContext : DbContext {
    readonly IConnectionStringProvider connectionStringProvider;

    public DemoDbContext(DbContextOptions<DemoDbContext> options, IConnectionStringProvider connectionStringProvider)
        : base(options) {
        this.connectionStringProvider = connectionStringProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //Beresnev: commented out, it looks like strange, and prevents adding MediaDataObject to other classes. I'll leave it that way until the reasons are explained.
        //modelBuilder.Entity<ApplicationUser>().HasOne(user => user.Photo).WithOne().HasForeignKey<MediaDataObject>(mdo => mdo.Id);
        modelBuilder.Entity<MediaDataObject>().HasOne(md => md.MediaResource).WithOne().HasForeignKey<MediaResourceObject>(p => p.ID);

        modelBuilder.Entity<ApplicationUserLoginInfo>(b => {
            b.HasIndex(nameof(ISecurityUserLoginInfo.LoginProviderName), nameof(ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
        });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        //Configure the connection string based on logon parameter values.
        if(!optionsBuilder.IsConfigured) {
            string connectionString = connectionStringProvider.GetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationUserLoginInfo> UserLoginInfos { get; set; }
    public DbSet<ModelDifference> ModelDifferences { get; set; }
    public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
    public DbSet<PermissionPolicyRole> Roles { get; set; }
    public DbSet<ModuleInfo> ModulesInfo { get; set; }
    public DbSet<ReportDataV2> ReportData { get; set; }
}


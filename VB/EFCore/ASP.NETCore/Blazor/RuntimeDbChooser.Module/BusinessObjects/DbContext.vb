Imports System
Imports DevExpress.ExpressApp.Design
Imports DevExpress.ExpressApp.EFCore.DesignTime
Imports DevExpress.ExpressApp.EFCore.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports RuntimeDbChooser.Services

Namespace RuntimeDbChooser.Module.BusinessObjects

    Public Class DbContextInitializer
        Inherits DbContextTypesInfoInitializerBase

        ' This code allows our Model Editor to get relevant EF Core metadata at design time.
        ' For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
        Protected Overrides Function CreateDbContext() As DbContext
            Dim optionsBuilder = New DbContextOptionsBuilder(Of DemoDbContext)().UseSqlServer(";")
            Return New DemoDbContext(optionsBuilder.Options, Nothing)
        End Function
    End Class

    'This factory creates DbContext for design-time services. For example, it is required for database migration.
    Public Class EFCoreDemoDesignTimeDbContextFactory
        Implements IDesignTimeDbContextFactory(Of DemoDbContext)

        Public Function CreateDbContext(ByVal args As String()) As DemoDbContext Implements IDesignTimeDbContextFactory(Of DemoDbContext).CreateDbContext
            Throw New InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.")
        'var optionsBuilder = new DbContextOptionsBuilder<EFCoreDemoDbContext>();
        'optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;MultipleActiveResultSets=true;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EFCoreDemo_v22.1;ConnectRetryCount=0;");
        'return new EFCoreDemoDbContext(optionsBuilder.Options);
        End Function
    End Class

    <TypesInfoInitializer(GetType(DbContextInitializer))>
    Public Class DemoDbContext
        Inherits DbContext

        Private ReadOnly connectionStringProvider As IConnectionStringProvider

        Public Sub New(ByVal options As DbContextOptions(Of DemoDbContext), ByVal connectionStringProvider As IConnectionStringProvider)
            MyBase.New(options)
            Me.connectionStringProvider = connectionStringProvider
        End Sub

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As ModelBuilder)
            'Beresnev: commented out, it looks like strange, and prevents adding MediaDataObject to other classes. I'll leave it that way until the reasons are explained.
            'modelBuilder.Entity<ApplicationUser>().HasOne(user => user.Photo).WithOne().HasForeignKey<MediaDataObject>(mdo => mdo.Id);
            modelBuilder.Entity(Of MediaDataObject)().HasOne(Function(md) md.MediaResource).WithOne().HasForeignKey(Of MediaResourceObject)(Function(p) p.Id)
            modelBuilder.Entity(Of ApplicationUserLoginInfo)(Function(b)
                b.HasIndex(NameOf(ISecurityUserLoginInfo.LoginProviderName), NameOf(ISecurityUserLoginInfo.ProviderUserKey)).IsUnique()
            End Function)
        End Sub

        Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
            MyBase.OnConfiguring(optionsBuilder)
            'Configure the connection string based on logon parameter values.
            If Not optionsBuilder.IsConfigured Then
                Dim connectionString As String = connectionStringProvider.GetConnectionString()
                optionsBuilder.UseSqlServer(connectionString)
            End If
        End Sub

        Public Property Users As DbSet(Of ApplicationUser)

        Public Property UserLoginInfos As DbSet(Of ApplicationUserLoginInfo)

        Public Property ModelDifferences As DbSet(Of ModelDifference)

        Public Property ModelDifferenceAspects As DbSet(Of ModelDifferenceAspect)

        Public Property Roles As DbSet(Of PermissionPolicyRole)

        Public Property ModulesInfo As DbSet(Of ModuleInfo)

        Public Property ReportData As DbSet(Of ReportDataV2)
    End Class
End Namespace

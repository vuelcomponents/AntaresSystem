
using System.Net;
using AntaresApi.Dto.Document;
using AntaresApi.Models;
using AntaresApi.Models.Car;
using AntaresApi.Models.Document;
using AntaresApi.Models.House;
using AntaresApi.Models.Mail;
using AntaresApi.Models.Offer;
using AntaresApi.Models.Position;
using AntaresApi.Models.Recruitment;
using AntaresApi.Models.Status;
using AntaresApi.Models.StatusAction;
using AntaresApi.Models.StoreModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Action = System.Action;

namespace AntaresApi;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect())
                {
                    databaseCreator.Create();
                }
                if (!databaseCreator.HasTables())
                {
                    databaseCreator.CreateTables();
                }

            }

            var seed = new Seed(this, new HttpContextAccessor());
            seed.Release();
            
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(); // Włącz logowanie szczegółowych danych
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Variant> Variants { get; set; }
    public DbSet<VariantRealisation> VariantRealisations { get; set; }
    public DbSet<Realisation> Realisations { get; set; }
    public DbSet<VariantType> VariantTypes { get; set; }

    public DbSet<_Document> Documents { get; set; }
    public DbSet<StoreModel> StoreModels { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<StatusAction> StatusActions { get; set; }
    public DbSet<StatusActionTrigger> StatusActionTriggers { get; set; }
    public DbSet<ActionFunction> ActionFunctions { get; set; }
    public DbSet<SystemFunction> SystemFunctions { get; set; }
    public DbSet<_DocumentType> DocumentTypes { get; set; }
    public DbSet<Comparator> Comparators { get; set; }
    public DbSet<Mail> Mails { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<PositionUnit> PositionUnits { get; set; }
    public DbSet<House> Houses { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Recruitment> Recruitments { get; set; }
    public DbSet<RecruitmentContact> RecruitmentContacts { get; set; }
    public DbSet<Plan> Plans { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Realisations)
            .WithMany(e => e.Employees);
        modelBuilder.Entity<Company>()
            .HasMany(e => e.Realisations)
            .WithMany(e => e.Companies);
        modelBuilder.Entity<_Document>()
            .HasMany(e => e.Realisations)
            .WithMany(e => e.Documents);
        modelBuilder.Entity<Variant>()
            .HasMany(e => e.VariantRealisations)
            .WithOne(e => e.Variant)
             .OnDelete(DeleteBehavior.Cascade);;
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Companies)
            .WithMany(c => c.Employees);
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Documents)
            .WithMany(c => c.Employees);

        modelBuilder.Entity<Company>()
            .HasMany(e => e.Documents)
            .WithMany(c => c.Companies);
        
        //statuses sign
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Status)
            .WithMany(c => c.Employees);

        modelBuilder.Entity<Company>()
            .HasOne(e => e.Status)
            .WithMany(c => c.Companies);
        modelBuilder.Entity<_Document>()
            .HasOne(e => e.Status)
            .WithMany(c => c.Documents);
        modelBuilder.Entity<Mail>()
            .HasMany(e => e.Documents)
            .WithMany(c => c.Mails);
        
        modelBuilder.Entity<Status>()
            .HasMany(e => e.StatusActions)
            .WithMany(e => e.Statuses);
        modelBuilder.Entity<_Document>()
            .HasOne(d => d.DocumentType)
            .WithMany(c => c.Documents);
        modelBuilder.Entity<StoreModel>()
            .HasMany(s => s.SystemFunctions)
            .WithMany(sf => sf.StoreModels);
        
        modelBuilder.Entity<Company>()
            .HasMany(c => c.Positions) .WithOne(p => p.Company);
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Positions)
            .WithMany(p => p.Employees);
        modelBuilder.Entity<Position>()
            .HasMany(e => e.Children)
            .WithOne(p => p.Parent)
            .OnDelete(DeleteBehavior.Cascade)
            ;
        modelBuilder.Entity<House>()
            .HasMany(e => e.Employees)
            .WithMany(e => e.Houses);
        modelBuilder.Entity<House>()
            .HasMany(h => h.Documents)
            .WithMany(d => d.Houses);
        modelBuilder.Entity<House>()
            .HasMany(h => h.Companies)
            .WithMany(c => c.Houses);
        modelBuilder.Entity<House>()
            .HasMany(h => h.Cars)
            .WithMany(c => c.Houses);

        modelBuilder.Entity<Car>()
            .HasMany(c => c.Passengers)
            .WithMany(e => e.PassengerCars);
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Drivers)
            .WithMany(e => e.DriverCars);
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Companies)
            .WithMany(e => e.Cars);
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Realisations)
            .WithMany(e => e.Cars);
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Documents)
            .WithMany(e => e.Cars);
        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Image)
            .WithMany(d => d.Offers);
        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Company)
            .WithMany(d => d.Offers);

        modelBuilder.Entity<Recruitment>()
            .HasOne(rp => rp.Offer)
            .WithOne(e => e.Recruitment);
        modelBuilder.Entity<Offer>()
            .HasOne(rp => rp.Position)
            .WithMany(e => e.Offers);
        modelBuilder.Entity<Recruitment>()
            .HasMany(r => r.Variants)
            .WithMany(v => v.Recruitments);
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.PreviousStatus)
            .WithMany(ps => ps.PreviousEmployees);

        modelBuilder.Entity<Recruitment>()
            .HasMany(s => s.DocumentTypes)
            .WithMany(e => e.Recruitments);
        modelBuilder.Entity<Plan>()
            .HasMany(p => p.Employees)
            .WithMany(e => e.Plans);
        modelBuilder.Entity<Plan>()
            .HasOne(p => p.Company)
            .WithMany(e => e.Plans);
    }
    
    private void Seed()
    {

    }
}
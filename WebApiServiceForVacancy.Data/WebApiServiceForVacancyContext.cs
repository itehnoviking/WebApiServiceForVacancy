using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.Data;

public class WebApiServiceForVacancyContext : DbContext
{
    private readonly IConfiguration _configuration;

    public WebApiServiceForVacancyContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
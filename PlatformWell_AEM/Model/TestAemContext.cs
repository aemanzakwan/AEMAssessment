using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlatformWell_AEM.Model;

public partial class TestAemContext : DbContext
{
    public TestAemContext()
    {
    }

    public TestAemContext(DbContextOptions<TestAemContext> options)
        : base(options)
    {
    }

    public DbSet<Platform> Platform { get; set; }

    public DbSet<Well> Well { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\Local;Initial Catalog=TestAEM;Integrated Security=True");
        }
    }
}

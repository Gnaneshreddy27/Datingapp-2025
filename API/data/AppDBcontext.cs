using System;
using API.entities;
using Microsoft.EntityFrameworkCore;

namespace API.data;

public class AppDBcontext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}

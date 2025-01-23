using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Web.Security;
using StudentManagerment2.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base("ApplicationDbContext")
    {
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }


}
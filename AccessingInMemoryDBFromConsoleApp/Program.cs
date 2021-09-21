using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.EntityFrameworkCore;


namespace AccessingInMemoryDBFromConsoleApp
{
    // What goes into the db
    public class ExampleDbModel
    {
        [Key] public int id { get; set; }
        public string name { get; set; }
    }
    
    // how we connect to the db from C#

    public class MyContext : DbContext
    {
        // only for sqlite to save to db on disk
        public string DbPath { get; private set; }
        public MyContext()
        {
            // replace the filepath cruft when the project is built so it always uses our db inside at 
            // AccessingInMemoryDBFromConsoleApp/AccessingInMemoryDBFromConsoleApp/test.db
            DbPath = Directory.GetCurrentDirectory()
                .Replace("/bin/Debug/net5.0", "")
                .Replace("\\bin\\Debug\\net5.0", "")
                .Replace("\\bin\\Debug\\net5.0", "") +
                $"{System.IO.Path.DirectorySeparatorChar}test.db";
            
            // display the filepath it's using for debugging purposes and so you can find it.
            Console.WriteLine(DbPath);
        }

        // What we'll access when we want to query the context
        public DbSet<ExampleDbModel> ExampleDbModels { get; set; }

        // Used for creating our table in the db during migrations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExampleDbModel>().ToTable("ExampleDbModels");
        }
        
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }


    class Program
    {
        static void Main(string[] args)
        {
            SaveSomeStuffToTheDb();
            Console.WriteLine("Done saving to SQLite!");
        }

        static void SaveSomeStuffToTheDb()
        {
            using (var dbConn = new MyContext())
            {
                dbConn.Add(new ExampleDbModel() { name = "test"});
                dbConn.SaveChanges();   
            }
        }
    }
}
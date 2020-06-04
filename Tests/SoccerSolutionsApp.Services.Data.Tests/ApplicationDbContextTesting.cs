namespace SoccerSolutionsApp.Services.Data.Tests
{
    using System;
    using System.Reflection;

    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels;

    public class ApplicationDbContextTesting : IDisposable
    {
        private readonly SqliteConnection connection;

        public ApplicationDbContextTesting()
        {
            this.connection = new SqliteConnection("DataSource=:memory:");
            this.InitiliazeDb();
            this.InitializaMapper();
        }

        public ApplicationDbContext DbContext { get; private set; }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }

        private void InitiliazeDb()
        {
            this.connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(this.connection);
            this.DbContext = new ApplicationDbContext(options.Options);
            this.DbContext.Database.EnsureCreated();
        }

        private void InitializaMapper()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

    }
}

namespace EffortBug.Domain.Entities
{
    using System.Data.Common;
    using System.Data.Entity;

    public class MvpDbContext : DbContext
    {
        // Your context has been configured to use a 'MvpDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EffortBug.Domain.Entities.MvpDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MvpDbContext' 
        // connection string in the application configuration file.
        public MvpDbContext()
            : base("name=MvpDbContext")
        { }

        /// <summary>
        /// Constructs a new context instance using the existing connection to connect to a database.
        /// The connection will not be disposed when the context is disposed if <paramref name="contextOwnsConnection" />
        /// is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection"> An existing connection to use for the new context. </param>
        /// <param name="contextOwnsConnection">
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise the caller must dispose the connection.
        /// </param>
        public MvpDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new MyEntity.Configuration());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}
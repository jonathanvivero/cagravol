using SGE.Cagravol.Infrastructure.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.DatabaseInitializers
{
    public sealed class AutoMigrationDatabaseInitializer<TContext> :
         CreateDatabaseIfNotExists<TContext>,
         IDatabaseInitializer<TContext>
         where TContext : SGEContext
    {
        private readonly DbMigrationsConfiguration configuration;

        public AutoMigrationDatabaseInitializer()
        {
            this.configuration = new Configuration();
        }

        public new void InitializeDatabase(TContext context)
        {

            Console.WriteLine("SGE => Comprobacion...");
            Debug.WriteLine("SGE => Comprobacion...");
            if (context.Database.Exists())
            {

                Console.WriteLine("SGE => BD Existe!");
                Debug.WriteLine("SGE => BD Existe!");
                if (!context.Database.CompatibleWithModel(false))
                {
                    if (!this.TryToMigrate())
                    {
                        context.Database.Delete();
                        this.TryToMigrate();
                    }
                }

                this.Seed(context);
            }
            else
            {
                Console.WriteLine("SGE => La BD No existe");
                Debug.WriteLine("SGE => La BD No existe");
                this.TryToMigrate();
            }
        }

        protected override void Seed(TContext context)
        {
            base.Seed(context);
        }

        private bool TryToMigrate()
        {
            var migrateSuccessful = true;
            try
            {
                var migrator = new DbMigrator(configuration);

                migrator.Update();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                migrateSuccessful = false;
            }

            return migrateSuccessful;
        }
    }
}

using MotorPlantDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace MotorPlantDatabaseImplement
{
	public class MotorPlantDatabase : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured == false)
			{
				optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MotorPlantDatabase1;Integrated Security=True;MultipleActiveResultSets=True;");
			}
			base.OnConfiguring(optionsBuilder);
		}

		public virtual DbSet<Component> Components { set; get; }

		public virtual DbSet<Engine> Engines { set; get; }

		public virtual DbSet<EngineComponent> EngineComponents { set; get; }

		public virtual DbSet<Order> Orders { set; get; }

		public virtual DbSet<Client> Clients { set; get; }

		public virtual DbSet<Implementer> Implementers { set; get; }
	}
}
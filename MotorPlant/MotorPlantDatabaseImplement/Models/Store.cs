using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MotorPlantDatabaseImplement.Models
{
	public class Store
	{
		public int Id { get; set; }
		[Required]
		public string StoreName { get; set; }
		[Required]
		public string ResponsibleName { get; set; }
		[Required]
		public DateTime DateCreation { get; set; }
		[ForeignKey("StoreId")]
		public virtual List<StoreComponent> StoreComponents { get; set; }
	}
}

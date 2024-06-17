using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace monoProjekt.Models
{
	public class VehicleMake
	{
		public Guid Id { get; set; }

		public string Abrv {  get; set; }

		public string Name { get; set; }
		public ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();



   
	}
}

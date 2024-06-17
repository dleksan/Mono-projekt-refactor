namespace monoProjekt.Models
{
	public class VehicleMakeModel
	{
		public VehicleMakeDto[] vehicleMakes { get; set; }

        public VehicleModelDto[] vehicleModels { get; set; }


        public string SearchString { get; set; }
        public string SortOrder { get; set; }

        public string SearchStringModel { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}

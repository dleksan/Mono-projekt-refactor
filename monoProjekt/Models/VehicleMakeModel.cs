using monoProjekt.Sorting__Filtering_and_Paging;

namespace monoProjekt.Models
{
	public class VehicleMakeModel
	{
		public VehicleMakeDto[] vehicleMakes { get; set; }

        public VehicleModelDto[] vehicleModels { get; set; }


        public Filter FilterParams { get; set; }
        public Sort SortParams { get; set; }
        public Paging PagingInfo { get; set; }
    }
}

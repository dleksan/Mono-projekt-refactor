using monoProjekt.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace monoProjekt.Services
{
	public interface IVehicleService
	{
		Task<VehicleMakeDto[]> GetMakes(string sortOrder, string searchString,int pageIndex, int pageSize);

		Task<VehicleModelDto[]> GetModels(string sortOrderModel,string searchStringModel);

       
        Task<bool> AddMake(VehicleMakeDto newMakeDto);

        Task<bool> AddModel(VehicleModelDto newModelDto);

        Task<bool> DeleteMake(Guid id);

        Task<bool> EditMake(VehicleMakeDto makeDto);

        Task<bool> EditModel(VehicleModelDto modelDto);

        Task<bool> DeleteModel(Guid id);

        Task<int> GetMakesCount(string searchString);
        
    }
}

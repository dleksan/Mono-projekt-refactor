using monoProjekt;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using monoProjekt.Sorting__Filtering_and_Paging;
using Projekt.Sevice.DatabaseModels;


namespace monoProjekt.Services
{
    public interface IVehicleMakeService
	{
		Task<VehicleMake[]> GetMakes(Sort sortParams, Filter filterParams, Paging pagingParams);
       
        Task<bool> AddMake(VehicleMake newMakeDto);

        Task<bool> AddModel(VehicleModel newModelDto);

        Task<bool> DeleteMake(Guid id);

        Task<bool> EditMake(VehicleMake makeDto);

        Task<int> GetMakesCount(Filter filterParams);
        
    }
}

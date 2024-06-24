using monoProjekt.Sorting__Filtering_and_Paging;
using Projekt.Sevice.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Sevice.Services
{
    public interface IVehicleModelService
    {      
        Task<VehicleModel[]> GetModels(Sort sortParams, Filter filterParams);

        Task<bool> AddModel(VehicleModel newModelDto);   

        Task<bool>  EditModel(VehicleModel modelDto);

        Task<bool> DeleteModel(Guid id);

    
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using monoProjekt.Sorting__Filtering_and_Paging;
using Projekt.Sevice.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Sevice.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public VehicleModelService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VehicleModel[]> GetModels(Sort sortParams, Filter filterParams)
        {

            var models = from m in _context.VehicleModels
                         select m;

            if (!string.IsNullOrEmpty(filterParams.SearchStringModel))
            {
                models = models.Where(m => m.Name.Contains(filterParams.SearchStringModel));
            }

            switch (sortParams.SortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.Name);
                    break;
                default:
                    models = models.OrderBy(m => m.Name);
                    break;
            }

            return _mapper.Map<VehicleModel[]>(await models.ToArrayAsync());
            //  await _context.SaveChangesAsync();         
        }



        public async Task<bool> AddModel(VehicleModel newModelDto)
        {
            var newModel = _mapper.Map<VehicleModel>(newModelDto);
            newModel.Id = Guid.NewGuid();

            _context.VehicleModels.Add(newModel);

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

       

        public async Task<bool> EditModel(VehicleModel modelDto)
        {
            var model = _mapper.Map<VehicleModel>(modelDto);

            _context.VehicleModels.Update(model);

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

       

        public async Task<bool> DeleteModel(Guid id)
        {
            var model = await _context.VehicleModels.FindAsync(id);

            if (model == null)
            {
                return false;
            }

            _context.VehicleModels.Remove(model);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}


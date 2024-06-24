using AutoMapper;

using Microsoft.EntityFrameworkCore;
using monoProjekt.Sorting__Filtering_and_Paging;
using Projekt.Sevice.DatabaseModels;
using System.Numerics;

namespace monoProjekt.Services
{
    public class VehicleMakeService:IVehicleMakeService
	{
		private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        

        public VehicleMakeService(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
            _mapper = mapper;
        }

        public async Task<VehicleMake[]> GetMakes(Sort sortParams, Filter filterParams, Paging pagingParams)
        {
            var makes = from m in _context.VehicleMakes
                        select m;

            if (!string.IsNullOrEmpty(filterParams.SearchString))
            {
                makes = makes.Where(m => m.Name.Contains(filterParams.SearchString));
            }

            switch (sortParams.SortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(m => m.Name);
                    break;
                default:
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }

            var pagedMakes = await makes.Skip((pagingParams.PageIndex - 1) * pagingParams.PageSize).Take(pagingParams.PageSize).ToArrayAsync();
            return _mapper.Map<VehicleMake[]>(pagedMakes);
        }


        public async Task<int> GetMakesCount(Filter filterParams)
        {
            var query = _context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(filterParams.SearchString))
            {
                query = query.Where(m => m.Name.Contains(filterParams.SearchString));
            }

            return await query.CountAsync();
        }


     

        public async Task<bool> AddMake(VehicleMake newMakeDto)
        {
            var newMake = _mapper.Map<VehicleMake>(newMakeDto);
            //var item2 = _mapper.Map<VehicleMakeDto>(newItem);

            newMake.Id = Guid.NewGuid();


            _context.VehicleMakes.Add(newMake);
            var result = await _context.SaveChangesAsync();

            return result > 0;


        }
        

        public async Task<bool> AddModel(VehicleModel newModelDto)
        {
            var newModel = _mapper.Map<VehicleModel>(newModelDto);
            newModel.Id = Guid.NewGuid();

            _context.VehicleModels.Add(newModel);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EditMake(VehicleMake makeDto)
        {
            var make = _mapper.Map<VehicleMake>(makeDto);
            //_context.Attach(item).Property(x => x.Name).IsModified = true;

            //_context.Attach(item).Property(x => x.Abrv).IsModified = true;

            _context.VehicleMakes.Update(make);

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

    

        public async Task<bool> DeleteMake(Guid id)

        {
            var item = await _context.VehicleMakes.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            var relatedModels = _context.VehicleModels.Where(VehicleModel => VehicleModel.MakeId == id);
             _context.VehicleModels.RemoveRange(relatedModels);
             _context.VehicleMakes.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

       
    }
}




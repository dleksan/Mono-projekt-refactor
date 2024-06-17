using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using monoProjekt.Data;
using monoProjekt.Models;
using System.Numerics;

namespace monoProjekt.Services
{
	public class VehicleService:IVehicleService
	{
		private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        

        public VehicleService(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
            _mapper = mapper;
        }

		public async Task<VehicleMakeDto[]> GetMakes(string sortOrder, string searchString, int pageIndex, int pageSize)
		{
            var makes = from m in _context.VehicleMakes
                        select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                makes = makes.Where(m => m.Name.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(m => m.Name);
                    break;
                default:
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }

			
			var pagedMakes =await makes.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArrayAsync();

			return _mapper.Map<VehicleMakeDto[]>(pagedMakes); 
                
		}

        public async Task<int> GetMakesCount(string searchString)
        {
            var query = _context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString));
            }

            return await query.CountAsync();
        }

        public async Task<VehicleModelDto[]> GetModels(string sortOrderModel, string searchStringModel)
        {

            var models = from m in _context.VehicleModels
                         select m;

            if (!string.IsNullOrEmpty(searchStringModel))
            {
                models = models.Where(m => m.Name.Contains(searchStringModel));


            }

            switch (sortOrderModel)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.Name);
                    break;
                default:
                    models = models.OrderBy(m => m.Name);
                    break;
            }


            //  await _context.SaveChangesAsync();

            return _mapper.Map<VehicleModelDto[]>(await models.ToArrayAsync());
        }


        public async Task<bool> AddMake(VehicleMakeDto newMakeDto)
        {
            var newMake = _mapper.Map<VehicleMake>(newMakeDto);
            //var item2 = _mapper.Map<VehicleMakeDto>(newItem);

            newMake.Id = Guid.NewGuid();


            _context.VehicleMakes.Add(newMake);
            var result = await _context.SaveChangesAsync();

            return result > 0;


        }
        

        public async Task<bool> AddModel(VehicleModelDto newModelDto)
        {
            var newModel = _mapper.Map<VehicleModel>(newModelDto);
            newModel.Id = Guid.NewGuid();

            _context.VehicleModels.Add(newModel);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EditMake(VehicleMakeDto makeDto)
        {
            var make = _mapper.Map<VehicleMake>(makeDto);
            //_context.Attach(item).Property(x => x.Name).IsModified = true;

            //_context.Attach(item).Property(x => x.Abrv).IsModified = true;

            _context.VehicleMakes.Update(make);

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

      public async Task<bool> EditModel(VehicleModelDto modelDto)
        {
            var model = _mapper.Map<VehicleModel>(modelDto);

            _context.VehicleModels.Update(model);

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




using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using monoProjekt.Models;
using monoProjekt.Services;
using SQLitePCL;
using System.Net;
using static Ninject.Web.AspNetCore.BindingIndex;
using Projekt.Sevice;
using Projekt.Sevice.Services;
using monoProjekt.Sorting__Filtering_and_Paging;
using Projekt.Sevice.DatabaseModels;





namespace monoProjekt.Controllers
{
    public class VehicleMakeController:Controller
	{
		private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;

        private const int PageSize = 2;

        public VehicleMakeController(IVehicleMakeService vehicleService, IVehicleModelService vehicleModelService, IMapper mapper)
		{
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleService;
            _mapper = mapper;

		}

		public async Task<IActionResult> Index(Sort sortParams, Filter filterParams, Paging pagingParams)
        {
            ViewData["CurrentSort"] = sortParams.SortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortParams.SortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = filterParams.SearchString;
            ViewData["CurrentFilterModel"] = filterParams.SearchStringModel;



            var makes = await _vehicleMakeService.GetMakes(sortParams, filterParams, pagingParams);
            var makesDto = _mapper.Map<VehicleMakeDto[]>(makes);

            if (makes == null)
            {
                return NotFound();
            }



            var models = await _vehicleModelService.GetModels(sortParams, filterParams);
            var modelsDto = _mapper.Map<VehicleModelDto[]>(models);

            if (models == null)
            {
                return NotFound();
            }

            var makesCount = await _vehicleMakeService.GetMakesCount(filterParams);

            var totalPages = (int)Math.Ceiling(makesCount / (double)PageSize);



            var model = new VehicleMakeModel()
            {
                vehicleMakes = makesDto,
                vehicleModels = modelsDto,
                FilterParams = filterParams,
                SortParams = sortParams,
                PagingInfo = new Paging
                {
                    PageSize = 2,
                    PageIndex = pagingParams.PageIndex,
                    TotalPages = totalPages
                }
            };


            return View(model);
        }

    

        [ValidateAntiForgeryToken]
		public async Task<IActionResult> AddMake(VehicleMakeDto newItem)
		{
            
            if (newItem.Name==null || newItem.Abrv==null)
            {
                return BadRequest();
            }
            var item = _mapper.Map<VehicleMake>(newItem);

            var result=await _vehicleMakeService.AddMake(item);

            if (!result)
            {
                return BadRequest("Could not add item.");
            }          

             return RedirectToAction("Index");
           
        }

		public async Task<IActionResult> AddModel(VehicleModelDto newModel)
		{
            if (newModel.Name == null || newModel.Abrv==null)
            {
                return BadRequest();
            }
            var newModelDto = _mapper.Map<VehicleModel>(newModel);

            var result = await _vehicleModelService.AddModel(newModelDto);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new model");
            }
          

            return RedirectToAction("Index");
        }

		[HttpPost]
		public async Task<IActionResult> DeleteMake(Guid Id)
		{
           
            if (Id == null)
            {
                return BadRequest();
            }

            var result = await _vehicleMakeService.DeleteMake(Id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


		[HttpPost]
		public async Task<IActionResult> EditMake(VehicleMakeDto makeDto)
		{
            var item = _mapper.Map<VehicleMake>(makeDto);

            var result = await _vehicleMakeService.EditMake(item);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating make");
            }

            return RedirectToAction("Index");
        }

    }
}

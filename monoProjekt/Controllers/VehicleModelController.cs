using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using monoProjekt.Models;
using monoProjekt.Services;
using monoProjekt.Sorting__Filtering_and_Paging;
using Projekt.Sevice.DatabaseModels;
using Projekt.Sevice.Services;

namespace monoProjekt.Views.Vehicle
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;

        private const int PageSize = 2;
        public VehicleModelController(IVehicleMakeService vehicleService, IVehicleModelService vehicleModelService, IMapper mapper)
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

        public async Task<IActionResult> EditModel(VehicleModelDto model)
        {
            var item = _mapper.Map<VehicleModel>(model);

            var result = await _vehicleModelService.EditModel(item);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating model");
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DeleteModel(Guid id)
        {


            var result = await _vehicleModelService.DeleteModel(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");


        }
    }

}

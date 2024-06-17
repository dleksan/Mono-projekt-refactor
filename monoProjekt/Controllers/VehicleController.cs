using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using monoProjekt.Models;
using monoProjekt.Services;
using SQLitePCL;
using System.Net;
using static Ninject.Web.AspNetCore.BindingIndex;




namespace monoProjekt.Controllers
{
	public class VehicleController:Controller
	{
		private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        private const int PageSize = 2;
        public VehicleController(IVehicleService vehicleService, IMapper mapper)
		{
			_vehicleService = vehicleService;
            _mapper = mapper;

		}
		public async Task<IActionResult> Index(string sortOrder, string searchString, string searchStringModel,int pageIndex=1 )
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

			
           // ViewData["CurrentSortModel"] = sortOrderModel;
            //ViewData["NameSortParmModel"] = String.IsNullOrEmpty(sortOrderModel) ? "name_desc" : "";
            ViewData["CurrentFilterModel"] = searchStringModel;

         
    

            var makes = await _vehicleService.GetMakes(sortOrder,searchString,pageIndex,PageSize);
          
            if (makes == null)
            {
                return NotFound();
            }
           


            var models = await _vehicleService.GetModels(sortOrder,searchStringModel);

            if (models == null)
            {
                return NotFound();
            }

            var makesCount = await _vehicleService.GetMakesCount(searchString);

            var totalPages = (int)Math.Ceiling(makesCount / (double)PageSize);

            if(totalPages<2)
            {
                pageIndex = 1;
            }




            var model = new VehicleMakeModel()
            {
                vehicleMakes = makes,
                vehicleModels = models,
                SearchString = searchString,
                SortOrder = sortOrder,
                SearchStringModel = searchStringModel,
                PageIndex = pageIndex,
                TotalPages = (int)Math.Ceiling(makesCount / (double)PageSize)
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
            var result=await _vehicleService.AddMake(newItem);

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

            var result = await _vehicleService.AddModel(newModel);

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

            var result = await _vehicleService.DeleteMake(Id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }



		[HttpPost]
		public async Task<IActionResult> EditMake(VehicleMakeDto itemDto)
		{
        
            var result = await _vehicleService.EditMake(itemDto);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating make");
            }

            return RedirectToAction("Index");
        }

		public async Task<IActionResult> EditModel(VehicleModelDto model)
		{
				

            var result = await _vehicleService.EditModel(model);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating model");
            }

            return RedirectToAction("Index");

        }

		public async Task<IActionResult> DeleteModel(Guid id)
		{


            var result = await _vehicleService.DeleteModel(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");

            
        }

	}
}

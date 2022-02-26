using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ServitalleresWeb.Repository.IRepository;
using ServitalleresWeb.Models;

namespace ServitalleresWeb.Controllers
{
    [Authorize]
    public class VehicleBrandsController : Controller
    {
        private readonly IVehicleBrandRepository _vehicleBrandRepo;

        public VehicleBrandsController(IVehicleBrandRepository vehicleBrandRepo)
        {
            _vehicleBrandRepo = vehicleBrandRepo;
        }
        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(new VehicleBrand() { });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            VehicleBrand obj = new VehicleBrand();

            if (id == null)
            {
                // This will be true for Insert/Create
                ViewBag.Name = HttpContext.User.Identity.Name;
                return View(obj);
            }

            //Flow will come here for update
            obj = await _vehicleBrandRepo.GetAsync(SD.VehicleBrandPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(VehicleBrand obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _vehicleBrandRepo.CreateAsync(SD.VehicleBrandPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _vehicleBrandRepo.UpdateAsync(SD.VehicleBrandPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllVehicleBrands()
        {
            return Json(new { data = await _vehicleBrandRepo.GetAllAsync(SD.VehicleBrandPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _vehicleBrandRepo.DeleteAsync(SD.VehicleBrandPath, id, HttpContext.Session.GetString("JWToken"));

            if (status)
            {
                return Json(new { success = true, message = "Eliminación exitosa"});
            }

            return Json(new { success = false,  message = "Eliminación fallida"});
        }
    }
}

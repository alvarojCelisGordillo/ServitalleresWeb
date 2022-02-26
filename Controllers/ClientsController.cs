using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ServitalleresWeb.Repository.IRepository;
using ServitalleresWeb.Models;

namespace ServitalleresWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepo;

        public ClientsController(IClientRepository clientRepo)
        {
            _clientRepo = clientRepo;
        }

        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(new Client() {});
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewClient(int? id)
        {
            Client obj = new Client();

            if (id == null)
            {
                return NoContent();
            }

            obj = await _clientRepo.GetAsync(SD.ClientPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(obj);

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            Client obj = new Client();

            if (id == null)
            {
                // This will be true for Insert/Create
                ViewBag.Name = HttpContext.User.Identity.Name;
                return View(obj);
            }

            //Flow will come here for update
            obj = await _clientRepo.GetAsync(SD.ClientPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
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
        public async Task<IActionResult> Upsert(Client obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _clientRepo.CreateAsync(SD.ClientPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _clientRepo.UpdateAsync(SD.ClientPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }


        public async Task<IActionResult> GetAllClients()
        {
            return Json(new {data = await _clientRepo.GetAllAsync(SD.ClientPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _clientRepo.DeleteAsync(SD.ClientPath, id, HttpContext.Session.GetString("JWToken"));

            if (status)
            {
                return Json(new { success = true, message = "Eliminación exitosa" });
            }

            return Json(new { success = false, message = "Eliminación fallida" });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using RunGroopWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        //Connect to db context/Repository
        private readonly IRaceRepository _raceRepository;
        private readonly IFileService _fileService;

        public RaceController(IRaceRepository raceRepository, IFileService fileService)
        {
            _raceRepository = raceRepository;
            _fileService = fileService;
        }

        // GET: /races/
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        // GET: /race/detail/<id>
        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        // GET: /race/create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /race/create
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _fileService.UploadFileAsync(raceVm.Image);
                var race = new Race()
                {
                    Title = raceVm.Title,
                    Description = raceVm.Description,
                    Image = result.Uri.AbsoluteUri.ToString(),
                    RaceCategory = raceVm.RaceCategory,
                    Address = new Address
                    {
                        Street = raceVm.Address.Street,
                        City = raceVm.Address.City,
                        State = raceVm.Address.State,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceVm);
        }
    }
}


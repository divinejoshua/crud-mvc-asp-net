using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        //Connect to db context/Repository
        private readonly IClubRepository _clubRepository;
        private readonly IFileService _fileService;

        public ClubController(IClubRepository clubRepository, IFileService fileService)
        {
            _clubRepository = clubRepository;
            _fileService = fileService;
        }

        // GET: /club/
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        // GET: /club/detail/<id>
        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        // GET: /club/create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /club/create
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                //Upload image
                var ImageResult = await _fileService.UploadFileAsync(clubVM.Image);
                var club = new Club()
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = ImageResult.Uri.AbsoluteUri.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    Address = new Address {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }

        //GET: /club/edit
        public async Task<ActionResult> Edit(int id)
        {
            var club  = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVm = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVm);
        }

        //POST: /club/edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null) { 

                //Upload image
                var ImageResult = await _fileService.UploadFileAsync(clubVM.Image);
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = ImageResult.Uri.AbsoluteUri.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address

                };

                _clubRepository.Update(club);

                return RedirectToAction("Index");

            }else{
                return View(clubVM);
            }
        }
    }
}


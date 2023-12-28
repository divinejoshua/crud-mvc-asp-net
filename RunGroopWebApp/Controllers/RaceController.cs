using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        //Connect to db context
        private readonly ApplicationDbContext _context;

        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /races/
        public IActionResult Index()
        {
            List<Race> races = _context.Races.ToList();
            return View(races);
        }

        // GET: /race/detail/<id>
        public IActionResult Detail(int id)
        {
            Race race = _context.Races.Include(addressObj => addressObj.Address).FirstOrDefault(raceObj => raceObj.Id == id);
            return View(race);
        }
    }
}


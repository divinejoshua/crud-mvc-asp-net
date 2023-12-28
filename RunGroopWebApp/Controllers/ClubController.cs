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
    public class ClubController : Controller
    {
        //Connect to db context
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /club/
        public IActionResult Index()
        {
            List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);
        }

        // GET: /club/detail/<id>
        public IActionResult Detail(int id)
        {
            Club club = _context.Clubs.Include(addressObj => addressObj.Address).FirstOrDefault(clubObj => clubObj.Id == id);
            return View(club);
        }
    }
}


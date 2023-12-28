using System;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
	public class ClubRepository : IClubRepository
	{
        //Connect to db context
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Implementation of the Interface

        //Add to the database
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        //Delete from the database
        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        //Get all clubs from the database
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        //Get a club by ID
        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(addressObj => addressObj.Address).FirstOrDefaultAsync(instance => instance.Id == id);
        }

        //Get club by city
        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(instance => instance.Address.City.Contains(city)).ToListAsync();
        }

        //Save to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        //Update instance in database
        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}


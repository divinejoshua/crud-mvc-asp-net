using System;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository
{
	public class RaceRepository : IRaceRepository
	{
        //Connect to db context
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
		{
            _context = context;
        }

        //Implementation of the Interface

        //Add to the database
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        //Delete from the database
        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        //Get all Races from the database
        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        //Get all races by city
        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(instance => instance.Address.City.Contains(city)).ToListAsync();
        }

        //Get race by ID
        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(addressObj => addressObj.Address).FirstOrDefaultAsync(instance => instance.Id == id);
        }

        //Get race by ID No Tracking 
        public async Task<Race> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(addressObj => addressObj.Address).AsNoTracking().FirstOrDefaultAsync(instance => instance.Id == id);
        }

        //Save to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        //Update instance in database
        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}


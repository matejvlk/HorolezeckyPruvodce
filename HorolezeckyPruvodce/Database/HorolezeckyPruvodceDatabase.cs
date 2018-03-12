using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Database
{
    public class HorolezeckyPruvodceDatabase
    {
        readonly SQLiteAsyncConnection database;

        public HorolezeckyPruvodceDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
        }

        public Task<List<Location>> GetAllLocationsAsync()
        {
            return database.Table<Location>().ToListAsync();
        }

        public Task<Location> GetLocationAsync(int locationId)
        {
            return database.Table<Location>().Where(i => i.LocationId == locationId).FirstOrDefaultAsync();
        }

        public Task<List<Location>> SearchLocations(string searchName)
        {
            return database.Table<Location>().Where(i => i.Name.Contains(searchName)).ToListAsync();
        }

        public Task<Area> GetArea(int areaId)
        {
            return database.Table<Area>().Where(i => i.AreaId == areaId).FirstOrDefaultAsync();
        }

        public Task<List<Area>> GetAreasByLocationId(int locationId)
        {
            return database.Table<Area>().Where(i => i.LocationId == locationId).ToListAsync();
        }

        public Task<List<Area>> SearchAreas(string searchName)
        {
            return database.Table<Area>().Where(i => i.Name.Contains(searchName)).ToListAsync();
        }

        public Task<Sector> GetSector(int sectorId)
        {
            return database.Table<Sector>().Where(i => i.SectorId == sectorId).FirstOrDefaultAsync();
        }

        public Task<List<Sector>> GetSectorsByAreaId(int areaId)
        {
            return database.Table<Sector>().Where(i => i.AreaId == areaId).ToListAsync();
        }

        public Task<List<Sector>> SearchSectors(string searchName)
        {
            return database.Table<Sector>().Where(i => i.Name.Contains(searchName)).ToListAsync();
        }

        public Task<List<Route>> GetRoutesBySectorId(int sectorId)
        {
            return database.Table<Route>().Where(i => i.SectorId == sectorId).ToListAsync();
        }

        public Task<List<Route>> SearchRoutes(string searchName)
        {
            return database.Table<Route>().Where(i => i.Name.Contains(searchName)).ToListAsync();
        }

        public Task<Route> GetRouteAsync(int routeId)
        {
            return database.Table<Route>().Where(i => i.RouteId == routeId).FirstOrDefaultAsync();
        }

        public Task<List<Diary>> GetDiariesByUserId(int userId)
        {
            return database.Table<Diary>().Where(i => i.UserId == userId).ToListAsync();
        }

        public Task<int> DeleteDiary(Diary diary)
        {
            return database.DeleteAsync(diary);
        }

        //máme pouze jednoho uživatele
        public Task<User> GetUser()
        {
            return database.Table<User>().FirstOrDefaultAsync();
        }

        public Task<int> SaveUser(User user)
        {
            if (user.UserId == 0)
            {//neexistuje - vytvoříme
                return database.InsertAsync(user);
            }
            else
            {//existuje - updatneme
                return database.UpdateAsync(user);
            }
        }

        public Task<int> SaveDiary(Diary diary)
        {
            if (diary.DiaryId == 0)
            {//neexistuje - vytvoříme
                return database.InsertAsync(diary);
            }
            else
            {//existuje - updatneme
                return database.UpdateAsync(diary);
            }
        }
    }
}

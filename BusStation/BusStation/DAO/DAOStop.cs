using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DAO
{
    public interface DAOStop
    {
        List<Stop> GetAll();
        Stop GetOne(long id_trip, long id_station);
        List<Stop> GetManyBySelector(Predicate<Stop> match);
        void Add(Stop stop);
        void Delete(long id_trip, long id_station);
        bool checkAddStop(Trip trip, DateTime date);
    }
}

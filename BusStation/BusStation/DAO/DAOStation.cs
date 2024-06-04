using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DAO
{
    public interface DAOStation
    {
        Station GetOne(long id);
        List<Station> GetAll();
        List<Station> GetManyBySelector(Predicate<Station> match);
        void Add(Station station);
        void Update(Station station);
        void Delete(long id);

    }
}

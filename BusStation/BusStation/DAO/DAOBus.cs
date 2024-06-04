using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DAO
{
    public interface DAOBus
    {
        Bus GetOne(long id);
        List<Bus> GetAll();
        List<Bus> GetManyBySelector(Predicate<Bus> match);
        void Add(Bus station);
        void Update(Bus station);
        void Delete(long id);
    }
}

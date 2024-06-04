using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DAO
{
    public interface DAOProfile
    {
        Profile GetOne(long id);
        //        List<Profile> GetAll();
        //        List<Profile> GetManyBySelector(Predicate<Profile> match);
        Profile Add(Profile profile);
        void Update(Profile profile);
    }
}

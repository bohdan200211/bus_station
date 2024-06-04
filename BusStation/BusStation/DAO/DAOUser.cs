using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DAO
{
    public interface DAOUser
    {
        User GetOne(long id);
        List<User> GetAll();
        List<User> GetManyBySelector(Predicate<User> match);
        void Add(User user);
        void UpdateUser(User user);
        void DeleteUser(long id);
    }
}

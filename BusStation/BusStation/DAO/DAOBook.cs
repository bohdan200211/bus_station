using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DAO
{
    public interface DAOBook
    {
        List<Book> GetAll();
        void Add(Book book);
        List<Book> GetByUserId(int id_user);

    }
}

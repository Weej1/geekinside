using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALFavorite
    {
        List<FavoriteModel> getFavoriteDocListByEmployeeNumber(int empNumber);
        Boolean deleteFavById(int employeeNumber, int documentId);
        Boolean addFav(int employeeNumber, int documentId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace DAL
{
    public class DALFavorite:IDALFavorite
    {
        public List<FavoriteModel> getFavoriteDocListByEmployeeNumber(int empNumber)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbFav = from f in gikms.Favorites
                              where f.EmployeeNumber.Equals(empNumber)
                              select f;
                List<DAL.Favorite> favdocTempList = dbFav.ToList();
                if (favdocTempList.Count == 0)
                {
                    return null;
                }
                List<FavoriteModel> favDocModelList = new List<FavoriteModel>();
                foreach (Favorite fav in favdocTempList)
                {
                    favDocModelList.Add(new FavoriteModel
                    {
                        Id = fav.Id,
                        EmployeeNumber = fav.EmployeeNumber,
                        DocumentId = fav.DocumentId
                    });
                }
                return favDocModelList;
            }
        }

        public Boolean deleteFavById(int employeeNumber, int documentId)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.Favorite dbFav = (from fav in context.Favorites
                                  where fav.DocumentId.Equals(documentId) && fav.EmployeeNumber.Equals(employeeNumber)
                                  select fav).FirstOrDefault();
            context.DeleteObject(dbFav);
            context.SaveChanges();
            return true;
        }

        public Boolean addFav(int employeeNumber, int documentId)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            DAL.Favorite fav = new DAL.Favorite();
            fav.EmployeeNumber = employeeNumber;
            fav.DocumentId = documentId;
            context.AddToFavorites(fav);
            context.SaveChanges();
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLFavorite
    {
        IDALFavorite favoriteDAL = DALFactory.DataAccess.CreateFavoriteDAL();
        IDALDocument documentDAL = DALFactory.DataAccess.CreateDocumentDAL();

        //增加收藏
        public Boolean addToMyFavorite(int employeeNumber, int documentId)
        {
            return favoriteDAL.addFav(employeeNumber, documentId);
        }

        //删除收藏
        public Boolean deleteMyFavorite(int employeeNumber, int documentId)
        {
            return favoriteDAL.deleteFavById(employeeNumber, documentId);
        }

        //查询收藏
        public List<DocumentModel> getFavoriteDocModelListByPublishNumber(int publisherNumber)
        {
            return documentDAL.getAllFavoriteDocListByPublisherNumber(publisherNumber);
        }

        //判断是否已经收藏过
        public Boolean isFavorite(int empNumber, int docid)
        {
            List<FavoriteModel> favModel = favoriteDAL.getFavoriteDocListByEmployeeNumber(empNumber);
            Boolean result = false;
            if (favModel == null)
            {
                return result;
            }
            foreach (FavoriteModel fav in favModel)
            {
                if (fav.DocumentId.Equals(docid))
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }

    }
}

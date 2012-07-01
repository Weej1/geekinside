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

    }
}

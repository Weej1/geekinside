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
        public Boolean addToMyFavorite(int publisherNumber,int documentId)
        {
            return true;
        }

        //删除收藏
        public Boolean deleteMyFavorite(int publisherNumber, int documentId)
        {
            return true;
        }

        //查询收藏
        public List<DocumentModel> getFavoriteDocModelListByPublishNumber(int publisherNumber)
        {
            return documentDAL.getAllFavoriteDocListByPublisherNumber(publisherNumber);
        }

    }
}

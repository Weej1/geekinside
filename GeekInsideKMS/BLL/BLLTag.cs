using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLTag
    {
        IDALTag tagDAL = DALFactory.DataAccess.CreateTagDAL();

        public List<TagModel> getTop50TagModelList()
        {
            return tagDAL.getTop50TagModelList();
        }
    }
}

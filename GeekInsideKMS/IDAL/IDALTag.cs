using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALTag
    {
        List<TagModel> getTagModelListByDocId(int docid);
        int[] getTagIdArrayByDocId(int docid);
        string getTagNameByTagId(int tagid);        List<TagModel> getTop50TagModelList();
        int GetTagIdByTagName(string tagName);
        int AddTag(string tagName);
        void AddTagOfDoc(int tagId, int documentId);
    }
}

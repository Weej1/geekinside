﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model.Models;

namespace DAL
{
    public class DALTag:IDALTag
    {
        public List<TagModel> getTagModelListByDocId(int docid)
        {
            List<TagModel> tagModelList = new List<TagModel>();
            int[] tagIdArray = getTagIdArrayByDocId(docid);
            foreach (int tagid in tagIdArray)
            {
                tagModelList.Add(new TagModel
                {
                    Id = tagid,
                    TagName = getTagNameByTagId(tagid)
                });
            }
            return tagModelList;
        }

        public int[] getTagIdArrayByDocId(int docid)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var dbTags = from t in gikms.DocumentTags
                             where t.DocumentId.Equals(docid)
                             select t;
                List<DAL.DocumentTag> tagsTempList = dbTags.ToList();
                int[] tagIdArray = new int[tagsTempList.Count];
                for (int i = 0; i < tagsTempList.Count;i++ )
                {
                    tagIdArray[i] = tagsTempList[i].TagId;
                }
                return tagIdArray;
            }
        }

        public string getTagNameByTagId(int tagid)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var tagName = from tn in gikms.Tags
                              where tn.Id.Equals(tagid)
                              select tn;
                return tagName.ToString();
            }
        }
    }
}
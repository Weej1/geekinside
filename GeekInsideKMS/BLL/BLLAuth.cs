using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLAuth
    {
        //emp是否有权查看此文档
        public Boolean ifEmpCanViewThisDoc(int empNumber, int docid)
        {
            DocumentModel tempDocModel = new BLLDocument().getDocumentById(docid);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(empNumber);
            BLLUserAccount bllAccount = new BLLUserAccount();
            if (tempDocModel.PublisherNumber.Equals(empNumber))
            {
                return true;
            }
            else
            {
                switch (tempDocModel.AuthLevel)
                {
                    //所有人都能看和下载
                    case 1:
                    //外部的人不能下载
                    case 2:
                        return true;
                    //外部的人不能看，也不能下载
                    case 3:
                    //外部的人不能看，也不能下载，内部人只能看不能下载
                    case 4:
                        if (empModel.DepartmentId.Equals(bllAccount.GetUserByEmpNumber(tempDocModel.PublisherNumber).DepartmentId))
                        {
                            return true;
                        }
                        return false;
                }
            }
            return false;
        }

        //emp是否有权下载此文档
        public Boolean ifEmpCanDownlaodThisDoc(int empNumber, int docid)
        {
            DocumentModel tempDocModel = new BLLDocument().getDocumentById(docid);
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(empNumber);
            BLLUserAccount bllAccount = new BLLUserAccount();
            if (tempDocModel.PublisherNumber.Equals(empNumber))
            {
                return true;
            }
            else
            {
                switch (tempDocModel.AuthLevel)
                {
                    //所有人都能看和下载
                    case 1:
                        return true;
                    //外部的人不能下载
                    case 2:
                    //外部的人不能看，也不能下载
                    case 3:
                        if (empModel.DepartmentId.Equals(bllAccount.GetUserByEmpNumber(tempDocModel.PublisherNumber).DepartmentId))
                        {
                            return true;
                        }
                        return false;
                    //外部的人不能看，也不能下载，内部人只能看不能下载
                    case 4:
                        return false;
                }
            }
            return false;
        }

        //过滤LIST<DocumentModel>：过滤到emp无权看的文档
        public List<DocumentModel> documentFilter(int empNumber, List<DocumentModel> docModelList)
        {
            UserEmployeeModel empModel = new BLLUserAccount().GetUserByEmpNumber(empNumber);
            List<DocumentModel> newDocModelList = new List<DocumentModel>();
            BLLUserAccount bllAccount = new BLLUserAccount();
            foreach (DocumentModel tempDocModel in docModelList)
            {
                if (tempDocModel.PublisherNumber.Equals(empNumber))
                {
                    newDocModelList.Add(tempDocModel);
                }
                else
                {
                    switch (tempDocModel.AuthLevel)
                    {
                        //所有人都能看和下载
                        case 1:
                        //外部的人不能下载
                        case 2:
                            newDocModelList.Add(tempDocModel);
                            break;
                        //外部的人不能看，也不能下载
                        case 3:
                        //外部的人不能看，也不能下载，内部人只能看不能下载
                        case 4:
                            if (empModel.DepartmentId.Equals(bllAccount.GetUserByEmpNumber(tempDocModel.PublisherNumber).DepartmentId))
                            {
                                newDocModelList.Add(tempDocModel);
                            }
                            break;
                    }
                }
            }
            return newDocModelList;
        }
    }
}

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    人员管理 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript" src="../../Scripts/jquery-1.7.2.min.js">
    function EmailValidate()
    {
           var search_str = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
           var email_val = $("#email").val();
           if (!search_str.test(email_val)) {
               alert("请输入正确的邮件地址 !");
               $('#email').focus();
               return false;
           }
           return true;
    }

 </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div class="admin-intro">
                编写员工信息</div>
            <div class="FilterPersonsField">
                 <form action="/Employee/doCreateUser" name="form" method="post">                 
                员工编号：<span>
                    <input name="employeeNumber" value="<%: ViewData["employeeNumber"]%>">
                </span>
                <br />
                设置为Manager：<span>
                    <select name="isManager" style="vertical-align: middle;">
                        <option value="0">否</option>
                        <option value="1">是</option>
                    </select>
                </span>

                <br />

                所属部门：<span>
                    <select name="dept_name" style="vertical-align: middle;">
                        <% if (ViewData["deptList"].Equals("nodata"))
                           { %>
                        <option value="0">尚未添加部门</option>
                        <% }
                           else
                           { %>
                        <% IList<Model.Models.DepartmentModel> deptList = (IList<Model.Models.DepartmentModel>)ViewData["deptList"]; %>
                        <%foreach (var dept in deptList)
                          {  %>
                        <option value="<%: dept.Id %>">
                            <%: dept.DepartmentName %></option>
                        <% } %>
                        <% } %>
                    </select>
                </span>


                设置为审核员：<span>
                    <select name="isChecker" style="vertical-align: middle;">
                        <option value="0">否</option>
                        <option value="1">是</option>
                    </select>
                </span>

                <br />
                员工姓名： <span>
                    <input name="name" value="<%: ViewData["name"]%>">
                </span>


                <br />

                邮件地址：<span>
                    <input name="email" id="email" value="<%: ViewData["email"]%>">
                </span>

                <% if (TempData["employeeNumberErrorMsg"] != "")
                   { %>
                <p style="color:red;font-weight:bold;"><%: TempData["employeeNumberErrorMsg"]%></p>
                <% } %>

                <br />

                移动电话：<span>
                    <input name="phone" value="<%: ViewData["phone"]%>">
                </span>

                <% if (TempData["phoneErrorMsg"] != "")
                   { %>
                <p style="color:red;font-weight:bold;"><%: TempData["phoneErrorMsg"]%></p>
                <% } %>

                <br />


            </div>
            <div id="PersonsTable">
                <form name="persons_form" class="PersonsForm" method="post">
                                
                        </tr>
                    </tbody>
                </table>
                <p>
                <input type="submit" class="button" value="添加员工" onclick="return EmailValidate()"><span style="margin-left: 10px;"></span></p>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

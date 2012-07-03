<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" %>

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
                    <div class="row">
                        <div class="field">
                            <label class="horizontal">
                                <span>员工编号：</span>
                            </label>
                            <div>
                                <%: ViewData["employeeNumber"]%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="field">
                            <label class="horizontal">
                                <span>设置为Manager：</span>
                            </label>
                            <div>
                                <select name="isManager" style="vertical-align: middle;">
                                    <option value="0">否</option>
                                    <option value="1">是</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="field">
                            <label class="horizontal">
                                <span>所属部门：</span>
                            </label>
                            <div>
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
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="field">
                            <label class="horizontal">
                                <span>设置为审核员：</span>
                            </label>
                            <div>
                                <select name="isChecker" style="vertical-align: middle;">
                                    <option value="0">否</option>
                                    <option value="1">是</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="field">
                            <label class="horizontal">
                                <span>员工姓名： </span>
                            </label>
                            <div>
                                <input name="name" value="<%: ViewData["name"]%>" size="12" style="color: #76797C;">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="field">
                            <label class="horizontal">
                                <span>邮件地址：</span>
                            </label>
                            <div>
                                <input name="email" id="Text1" value="<%: ViewData["email"]%>" size="12" style="color: #76797C;">
                                <% if (TempData["employeeNumberErrorMsg"] != "")
                                   { %>
                                <p style="color: red; font-weight: bold;">
                                    <%: TempData["employeeNumberErrorMsg"]%></p>
                                <% } %>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="field">
                            <label class="horizontal">
                                <span>移动电话：</span>
                            </label>
                            <div>
                                <input name="phone" value="<%: ViewData["phone"]%>" size="12" style="color: #76797C;">
                                <% if (TempData["phoneErrorMsg"] != "")
                                   { %>
                                <p style="color: red; font-weight: bold;">
                                    <%: TempData["phoneErrorMsg"]%></p>
                                <% } %>
                            </div>
                        </div>
                    </div>
            </div>
            <div id="PersonsTable">
                <form name="persons_form" class="PersonsForm" method="post">                        
                <p>
                <input type="submit" class="button" value="添加员工" onclick="return EmailValidate()"><span
                        style="margin-left: 10px;"></span>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

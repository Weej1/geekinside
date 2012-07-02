<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    人员管理 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div class="admin-intro">
                关于人员信息的添加、修改、删除和其它操作</div>
            <p>
                <input type="button" class="button" value="添加员工" onclick="/Employee/CreateUser"><span style="margin-left: 10px;"></span><input
                    type="button" class="button" value="批量添加员工"></p>
            <div class="FilterPersonsField">
                <form name="filter_persons_form" method="post" action="/Employee/CreateUser">
                <strong>筛选/搜索：</strong> <span>部门:
                    <select name="dept_name" style="vertical-align: middle;">
                        <% if (ViewData["deptList"].Equals("nodata"))
                           { %>
                        <option value="0">尚未添加部门</option>
                        <% }
                           else
                           { %>
                        <% IList<Model.Models.DepartmentModel> deptList = (IList<Model.Models.DepartmentModel>)ViewData["deptList"]; %>
                        <option value="0">
                            所有部门</option>
                        <%foreach (var dept in deptList)
                          {  %>
                        <option value="<%: dept.Id %>">
                            <%: dept.DepartmentName %></option>
                        <% } %>
                        <% } %>
                    </select>
                </span><span>
                    <input class="SearchKey" name="search_key" size="12" value="登录名/姓名" style="color: #76797C;">
                </span><span>
                    <input class="button filterPersons" type="submit" name="filter_persons" value="筛选/搜索"  />
                </span>
                </form>
            </div>
            <div id="PersonsTable">
                <form name="persons_form" class="PersonsForm" method="post">
                <table class="listing">
                    <thead>
                        <tr>
                            <th>
                                <a class="SelectAll" href="#">全选</a> <a class="UnSelectAll hidden" href="#">不选</a>
                            </th>
                            <th>
                                员工号
                            </th>
                            <th>
                                姓名
                            </th>
                            <th>
                                部门
                            </th>
                            <th>
                                部门岗位
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <% if (ViewData["empList"].Equals("nodata"))
                           { %>
                        <p style="color: red;">
                            暂无员工信息</p>
                        <% }
                           else
                           { %>
                        <% List<Model.Models.UserEmployeeModel> empList = (List<Model.Models.UserEmployeeModel>)ViewData["empList"]; %>
                        <% foreach (var empDetail in empList)
                           { %>
                        <tr class="even">
                            <td>
                                <input type="checkbox" class="checkitem" name="selected_persons" value="admin-admin">
                            </td>
                            <td>
                                <a href="#">
                                    <%: empDetail.EmployeeNumber %></a>
                            </td>
                            <td>
                                <%: empDetail.Name %>
                            </td>
                            <td>
                            <% List<Model.Models.DepartmentModel> deptList = (List<Model.Models.DepartmentModel>)ViewData["deptList"]; %>
                            <% string deptName=""; %>
                                <% foreach (var dept in deptList)
                                   {%>
                                    <% if (empDetail.DepartmentId == dept.Id)%>
                                        <% deptName = dept.DepartmentName;%>
                                <%} %>
                                <%: deptName%>
                            </td>
                            <td>
                                <%if (empDetail.IsManager == true)
                                  {
                                      Response.Write("经理");
                                  }
                                  else
                                  {
                                      Response.Write("员工");
                                  }%>
                            </td>
                            <td>
                                <a href="/Employee/Edit?empNo=<%: empDetail.EmployeeNumber %>" title="编辑">
                                    <img src="/Content/images/icons/edit.png" alt="编辑"></a>
                                 <a href="/Employee/Delete?empNo=<%: empDetail.EmployeeNumber %>&returnURL=Index">删除</a>
                            </td>
                        </tr>
                        <% } %>
                        <% } %>
                    </tbody>
                </table>
                <p>
                    <input type="button" class="button" value="删除选中"><span style="margin-left: 10px;"></span><input
                        type="button" class="button" value="导出所有员工资料"></p>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

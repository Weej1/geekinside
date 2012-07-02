<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage<Admin.Models.DepartmentRow[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	部门管理 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div class="admin-intro">
                所有部门</div>
            <% if ( TempData["errorMsg"] != ""){ %>
            <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
            <% } %>
            <% if ( TempData["successMsg"] != ""){ %>
            <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
            <% } %>
            <p><input type="button" class="button" value="添加部门"  onclick= "self.location='/Department/Create'"></p>
             <div id="DepartmentsTable">
                <table class="listing">
                    <thead>
                        <tr>
                            <th>
                                部门名称（点击进入编辑界面）
                            </th>
                            <th>
                                部门文件夹
                            </th>
                            <th>
                                部门经理
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <%
                            foreach(var row in Model)
                            { %>
                            <tr class="odd">
                            <td>
                                <%= Html.ActionLink(row.DepartmentName, "Edit", new{Id = row.Id})%>
                            </td>
                            <td>
                                <%= Html.Encode(row.FolderPath) %>
                            </td>
                            <td>
                                王小强,李宁
                            </td>
                            <td>
                                <%= Html.ActionLink("编辑", "Edit", new{Id = row.Id})%>
                            </td>
                        </tr>
                        <% }%>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

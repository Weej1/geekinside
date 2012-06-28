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
                <input type="button" class="button" value="添加员工"><span style="margin-left: 10px;"></span><input
                    type="button" class="button" value="批量添加员工"></p>
            <div class="FilterPersonsField">
                <form name="filter_persons_form" method="post">
                <strong>筛选/搜索：</strong> <span>部门:
                    <select name="filter_dept_name" style="vertical-align: middle;">
                        <option value="0">所有部门</option>
                        <option value="1">财务部</option>
                        <option value="2">市场部</option>
                    </select>
                </span><span>
                    <input class="SearchKey" name="search_key" size="12" value="登录名/姓名" style="color: #76797C;">
                </span><span>
                    <input class="button filterPersons" type="submit" name="filter_persons" value="筛选/搜索">
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
                                部门岗位
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="even">
                            <td>
                                <input type="checkbox" class="checkitem" name="selected_persons" value="admin-admin">
                            </td>
                            <td>
                                <a href="#">007</a>
                            </td>
                            <td>
                                admin
                            </td>
                            <td>
                                系统管理员
                            </td>
                            <td>
                                <a href="#" title="编辑">
                                    <img src="/Content/images/icons/edit.png" alt="编辑"></a>
                            </td>
                        </tr>
                        <tr class="odd">
                            <td>
                                <input type="checkbox" class="checkitem" name="selected_persons" value="admin-admin">
                            </td>
                            <td>
                                <a href="#">007</a>
                            </td>
                            <td>
                                admin
                            </td>
                            <td>
                                系统管理员
                            </td>
                            <td>
                                <a href="#" title="编辑">
                                    <img src="/Content/images/icons/edit.png" alt="编辑"></a>
                                <img class="delCtrl" title="删除" alt="删除" src="images/icons/trash.gif" onclick="#">
                            </td>
                        </tr>
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

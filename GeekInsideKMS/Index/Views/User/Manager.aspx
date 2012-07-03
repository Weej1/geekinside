<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Workshop.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div id="workdesk-content">
                <div id="deskDocsList">
                    <% if ( TempData["errorMsg"] != ""){ %>
                    <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
                    <% } %>
                    <% if ( TempData["successMsg"] != ""){ %>
                    <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
                    <% } %>
                    <p class="discreet">
                        下面是您部门的文件夹列表：</p>
                    <% if (ViewData["folderModelList"].Equals("nodata")){ %>
                        <p style="color:red;">暂无文件夹</p>
                    <%}else{%>
                        <% List<Model.Models.FolderModel> folderModelList = (List<Model.Models.FolderModel>)ViewData["folderModelList"]; %>
                        <p>财务部
                            <span class="docListOperation">
                                <a href="/User/addFolder?parentId=<%:ViewData["outsideFolderId"] %>">添加子文件夹</a>
                            </span>
                        </p>
                        <% foreach (var folderModel in folderModelList)
                        {%>
                            <p>|---- <%:folderModel.FolderName %>
                            <span class="docListOperation">
                                <a href="/User/addFolder?parentId=<%:folderModel.Id %>">添加子文件夹</a>
                                <a href="/User/deleteFolder?folderId=<%:folderModel.Id%>">删除</a>
                            </span>
                            </p>
                            <% if (!folderModel.SubFolders.Equals(null)) {%>
                                <% foreach (var folderModel2 in folderModel.SubFolders){%>
                                    <p style="padding-left:30px;">|---- <%:folderModel2.FolderName %>
                                    <span class="docListOperation">
                                        <a href="/User/addFolder?parentId=<%:folderModel2.Id %>">添加子文件夹</a>
                                        <a href="/User/deleteFolder?folderId=<%:folderModel2.Id%>">删除</a>
                                    </span>
                                    </p>
                                    <% if (!folderModel2.SubFolders.Equals(null)) {%>
                                        <% foreach (var folderModel3 in folderModel2.SubFolders){%>
                                            <p style="padding-left:60px;">|---- <%:folderModel3.FolderName %>
                                            <span class="docListOperation">
                                                <a href="/User/deleteFolder?folderId=<%:folderModel3.Id%>">删除</a>
                                            </span>
                                            </p>
                                        <%} %>
                                    <%} %>
                                <%} %>
                            <%} %>
                        <%} %>
                    <% } %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
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
                    <p>
                        <input type="button" class="button" value="添加文件夹"  onclick= "self.location='/User/addFolder?parentId=2'">
                    </p>
                    <p class="discreet">
                        下面是您部门的文件夹列表：</p>
                    <% if (ViewData["folderModelList"].Equals("nodata")){ %>
                        <p style="color:red;">暂无文件夹</p>
                    <%}else{%>
                        <% List<Model.Models.FolderModel> folderModelList = (List<Model.Models.FolderModel>)ViewData["folderModelList"]; %>
                        <p>财务部</p>
                        <% foreach (var folderModel in folderModelList)
                        {%>
                        <p>|---- <%:folderModel.FolderName %></p>
                        <%}
                         %>
                    <% } %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
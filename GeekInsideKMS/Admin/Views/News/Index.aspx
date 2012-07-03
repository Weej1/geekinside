<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
公告管理 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div class="admin-intro">
                公告列表</div>
            <% if ( TempData["errorMsg"] != ""){ %>
            <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
            <% } %>
            <% if ( TempData["successMsg"] != ""){ %>
            <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
            <% } %>
            <p>
                <input type="button" class="button" value="添加公告"  onclick= "self.location='/News/addNews'"></p>
            
            <div id="NewsTable">
                <form name="news_form" class="NewsForm" method="post" action="/News/deleteMultiNews">
                <table class="listing">
                    <thead>
                        
                        <tr>
                            <th>
                                <a class="SelectAll" href="#">全选</a> <a class="UnSelectAll hidden" href="#">不选</a>
                            </th>
                            <th>
                                置顶
                            </th>
                            <th>
                                公告标题（点击标题可直接进入编辑界面）
                            </th>
                            <th>
                                发布时间
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <% List<Model.Models.SiteNewsModel> newsList = (List<Model.Models.SiteNewsModel>)ViewData["newsList"]; %>
                        <% foreach (var news in newsList) { %>
                        <tr class="odd">
                            <td>
                                <input type="checkbox" class="checkitem" name="selected_news" id="selected_news" value="<%: news.Id %>" />
                            </td>
                            <td>
                                <%: news.IsOnTop %>
                            </td>
                            <td>
                                <a href="/News/Edit?newsid=<%: news.Id %>"><%: news.Title %></a>
                            </td>
                            <td>
                                <%: news.PubTime %>
                            </td>
                            <td>
                                <a href="/News/Edit?newsid=<%: news.Id %>" title="编辑">
                                    <img src="/Content/images/icons/edit.png" alt="编辑" /></a>
                                <a href="/News/Delete?newsid=<%: news.Id %>" title="删除">
                                    <img src="/Content/images/icons/trash.gif" alt="删除" /></a>
                            </td>
                        </tr>
                        <% } %>
                        <div class="page-nav">
                            <% Model.Models.PageModel pageModel = (Model.Models.PageModel)ViewData["pageModel"]; %>
                            当前是第 <%:pageModel.pageNumber %> 页  
                            <% for (int i = 0; i < (pageModel.TotalCount + pageModel.pageSize - 1) / (pageModel.pageSize); i++)
                               {%>
                                <% if (i == pageModel.pageNumber-1)
                                {%>
                                <span><%:i+1 %>  </span>
                                <%}else{
                                 %>
                                <a href="/News/Index?pageNumber=<%:i+1 %>"><%:i+1 %>  </a>
                                <% } %>
                            <% } %>
                        </div>
                    </tbody>
                </table>
                <p>
                    <input type="button" class="button" value="删除选中" onclick="javascript:document.forms[0].submit();"><span style="margin-left: 10px;"></span>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

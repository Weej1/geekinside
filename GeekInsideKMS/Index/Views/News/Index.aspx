<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    公告列表 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftPlaceHolder" runat="server">
    <style type="text/css">
    #portal-column-one{display:none;width:0;}
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div class="page-intro">公告列表！</div>
    <div class="news-list">
        <ul class="newslist">
            
            <% List<Model.Models.SiteNewsModel> newsList = (List<Model.Models.SiteNewsModel>)ViewData["newsList"]; %>
            <% foreach (var news in newsList) { %>
                <li><a href="/News/Detail?newsid=<%:news.Id %>">
                    <% if (news.IsOnTop.Equals(true))
                    {
                        Response.Write("【置顶】"+news.Title);
                    } 
                    else
                    {
                        Response.Write(news.Title);
                    }
                     %>
                    
                    </a><em><%:news.PubTime.Date %></em>
                </li>
            <% } %>
        </ul>
    </div>
    <div class="page-nav">
        <% Model.Models.PageModel pageModel = (Model.Models.PageModel)ViewData["pageModel"]; %>
        当前是第 <%:pageModel.pageNumber %> 页  
        <% for (int i = 0; i < (pageModel.TotalCount) / (pageModel.pageSize); i++){%>
            <% if (i == pageModel.pageNumber-1)
            {%>
            <span><%:i+1 %>  </span>
            <%}else{
             %>
            <a href="/News/Index?pageNumber=<%:i+1 %>"><%:i+1 %>  </a>
            <% } %>
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="RightPlaceHolder" runat="server">
    <style type="text/css">
    #portal-column-two{display:none;width:0;}
    </style>
</asp:Content>

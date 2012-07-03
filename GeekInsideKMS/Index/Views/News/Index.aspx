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
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="RightPlaceHolder" runat="server">
    <style type="text/css">
    #portal-column-two{display:none;width:0;}
    </style>
</asp:Content>

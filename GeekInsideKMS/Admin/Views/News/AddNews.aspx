<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" validateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    添加公告 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/content/js/editor/kindeditor.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <form action="/News/doAddNews" name="form" method="post">
            <% if ( TempData["errorMsg"] != ""){ %>
            <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
            <% } %>
            <% if ( TempData["successMsg"] != ""){ %>
            <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
            <% } %>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>公告标题</span>
                    </label>
                    <input type="text" name="title" />
                    <div>是否置顶：<input type="checkbox" class="checkitem" name="isontop" id="isontop" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>公告内容</span>
                    </label>
                    <div>
                        <textarea name="newscontent" id="newscontent" style="width:650px; height:300px; border:#CCCCCC solid 1px;"></textarea>
                        <script>loadEditor('newscontent');</script>
                    </div>
                </div>
            </div>
            <input type="submit" value="保存信息" />
            </form>
            
        </div>
    </div>
</asp:Content>

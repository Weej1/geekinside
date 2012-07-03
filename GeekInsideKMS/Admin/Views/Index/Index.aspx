<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    后台管理 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div class="admin-intro">
                关于Geek Inside文档管理系统的信息修改</div>
            <form action="/Index/doIndex" name="form" method="post">
            <% if ( TempData["errorMsg"] != ""){ %>
            <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
            <% } %>
            <% if ( TempData["successMsg"] != ""){ %>
            <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
            <% } %>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>站点标题</span>
                    </label>
                    <div>
                        <input type="text" name="sitename" value="<%: ViewData["sitename"]%>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>SMTP服务器地址</span>
                    </label>
                    <div>
                        <input type="text" name="smtpaddress" value="<%: ViewData["smtpaddress"]%>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>SMTP用户名</span>
                    </label>
                    <div>
                        <input type="text" name="smtpusername" value="<%: ViewData["smtpusername"]%>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>SMTP用户密码</span>
                    </label>
                    <div>
                        <input type="text" name="smtppassword" value="<%: ViewData["smtppassword"]%>"></div>
                </div>
            </div>
            <input type="submit" value="保存信息">
            </form>
        </div>
    </div>
</asp:Content>

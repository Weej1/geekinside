<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Base.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    登录 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/css/login.css" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div class="admin-intro" style="text-align:center;">请您登陆</div>
            
            <div class="login-section">
            <% if ( ViewData["errorMsg"] != ""){ %>
            <p style="color:red;"><%: ViewData["errorMsg"] %></p>
            <% } %>
                <form action="" method="post">
                    <fieldset>
					    <div class="loginrow">
						    <label>用户名：</label>
						    <div class="inputarea">
							    <input id="username" name="username" size="50" style="width:200px;" type="text">
						    </div>
					    </div>
					    <div class="loginrow">
						    <label>密码：</label>
						    <div class="inputarea">
							    <input id="password" name="password" size="50" style="width:200px;" type="password">
						    </div>
					    </div>
					
					    <div class="form-actions">
					      <input class="btn btn-primary" name="commit" type="submit" value="登录">
					    </div>
				    </fieldset>
                </form>
            </div>
       </div>
    </div>
</asp:Content>

        

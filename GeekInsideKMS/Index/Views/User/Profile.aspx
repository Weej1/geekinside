<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <form action="/User/doProfile" name="form" method="post">
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>姓名</span>
                    </label>
                    <div>
                        <input type="text" name="username" value="<%: ViewData["username"] %>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>邮箱</span>
                    </label>
                    <div>
                        <input type="text" name="email" value="<%: ViewData["email"] %>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>手机号码</span>
                    </label>
                    <div>
                        <input type="text" name="phone" value="<%: ViewData["phone"] %>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>是否是审核员：
                        <%if (ViewData["ischecker"] == "True")
                          {
                              Response.Write("是");
                          }
                          else
                          {
                              Response.Write("不是");
                          }%>
                        </span>
                    </label>
                </div>
            </div>
            <input type="submit" value="保存信息">
            </form>
        </div>
    </div>
</asp:Content>

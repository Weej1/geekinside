<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Profile.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
    个人资料修改
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
        <% Model.Models.UserEmployeeModel empModel = (Model.Models.UserEmployeeModel)ViewData["empModel"]; %>
        <% Model.Models.UserEmployeeDetailModel empDetailModel = (Model.Models.UserEmployeeDetailModel)ViewData["empDetailModel"]; %>
            <form action="/User/doProfile" name="form" method="post">
            <% if ( TempData["errorMsg"] != ""){ %>
            <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
            <% } %>
            <% if ( TempData["successMsg"] != ""){ %>
            <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
            <% } %>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>姓名</span>
                    </label>
                    <div>
                        <input type="text" name="name" value="<%: empDetailModel.Name %>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>邮箱</span>
                    </label>
                    <div>
                        <input type="text" name="email" value="<%: empDetailModel.Email %>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>手机号码</span>
                    </label>
                    <div>
                        <input type="text" name="phone" value="<%: empDetailModel.Phone %>"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>是否是审核员(不可修改)：
                        <%if (empModel.IsChecker.Equals(true))
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
            <input type="hidden" name="empDetailId" value="<%: empDetailModel.Id %>" />
            <input type="submit" value="保存信息">
            </form>
        </div>
    </div>
</asp:Content>

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
            <form action="#" name="form" method="post">
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>站点标题</span>
                    </label>
                    <div>
                        <input type="text" name="" value="Geek Inside文档管理系统"></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>SMTP服务器地址</span>
                    </label>
                    <div>
                        <input type="text" name=""></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>SMTP用户名</span>
                    </label>
                    <div>
                        <input type="text" name=""></div>
                </div>
            </div>
            <div class="row">
                <div class="field">
                    <label class="horizontal">
                        <span>SMTP用户密码</span>
                    </label>
                    <div>
                        <input type="text" name=""></div>
                </div>
            </div>
            <input type="submit" value="保存信息">
            </form>
            <div class="admin-intro">
                站点备份</div>
            点击<a href="#">此处</a>开始备份整个站点的数据
        </div>
    </div>
</asp:Content>

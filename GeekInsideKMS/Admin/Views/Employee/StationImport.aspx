<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StationImport
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="admin-intro">
        <form method="post" enctype="multipart/form-data">
        <div>
            人员信息的批量上传</div>
        <div style="margin-top: 20px;">
            <fieldset id="myfieldset1">
                <p>
                    选择文件：<input id="FileUpload" type="file" name="files" style="width: 250px; height: 24px;
                        background: White" /></p>
                <p>
                    <input id="btnImport" type="submit" value="导入" class="button" /></p>
                <p style="color: Red; text-align: center;">
                    <%: ViewData["errorMsg"]%></p>
            </fieldset>
        </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

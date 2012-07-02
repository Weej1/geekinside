<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	添加部门 - Geek Inside 知识管理系统
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <form method="post">
                <div class="row">
                    <div class="field">
                        <label class="horizontal">
                            <span>部门名称:</span>
                        </label>
                        <input type="text" name="deptName" />
                    </div>
                </div>
                <div class="row">
                    <div class="field">
                        <label class="horizontal">
                            <span>部门文件夹描述:</span>
                        </label>
                        <div>
                            <textarea name="folderDesc" style="width:500px; height:100px; border:#CCCCCC solid 1px;"></textarea>
                        </div>
                    </div>
                </div>
                <input type="submit"/>
            </form>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

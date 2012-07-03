<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Workshop.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div id="workdesk-content">
                <div id="deskDocsList">
                    <% Model.Models.FolderModel parentFolderModel = (Model.Models.FolderModel)ViewData["parentFolderModel"]; %>
                    <p class="discreet">添加【<%:parentFolderModel.FolderName %>】下的文件夹：</p>
                    <form action="/User/doAddFolder" name="form" method="post">
                        <div class="row">
                            <div class="field">
                                <label class="horizontal">
                                    <span>子文件夹名称</span>
                                </label>
                                <input type="text" name="FolderName" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="field">
                                <label class="horizontal">
                                    <span>文件夹描述</span>
                                </label>
                                <textarea name="Description" id="Description" rows="5">示例描述</textarea>
                            </div>
                        </div>
                        <input type="hidden" name="ParentFolderId" value="<%:parentFolderModel.Id %>"/>
                        <input type="submit" value="添加文件夹" />
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
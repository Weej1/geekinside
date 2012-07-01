<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Workshop.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div id="workdesk-content">
                <div id="deskDocsList">
                    <p class="discreet">
                        下面是您待审核的文档列表：</p>
                    <% if (ViewData["docList"].Equals("nodata")) { %>
                        <p style="color:red;">暂无文档</p>
                    <%}else{%>
                        <% List<Model.Models.DocumentModel> docList = (List<Model.Models.DocumentModel>)ViewData["docList"]; %>
                        <% foreach (var doc in docList) { %>
                        <div class="contentItem">
                            <div class="itemIcon">
                                <img src="/Content/images/icons/ms_word.gif">
                            </div>
                            <div class="itemInfo">
                                <h1>
                                    <a class="title clean" target="_blank" href="/Document/Detail?docid=<%: doc.Id %>"><%: doc.FileDisplayName%></a>
                                    <span class="docListOperation">
                                        <a href="#">编辑</a>
                                        <a href="#">删除</a>
                                    </span>
                                </h1>
                                <p class="discreet" style="margin: -5pt 0 2px;">
                                    上传者：<%: doc.PublisherName %>， 上传于 <%: doc.PubTime %> <%: doc.Size %>， 文件位于/文档库/公司管理/1123
                                </p>
                            </div>
                        </div>
                        <% } %>
                    <% } %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
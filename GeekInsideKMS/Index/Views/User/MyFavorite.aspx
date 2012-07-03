<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Workshop.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div id="workdesk-content">
                <div id="deskDocsList">
                    <% if ( TempData["errorMsg"] != ""){ %>
                    <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
                    <% } %>
                    <% if ( TempData["successMsg"] != ""){ %>
                    <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
                    <% } %>
                    <p class="discreet">
                        下面是您收藏的文档列表：</p>
                    <% if (ViewData["docList"].Equals("nodata")) { %>
                        <p style="color:red;">暂无收藏的文档</p>
                    <%}else{%>
                        <% List<Model.Models.DocumentModel> docList = (List<Model.Models.DocumentModel>)ViewData["docList"]; %>
                        <% foreach (var doc in docList) { %>
                        <div class="contentItem">
                            <div class="itemIcon">
                                <%if(doc.FileTypeName.Equals("doc") || doc.FileTypeName.Equals("docx")) {%>
                                    <img src="/Content/images/icons/word.gif">
                                <% }else if(doc.FileTypeName.Equals("xls") || doc.FileTypeName.Equals("xlsx")){ %>
                                    <img src="/Content/images/icons/excel.gif">
                                <% }else if(doc.FileTypeName.Equals("ppt") || doc.FileTypeName.Equals("pptx")){ %>
                                    <img src="/Content/images/icons/powerpoint.gif">
                                <% }else if(doc.FileTypeName.Equals("pdf")){ %>
                                    <img src="/Content/images/icons/pdf.gif">
                                <% }else if(doc.FileTypeName.Equals("wmv")){ %>
                                    <img src="/Content/images/icons/video.png">
                                <% }else{ %>
                                    <img src="/Content/images/icons/document.gif">
                                <% } %>
                            </div>
                            <div class="itemInfo">
                                <h1>
                                    <a class="title clean" target="_blank" href="/Document/Detail?docid=<%: doc.Id %>"><%: doc.FileDisplayName%></a>
                                    <span class="docListOperation">
                                        <a href="/User/DeleteFavorite?docid=<%: doc.Id %>">删除此收藏</a>
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
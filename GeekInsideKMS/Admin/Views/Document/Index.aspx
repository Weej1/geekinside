<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文档管理 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div class="admin-intro">文章列表</div>
                <% if ( TempData["errorMsg"] != ""){ %>
                <p style="color:red;font-weight:bold;"><%: TempData["errorMsg"]%></p>
                <% } %>
                <% if ( TempData["successMsg"] != ""){ %>
                <p style="color:green;font-weight:bold;"><%: TempData["successMsg"]%></p>
                <% } %>
                <p>
            <div id="NewsTable">
                <form name="doc_form" class="DocsForm" method="post" action="/Document/deleteMultiDocs">
                <table class="listing">
                    <thead>
                        <tr>
                            <th>
                                <a class="SelectAll" href="#">全选</a> <a class="UnSelectAll hidden" href="#">不选</a>
                            </th>
                            <th>
                                审核
                            </th>
                            <th>
                                编号
                            </th>
                            <th>
                                文档标题
                            </th>
                            <th>
                                上传者
                            </th>
                            <th>
                                上传时间
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <% List<Model.Models.DocumentModel> docList = (List<Model.Models.DocumentModel>)ViewData["docList"]; %>
                        <% foreach (var doc in docList)
                           { %>
                        <tr class="odd">
                            <td>
                                <input type="checkbox" class="checkitem" name="selected_docs" id="selected_docs" value="<%: doc.Id %>" />
                            </td>
                            <td>
                                <% if (doc.IsChecked.Equals(false))
                                {
                                    Response.Write("未审核");
                                } 
                                else
                                {
                                    Response.Write("通过");
                                }
                                 %>
                            </td>
                            <td>
                                <%: doc.Id%>
                            </td>
                            <td>
                                <a href="/Document/Edit?docid=<%: doc.Id %>"><%: doc.FileDisplayName%></a>
                            </td>
                            <td>
                                <%: doc.PublisherName%>
                            </td>
                            <td>
                                <%: doc.PubTime%>
                            </td>
                            <td>
                                <a href="/Document/Edit?docid=<%: doc.Id %>" title="编辑">
                                    <img src="/Content/images/icons/edit.png" alt="编辑" /></a>
                                <a href="/Document/Delete?docid=<%: doc.Id %>" title="删除">
                                    <img src="/Content/images/icons/trash.gif" alt="删除" /></a>
                            </td>
                        </tr>
                        <% } %>
                    </tbody>
                </table>
                <p>
                    <input type="button" class="button" value="删除选中" onclick="javascript:document.forms[0].submit();"><span style="margin-left: 10px;"></span>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

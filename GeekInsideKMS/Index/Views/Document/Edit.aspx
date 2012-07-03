<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑文档信息 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftPlaceHolder" runat="server">
    <style type="text/css">
    #portal-column-one{display:none;width:0;}
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <% Model.Models.DocumentModel docModel = (Model.Models.DocumentModel)ViewData["docModel"]; %>
    <% if (docModel.IsChecked.Equals(true))
       {%>
        <div class="page-intro">这是一篇已发布的文档，编辑之后需要重新审核！</div>
    <%}
       else
       { %>
       <div class="page-intro">这是一篇正在审核中的文档，编辑之后请继续等待审核！</div>
    <%} %>
    <form action="/Document/doEdit" method="post" name="docdetail" id="docdetail">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
        	<tr style="height:50px;">
        		<td style="width:170px;">
                标题：</td>
                <td><input type="text" name="filedisplayname" value="<%:docModel.FileDisplayName %>"/>.<%:docModel.FileTypeName %>
                <%if(docModel.FileTypeName.Equals("doc") || docModel.FileTypeName.Equals("docx")) {%>
                    <img src="/Content/images/icons/word.gif">
                <% }else if(docModel.FileTypeName.Equals("xls") || docModel.FileTypeName.Equals("xlsx")){ %>
                    <img src="/Content/images/icons/excel.gif">
                <% }else if(docModel.FileTypeName.Equals("ppt") || docModel.FileTypeName.Equals("pptx")){ %>
                    <img src="/Content/images/icons/powerpoint.gif">
                <% }else if(docModel.FileTypeName.Equals("pdf")){ %>
                    <img src="/Content/images/icons/pdf.gif">
                <% }else if(docModel.FileTypeName.Equals("wmv")){ %>
                    <img src="/Content/images/icons/video.png">
                <% }else{ %>
                    <img src="/Content/images/icons/document.gif">
                <% } %>
                </td>
        	</tr>
            <tr style="height:50px;">
                <td>描述：</td>
                <td><input type="text" name="description" value="<%:docModel.Description %>"/></td>
            </tr>
            <tr style="height:50px;">
                <td>标签：</td>
                <td><%
                        foreach (Model.Models.TagModel tag in docModel.FileTagIdArray)
                        {
                            Response.Write(tag.TagName+", ");
                        }
                %></td>
            </tr>
            <tr style="height:50px;">
                <td>预览：</td>
                <td><a href="#">击展开预览</a></td>
            </tr>
            <tr style="height:50px;">
                <td>
                <input type="hidden" name="id" value="<%:docModel.Id %>" />
                <input type="button" class="button" value="提交修改"  onclick="javascript:document.forms[1].submit();"></td>
                <td><td>
            </tr>
        </table>
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="RightPlaceHolder" runat="server">
    <style type="text/css">
    #portal-column-two{display:none;width:0;}
    </style>
</asp:Content>

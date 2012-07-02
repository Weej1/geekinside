<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    高级搜索 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftPlaceHolder" runat="server">
    <style type="text/css">
    #portal-column-one{display:none;width:0;}
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div class="page-intro">高级搜索 - 请输入搜索详细信息</div>
    <form action="/Search/doAdvanceSearch" method="post">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
        	<tr style="height:50px;">
        		<td style="width:170px;">包含以下关键字：</td>
                <td><input type="text" name="sw" /></td>
        	</tr>
            <tr style="height:50px;">
                <td>不包含以下关键字：</td>
                <td><input type="text" name="sw_notinclude" /></td>
            </tr>
            <tr style="height:50px;">
                <td>要检索的资源种类：</td>
                <td>
                    <input type="checkbox" name="sw_doctype" id="sw_doctype" value="doc" /><span style="margin-right:10px;">doc/docx</span>
                    <input type="checkbox" name="sw_doctype" id="sw_doctype" value="xls" /><span style="margin-right:10px;">xls/xlsx</span>
                    <input type="checkbox" name="sw_doctype" id="sw_doctype" value="ppt" /><span style="margin-right:10px;">ppt/pptx</span>
                    <input type="checkbox" name="sw_doctype" id="sw_doctype" value="pdf" /><span style="margin-right:10px;">pdf</span>
                    <input type="checkbox" name="sw_doctype" id="sw_doctype" value="wmv" /><span style="margin-right:10px;">wmv</span>
                </td>
            </tr>
            <tr style="height:50px;">
                <td><input type="button" class="button" value="开始搜索"  onclick="javascript:document.forms[1].submit();"></td>
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

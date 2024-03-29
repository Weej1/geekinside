﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage" validateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑文档 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/content/js/editor/kindeditor.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <% Model.Models.DocumentModel docModel = (Model.Models.DocumentModel)ViewData["docModel"]; %>
    <% if (docModel.IsChecked.Equals(true))
       {%>
        <div class="admin-intro">这是一篇已发布的文档</div>
    <%}
       else
       { %>
       <div class="admin-intro">这是一篇正在审核中的文档，编辑之后仍需继续等待审核。</div>
    <%} %>
    <form action="/Document/doEdit" method="post" name="docdetail" id="docdetail">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
        	<tr style="height:50px;">
        		<td style="width:170px;">
                标题：</td>
                <td><input type="text" name="filedisplayname" value="<%:docModel.FileDisplayName %>"/>
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
                <td>权限：</td>
                <td> <select name="auth_" style="width:200px" >
                       <option value='1'>所有人都可以查看和下载</option>
                       <option value='2'>非本部门人不可以下载</option>
                       <option value='3'>只有本部门人可以查看和下载</option>
                       <option value='4'>只有本部门人可以查看</option>
                    </select></td>
            </tr>
            <tr style="height:50px;">
                <td>预览：</td>
                <td>
                <div >
                            <% string filename = docModel.FileDiskName;%>
                            <% string fileType = docModel.FileTypeName; %>
                            <% if(fileType.Equals("flv")) {%>
                                <div style="text-align: center; width: 100%; height: 100%; overflow: auto; margin-top: 5px;" id="previewBody" >
                                    <div align="left" style="margin-top: 10px"><div id="player5" style="width:675px; height:450px margin:0px; border:solid 0px #50031a;color:#000000;">
                                        <a href="http://www.89525.net/FlvPlayer/" target="_blank"></a>
                                    </div>
                                </div>                                
                                <script type="text/javascript" src="../../../Scripts/swfobject.js"></script>
                                <script type="text/javascript">
                                    var s5 = new SWFObject("../../../Scripts/FlvPlayer201002.swf", "playlist", "675", "450", "7");
                                    s5.addParam("allowfullscreen", "true");
                                    s5.addVariable("autostart", "false");
                                    s5.addVariable("file", "/Document/getfilepath?FileDownloadName=" + "<%=filename %>");
                                    s5.addVariable("width", "675");
                                    s5.addVariable("height", "450");
                                    s5.write("player5");
                                 </script>                            
                            <% }else{ %>
                                <div align="left">
                                    <script type="text/javascript" src="../../../Scripts/flexpaper_flash.js"></script>
                                    <a id="viewerPlaceHolder" style="width:800px;height:550px;display:block"></a>    
                                    <script type="text/javascript">
                                        var fp = new FlexPaperViewer(
                                            '../../../Scripts/FlexPaperViewer',
                                            'viewerPlaceHolder', {
                                                config: {
                                                    SwfFile:  "/Document/getfilepath?FileDownloadName=" + "<%=filename %>",
                                                    Scale: 0.6,
                                                    ZoomTransition: 'easeOut',
                                                    ZoomTime: 0.5,
                                                    ZoomInterval: 0.2,
                                                    FitPageOnLoad: false,
                                                    FitWidthOnLoad: true,
                                                    FullScreenAsMaxWindow: true,
                                                    ProgressiveLoading: false,
                                                    MinZoomSize: 0.5,
                                                    MaxZoomSize: 3,
                                                    SearchMatchAll: false,
                                                    InitViewMode: 'Portrait',

                                                    ViewModeToolsVisible: true,
                                                    ZoomToolsVisible: true,
                                                    NavToolsVisible: true,
                                                    CursorToolsVisible: false,
                                                    SearchToolsVisible: true,

                                                    localeChain: 'en_US'
                                                }
                                            });
                                    </script> 
                                 </div>
                            <% } %>
                         </div>
                    </div>
                </td>
            </tr>
            <tr style="height:50px;">
                <td>
                <input type="hidden" name="id" value="<%:docModel.Id %>" />
                <input type="button" class="button" value="提交修改"  onclick="javascript:document.forms[0].submit();"></td>
                <td><td>
            </tr>
        </table>
    </form>
</asp:Content>

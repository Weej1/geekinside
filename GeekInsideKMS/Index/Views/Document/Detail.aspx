<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文档查看 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".portletHeaderContent_1").click(function () {
                $(".KSSShowHideTarget_1").toggle();
            });
            $(".portletHeaderContent_2").click(function () {
                $(".KSSShowHideTarget_2").toggle();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="viewlet-above-content" class="KSSTabArea KSSShowHideArea">
        <div id="above-content-bar">
            <div class="contentbar_content contentbarcontent clearfix">
            <% Model.Models.DocumentModel docModel = (Model.Models.DocumentModel)ViewData["docModel"]; %>
                <div class="contentbar_left">
                    <span style="font-size: 12px; margin-left: -2px">路径：</span> <span style="font-size: 11px;
                        color: #555">/ </span><a href="#s" style="font-size: 12px">文档库</a>
                </div>
                <div class="contentbar_right">
                    <div class="button-group mini">
                        <button class="KSSActionServer KSSLoad button" onclick= "self.location='/Document/Download?docid=<%:docModel.Id %>">
                            下载</button>
                        <button class="KSSActionServer KSSLoad button" onclick= "self.location='/User/addFavorite?docid=<%:docModel.Id %>&returnURL=MyFavorite'">
                            收藏</button>
                        <!-- 上传者可见 -->
                        <% if (docModel.PublisherNumber.Equals(Convert.ToInt32(Page.User.Identity.Name)))
                        {%>
                        <button class="KSSActionServer KSSLoad button" onclick= "self.location='/Document/Edit?docid=<%:docModel.Id %>'">编辑信息</button>
                        <% }
                         %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="region-content" class="documentContent">
        <div id="content">
            <div id="workdesk-content">
                <div id="filesView">
                    <div class="content_field">
                        <div class="KSSFileTitle">
                            <div>
                                <h1 style="margin: 0; display: inline; vertical-align: middle;">
                                    <span><%:docModel.FileDisplayName %></span>
                                </h1>
                                <div class="doc_description">
                                    <%:docModel.Description %>
                                </div>
                                <div class="discreet">
                                    <span>大小：<%:docModel.Size %> 查看次数：<%:docModel.ViewNumber %> 下载次数：<%:docModel.DownloadNumber %> </span>
                                    <div style="margin-top: 5px">
                                        由 <a href="/Document/GetDocByEmpployeeNumber?empno=<%:docModel.PublisherNumber %>"><%:docModel.PublisherName %></a> 上传于 <%:docModel.PubTime %> 
                                        标签：
                                        <% foreach (Model.Models.TagModel tag in docModel.FileTagIdArray)
                                        {%>
                                            <a href="/Document/getDocByTagId?tagid=<%:tag.Id %>"><%:tag.TagName %> </a>
                                        <%}
                                         %>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="visualClear">
                        </div>
                        <div id="operationTip-previewBody">
                            <div style="text-align: center; width: 100%; height: 100%; overflow: auto; margin-top: 5px;"
                                id="previewBody">
                                预览区域<br>
                                预览区域<br>
                                预览区域<br>
                                预览区域<br>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightPlaceHolder" runat="server">
    <div class="visualPadding">
        <dl class="portlet transparentPortlet">
            <dt class="portletHeader deltaPortletHeader">
                <div class="portletHeaderContent portletHeaderContent_1">
                    <span class="KSSShowHideAction"><span class="downwardDelta"></span><span class="rightwardDelta hidden">
                    </span><span>浏览排行</span> </span>
                </div>
            </dt>
            <dd class="portletItem KSSShowHideTarget_1">
                <% List<Model.Models.DocumentModel> viewTop10Doc = (List<Model.Models.DocumentModel>)ViewData["viewTop10Doc"]; %>
                <% foreach (var doc in viewTop10Doc){%>
                    <p class="discreet toplist"><a href="/Document/Detail?docid=<%:doc.Id %>"><%:doc.FileDisplayName %>.<%:doc.FileTypeName %></a></p>
                <% }%>
            </dd>
        </dl>
        <dl class="portlet transparentPortlet">
            <dt class="portletHeader deltaPortletHeader">
                <div class="portletHeaderContent portletHeaderContent_2">
                    <span class="KSSShowHideAction"><span class="downwardDelta"></span><span class="rightwardDelta hidden">
                    </span><span>下载排行</span> </span>
                </div>
            </dt>
            <dd class="portletItem KSSShowHideTarget_2">
                <% List<Model.Models.DocumentModel> dlTop10Doc = (List<Model.Models.DocumentModel>)ViewData["dlTop10Doc"]; %>
                <% foreach (var doc in dlTop10Doc){%>
                    <p class="discreet toplist"><a href="/Document/Detail?docid=<%:doc.Id %>"><%:doc.FileDisplayName %>.<%:doc.FileTypeName %></a></p>
                <% }%>
            </dd>
        </dl>
    </div>
</asp:Content>

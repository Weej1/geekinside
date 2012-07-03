<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文档库 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".portletHeaderContent_1").click(function () {
                $(".KSSShowHideTarget_1").toggle("fast");
            });
            $(".portletHeaderContent_2").click(function () {
                $(".KSSShowHideTarget_2").toggle("fast");
            });
            $(".portletHeaderContent_3").click(function () {
                $(".KSSShowHideTarget_3").toggle("fast");
            });
            $(".portletHeaderContent_4").click(function () {
                $(".KSSShowHideTarget_4").toggle("fast");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftPlaceHolder" runat="server">
    <div class="visualPadding">
        <dl class="portlet portlet-navigation-tree">
            <dt class="portletHeader"><span class="portletTopLeft"></span><span>文档分类导航</span> <span
                class="portletTopRight"></span></dt>
        </dl>
        <dd class="portletItem">
            <ul class="portletNavigationTree navTreeLevel0">
                <li class="navTreeItem visualNoMarker collapsed"><a href="#">财务部</a> </li>
                <ul class="navTree navTreeLevel2">
                    <li class="navTreeItem visualNoMarker collapsed"><a href="#">常用表格</a> </li>
                    <li class="navTreeItem visualNoMarker collapsed"><a href="#">培训材料</a> </li>
                </ul>
                <li class="navTreeItem visualNoMarker collapsed"><a href="#">市场部</a> </li>
                <li class="navTreeItem visualNoMarker collapsed"><a href="#">技术部</a> </li>
                <li class="navTreeItem visualNoMarker collapsed"><a href="#">人力资源部</a> </li>
            </ul>
        </dd>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="viewlet-above-content" class="KSSTabArea KSSShowHideArea">
        <div id="above-content-bar">
            <div class="contentbar_content contentbarcontent clearfix">
                <div class="contentbar_left">
                    <span style="font-size: 12px; margin-left: -2px">路径：</span> <span style="font-size: 11px;
                        color: #555">/ </span><a href="#s" style="font-size: 12px">文档库</a>
                </div>
            </div>
        </div>
    </div>
    <div id="region-content" class="documentContent">
        <div id="content">
            <div id="workdesk-content">
                <div id="filesView">
                    <div class="content_field">
                        <div class="KSSFileTitle KSSShowHideArea">
                            <div class="KSSShowHideTarget">
                                <h1 class="KSSShowHideArea2" style="margin: 0; display: inline; vertical-align: middle;">
                                    <span class="KSSHoverHilight">文档库</span>
                                </h1>
                                <div>
                                    <span class="discreet">集中管理文件的区域，多层子文件管理，文件预览、播放等</span>
                                </div>
                            </div>
                        </div>
                        <div class="visualClear">
                        </div>
                        <div class="listingContents" id="previewBody">
                            <div class="contentItem">
                                <div class="itemIcon">
                                    <a href="#">
                                        <img src="/Content/images/folder.gif"></a>
                                </div>
                                <div class="itemInfo">
                                    <h1>
                                        <a href="#" class="title clean">财务部</a></h1>
                                    <p style="margin: 0">
                                        <span class="discreet">存放各种公司管理制度</span>
                                    </p>
                                    <div class="visualClear">
                                    </div>
                                </div>
                            </div>
                            <div class="contentItem">
                                <div class="itemIcon">
                                    <a href="#">
                                        <img src="/Content/images/folder.gif"></a>
                                </div>
                                <div class="itemInfo">
                                    <h1>
                                        <a href="#" class="title clean">财务部</a></h1>
                                    <p style="margin: 0">
                                        <span class="discreet">存放各种公司管理制度</span>
                                    </p>
                                    <div class="visualClear">
                                    </div>
                                </div>
                            </div>
                            <div class="contentItem">
                                <div class="itemIcon">
                                    <a href="#">
                                        <img src="/Content/images/folder.gif"></a>
                                </div>
                                <div class="itemInfo">
                                    <h1>
                                        <a href="#" class="title clean">财务部</a></h1>
                                    <p style="margin: 0">
                                        <span class="discreet">存放各种公司管理制度</span>
                                    </p>
                                    <div class="visualClear">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="RightPlaceHolder" runat="server">
    <div class="visualPadding">
        <dl class="portlet transparentPortlet">
            <dt class="portletHeader deltaPortletHeader">
                <div class="portletHeaderContent portletHeaderContent_1">
                    <span class="KSSShowHideAction"><span class="downwardDelta"></span><span class="rightwardDelta hidden">
                    </span><span>热门标签</span> </span>
                </div>
            </dt>
            <dd class="portletItem KSSShowHideTarget_1">
                    <%List<Model.Models.TagModel> tagModel = (List<Model.Models.TagModel>)ViewData["tagModel"]; %>
                    <% foreach (Model.Models.TagModel tag in tagModel)
                    {%>
                         <span><a href="/Document/getDocByTagId?tagid=<%:tag.Id %>" class="tag_href"><%:tag.TagName %>  </a></span>  
                    <%}
                     %>
            </dd>
        </dl>
        <dl class="portlet transparentPortlet">
            <dt class="portletHeader deltaPortletHeader">
                <div class="portletHeaderContent portletHeaderContent_3">
                    <span class="KSSShowHideAction"><span class="downwardDelta"></span><span class="rightwardDelta hidden">
                    </span><span>浏览排行</span> </span>
                </div>
            </dt>
            <dd class="portletItem KSSShowHideTarget_3">
                <% List<Model.Models.DocumentModel> viewTop10Doc = (List<Model.Models.DocumentModel>)ViewData["viewTop10Doc"]; %>
                <% foreach (var doc in viewTop10Doc){%>
                    <p class="discreet toplist"><a href="/Document/Detail?docid=<%:doc.Id %>"><%:doc.FileDisplayName %>.<%:doc.FileTypeName %></a></p>
                <% }%>
                
            </dd>
        </dl>
        <dl class="portlet transparentPortlet">
            <dt class="portletHeader deltaPortletHeader">
                <div class="portletHeaderContent portletHeaderContent_4">
                    <span class="KSSShowHideAction"><span class="downwardDelta"></span><span class="rightwardDelta hidden">
                    </span><span>下载排行</span> </span>
                </div>
            </dt>
            <dd class="portletItem KSSShowHideTarget_4">
                <% List<Model.Models.DocumentModel> dlTop10Doc = (List<Model.Models.DocumentModel>)ViewData["dlTop10Doc"]; %>
                <% foreach (var doc in dlTop10Doc){%>
                    <p class="discreet toplist"><a href="/Document/Detail?docid=<%:doc.Id %>"><%:doc.FileDisplayName %>.<%:doc.FileTypeName %></a></p>
                <% }%>
            </dd>
        </dl>
    </div>
</asp:Content>

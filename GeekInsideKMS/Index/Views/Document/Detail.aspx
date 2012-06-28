<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文件标题 - Geek Inside 知识管理系统
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
                <div class="contentbar_left">
                    <span style="font-size: 12px; margin-left: -2px">路径：</span> <span style="font-size: 11px;
                        color: #555">/ </span><a href="#s" style="font-size: 12px">文档库</a>
                </div>
                <div class="contentbar_right">
                    <div class="button-group mini">
                        <button class="KSSActionServer KSSLoad button">
                            下载</button>
                        <button class="KSSActionServer KSSLoad button">
                            收藏</button>
                        <!-- 上传者可见 -->
                        <button class="KSSActionServer KSSLoad button">
                            编辑信息</button>
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
                                    <span>DocumentName.doc</span>
                                </h1>
                                <div class="doc_description">
                                    这里是文档描述
                                </div>
                                <div class="discreet">
                                    <span>大小：500KB 查看次数：200 下载次数：40 </span>
                                    <div style="margin-top: 5px">
                                        由 admin 创建于 2012-06-26 13:23 标签：标签1 标签2</div>
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
                    </span><span>关注</span> </span>
                </div>
            </dt>
            <dd class="portletItem KSSShowHideTarget_1">
                <p class="discreet">
                    关注人将自动接收新的评注通知</p>
                <p class="discreet">
                    关注人将自动接收新的评注通知</p>
                <p class="discreet">
                    关注人将自动接收新的评注通知</p>
            </dd>
        </dl>
        <dl class="portlet transparentPortlet">
            <dt class="portletHeader deltaPortletHeader">
                <div class="portletHeaderContent portletHeaderContent_2">
                    <span class="KSSShowHideAction"><span class="downwardDelta"></span><span class="rightwardDelta hidden">
                    </span><span>关注</span> </span>
                </div>
            </dt>
            <dd class="portletItem KSSShowHideTarget_2">
                <p class="discreet">
                    关注人将自动接收新的评注通知</p>
                <p class="discreet">
                    关注人将自动接收新的评注通知</p>
                <p class="discreet">
                    关注人将自动接收新的评注通知</p>
            </dd>
        </dl>
    </div>
</asp:Content>

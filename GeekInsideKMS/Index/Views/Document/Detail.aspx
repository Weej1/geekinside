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
                                            <a href="/Document/getDocByTagId?tagid=<%:tag.Id %>"><%:tag.TagName %></a>
                                        <%}
                                         %>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="visualClear" >
                        </div>
                        <div id="operationTip-previewBody" style="margin-top:30px; margin-bottom:55px;">
                            <% string filename = docModel.FileDiskName; %>
                            <% string fileType = docModel.FileTypeName; %>
                            <% if(fileType.Equals("flv")) {%>
                            <div style="text-align: center; width: 100%; height: 100%; overflow: auto;" id="previewBody" >
                                <div align="center">
                                    <div id="player5" style="width:675px; height:450px margin:0px; border:solid 0px #50031a;color:#000000;">
                                        <a href="http://www.89525.net/FlvPlayer/" target="_blank"></a>
                                    </div>
                                </div>                                
                                <script type="text/javascript" src="../../../Scripts/swfobject.js"></script>
                                <script type="text/javascript">
                                    var s5 = new SWFObject("../../../Scripts/FlvPlayer201002.swf", "playlist", "675", "450", "7");
                                    s5.addParam("allowfullscreen", "true");
                                    s5.addVariable("autostart", "true");
                                    s5.addVariable("file", "/Document/getfile?FileDownloadName=" + "<%=filename %>");
                                    s5.addVariable("width", "675");
                                    s5.addVariable("height", "450");
                                    s5.write("player5");
                                 </script>       
                             </div>                     
                            <% }else{ %>
                                <div align="center">
                                    <script type="text/javascript" src="../../../Scripts/flexpaper_flash.js"></script>
                                    <a id="viewerPlaceHolder" style="width:850px;height:530px;display:block"></a>    
                                    <script type="text/javascript">
                                        var fp = new FlexPaperViewer(
                                            '../../../Scripts/FlexPaperViewer',
                                            'viewerPlaceHolder', {
                                                config: {
                                                    SwfFile: "../../../swf/" + "<%=filename %>".split('.')[0]+".swf",
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
            </div>
            
        </div>
        <div id="disqus_thread"></div>
            <script type="text/javascript">
                /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
                var disqus_shortname = 'hackechoblog'; // required: replace example with your forum shortname

                /* * * DON'T EDIT BELOW THIS LINE * * */
                (function () {
                    var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
                    dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
                    (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
                })();
            </script>
            <noscript>Please enable JavaScript to view the <a href="http://disqus.com/?ref_noscript">comments powered by Disqus.</a></noscript>
            <a href="http://disqus.com" class="dsq-brlink">comments powered by <span class="logo-disqus">Disqus</span></a>
        
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

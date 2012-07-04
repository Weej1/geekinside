<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Model.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    文档库 - Geek Inside 知识管理系统
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="Stylesheet" href="/Content/css/folder.css"/>
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>   
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
    <script type="text/javascript">
        var folders;
        var isAnyFolderChoose = false;
        var page = 1;
        var folderCurrent = 0;

        function dateFormat(date) {
            var now = new Date();
            date = date.replace("/", "");
            date = date.replace("Date", "");
            date = date.replace("(", "");
            date = date.replace(")", "");
            date = date.replace("/", "");
            now.setTime(date);
            var year = now.getFullYear();
            var month = now.getMonth() + 1;
            var day = now.getDate();
            var time = year + "-" + month + "-" + day;
            return time;
        }

        $(function () {
            $.ajax({ url: "/Folder/AllFolders",
                type: 'post',
                async: false,
                dataType: 'json',
                success: function (data) {
                    folders = data;
                }
            });
            $.each(folders, function (i, m) {
                var content = "<div class='contentItem'>" +
                                "<div class='itemIcon'>" +
                                    "<a href='#'>" +
                                        "<img src='/Content/images/folder.gif'></a>" +
                                "</div>" +
                                "<div class='itemInfo'>" +
                                    "<h1>" +
                                        "<a href='#' class='title clean' id='f_" + m.Id + "'>" + m.FolderName + "</a></h1>" +
                                    "<p style='margin: 0'>" +
                                        "<span class='discreet'>" + m.Description + "</span>" +
                                    "</p>" +
                                   "<div class='visualClear'>" +
                                "</div>" +
                              "</div>" +
                            "</div>";
                $("#previewBody").append(content);
                $("#f_" + m.Id).click(function (e) {
                    triggerFolderListener(m.Id, m.SubFolders, e);
                });
            });
        });

        function triggerFolderListener(folder_id, subFolders, e) {
            if (e.stopPropagation) {
                e.stopPropagation();
            }
            else {
                e.cancelBubble = true;
            }
            $("#previewBody").html("");
            $.each(subFolders, function (i, m) {
                var content = "<div class='contentItem'>" +
                                "<div class='itemIcon'>" +
                                    "<a href='#'>" +
                                        "<img src='/Content/images/folder.gif'></a>" +
                                "</div>" +
                                "<div class='itemInfo'>" +
                                    "<h1>" +
                                        "<a href='#' class='title clean' id='f_" + m.Id + "' >" + m.FolderName + "</a></h1>" +
                                    "<p style='margin: 0'>" +
                                        "<span class='discreet'>" + m.Description + "</span>" +
                                    "</p>" +
                                   "<div class='visualClear'>" +
                                "</div>" +
                              "</div>" +
                            "</div>";
                $("#previewBody").append(content);
                $("#f_" + m.Id).click(function (e) {
                    triggerFolderListener(m.Id, m.SubFolders, e);
                });
            });
            page = 1;
            $.ajax({ url: "/Document/GetFolderList",
                type: 'post',
                dataType: 'json',
                data: { 'folderId': folder_id, 'page': 1 },
                success: function (data) {
                    $.each(data, function (i, m) {
                        var content = "<div class='contentItem'>" +
                                "<div class='itemIcon'>";
                        if (m.FileTypeName === "doc" || m.FileTypeName == "docx") {
                            content += "<img src='/Content/images/icons/word.gif'/>";
                        } else if (m.FileTypeName == "xls" || m.FileTypeName == "xlsx") {
                            content += "<img src='/Content/images/icons/excel.gif'/>";
                        } else if (m.FileTypeName == "ppt" || m.FileTypeName == "pptx") {
                            content += "<img src='/Content/images/icons/powerpoint.gif'/>";
                        } else if (m.FileTypeName == "pdf") {
                            content += "<img src='/Content/images/icons/pdf.gif'/>";
                        } else if (m.FileTypeName == "flv") {
                            content += "<img src='/Content/images/icons/video.png'/>";
                        } else {
                            content += "<img src='/Content/images/icons/document.gif'/>";
                        }                 
                        content += "</div>" +
                                "<div class='itemInfo'>" +
                                    "<h1>" +
                                        "<a href='/Document/Detail?docid=" + m.Id +"' class='title clean'>" + m.FileDisplayName + "</a></h1>" +
                                   "<p class='discreet' style='margin: -5pt 0 2px;'>" +
                                                                             "上传者：" + m.PublisherName + ",上传于 " + dateFormat(m.PubTime) + " " + m.Size + "， 查看次数：" + m.ViewNumber + " 下载次数：" + m.DownloadNumber +
                                   "</p>" +
                                   "<div class='visualClear'>" +
                                   "</div>" +
                               "</div>" +
                            "</div>";
                        $("#previewBody").append(content);
                    });
                }
            });

            folderCurrent = folder_id;
            isAnyFolderChoose = true;
        }

        function triggerSubFolders(subFolders) {
            $.each(subFolders, function (i, n) {
                $("#folder_" + n.Id).click(function (e) {
                    triggerFolderListener(n.Id, n.SubFolders, e);
                    triggerSubFolders(n.SubFolders);
                })
            });
        }

        function triggerAnimate() {
            $("#folder_wrapper img.collapseFlag").click(function (e) {
                $(this).css('display', 'none');
                $(this).parent(this).children('.expandFlag').css('display', 'inline');
                $(this).parents('.navTreeItem:first').children('ul:first').css('display', 'block');
                return false;
            });
            $("#folder_wrapper img.expandFlag").click(function (e) {
                $(this).css('display', 'none');
                $(this).parent(this).children('.collapseFlag').css('display', 'inline');
                $(this).parents('.navTreeItem:first').children('ul:first').css('display', 'none');
                return false;
            });
            $.each(folders, function (i, n) {
                $("#folder_" + n.Id).click(function (e) {
                    triggerFolderListener(n.Id, n.SubFolders, e);
                });
                triggerSubFolders(n.SubFolders);
            });
        }


        function getSubFolders(n) {
            var s = "";
            var isHas = false;
            $.each(n.SubFolders, function (i, k) {
                if (i == 0) {
                    s = s + "<ul class='ul_tree sub'>";
                    isHas = true;
                }
                s = s + "<li class='navTreeItem'>" +
                            "<div class='content_folder'>" +
                                "<a href='#' id='folder_" + k.Id + "' >" +
                                   "<img src='/Content/images/pl.gif' alt='打开' class='collapseFlag'/>" +
                                   "<img src='/Content/images/mi.gif' alt='关闭' class='expandFlag'/>" +
                                   "<img src='/Content/images/folder.gif' class='folder_icon'/>" +
                                   "<span>" + k.FolderName + "</span>" +
                                "</a>" +
                            "</div>";
                s = s + getSubFolders(k) + "</li>";
            });
            if (isHas) {
                s = s + "</ul>";
            }
            return s;
        }

        $(function () {
            str = "<div class='folder_wrapper' id='folder_wrapper'>" +
                    "<div class='folder_content'>" +
                     "<div class='folder_bg'>" +
                     "<dl class='portal_dl'>" +
                      "<dd class='portal_dd'>" +
                       "<ul class='ul_tree' style='margin-left: -15px;'>";
            $.each(folders, function (i, n) {
                str = str + "<li class='navTreeItem'>" +
                                 "<div class='content_folder'>" +
                                   "<a href='#' id='folder_" + n.Id + "' >" +
                                      "<img src='/Content/images/pl.gif' alt='打开' class='collapseFlag'/>" +
                                      "<img src='/Content/images/mi.gif' alt='关闭' class='expandFlag'/>" +
                                      "<img src='/Content/images/folder.gif' class='folder_icon'/>" +
                                      "<span>" + n.FolderName + "</span>" +
                                    "</a>" +
                                  "</div>";
                str = str + getSubFolders(n) + "</li>";
            });
            str = str + "</ul>" +
                        "</dd>" +
                       "</dl>" +
                      "</div>" +
                     "</div>" +
                    "</div>";

            $("#folder_menu").append(str);
            triggerAnimate();

        });
        function requestMoreDocument(p) {
            var dnumber = 0;
            $.ajax({ url: "/Document/GetFolderList",
                type: 'post',
                dataType: 'json',
                data: { 'folderId': folderCurrent, 'page': page },
                success: function (data) {
                    $.each(data, function (i, m) {
                        dnumber++;
                        var content = "<div class='contentItem'>" +
                                "<div class='itemIcon'>";
                        if (m.FileTypeName === "doc" || m.FileTypeName == "docx") {
                            content += "<img src='/Content/images/icons/word.gif'/>";
                        } else if (m.FileTypeName == "xls" || m.FileTypeName == "xlsx") {
                            content += "<img src='/Content/images/icons/excel.gif'/>";
                        } else if (m.FileTypeName == "ppt" || m.FileTypeName == "pptx") {
                            content += "<img src='/Content/images/icons/powerpoint.gif'/>";
                        } else if (m.FileTypeName == "pdf") {
                            content += "<img src='/Content/images/icons/pdf.gif'/>";
                        } else if (m.FileTypeName == "flv") {
                            content += "<img src='/Content/images/icons/video.png'/>";
                        } else {
                            content += "<img src='/Content/images/icons/document.gif'/>";
                        }
                        content += "</div>" +
                                "<div class='itemInfo'>" +
                                    "<h1>" +
                                        "<a href='/Document/Detail?docid=" + m.Id + "' class='title clean'>" + m.FileDisplayName + "</a></h1>" +
                                   "<p class='discreet' style='margin: -5pt 0 2px;'>" +
                                       "上传者：" + m.PublisherName + ",上传于 " + dateFormat(m.PubTime) + " " + m.Size + "， 查看次数：" + m.ViewNumber + " 下载次数：" + m.DownloadNumber +
                                   "</p>" +
                                   "<div class='visualClear'>" +
                                   "</div>" +
                               "</div>" +
                            "</div>";
                        $("#previewBody").append(content);
                    });
                    $(".overlay").css("display", "none");
                    if (dnumber == 0) {
                        $(".al").css("display", "block");
                    }
                }
            });
        }

        $(function () {
            $(window).scroll(function (e) {
                total = $("#previewBody").height();
                scorll = $(window).scrollTop();
                rate = scorll / total;
                if (rate > 0.6) {
                    if (isAnyFolderChoose) {
                        overlay = $(".overlay");
                        al = $(".al");
                        overlay.css("width", $("#previewBody").width());
                        overlay.css("height", "100px");
                        al.css("width", $("#previewBody").width());
                        al.css("height", "60px");
                        al.css("display", "none");
                        overlay.css("display", "block");
                        requestMoreDocument(++page);
                    }
                }
            });
        });
        
    </script>
    <style type="text/css">
        .folder_wrapper
        {
            display:block;
            width: 195px;
            margin-left: -7px;
            margin-top: -21px;
        }
        .overlay {
            background: #000 url('/Content/images/loadingAnimation.gif') no-repeat center;
            filter: alpha(opacity=70); 
            opacity: 0.7; 
            display: none;
            position: relative;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            z-index: 300; 
            display:none;
        }
        .al 
        {
            margin-top:10px; 
            display: none;
            position: relative;
            top: 0px;
            left: 0px;
            width: 900px;
            height: 60px;
            z-index: 300; 
            display:none;
            text-align:center;
            font-size:20px;
            font-weight: bold;
            border: 1px solid #CEE1EE;
            background-color: #F0F5F8;
            color:#369;
            line-height: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftPlaceHolder" runat="server">
    <div class="visualPadding">
        <dl class="portlet portlet-navigation-tree">
            <dt class="portletHeader"><span class="portletTopLeft"></span><span>文档分类导航</span> <span
                class="portletTopRight"></span></dt>
        </dl>
        <dl id="folder_menu" class="portletItem">


    
        </dl>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="viewlet-above-content" class="KSSTabArea KSSShowHideArea">

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
                                    <span class="discreet">在这里可以浏览系统内的文件和文件夹</span>
                                </div>
                            </div>
                        </div>
                        <div class="visualClear">
                        </div>
                        <div class="listingContents" id="previewBody">
         
     
                        </div>
                        <div>
                          <div class="overlay">
                          </div>
                          <div class="al">
                             没有更多文档
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
                    <p class="discreet toplist"><a href="/Document/Detail?docid=<%:doc.Id %>"><%:doc.FileDisplayName %></a></p>
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
                    <p class="discreet toplist"><a href="/Document/Detail?docid=<%:doc.Id %>"><%:doc.FileDisplayName %></a></p>
                <% }%>
            </dd>
        </dl>
    </div>
</asp:Content>

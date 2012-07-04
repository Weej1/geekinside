<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WorkShop.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/base/jquery-ui.css"
        type="text/css" />
    <link rel="stylesheet" href="/Scripts/jquery.ui.plupload/css/jquery.ui.plupload.css"
        type="text/css" />
    <link rel="Stylesheet" href="/Content/css/upload.css" type="text/css" />
    <link rel="Stylesheet" href="/Content/css/folder.css" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/jquery-ui.min.js"></script>
    <script type="text/javascript" src="http://bp.yahooapis.com/2.4.21/browserplus-min.js"></script>
    <script type="text/javascript" src="/Scripts/plupload.js"></script>
    <script type="text/javascript" src="/Scripts/plupload.gears.js"></script>
    <script type="text/javascript" src="/Scripts/plupload.silverlight.js"></script>
    <script type="text/javascript" src="/Scripts/plupload.flash.js"></script>
    <script type="text/javascript" src="/Scripts/plupload.browserplus.js"></script>
    <script type="text/javascript" src="/Scripts/plupload.html4.js"></script>
    <script type="text/javascript" src="/Scripts/plupload.html5.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.ui.plupload/jquery.ui.plupload.js"></script>
    <script type="text/javascript">
        // Convert divs to queue widgets when the DOM is ready
        var folders;
        $(function () {
            $.ajax({ url: "/Folder/AllFolders",
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    folders = data;
                }
            });

            $("#uploader").plupload({
                // General settings
                runtimes: 'silverlight,flash,html5,gears,browserplus',
                url: '/UploadHandler.ashx',
                max_file_size: '2gb',
                chunk_size: '1mb',
                max_file_count: 10,
                multipart: true,
                multipart_params: { id: '1' },

                // Resize images on clientside if we can
                resize: { width: 320, height: 240, quality: 90 },

                // Specify what files to browse for
                filters: [
                         { title: "MS Office文档", extensions: "doc,docx,ppt,pptx,xls,xlsx" },
                         { title: "Adobe PDF", extensions: "pdf" },
                         { title: "视频文件", extensions: "flv" },
                    ],

                multiple_queues: true,
                // Flash settings
                flash_swf_url: '/Scripts/plupload.flash.swf',

                // Silverlight settings
                silverlight_xap_url: '/Scripts/plupload.silverlight.xap'
            });

            var uploader = $('#uploader').plupload('getUploader');

            uploader.bind('UploadFile', function (up, file) {
                $("#attention").hide();
                var str = "<div class='file_detail' id='file_" + file.id + "'>" +
            "<h3 class='fill_one'>填写文档信息: " + file.name + "</h3>" +
            "<form action='' method='post' id='file_D'>" +
              "<table style='border-spacing:10px !important'>" +

                 "<tr>" +
                   "<th>" +
                    "<label><font color='red'>*</font>文件夹</label>" +
                   "</th>" +
                   "<td>" +
                    "<input type='button' name='typeChoose' class='type_choose' id='typeChoose_" + file.id + "'value='▼上传至'/>" +
                    "<label for='typeChoose_" + file.id + "' class='error' style='display:none' id='folder_label" + file.id + "'>" + '请选择上传至哪个文件夹' + "</label>" +
                    "<div class='folder_wrapper' id='folder_wrapper_" + file.id + "'>" +
                     "<div class='folder_header'>" +
                       "<span class='close_bg'>" +
                        "<img id='type_ui_" + file.id + "'src='/Content/images/tool-sprites.gif' alt='关闭' />" +
                       "</span>" +
                     "</div>" +
                    "<div class='folder_content'>" +
                     "<div class='folder_bg'>" +
                     "<dl class='portal_dl'>" +
                      "<dd class='portal_dd'>" +
                       "<ul class='ul_tree'>";
                $.each(folders, function (i, n) {
                    str = str + "<li class='navTreeItem'>" +
                                 "<div class='content_folder'>" +
                                    "<a href='#file_" + file.id + "' id='folder_" + n.Id + "' name='" + n.Id + "' " + 'onclick="triggerFolderListener(' + n.Id + "," + "'" + file.id + "'," + "'" + n.FolderName + "'" + ', event)">' +
                                      "<img src='/Content/images/pl.gif' alt='打开' class='collapseFlag'/>" +
                                      "<img src='/Content/images/mi.gif' alt='关闭' class='expandFlag'/>" +
                                      "<img src='/Content/images/folder.gif' class='folder_icon'/>" +
                                      "<span>" + n.FolderName + "</span>" +
                                    "</a>" +
                                  "</div>";
                    str = str + getSubFolders(n, file.id) + "</li>";
                });
                str = str + "</ul>" +
                        "</dd>" +
                       "</dl>" +
                      "</div>" +
                     "</div>" +
                    "</div>" +
                    "<br/>" +
                    "<label for='chooseType' class='error' style='display:none' id='type_label" + file.id + "'></label>" +
                  "</td>" +
                 "</tr>" +
                 "<tr>" +
                   "<th>" +
                    "<label><font color='red'>*</font>介绍</label>" +
                   "</th>" +
                   "<td>" +
                    "<textarea id='content" + file.id + "'name='content' rows='10' cols='70' class='required'></textarea>" +
                    "<br/>" +
                    "<label for='content" + file.id +  "' class='error' style='display:none' id='content_label" + file.id + "'></label>" +
                  "</td>" +
                 "</tr>" +                 
                 "<tr>" +
                   "<th>" +
                    "<label><font color='red'></font>标签</label>" +
                   "</th>" +
                   "<td>" +
                    "<input type='text' id='tag" + file.id + "' style='width:300px' />" +
                    "<br/>" +
                    "<label for='tag" + file.id  + "' class='tags' id='content_label" + file.id + "'>" + "多个标签请以空格分开" + "</label>" +
                  "</td>" +
                 "</tr>" +
                 "<tr>" +
                   "<th>" +
                     "<label><font color='red'>*</font>权限</label>" +
                   "</th>" +
                  "<td>" +
                    "<select id='auth_" + file.id + "' style='width:200px'>" +
                       "<option value='1'>所有人都可以查看和下载</option>" +
                       "<option value='2'>非本部门人不可以下载</option>" +
                       "<option value='3'>只有本部门人可以查看和下载</option>" +
                       "<option value='4'>只有本部门人可以查看</option>" +
                    "</select>" +
                  "</td>" +
                 "</tr>" +
                 "<tr>" +
                   "<td style='text-align:right' colspan='2'>" +
                     "<input type='hidden' value='0' name='folder' id='type_" + file.id + "'/>" +
                     "<input id='fileD_submit" + file.id + "' type='submit' disabled='disabled' class='submitBtnDisabled'/>" + "<div class='overlay' id='overlay_" + file.id + "'></div>"+
                   "</td>" +
                 "</tr>" +
               "</table>" +
            "</form>" +
          "</div>";
                $("#detail").append(str);
                $("#file_" + file.id).slideDown();
                triggerValidate(file.id, file, up);
                triggerAnimate(file.id);
            });

            uploader.bind('BeforeUpload', function (up, file, res) {
                $.extend(up.settings.multipart_params, { id: file.id });
            });

            uploader.bind('FileUploaded', function (up, file, res) {
                $("#fileD_submit" + file.id).removeAttr("disabled");
                $("#fileD_submit" + file.id).attr("class", "submitBtn");
            });

            uploader.bind('FilesRemoved', function (up, files) {
                if (files[0].status != 1) {
                    $.ajax({ url: "/Document/CancelFileUploaded",
                        type: 'post',
                        dataType: 'json',
                        data: { 'id': files[0].id, 'fileName': files[0].name }
                    });
                }
                $("#file_" + files[0].id).remove();
                if (up.files.length == 0) {
                    $("#attention").show();
                }
            });

        });
    </script>

    <script type='text/javascript'>
        function getSubFolders(n, file_id) {
            var s = "";
            var isHas = false;
            $.each(n.SubFolders, function (i, k) {
                if (i == 0) {
                    s = s + "<ul class='ul_tree sub'>";
                    isHas = true;
                }
                s = s + "<li class='navTreeItem'>" +
                            "<div class='content_folder'>" +
                                "<a href='#file_" + file_id + "' id='folder_" + k.Id + "' name='" + k.Id + "'" + 'onclick="triggerFolderListener(' + k.Id + "," + "'" + file_id + "'," + "'" +k.FolderName + "'" + ', event)">' +
                                   "<img src='/Content/images/pl.gif' alt='打开' class='collapseFlag'/>" +
                                   "<img src='/Content/images/mi.gif' alt='关闭' class='expandFlag'/>" +
                                   "<img src='/Content/images/folder.gif' class='folder_icon'/>" +
                                   "<span>" + k.FolderName + "</span>" +
                                "</a>" +
                            "</div>";
                s = s + getSubFolders(k, file_id) + "</li>";
            });
            if (isHas) {
                s = s + "</ul>"; 
            }
            return s;
        }
        function triggerAnimate(file_id) {
            $("#typeChoose_" + file_id).click(function (e) {
                $(this).parents('td:first').children('.folder_wrapper:first').toggle();
            });
            $('#type_ui_'+file_id).click(function (e) {
                $(this).parents('.folder_wrapper:first').css('display', 'none');
            });
            $("#folder_wrapper_" + file_id + "  img.collapseFlag").click(function (e) {
                $(this).css('display', 'none');
                $(this).parent(this).children('.expandFlag').css('display', 'inline');
                $(this).parents('.navTreeItem:first').children('ul:first').css('display', 'block');
                return false;
            });
            $("#folder_wrapper_" + file_id + "  img.expandFlag").click(function (e) {
                $(this).css('display', 'none');
                $(this).parent(this).children('.collapseFlag').css('display', 'inline');
                $(this).parents('.navTreeItem:first').children('ul:first').css('display', 'none');
                return false;
            });
        }

        function triggerFolderListener(folder_id, file_id, file_name,e) {
             $("#folder_wrapper_" + file_id).slideUp();
             $("#type_" + file_id).val(folder_id);
             $("#folder_label" + file_id).css("display", "none");
             $("#typeChoose_" + file_id).val(file_name);
             if (e.stopPropagation) {
                 e.stopPropagation();
               }
             else {
                 e.cancelBubble = true;
             }
        }
    </script>

    <script type="text/javascript">
        function triggerValidate(file_id, file, up) {
            var isLegal = false;
            $("#content" + file_id).blur(function (e) {
                if ($("#content" + file_id).val() == "") {
                    $("#content_label" + file_id).css("display", "inline-block");
                    $("#content_label" + file_id).html("介绍不能为空");
                    isLegal = false;
                }
                else if ($("#content" + file_id).val().length >= 5 && $("#content" + file_id).val().length <= 300) {
                    $("#content_label" + file_id).css("display", "none");
                    isLegal = true;
                }
                else {
                    $("#content_label" + file_id).css("display", "inline-block");
                    $("#content_label" + file_id).html("介绍的字数在5到300之间");
                    isLegal = false;
                }
            });

            $("#fileD_submit" + file_id).click(function (e) {
                $("#content" + file_id).blur();
                if ($("#type_" + file_id).val() == 0) {
                    $("#folder_label" + file_id).css("display", "inline-block");
                    isLegal = false;
                }
                var fileName = file.name;
                var str = fileName.substring(fileName.lastIndexOf("."));
                if (isLegal) {
                    var tags = $("#tag" + file_id).val();
                    tagArr = tags.split(/\s+/);
                    var tagStr = "";

                    for (var i = 0; i < tagArr.length; i++) {
                        for (var j = i + 1; j < tagArr.length; j++) {
                            if (tagArr[j] === tagArr[i]) {
                                tagArr.splice(j, 1);
                                j--;
                            }
                        }
                    }
                    $.each(tagArr, function (i, n) {
                        tagStr += n + " ";
                    });
                    var overlay = $("#overlay_" + file_id);
                    overlay.css("width", $("#file_" + file_id).width());
                    overlay.css("height", $("#file_" + file_id).height() + 15);
                    overlay.css("display", "block");
                    $.ajax({ url: "/Document/FileDetail",
                        type: 'post',
                        dataType: 'json',
                        data: { 'fileDiskName': file_id + str,
                            'fileDisplayName': file.name,
                            'size': file.size,
                            'description': $("#content" + file_id).val(),
                            'folderId': $("#type_" + file_id).val(),
                            'authLevel':$("#auth_" + file_id).val(),
                            'tags': tags
                        },
                        success: function (response) {
                            if (response) {
                                alert("文件: "+file.name + "\n提交成功");
                                $("#file_" + file_id).remove();
                                up.removeFile(up.getFile(file_id));
                                if (up.files.length == 0) {
                                    $("#attention").show();
                                }
                            } else {
                                alert("提交失败，请重新提交");
                            }
                            overlay.css("display", "none");
                        }
                    });
                }
                return false;
            });
        }
    </script>
    <style type="text/css">
        #file_D label.error
        {
            display: -moz-inline-box;
            display: inline-block;
            font-size: 11px;
            font: red;
            background: #fbfcda url('/Content/images/cancel.png') no-repeat left;
            border: 1px solid #dbdbd3;
            width: 200px !important;
            height: 20px !important;
            margin-top: 1px;
            padding-top: 4px;
            padding-left: 20px;
        }
        
        #file_D label.tags
        {
            display: -moz-inline-box;
            display: inline-block;
            font-size: 11px;
            font: red;
            background: #fbfcda;
            border: 1px solid #dbdbd3;
            width: 200px !important;
            height: 20px !important;
            margin-top: 1px;
            padding-top: 4px;
            padding-left: 20px;
        }
        
        .overlay {
            background: #000 url('/Content/images/loadingAnimation.gif') no-repeat center;
            filter: alpha(opacity=50); 
            opacity: 0.5; 
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            z-index: 300; 
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div>
        <div>
            <form method="post" action="" enctype="multipart/form-data">
            <div id="uploader">
            </div>
            </form>
        </div>
        <br />
        <div id="attention">
            <h3 id="Remind_S" class="header">
                文档上传须知</h3>
            <div id="taskRemind">
                <p>
                    每次最多上传10份文档，每份文档不超过2GB</p>
                <p>
                    为了保证文档能正常显示，我们支持以下格式的文档上传</p>
                <table cellpadding="0" width="600" cellspacing="0" border="0">
                    <tbody>
                        <tr>
                            <td class="r">
                                MS Office文档
                            </td>
                            <td valign="top">
                                <span class="icon doc"></span>doc,docx<span class="icon ppt"></span>
                                ppt,pptx<span class="icon xls"></span>xls,xlsx
                            </td>
                        </tr>
                        <tr>
                            <td class="r">
                                PDF
                            </td>
                            <td>
                                <span class="icon pdf"></span>pdf
                            </td>
                        </tr>
                        <tr>
                            <td class="r">
                                视频
                            </td>
                            <td>
                                <span class="icon wmv"></span>flv
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div id="detail">
        </div>
    </div>
</asp:Content>

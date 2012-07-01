<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WorkShop.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/base/jquery-ui.css" type="text/css" />
    <link rel="stylesheet" href="/Scripts/jquery.ui.plupload/css/jquery.ui.plupload.css" type="text/css" />
    <link rel="Stylesheet" href="/Content/css/upload.css" type="text/css" />

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
        $(function () {
            $("#uploader").plupload({
                // General settings
                runtimes: 'html5,silverlight,flash,gears,browserplus',
                url: '/UploadHandler.ashx',
                max_file_size: '2gb',
                chunk_size: '128kb',
                max_file_count: 10,
                multipart: true,
                multipart_params: { id: '1' },

                // Resize images on clientside if we can
                resize: { width: 320, height: 240, quality: 90 },

                // Specify what files to browse for
                filters: [
                         { title: "MS Office文档", extensions: "doc,docx,ppt,pptx,xls,xlsx,vsd,rtf" },
                         { title: "Adobe PDF", extensions: "pdf" },
                         { title: "文本文件", extensions: "txt" },
                         { title: "视频文件", extensions: "wmv" },
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
                var str = "<div class='file_detail' id=file_" + file.id + ">" +
            "<h3 class='fill_one'>填写文档信息: " + file.name + "</h3>" +
            "<form action='' method='post' id='file_D'>" +
              "<table style='border-spacing:10px !important'>" +
                 "<tr>" +
                   "<th>" +
                    "<label><font color='red'>*</font>介绍</label>" +
                   "</th>" +
                   "<td>" +
                    "<textarea id='content" + file.id + "'name='content' rows='10' cols='70' class='required'></textarea>" +
                    "<br/>" +
                    "<label for='content' class='error' style='display:none' id='content_label" + file.id + "'></label>" +
                  "</td>" +
                 "</tr>" +
                 "<tr>" +
                   "<td style='text-align:right' colspan='2'>" +
                     "<input type='hidden' value='1' name='type'/>" +
                     "<input id='fileD_submit" + file.id + "' type='submit' disabled='disabled' class='submitBtnDisabled'/>" +
                   "</td>" +
                 "</tr>" +
               "</table>" +
            "</form>" +
          "</div>";
                $("#detail").append(str);
                $("#file_" + file.id).slideDown();
                triggerValidate(file.id, file, up);
            });

            uploader.bind('BeforeUpload', function (up, file, res) {
                $.extend(up.settings.multipart_params, { id: file.id });
            });

            uploader.bind('FileUploaded', function (up, file, res) {
                $("#fileD_submit" + file.id).removeAttr("disabled");
                $("#fileD_submit" + file.id).attr("class","submitBtn") ;
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
    <script type="text/javascript">
        function triggerValidate(file_id, file, up) {
            var isLegal = false;
            $("#content" + file_id).blur(function (e) {
                if ($("#content" + file_id).val() == "") {
                    $("#content_label" + file_id).css("display", "inline-block");
                    $("#content_label" + file_id).html("介绍不能为空");
                    isLegal = false;
                }
                else if ($("#content" + file_id).val().length >= 10 && $("#content" + file_id).val().length <= 300) {
                    $("#content_label" + file_id).css("display", "none");
                    isLegal = true;
                }
                else {
                    $("#content_label" + file_id).css("display", "inline-block");
                    $("#content_label" + file_id).html("介绍的字数在10到300之间");
                    isLegal = false;
                }
            });

            $("#fileD_submit" + file_id).click(function (e) {
                $("#content" + file_id).blur();
                var fileName = file.name;
                var str = fileName.substring(fileName.lastIndexOf("."));
                if (isLegal) {
                    $.ajax({ url: "/Document/FileDetail",
                        type: 'post',
                        dataType: 'json',
                        data: { 'fileDiskName': file.id + str,
                            'fileDisplayName': file.name,
                            'size': file.size,
                            'description': $("#content" + file_id).val(),
                            'folderId': 1,
                            'success': function (response) {
                                if (response) {
                                    $("#file_" + file_id).remove();
                                    if (up.files.length == 0) {
                                        $("#attention").show();
                                    }

                                } else {
                                    alert("提交失败，请重新提交");
                                }
                            }
                        }
                    });
                }
                return false;
            });
        };
    </script>
    <style type="text/css">
        #file_D label.error
        {
          display:-moz-inline-box;
          display:inline-block; 
          font-size: 11px;
          font:red;
          background: #fbfcda url('/Content/images/cancel.png') no-repeat left;
          border:1px solid #dbdbd3;
          width:200px !important;
          height:20px !important;
          margin-top:1px;
          padding-top:4px;
          padding-left:20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
     <div>
        <div>
           <form method="post" action="" enctype="multipart/form-data">
             <div id="uploader"> </div>
           </form>
        </div>
        <br />
        <div id="attention">
          <h3 id="Remind_S" class="header">文档上传须知</h3>
          <div id="taskRemind">
            <p>每次最多上传10份文档，每份文档不超过2GB</p>
            <p>为了保证文档能正常显示，我们支持以下格式的文档上传</p>
            <table cellpadding="0" width="600" cellspacing="0" border="0">
              <tbody>
                <tr>
                  <td class="r">MS Office文档</td>
                  <td valign="top">
                    <span class="icon doc"></span> doc,docx  &nbsp;&nbsp;
                    <span class="icon ppt"></span> ppt,pptx  &nbsp;&nbsp;
                    <span class="icon xls"></span> xls,xlsx &nbsp;&nbsp;
                    <span class="icon vsd"></span> vsd &nbsp;&nbsp;
                    <span class="icon rtf"></span> rtf &nbsp;&nbsp;
                  </td>
                </tr>
                <tr><td class="r">PDF</td><td><span class="icon pdf"></span> pdf</td></tr>
                <tr><td class="r">纯文本</td><td><span class="icon txt"></span> txt</td></tr>
                <tr><td class="r">视频</td><td><span class="icon wmv"></span> wmv</td></tr>
              </tbody>
            </table>
          </div>
         </div>
         <div id="detail">
        </div>
     </div>
</asp:Content>



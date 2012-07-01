<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
    <meta content="text/html;charset=utf-8" http-equiv="Content-Type" />
    <link type="text/css" rel="stylesheet" media="all" href="/Content/css/style.css" />
    <title>登录 GIKMS</title>
    <script type="text/javascript" src="../../Scripts/jquery-1.2.6.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.validate.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#loginForm").validate();          
        });
    </script>

    <style type="text/css">
        #loginForm label.error
        {
          display:-moz-inline-box;
          display:inline-block;
          font-size: 11px;
          background: #fbfcda url('/Content/images/cancel.png') no-repeat left;
          border:1px solid #dbdbd3;
          width:200px !important;
          height:10px !important;
          margin-top:-5px;
          padding-top:5px;
          padding-left:20px;
        }
    </style>
</head>

<body>
    <div id="wrapper">
		<div id="left">
		</div>
		<div id="right" style="height:350px">
			<img src="/Content/images/login_18.png" />
			<div style="font-weight:bold;margin:20px 0;">欢迎进入Geek Inside知识管理系统</div>
			<form action="/Index/Login" method="post" id="loginForm">
				<fieldset>
                   <%var error = ViewData["errorMsg"];%>
                   <%if (error != null){ %>
                    <div>
                      <label id="error" class="error" style="margin-top:-10px;height:20px !important"><%=error%></label>
                    </div>
                   <% } %>
					<div class="loginrow">
						<label>用户名：</label>
						<div class="inputarea">
							<input id="username" name="employeeNumber" size="40" style="width:200px;height:15px;font-size:13px" type="text" class="required digits"/>
                            <br/>
                            <label class="error" generated="true" for="username" style="display:none">用户名必须填写</label>						
                        </div>
					</div>
					<div class="loginrow">
						<label>密码：</label>
						<div class="inputarea">
							<input id="password" name="password" size="40" style="width:200px;height:15px;font-size:13px" type="password" class="required"/>
                            <br/>
                            <label class="error" generated="true" for="password" style="display:none">密码必须填写</label>						
                        </div>
					</div>
					
					<div class="form-actions">
					  <input class="btn btn-primary" name="commit" type="submit" value="登录"/>
					</div>
				</fieldset>
			</form>
		</div>
	</div>
	<div id="footer">
		<p>Geek Inside 小组全力打造</p>
	</div>
</body>

</html>

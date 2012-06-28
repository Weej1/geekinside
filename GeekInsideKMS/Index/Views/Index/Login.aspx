<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>

<head>
    <meta content="text/html;charset=utf-8" http-equiv="Content-Type" />
    <link type="text/css" rel="stylesheet" media="all" href="/Content/css/style.css">
    <title>登录 GIKMS</title>
</head>

<body>
    <div id="wrapper">
		<div id="left">
		</div>
		<div id="right">
			<img src="/Content/images/login_18.png" />
			<div style="font-weight:bold;margin:20px 0;">欢迎进入Geek Inside知识管理系统</div>
			
			<form action="/Index/Index" method="post">
				<fieldset>
					<div class="loginrow">
						<label>用户名：</label>
						<div class="inputarea">
							<input id="username" name="username" size="50" style="width:200px;" type="text">
						</div>
					</div>
					<div class="loginrow">
						<label>密码：</label>
						<div class="inputarea">
							<input id="password" name="password" size="50" style="width:200px;" type="password">
						</div>
					</div>
					
					<div class="form-actions">
					  <input class="btn btn-primary" name="commit" type="submit" value="登录">
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

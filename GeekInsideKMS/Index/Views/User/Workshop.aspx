﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Workshop.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MiddlePlaceHolder" runat="server">
    <div id="region-content" class="documentContent">
        <div id="content">
            <div id="workdesk-content">
                <div id="deskDocsList">
                    <p class="discreet">
                        下面是您所有已发布的文档列表：</p>
                    <div class="contentItem">
                        <div class="itemIcon">
                            <img src="/Content/images/icons/pdf.gif">
                        </div>
                        <div class="itemInfo">
                            <h1>
                                <a class="title clean" target="_blank" href="/Document/Detail">Packtpub.NHibernate.3.0.Cookbook.Oct.2010.pdf</a>
                                <span class="docListOperation">
                                    <a href="#">编辑</a>
                                    <a href="#">删除</a>
                                </span>
                            </h1>
                            <p class="discreet" style="margin: -5pt 0 2px;">
                                zhaoyulee 负责， 更新于 2012-06-26 2.21 MB， 文件位于/文档库/公司管理/1123
                            </p>
                        </div>
                    </div>
                    <div class="contentItem">
                        <div class="itemIcon">
                            <img src="/Content/images/icons/ms-word.gif">
                        </div>
                        <div class="itemInfo">
                            <h1>
                                <a class="title clean" target="_blank" href="#">123.doc</a>
                                <span class="docListOperation">
                                    <a href="#">编辑</a>
                                    <a href="#">删除</a>
                                </span>
                            </h1>
                            <p class="discreet" style="margin: -5pt 0 2px;">
                                zhaoyulee 负责， 更新于 2012-06-26 2.21 MB， 文件位于/文档库/公司管理/1123
                            </p>
                        </div>
                    </div>
                    <div class="contentItem">
                        <div class="itemIcon">
                            <img src="/Content/images/icons/ms-word.gif">
                        </div>
                        <div class="itemInfo">
                            <h1>
                                <a class="title clean" target="_blank" href="#">Packtpub.NHibernate.3.0.Cookbook.Oct.2010.pdf</a>
                                <span class="docListOperation">
                                    <a href="/Document/Edit?id=1">编辑</a>
                                    <a href="/Document/Delete?id=1">删除</a>
                                </span>
                            </h1>
                            <p class="discreet" style="margin: -5pt 0 2px;">
                                zhaoyulee 负责， 更新于 2012-06-26 2.21 MB， 文件位于/文档库/公司管理/1123
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
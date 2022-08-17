﻿<%@ Page Language="c#" AutoEventWireup="false" Inherits="ErrorPage" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Error.aspx.cs" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Error</title>
    <meta http-equiv="Expires" content="0" />
    <style type="text/css">
        .ClosePopupBT
        {
            display:none;
            top:20px;
            right:20px;
            position:absolute;
            padding:9px 21px 8px 22px;
            border-radius:3px;
            background-color:white;
            border:1px solid #c6c6c6;
            font-size:14px;
            color:#2C86D3;
        }
        input[type="button"]:hover
        {
          background-color:#f0f0f0;
          cursor: pointer;
        }
        .PopupNewStyle .ClosePopupBT
        {
            display:block;
        }
    </style>
</head>
<body class="Dialog Error" onload="javascript:ClientPageLoad()">
    <div id="PageContent" class="PageContent DialogPageContent">
        <table id="formTable">
            <tr>
                <td>
                    <form id="form1" runat="server">
                    <div class="Header">
                        <table border="0">
                            <tr>
                                <td class="ViewCaption">
                                    <h1>
                                        <asp:Literal ID="ApplicationTitle" runat="server" Text="Test Application" />
                                    </h1>
                                </td>
                                <td class="InfoMessagesPanel">
                                    <asp:Literal ID="InfoMessagesPanel" runat="server" Text=""></asp:Literal>
                                </td>
                            </tr>
                        </table>
                        <input type="button" class="ClosePopupBT" id="ClosePopupBT" onclick="Back();" value="Close"/>
                    </div>
                    <table class="DialogContent Content" border="0">
                        <tr>
                            <td class="ContentCell">
                                <asp:Table ID="Table1" runat="server" Width="100%" BorderWidth="0px" CellPadding="0"
                                    CellSpacing="0">
                                    <asp:TableRow ID="TableRow2" runat="server">
                                        <asp:TableCell runat="server" ID="ViewSite">
                                            <h2 id="FormCaption">
                                                <asp:Literal ID="ErrorTitleLiteral" runat="server" Text="Application Error" /></h2>
                                            <asp:Panel runat="server" ID="ErrorPanel" Width="100%">
                                                <p class="StaticText" id="MainErrorText">
                                                    <asp:PlaceHolder ID="ApologizeMessage" runat="server">
                                                        We are currently unable to serve your request.<br />
                                                    You could go&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:Back();">back</asp:HyperLink>&nbsp;and
                                                    try again or  
                                                    <asp:LinkButton ID="NavigateToStart" runat="server" OnClick="NavigateToStart_Click">restart the application</asp:LinkButton>.
                                                    </asp:PlaceHolder>
                                                </p>

                                                <asp:Panel ID="Details" runat="server" Width="100%">
                                                    <a style="text-decoration: underline; cursor: pointer;" id="ShowErrorDetails" onclick="javascript:ShowDetails();">
                                                        Show Error details</a>
                                                    <div id="DetailsContent" style="display: none;">
                                                        <span class="StaticText" style="font-weight: bold;">Error details</span>
                                                        <hr />
                                                        <a style="text-decoration: underline; cursor: pointer;" id="HideErrorDetails"
                                                            onclick="javascript:HideDetails();">Hide Error details</a>
                                                        <pre class="ErrorDetails">
                                                            <asp:Literal ID="DetailsText" runat="server" />
                                                        </pre>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="ReportForm" runat="server" Width="100%">
                                                    <span class="StaticText" style="font-weight: bold;">Report error</span>
                                                    <hr />
                                                    <p class="StaticText">
                                                        This error has been logged. If you have additional information that you believe
                                                        may have caused this error please report the problem.</p>
                                                    <table border="0">
                                                        <tr>
                                                            <td style="text-align: right; padding-bottom: 10px">
                                                                <asp:TextBox ID="DescriptionTextBox" runat="server" Columns="60" Rows="8" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right;">
                                                                <asp:Button ID="ReportButton" runat="server" Text="Send Report" OnClick="ReportButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <div id="Spacer" class="Spacer">
                    </div>
                    </form>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            <!--
            function Back() {
                if(window.xaf && xaf.Utils.isNestedWindow) {
                    window.parent.closeActiveXafPopupWindow();
                }
                else {
                    history.go(-1);
                }
                return false;
            }
            function ShowDetails() {
                document.getElementById('DetailsContent').style.display = 'block';
                document.getElementById('ShowErrorDetails').style.display = 'none';
                return false;
            }
            function HideDetails() {
                document.getElementById('DetailsContent').style.display = 'none';
                document.getElementById('ShowErrorDetails').style.display = 'block';
                return false;
            }
            function ClientPageLoad() {
                if(window != window.top) {
                    document.getElementById('MainErrorText').style.display = 'none';
                }
            }
            //-->
        </script>
    </div>
</body>
</html>

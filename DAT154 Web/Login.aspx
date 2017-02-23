<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DAT154_Web.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="usernameLabel" runat="server" Text="Your e-mail: ">
        <asp:TextBox ID="email" name="email" runat="server" ClientIDMode="Static"></asp:TextBox>
    </asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Your password: ">
        <asp:TextBox ID="password" name="password" runat="server" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
    </asp:Label>
    <br />
    <input id="submitLogin" type="submit" value="Log in" />

</asp:Content>

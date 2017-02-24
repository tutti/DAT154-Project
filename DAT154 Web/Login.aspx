<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DAT154_Web.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Log in</h1>

    <asp:Repeater ID="errorList" runat="server">
        <ItemTemplate>
            <span style="color: red;"><%# Container.DataItem %></span><br />
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="emailLabel" runat="server" Text="Your e-mail: " style="display: inline-block; width: 10em;"></asp:Label>
    <asp:TextBox ID="email" runat="server" ClientIDMode="Static"></asp:TextBox>
    <br />

    <asp:Label ID="passwordLabel" runat="server" Text="Your password: " style="display: inline-block; width: 10em;"></asp:Label>
    <asp:TextBox ID="password" runat="server" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
    <br />

    <input id="submitLogin" type="submit" value="Log in" />

</asp:Content>

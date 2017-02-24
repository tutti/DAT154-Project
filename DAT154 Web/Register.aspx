<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DAT154_Web.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Register account</h1>

    <asp:Repeater ID="errorList" runat="server">
        <ItemTemplate>
            <span style="color: red;"><%# Container.DataItem %></span><br />
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="emailLabel" runat="server" Text="Your e-mail:" style="display: inline-block; width: 10em;"></asp:Label>
    <asp:TextBox ID="email" runat="server" ClientIDMode="Static"></asp:TextBox>
    <br />

    <asp:Label ID="nameLabel" runat="server" Text="Your name:" style="display: inline-block; width: 10em;"></asp:Label>
    <asp:TextBox ID="name" runat="server" ClientIDMode="Static"></asp:TextBox>
    <br />

    <asp:Label ID="password1Label" runat="server" Text="Your password:" style="display: inline-block; width: 10em;"></asp:Label>
    <asp:TextBox ID="password1" runat="server" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
    <br />

    <asp:Label ID="password2Label" runat="server" Text="Your password (again):" style="display: inline-block; width: 10em;"></asp:Label>
    <asp:TextBox ID="password2" runat="server" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
    <br />

    <input id="submitLogin" type="submit" value="Register" />

</asp:Content>

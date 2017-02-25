<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="DAT154_Web.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Repeater ID="errorList" runat="server">
        <ItemTemplate>
            <span style="color: red;"><%# Container.DataItem %></span><br />
        </ItemTemplate>
    </asp:Repeater>

    <style>
        .label {
            display: inline-block;
            width: 100px;
            color: black;
            text-align: left;
        }

        input.number {
            width: 100px;
        }
    </style>

    <h1>Finding of the Room</h1>

    <div>
        <asp:Label ID="bedsLabel" CssClass="label" runat="server" Text="Beds:"></asp:Label>
        <asp:TextBox ID="minBeds" CssClass="number" runat="server"></asp:TextBox>
        -
        <asp:TextBox ID="maxBeds" CssClass="number" runat="server"></asp:TextBox>
        <br />
    </div>

    <div>
        <asp:Label ID="sizeLabel" CssClass="label" runat="server" Text="Room size:"></asp:Label>
        <asp:TextBox ID="minSize" CssClass="number" runat="server"></asp:TextBox>
        -
        <asp:TextBox ID="maxSize" CssClass="number" runat="server"></asp:TextBox>
        <br />
    </div>

    <div>
        <asp:Label ID="qualityLabel" CssClass="label" runat="server" Text="Quality:"></asp:Label>
        <asp:TextBox ID="minQuality" CssClass="number" runat="server"></asp:TextBox>
        -
        <asp:TextBox ID="maxQuality" CssClass="number" runat="server"></asp:TextBox>
        <br />
    </div>

    <div>
        <asp:Label ID="startDateLabel" CssClass="label" runat="server" Text="From:"></asp:Label>
        <asp:TextBox ID="startDate" runat="server"></asp:TextBox>
    </div>

    <div>
        <asp:Label ID="endDateLabel" CssClass="label" runat="server" Text="Until:"></asp:Label>
        <asp:TextBox ID="endDate" runat="server"></asp:TextBox>
    </div>

    <asp:Button ID="searchBtn" runat="server" Text="Search" />
</asp:Content>

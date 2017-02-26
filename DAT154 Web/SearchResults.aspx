<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="DAT154_Web.SearchResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Resulted</h3>

    <asp:Label ID="bedsLabel" runat="server" Text="Beds:" style="width: 100px;"></asp:Label>
    <%= foundRoom.beds %>
    <br />

    <asp:Label ID="sizeLabel" runat="server" Text="Room size:" style="width: 100px;"></asp:Label>
    <%= foundRoom.room_size %>
    <br />

    <asp:Label ID="qualityLabel" runat="server" Text="Quality:" style="width: 100px;"></asp:Label>
    <%= foundRoom.quality %>
    <br />

    <asp:HiddenField ID="room_id" runat="server" value="<%# foundRoom.id %>" />
    <asp:HiddenField ID="start_date" runat="server" value="<%# startDate %>" />
    <asp:HiddenField ID="end_date" runat="server" value="<%# endDate %>" />

    <asp:Button ID="nope" runat="server" Text="Unacceptable!" PostBackUrl="~/Search.aspx" />
    <asp:Button ID="yup" runat="server" Text="Is good!" />

</asp:Content>

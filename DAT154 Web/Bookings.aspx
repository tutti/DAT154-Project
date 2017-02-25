<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="DAT154_Web.Bookings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Import Namespace="DAT154_Libs" %>

    <style>
        table {
            border-collapse: collapse;
        }

        th, td {
            border: 1px solid black;
            padding: 5px;
        }
    </style>

    <h3>You are booking</h3>

    <asp:Repeater ID="bookingList" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Room</th>
                    <th>From</th>
                    <th>Until</th>
                    <th>Status</th>
                    <th></th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td><%# room_numbers[((Booking)Container.DataItem).room_id] %></td>
                    <td><%# ((Booking)Container.DataItem).start_date.ToString("yyyy-MM-dd") %></td>
                    <td><%# ((Booking)Container.DataItem).end_date.ToString("yyyy-MM-dd") %></td>
                    <td><%# status[((Booking)Container.DataItem).booking_status] %></td>
                    <td>
                        <button name="cancel_id" runat="server" value="<%# ((Booking)Container.DataItem).id %>" type="submit" visible="<%# ((Booking)Container.DataItem).booking_status == Booking.STATUS.BOOKED %>">Cancel</button>
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>

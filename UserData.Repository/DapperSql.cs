﻿using Cosmonaut.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Repository
{
    public class DapperSql
    {
        public const string CheckDetail = "Select EmailId as EmailId, Hash_Password as HashPassword from UserOrAdminSignup_Table where EmailId=@emailid ";
        public const string LoginDetail = "Select Hash_Password as HashPassword,Salt_Password as SaltPassword from UserOrAdminSignup_Table where EmailId=@emailid";
        public const string InsertDetail = "INSERT INTO UserOrAdminSignup_Table(Name,Gender,DOB,EmailId,Address,Phonenumber,Hash_Password,Salt_Password,Is_Admin) VALUES(@name,@gender,@dob,@email,@address,@phonenumber,@hash,@salt,@admin)";
        public const string GetUserDetail = "\tselect Is_Admin as IsAdmin ,EmailId as UserEmailid,UserId as UserId from UserOrAdminSignup_Table where EmailId=@emailid";
        public const string DashBoardData = "select Bus_Id as BusId,Bus_Name as BusName,Window_Seats as WindowSeats,NonWindow_Seats as NonWindowSeats,Is_Deleted as IsDeleted,Update_Time_Stramp as UpdateTimeStamp,Create_Time_Stramp as CreateTimeStamp From BusMaster_Table  where Is_Deleted=0 ORDER BY Update_Time_Stramp DESC";
        public const string GetBusDataById = "Select Bus_Id as BusId,Bus_Name as BusName,Window_Seats as WindowSeats,NonWindow_Seats as NonWindowSeats from BusMaster_Table where Bus_Id=@id and Is_Deleted=0";
        public const string BusDetailUpdate = "Update BusMaster_Table Set Bus_Name=@name,Window_Seats=@windowseats,NonWindow_Seats=@nonwindow,Update_Time_Stramp=@updatedtimestamp  where Bus_Id=@id";
        public const string InsertBus = "INSERT INTO BusMaster_Table(Bus_Name,Window_Seats,NonWindow_Seats,Update_Time_Stramp,Create_Time_Stramp) VALUES(@name,@windowseats,@nonwindow,@updatedtimestamp,@createdtimestamp)";
        public const string GetTravelScheduleData = "SELECT bts.TravelId,\r\n       bts.BusId AS BusId,\r\n\t   bts.Source,\r\n\t   bts.Destination, \r\n\t   bts.Fare,\r\n\t   bts.DepatureDateTime, \r\n\t   bts.Is_Deleted AS IsDeleted,\r\n\t   bts.Update_Time_Stamp AS UpdateTimeStamp,\r\n\t   bts.Create_Time_Stamp AS CreateTimeStamp,\r\n\t   (SELECT COUNT(PassengerId) FROM Passengertable pt WHERE pt.TravelId = bts.TravelId and pt.Iscanceled=0 and pt.IsPaid=1) AS SeatsAvailable\r\n\t   FROM  BusTravelScheduleTbl bts\r\n\t   WHERE    bts.Is_Deleted = 0    AND bts.BusId = @busid\r\n\t   ORDER BY  bts.Update_Time_Stamp DESC;";
        public const string GetScheduleByTravelId = "select TravelId as TravelId,BusId as BusId,Source as Source,Destination as Destination,Fare as Fare,CONVERT(Char(16), DepatureDateTime ,20) AS DepatureDateTime ,Is_Deleted as IsDeleted,Update_Time_Stamp as UpdateTimeStamp,Create_Time_Stamp as CreateTimeStamp From BusTravelScheduleTbl  where TravelId=@travelid and Is_Deleted=0";
        public const string CreateTravelId = "INSERT INTO BusTravelScheduleTbl(BusId,Source,Destination,Fare,DepatureDateTime,Update_Time_Stamp,Create_Time_Stamp)VALUES(@busid,@source,@destination,@fare,@depaturetime,@updatetimestamp,@createtimestamp)";
        public const string UpdateTravelId = "UPDATE BusTravelScheduleTbl SET BusId=@busid,Source=@source,Destination=@destination,Fare=@fare,DepatureDateTime=@depaturetime,Update_Time_Stamp=@updatetimestamp WHERE TravelId=@travelid ";
        public const string UserPage = "SELECT tt.ReferenceId AS referenceNumber,tt.TravelId AS TravelId,tt.Ticketid AS ticketId, bt.Source AS Source,bt.Destination AS Destination,bt.DepatureDateTime AS DateTime\r\n\t\tFROM TicketTable tt\r\n\t\tINNER JOIN BusTravelScheduleTbl bt\r\n\t\tON tt.TravelId = bt.TravelId\r\n\t\twhere tt.UserId =@userid";
        public const string GetUserProfile = "SELECT Name AS Name,EmailId AS EmailId,Address AS Address,Phonenumber AS Phonenumber FROM UserOrAdminSignup_Table WHERE UserId=@id";
        public const string FindBusesWithDate = "SELECT bts.TravelId,\r\nbts.BusId,\r\nbts.Source,\r\nbts.Destination,\r\nbts.Fare,\r\nbts.DepatureDateTime,\r\nbts.Is_Deleted AS IsDeleted,\r\nbts.Update_Time_Stamp AS UpdateTimeStamp,\r\nbts.Create_Time_Stamp AS CreateTimeStamp,\r\n(SELECT Bus_Name FROM BusMaster_Table WHERE Bus_Id=bts.BusId ) AS BName,\r\n(SELECT COUNT(PassengerId) FROM Passengertable pt WHERE pt.TravelId = bts.TravelId and pt.IsPaid=1 and IsCanceled=0) AS SeatsAvailable\r\nFROM\r\nBusTravelScheduleTbl bts\r\nWHERE    bts.Is_Deleted = 0 AND bts.Source=@source and bts.Destination=@destination and bts.DepatureDateTime=@date and bts.BusId!=0\r\nORDER BY   bts.Update_Time_Stamp DESC;";
        public const string FindBusesWithoutDate = "SELECT bts.TravelId,\r\nbts.BusId,\r\nbts.Source,\r\nbts.Destination,\r\nbts.Fare,\r\nbts.DepatureDateTime,\r\nbts.Is_Deleted AS IsDeleted,\r\nbts.Update_Time_Stamp AS UpdateTimeStamp,\r\nbts.Create_Time_Stamp AS CreateTimeStamp,\r\n(SELECT Bus_Name FROM BusMaster_Table WHERE Bus_Id=bts.BusId ) AS BName,\r\n(SELECT COUNT(PassengerId) FROM Passengertable pt WHERE pt.TravelId = bts.TravelId and pt.IsCanceled=0 and pt.Ispaid=1) AS SeatsAvailable\r\nFROM\r\nBusTravelScheduleTbl bts\r\nWHERE    bts.Is_Deleted = 0 AND bts.Source=@source and bts.Destination=@destination and bts.BusId!=0\r\nORDER BY   bts.Update_Time_Stamp DESC;";
        public const string GetSeatNo = "SELECT Seatno AS Seatno FROM PassengerTable WHERE TravelId=12  and IsCanceled=0 and (Create_Time_Stamp >= dateadd(minute, -5, getdate())or IsPaid=1)\r\n";
        public const string InsertPasengerDeatils = "INSERT INTO PassengerTable(UserId,TravelId,PassengerName,Age,Seatno,ReferenceId,Create_Time_Stamp) VALUES(@userid,@travelid,@passengername,@age,@seatno,@referenceid,@createtimestamp)";
        public const string InsertPaymentDeatils = "INSERT INTO TicketTable(UserId,TravelId,BusId,TotalSeatsReserved,TotalAmount,HolderName,CardNumber,CardType,ReferenceId) VALUES(@userid,@travelid,@busid,@totalseatreserved,@totalamount,@holdername,@cardnumber,@cardtype,@referenceid)\r\n";
        public const string ViewPasengerDeatils = "SELECT PassengerId AS PassengerId, PassengerName AS PassengerName,age AS Age,Seatno AS Seatno,IsCanceled AS IsCanceled FROM PassengerTable WHERE UserId=@userid and ReferenceId=@referenceid and TicketId=@ticketid \r\n";
        public const string CancelTicket = "UPDATE PassengerTable SET IsCanceled=1 WHERE PassengerId=@passengerid ";
        public const string GetTicketlist = "SELECT PassengerId, TicketId, PassengerName, Age, Seatno\r\nFROM PassengerTable WHERE TravelId=@travelid and IsCanceled=0 IsPaid=1\r\nORDER BY Seatno;";
        public const string DeleteBusTravel = "UPDATE BusTravelScheduleTbl SET Is_Deleted=1 WHERE TravelId=@travelid";
        public const string GetReferenceId = "SELECT \r\n\t   ReferenceId AS ReferenceId,\r\n\t   BusId,\r\n\t   TravelId,\r\n       (SELECT Source FROM BusTravelScheduleTbl bt WHERE bt.BusId=PassengerTable.BusId and bt.TravelId=PassengerTable.TravelId) AS Source,\r\n\t   (SELECT Destination FROM BusTravelScheduleTbl bt WHERE bt.BusId=PassengerTable.BusId and bt.TravelId=PassengerTable.TravelId) AS Destination,\r\n\t   (SELECT DepatureDateTime FROM BusTravelScheduleTbl bt WHERE bt.BusId=PassengerTable.BusId and bt.TravelId=PassengerTable.TravelId) AS Date\r\nFROM PassengerTable WHERE UserId=@userid and TravelId=@travelid and BusId=@busid ";
        public const string UpdateRef = "UPDATE PassengerTable SET TicketId=(SELECT TicketId FROM TicketTable WHERE ReferenceId=@referenceid),IsPaid=1  WHERE ReferenceId=@referenceid";
        public const string GetFare = "SELECT Fare AS Fare,BusId AS BusId FROM BusTravelScheduleTbl WHERE TravelId=@travelid and Is_Deleted=0";
        public const string Downloadticket = "SELECT bt.Source AS Source,bt.Destination AS Destination,bt.DepatureDateTime AS Date,tt.ReferenceId AS ReferenceId\r\nFROM BusTravelScheduleTbl bt\r\nINNER JOIN TicketTable tt\r\nON bt.TravelId =tt.TravelId \r\nWHERE tt.UserId=@userid and bt.TravelId=@travelid";
        public const string Getpassenger = "SELECT pt.PassengerName as Name,pt.Age AS Age,pt.Seatno AS Seatno\r\nFROM PassengerTable pt\r\nWHERE pt.ReferenceId= @referenceid";
        public const string GetBusName = "SELECT Bus_Name AS BusName FROM BusMaster_Table WHERE Bus_Id=(SELECT Bus_Id FROM BusTravelScheduleTbl WHERE TravelId=@travelid)";
    }
}

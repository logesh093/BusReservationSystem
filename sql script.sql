USE [master]
GO
/****** Object:  Database [BusReservationdb]    Script Date: 15-12-2023 10:40:45 ******/
CREATE DATABASE [BusReservationdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BusReservationdb', FILENAME = N'C:\Users\STS821-LOGESH P\BusReservationdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BusReservationdb_log', FILENAME = N'C:\Users\STS821-LOGESH P\BusReservationdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BusReservationdb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BusReservationdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BusReservationdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BusReservationdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BusReservationdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BusReservationdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BusReservationdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BusReservationdb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BusReservationdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BusReservationdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BusReservationdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BusReservationdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BusReservationdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BusReservationdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BusReservationdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BusReservationdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BusReservationdb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BusReservationdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BusReservationdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BusReservationdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BusReservationdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BusReservationdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BusReservationdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BusReservationdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BusReservationdb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BusReservationdb] SET  MULTI_USER 
GO
ALTER DATABASE [BusReservationdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BusReservationdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BusReservationdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BusReservationdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BusReservationdb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BusReservationdb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BusReservationdb] SET QUERY_STORE = OFF
GO
USE [BusReservationdb]
GO
/****** Object:  Table [dbo].[BusMaster_Table]    Script Date: 15-12-2023 10:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusMaster_Table](
	[Bus_Id] [int] IDENTITY(1,1) NOT NULL,
	[Bus_Name] [varchar](50) NOT NULL,
	[Window_Seats] [int] NOT NULL,
	[NonWindow_Seats] [int] NOT NULL,
	[Is_Deleted] [bit] NOT NULL,
	[Update_Time_Stramp] [datetime] NOT NULL,
	[Create_Time_Stramp] [datetime] NOT NULL,
 CONSTRAINT [PK_BusMaster_Table] PRIMARY KEY CLUSTERED 
(
	[Bus_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusTravelScheduleTbl]    Script Date: 15-12-2023 10:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusTravelScheduleTbl](
	[TravelId] [int] IDENTITY(1,1) NOT NULL,
	[BusId] [int] NOT NULL,
	[Source] [varchar](50) NOT NULL,
	[Destination] [varchar](50) NOT NULL,
	[Fare] [int] NOT NULL,
	[DepatureDateTime] [datetime] NOT NULL,
	[Is_Deleted] [bit] NOT NULL,
	[Update_Time_Stamp] [datetime] NOT NULL,
	[Create_Time_Stamp] [datetime] NOT NULL,
 CONSTRAINT [PK_BusTravelScheduleTbl] PRIMARY KEY CLUSTERED 
(
	[TravelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PassengerTable]    Script Date: 15-12-2023 10:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PassengerTable](
	[PassengerId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TravelId] [int] NOT NULL,
	[Ispaid] [int] NOT NULL,
	[PassengerName] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Seatno] [int] NOT NULL,
	[TicketId] [int] NULL,
	[IsSelected] [bit] NOT NULL,
	[IsCanceled] [bit] NOT NULL,
	[ReferenceId] [int] NULL,
	[Create_Time_Stamp] [datetime] NOT NULL,
 CONSTRAINT [PK_PassengerTable] PRIMARY KEY CLUSTERED 
(
	[PassengerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketTable]    Script Date: 15-12-2023 10:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketTable](
	[Ticketid] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TravelId] [int] NOT NULL,
	[BusId] [int] NOT NULL,
	[TotalSeatsReserved] [int] NOT NULL,
	[TotalAmount] [int] NOT NULL,
	[HolderName] [varchar](50) NOT NULL,
	[CardNumber] [varchar](20) NOT NULL,
	[CardType] [varchar](50) NOT NULL,
	[ReferenceId] [int] NOT NULL,
	[IsPaid] [bit] NOT NULL,
 CONSTRAINT [PK_TicketTable] PRIMARY KEY CLUSTERED 
(
	[Ticketid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOrAdminSignup_Table]    Script Date: 15-12-2023 10:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOrAdminSignup_Table](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[DOB] [varchar](20) NOT NULL,
	[EmailId] [varchar](80) NOT NULL,
	[Address] [varchar](500) NOT NULL,
	[Phonenumber] [varchar](10) NOT NULL,
	[Hash_Password] [nvarchar](max) NOT NULL,
	[Salt_Password] [varbinary](max) NOT NULL,
	[Is_Admin] [bit] NOT NULL,
 CONSTRAINT [PK_UserOrAdminSignup_Table] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BusMaster_Table] ADD  CONSTRAINT [DF_BusMaster_Table_Is_Deleted]  DEFAULT ((0)) FOR [Is_Deleted]
GO
ALTER TABLE [dbo].[BusMaster_Table] ADD  CONSTRAINT [DF_BusMaster_Table_Update_Time_Stramp]  DEFAULT (getdate()) FOR [Update_Time_Stramp]
GO
ALTER TABLE [dbo].[BusMaster_Table] ADD  CONSTRAINT [DF_BusMaster_Table_Create_Time_Stramp]  DEFAULT (getdate()) FOR [Create_Time_Stramp]
GO
ALTER TABLE [dbo].[BusTravelScheduleTbl] ADD  CONSTRAINT [DF_BusTravelScheduleTbl_Is_Deleted]  DEFAULT ((0)) FOR [Is_Deleted]
GO
ALTER TABLE [dbo].[BusTravelScheduleTbl] ADD  CONSTRAINT [DF_BusTravelScheduleTbl_Update_Time_Stamp]  DEFAULT (getdate()) FOR [Update_Time_Stamp]
GO
ALTER TABLE [dbo].[BusTravelScheduleTbl] ADD  CONSTRAINT [DF_BusTravelScheduleTbl_Create_Time_Stamp]  DEFAULT (getdate()) FOR [Create_Time_Stamp]
GO
ALTER TABLE [dbo].[PassengerTable] ADD  CONSTRAINT [DF_PassengerTable_Ispaid_1]  DEFAULT ((0)) FOR [Ispaid]
GO
ALTER TABLE [dbo].[PassengerTable] ADD  CONSTRAINT [DF_PassengerTable_IsPaid]  DEFAULT ((1)) FOR [IsSelected]
GO
ALTER TABLE [dbo].[PassengerTable] ADD  CONSTRAINT [DF_PassengerTable_IsCanceled]  DEFAULT ((0)) FOR [IsCanceled]
GO
ALTER TABLE [dbo].[TicketTable] ADD  CONSTRAINT [DF_TicketTable_IsPaid]  DEFAULT ((1)) FOR [IsPaid]
GO
ALTER TABLE [dbo].[UserOrAdminSignup_Table] ADD  CONSTRAINT [DF_UserOrAdminSignup_Table_Is_Admin]  DEFAULT ((0)) FOR [Is_Admin]
GO
USE [master]
GO
ALTER DATABASE [BusReservationdb] SET  READ_WRITE 
GO

USE [EliteHospital]
GO
/****** Object:  Table [dbo].[AboutUS]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AboutUS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShortDescription] [nvarchar](max) NOT NULL,
	[OurVision] [nvarchar](max) NOT NULL,
	[OurMission] [nvarchar](max) NOT NULL,
	[LongDescription] [nvarchar](max) NULL,
	[Image] [image] NULL,
	[ImagePath] [varchar](150) NULL,
 CONSTRAINT [PK_AboutUS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BannerTitle] [nvarchar](max) NOT NULL,
	[BannerTitleArabic] [nvarchar](50) NULL,
	[BannerSubTitle] [nvarchar](max) NOT NULL,
	[BannerSubTitleArabic] [nvarchar](50) NULL,
	[BannerImage] [image] NULL,
	[BannerImagePath] [varchar](150) NULL,
	[BannerImageMobile] [image] NULL,
	[BannerImageMobilePath] [varchar](150) NULL,
	[DepartmentName] [varchar](100) NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Careers]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Careers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Vacancy] [varchar](50) NOT NULL,
	[PositionDescription] [varchar](100) NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Careers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[WorkingHours] [nvarchar](max) NOT NULL,
	[Location] [nvarchar](max) NULL,
	[AddressMob] [nvarchar](max) NULL,
	[WorkingHoursMob] [nvarchar](max) NULL,
	[AddressArabicMob] [nvarchar](max) NULL,
	[WorkingHoursArabicMob] [nvarchar](max) NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CovidBanner]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CovidBanner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Image] [image] NOT NULL,
	[ImagePath] [varchar](100) NULL,
 CONSTRAINT [PK_CovidBanner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DailyOffers]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyOffers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Image] [image] NULL,
	[ImagePath] [varchar](150) NULL,
	[ImageMob] [image] NULL,
	[ImageMobPath] [varchar](150) NULL,
 CONSTRAINT [PK_DailyOffers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentName] [varchar](100) NOT NULL,
	[DepartmentNameArabic] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[DescriptionArabic] [nvarchar](250) NULL,
	[LongDescription] [nvarchar](max) NULL,
	[DepartmentImage] [image] NULL,
	[DepartmentImagePath] [varchar](150) NULL,
	[DepartmentImageMob] [image] NULL,
	[DepartmentImageMobPath] [varchar](150) NULL,
	[Status] [char](1) NULL,
	[OrderNo] [int] NULL,
	[DepartmentIconImage] [image] NULL,
	[DepartmentIconImagePath] [varchar](150) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [varchar](50) NOT NULL,
	[DoctorName] [varchar](100) NOT NULL,
	[DoctorNameArabic] [nvarchar](100) NULL,
	[DoctorImage] [image] NULL,
	[DoctorImagePath] [varchar](100) NULL,
	[DoctorImageMob] [image] NULL,
	[DoctorImageMobPath] [varchar](100) NULL,
	[OrderNo] [int] NULL,
	[Status] [char](1) NULL,
	[Position] [varchar](100) NULL,
 CONSTRAINT [PK_Doctor_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[ShortDescription] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Image] [image] NOT NULL,
	[ImagePath] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventImages]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventImages](
	[Id] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Image] [image] NOT NULL,
	[ImagePath] [varchar](150) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeBanner]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeBanner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Image] [image] NOT NULL,
	[ImagePath] [varchar](100) NOT NULL,
	[ExploreUrl] [varchar](50) NOT NULL,
 CONSTRAINT [PK_HomePageBanner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Insurance]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insurance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Image] [image] NULL,
	[ImagePath] [varchar](150) NULL,
	[ImageMob] [image] NULL,
	[ImageMobPath] [varchar](150) NULL,
 CONSTRAINT [PK_Insurance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OnlineConsultation]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlineConsultation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[Image] [image] NULL,
	[Path] [varchar](100) NULL,
	[Url] [varchar](500) NULL,
 CONSTRAINT [PK_OnlineConsultation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientVerification]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientVerification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MobileNo] [varchar](13) NOT NULL,
	[VerificationCode] [int] NULL,
	[ExpiringOnUTC] [datetime] NULL,
	[RetryAttempts] [int] NOT NULL,
	[CreatedOnUTC] [datetime] NOT NULL,
	[LastVerifiedOnUTC] [datetime] NULL,
 CONSTRAINT [PK_PatientVerification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PatientVerification] UNIQUE NONCLUSTERED 
(
	[MobileNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpecialOffer]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialOffer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Image] [image] NULL,
	[ImagePath] [varchar](150) NULL,
	[ImageMob] [image] NULL,
	[ImageMobPath] [varchar](150) NULL,
 CONSTRAINT [PK_SpecialOffer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Status] [char](1) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoUrls]    Script Date: 20-07-2022 12:09:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoUrls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OnlineConsultationId] [int] NOT NULL,
	[VideoUrls] [varchar](100) NOT NULL,
	[Description] [varchar](250) NULL,
 CONSTRAINT [PK_VideoUrls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Banner]  WITH CHECK ADD  CONSTRAINT [FK_Banner_Department] FOREIGN KEY([DepartmentName])
REFERENCES [dbo].[Department] ([DepartmentName])
GO
ALTER TABLE [dbo].[Banner] CHECK CONSTRAINT [FK_Banner_Department]
GO
ALTER TABLE [dbo].[EventImages]  WITH CHECK ADD  CONSTRAINT [FK_EventImages_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[EventImages] CHECK CONSTRAINT [FK_EventImages_Event]
GO
ALTER TABLE [dbo].[VideoUrls]  WITH CHECK ADD  CONSTRAINT [FK_VideoUrls_OnlineConsultation] FOREIGN KEY([OnlineConsultationId])
REFERENCES [dbo].[OnlineConsultation] ([Id])
GO
ALTER TABLE [dbo].[VideoUrls] CHECK CONSTRAINT [FK_VideoUrls_OnlineConsultation]
GO

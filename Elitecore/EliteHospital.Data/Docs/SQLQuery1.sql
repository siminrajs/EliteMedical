
USE [EliteHospital]
GO
/**** Object:  Table [dbo].[PatientVerification]    Script Date: 06/13/2021 22:07:41 ****/
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
ALTER AUTHORIZATION ON [dbo].[PatientVerification] TO  SCHEMA OWNER 
GO
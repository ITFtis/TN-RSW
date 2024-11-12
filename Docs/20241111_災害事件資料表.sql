CREATE TABLE [dbo].[Disaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[SDate] [date] NULL,
	[EDate] [date] NULL,
	[Note] [nvarchar](2000) NULL,
 CONSTRAINT [PK_Disaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


SET IDENTITY_INSERT [dbo].[Disaster] ON 

INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (1, N'�����䭷', CAST(N'2023-05-29' AS Date), CAST(N'2023-05-31' AS Date), N'����')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (2, N'��Ĭͺ�䭷', CAST(N'2023-07-24' AS Date), CAST(N'2023-07-28' AS Date), N'����')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (3, N'�d�V�䭷', CAST(N'2023-08-01' AS Date), CAST(N'2023-08-04' AS Date), N'����')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (4, N'Ĭ�Ի䭷', CAST(N'2023-08-28' AS Date), CAST(N'2023-08-31' AS Date), N'�j�P')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (5, N'�����䭷', CAST(N'2023-09-01' AS Date), CAST(N'2023-09-05' AS Date), N'����')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (6, N'�p���䭷', CAST(N'2023-10-02' AS Date), CAST(N'2023-10-06' AS Date), N'����')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (7, N'�̻ͦ䭷', CAST(N'2024-07-22' AS Date), CAST(N'2024-07-26' AS Date), N'�j�P')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (8, N'�s����䭷', CAST(N'2024-09-29' AS Date), CAST(N'2024-10-04' AS Date), N'�j�P')
INSERT [dbo].[Disaster] ([Id], [Name], [SDate], [EDate], [Note]) VALUES (9, N'�dͺ�䭷', CAST(N'2024-10-29' AS Date), CAST(N'2024-11-01' AS Date), N'�j�P')
SET IDENTITY_INSERT [dbo].[Disaster] OFF
GO

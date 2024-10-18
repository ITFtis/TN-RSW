/****** Object:  Table [dbo].[CWA_RainStPredict]    Script Date: 2024/10/18 ¤U¤È 05:06:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CWA_RainStPredict](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[rain_st_id] [nvarchar](50) NOT NULL,
	[rain_st_name] [nvarchar](50) NOT NULL,
	[predict_datetime] [datetime] NOT NULL,
	[h0] [decimal](10, 3) NOT NULL,
	[h1] [decimal](10, 3) NOT NULL,
	[h2] [decimal](10, 3) NOT NULL,
	[h3] [decimal](10, 3) NOT NULL,
	[h4] [decimal](10, 3) NOT NULL,
	[h5] [decimal](10, 3) NOT NULL,
	[h6] [decimal](10, 3) NOT NULL,
	[h7] [decimal](10, 3) NOT NULL,
	[h8] [decimal](10, 3) NOT NULL,
	[h9] [decimal](10, 3) NOT NULL,
	[h10] [decimal](10, 3) NOT NULL,
	[h11] [decimal](10, 3) NOT NULL,
	[h12] [decimal](10, 3) NOT NULL,
	[acc0] [decimal](10, 3) NOT NULL,
	[acc1] [decimal](10, 3) NOT NULL,
	[acc2] [decimal](10, 3) NOT NULL,
	[acc3] [decimal](10, 3) NOT NULL,
	[acc4] [decimal](10, 3) NOT NULL,
	[acc5] [decimal](10, 3) NOT NULL,
	[acc6] [decimal](10, 3) NOT NULL,
	[acc7] [decimal](10, 3) NOT NULL,
	[acc8] [decimal](10, 3) NOT NULL,
	[acc9] [decimal](10, 3) NOT NULL,
	[acc10] [decimal](10, 3) NOT NULL,
	[acc11] [decimal](10, 3) NOT NULL,
	[acc12] [decimal](10, 3) NOT NULL,
 CONSTRAINT [PK_CWA_RainStPredict] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [IX_CWA_RainStPredict_ST]    Script Date: 2024/10/18 ¤U¤È 05:07:14 ******/
CREATE NONCLUSTERED INDEX [IX_CWA_RainStPredict_ST] ON [dbo].[CWA_RainStPredict]
(
	[rain_st_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


/****** Object:  Index [IX_CWA_RainStPredict_TIME]    Script Date: 2024/10/18 ¤U¤È 05:07:41 ******/
CREATE NONCLUSTERED INDEX [IX_CWA_RainStPredict_TIME] ON [dbo].[CWA_RainStPredict]
(
	[predict_datetime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO



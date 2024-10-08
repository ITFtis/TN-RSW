
/****** Object:  Table [dbo].[WaterLevelPredictionAlarm]    Script Date: 2024/10/8 ¤U¤È 10:32:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [WaterLevelPredictionAlarm](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[AlarmDateTime] [datetime] NOT NULL,
	[dev_id] [nvarchar](255) NOT NULL,
	[stt_no] [nvarchar](50) NULL,
	[stt_name] [nvarchar](100) NULL,
	[rain_st] [nvarchar](50) NOT NULL,
	[rain_st_name] [nvarchar](100) NULL,
	[PredictDateTime] [datetime] NOT NULL,
	[input_x] [numeric](10, 3) NOT NULL,
	[predict_mx] [numeric](10, 8) NOT NULL,
	[predict_dy] [numeric](10, 8) NOT NULL,
	[calc_y] [float] NOT NULL,
	[alarm_type] [nvarchar](50) NOT NULL,
	[alarm_threshold] [float] NOT NULL,
	[alarm_message] [text] NOT NULL,
	[line_messaging_result] [text] NULL,
	[lon] [numeric](10, 6) NULL,
	[lat] [numeric](10, 6) NULL,
	[county_code] [nvarchar](50) NULL,
	[county_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_RainPredictAlarm] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



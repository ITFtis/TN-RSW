USE [TN_RSW]
GO
/****** Object:  Table [dbo].[WaterLevelPrediction]    Script Date: 2024/10/1 上午 09:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterLevelPrediction](
	[dev_id] [nvarchar](50) NOT NULL,
	[stt_no] [nvarchar](50) NOT NULL,
	[stt_name] [nvarchar](100) NULL,
	[county_code] [nvarchar](50) NOT NULL,
	[rain_st] [nvarchar](50) NULL,
	[rain_st_name] [nvarchar](100) NULL,
	[predict_mx] [numeric](10, 8) NULL,
	[predict_dy] [numeric](10, 8) NULL,
	[r2] [numeric](10, 8) NULL,
 CONSTRAINT [PK_WaterLevelPrediction] PRIMARY KEY CLUSTERED 
(
	[dev_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000001', N'67000-000001', N'安南區-正28', N'67000350', N'SW000001', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000002', N'67000-000002', N'安南區-正37', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000003', N'67000-000003', N'仁德區-正03', N'67000270', N'SW000024', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000004', N'67000-000004', N'仁德區-正09', N'67000270', N'SW000024', N'港尾溝溪水門', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000005', N'67000-000005', N'仁德區-正08', N'67000270', N'SW000001', N'港尾溝溪水門', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000006', N'67000-000006', N'仁德區-正10', N'67000270', N'SW000001', N'仁德滯洪北池', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000007', N'67000-000007', N'仁德區-正06', N'67000270', N'SW000001', N'東區公所', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000008', N'67000-000008', N'仁德區-正18', N'67000270', N'SW000024', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000009', N'67000-000009', N'仁德區-正04', N'67000270', N'SW000024', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000010', N'67000-000010', N'仁德區-正01', N'67000270', N'SW000001', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000011', N'67000-000011', N'仁德區-正05', N'67000270', N'SW000001', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000012', N'67000-000012', N'仁德區-正12', N'67000270', N'SW000001', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000013', N'67000-000013', N'仁德區-正07', N'67000270', N'SW000001', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000014', N'67000-000014', N'仁德區-正17', N'67000270', N'SW000024', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000015', N'67000-000015', N'永康區-正1', N'67000310', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000016', N'67000-000016', N'安南區-正22', N'67000350', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000017', N'67000-000017', N'安南區-正38', N'67000350', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000018', N'67000-000018', N'永康區-正16', N'67000310', N'SW000001', N'新市(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000019', N'67000-000019', N'安南區-正24', N'67000350', N'SW000001', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000020', N'67000-000020', N'安南區-正02', N'67000350', N'SW000001', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000021', N'67000-000021', N'安南區-正23', N'67000350', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000022', N'67000-000022', N'安南區-正29', N'67000350', N'SW000001', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000023', N'67000-000023', N'安南區-正01', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000024', N'67000-000024', N'安南區-正07', N'67000350', N'SW000001', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000025', N'67000-000025', N'安南區-正06', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000026', N'67000-000026', N'安南區-正03', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000027', N'67000-000027', N'安南區-正04', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000028', N'67000-000028', N'安南區-正10', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000029', N'67000-000029', N'安南區-正30', N'67000350', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000030', N'67000-000030', N'安南區-正16', N'67000350', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000031', N'67000-000031', N'安南區-正15', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000032', N'67000-000032', N'安南區-正21', N'67000350', N'SW000001', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000033', N'67000-000033', N'新營區-正9', N'67000010', N'SW000024', N'國一N288K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000034', N'67000-000034', N'安南區-正08', N'67000350', N'SW000001', N'曾文溪排水無名橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000035', N'67000-000035', N'安南區-正11', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000036', N'67000-000036', N'安南區-正18', N'67000350', N'SW000024', N'安南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000037', N'67000-000037', N'安南區-正33', N'67000350', N'SW000024', N'安清城南路口', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000038', N'67000-000038', N'安南區-正32', N'67000350', N'SW000024', N'安清城南路口', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000039', N'67000-000039', N'安南區-正34', N'67000350', N'SW000024', N'四草(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000040', N'67000-000040', N'安南區-正20', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000041', N'67000-000041', N'安南區-正26', N'67000350', N'SW000001', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000042', N'67000-000042', N'安南區-正27', N'67000350', N'SW000001', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000043', N'67000-000043', N'永康區-正22', N'67000310', N'SW000001', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000044', N'67000-000044', N'大內區-正02', N'67000110', N'SW000001', N'大內(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000045', N'67000-000045', N'永康區-正02', N'67000310', N'SW000001', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000046', N'67000-000046', N'永康區-正11', N'67000310', N'SW000001', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000047', N'67000-000047', N'安南區-正12', N'67000350', N'SW000024', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000048', N'67000-000048', N'永康區-正19', N'67000310', N'SW000001', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000049', N'67000-000049', N'永康區-正14', N'67000310', N'SW000001', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000050', N'67000-000050', N'永康區-正17', N'67000310', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000051', N'67000-000051', N'永康區-正09', N'67000310', N'SW000024', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000052', N'67000-000052', N'永康區-正05', N'67000310', N'SW000024', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000053', N'67000-000053', N'永康區-正12', N'67000310', N'SW000001', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000054', N'67000-000054', N'永康區-正15', N'67000310', N'SW000024', N'國一N323K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000055', N'67000-000055', N'北區-正01', N'67000340', N'SW000001', N'小東地下道前(W1770402)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000056', N'67000-000056', N'北區-正09', N'67000340', N'SW000001', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000057', N'67000-000057', N'北區-正08', N'67000340', N'SW000001', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000058', N'67000-000058', N'北區-正06', N'67000340', N'SW000024', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000059', N'67000-000059', N'北區-正05', N'67000340', N'SW000001', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000060', N'67000-000060', N'北區正-7', N'67000340', N'SW000001', N'小東地下道前(W1770402)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000061', N'67000-000061', N'北區-正04', N'67000340', N'SW000001', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000062', N'67000-000062', N'北區-正03', N'67000340', N'SW000024', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000063', N'67000-000063', N'北區-正10', N'67000340', N'SW000024', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000064', N'67000-000064', N'南區-正07', N'67000330', N'SW000024', N'臺南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000065', N'67000-000065', N'中西區-正05', N'67000370', N'SW000024', N'臺南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000066', N'67000-000066', N'中西區-正06', N'67000370', N'SW000001', N'臺南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000067', N'67000-000067', N'中西區-正04', N'67000370', N'SW000024', N'臺南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000068', N'67000-000068', N'永康區-正6', N'67000310', N'SW000024', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000069', N'67000-000069', N'中西區-正08', N'67000370', N'SW000001', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000070', N'67000-000070', N'中西區-正07', N'67000370', N'SW000024', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000071', N'67000-000071', N'中西區-正02', N'67000370', N'SW000024', N'臺南市北區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000072', N'67000-000072', N'安平區-正06', N'67000360', N'SW000024', N'安平(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000073', N'67000-000073', N'安平區-正03', N'67000360', N'SW000024', N'安平(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000074', N'67000-000074', N'安平區-正02', N'67000360', N'SW000024', N'安平(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000075', N'67000-000075', N'安平區-正01', N'67000360', N'SW000024', N'安平(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000076', N'67000-000076', N'南區-正04', N'67000330', N'SW000024', N'大甲橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000077', N'67000-000077', N'南區-正03', N'67000330', N'SW000024', N'臺南市南區(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000078', N'67000-000078', N'南區-正01', N'67000330', N'SW000024', N'大甲橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000079', N'67000-000079', N'南區-正09', N'67000330', N'SW000024', N'大甲橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000080', N'67000-000080', N'南區-正06', N'67000330', N'SW000024', N'大甲橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000081', N'67000-000081', N'南區-正05', N'67000330', N'SW000024', N'大甲橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000082', N'67000-000082', N'新市區-正01', N'67000200', N'SW000024', N'新市(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000083', N'67000-000083', N'新化區-正03', N'67000180', N'SW000024', N'穗芳橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000084', N'67000-000084', N'新化區-正01', N'67000180', N'SW000024', N'穗芳橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000085', N'67000-000085', N'新化區-正02', N'67000180', N'SW000024', N'穗芳橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000086', N'67000-000086', N'歸仁區-正01', N'67000280', N'SW000024', N'媽廟(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000087', N'67000-000087', N'安南區-正14', N'67000350', N'SW000024', N'安南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000088', N'67000-000088', N'安南區-正13', N'67000350', N'SW000024', N'安清城南路口', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000089', N'67000-000089', N'安南區-正19', N'67000350', N'SW000024', N'曾文溪排水無名橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000090', N'67000-000090', N'安南區-正36', N'67000350', N'SW000024', N'曾文溪排水無名橋', CAST(0.00920000 AS Numeric(10, 8)), CAST(0.61700000 AS Numeric(10, 8)), CAST(0.68300000 AS Numeric(10, 8)))
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000091', N'67000-000091', N'安南區-正09', N'67000350', N'SW000024', N'總頭橋', CAST(0.00970000 AS Numeric(10, 8)), CAST(1.90990000 AS Numeric(10, 8)), CAST(0.69680000 AS Numeric(10, 8)))
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000092', N'67000-000092', N'安南區-正35', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000093', N'67000-000093', N'六甲區-正01', N'67000090', N'SW000024', N'國三S329K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000094', N'67000-000094', N'六甲區-正02', N'67000090', N'SW000024', N'國三S329K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000095', N'67000-000095', N'官田區-正01', N'67000100', N'SW000024', N'官田(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000096', N'67000-000096', N'大內區-正01', N'67000110', N'SW000024', N'大內(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000097', N'67000-000097', N'善化區-正01', N'67000190', N'SW000024', N'善化(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000098', N'67000-000098', N'麻豆區-正02', N'67000070', N'SW000024', N'麻豆(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000099', N'67000-000099', N'仁德區-正13', N'67000270', N'SW000024', N'仁德(氣象)', CAST(0.01800000 AS Numeric(10, 8)), CAST(3.27430000 AS Numeric(10, 8)), CAST(0.72480000 AS Numeric(10, 8)))
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000100', N'67000-000100', N'麻豆區-正05', N'67000070', N'SW000001', N'麻豆(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000101', N'67000-000101', N'麻豆區-正03', N'67000070', N'SW000024', N'麻豆(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000102', N'67000-000102', N'麻豆區-正04', N'67000070', N'SW000024', N'麻豆(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000103', N'67000-000103', N'麻豆區-正01', N'67000070', N'SW000024', N'麻豆(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000104', N'67000-000104', N'柳營區-正01', N'67000040', N'SW000024', N'櫻花社區滯洪池', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000105', N'67000-000105', N'柳營區-正02', N'67000040', N'SW000024', N'櫻花社區滯洪池', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000106', N'67000-000106', N'柳營區-正03', N'67000040', N'SW000024', N'八老爺橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000107', N'67000-000107', N'下營區-正02', N'67000080', N'SW000024', N'下營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000108', N'67000-000108', N'下營區-正01', N'67000080', N'SW000024', N'下營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000109', N'67000-000109', N'西港區-正01', N'67000140', N'SW000024', N'西港(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000110', N'67000-000110', N'後壁區-正01', N'67000050', N'SW000024', N'後壁(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000111', N'67000-000111', N'新營區-正03', N'67000010', N'SW000024', N'新營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000112', N'67000-000112', N'新營區-正06', N'67000010', N'SW000024', N'新營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000113', N'67000-000113', N'永康區-正21', N'67000310', N'SW000024', N'國一N323K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000114', N'67000-000114', N'新營區-正08', N'67000010', N'SW000024', N'新營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000115', N'67000-000115', N'新營區-正05', N'67000010', N'SW000024', N'新營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000116', N'67000-000116', N'新營區-正02', N'67000010', N'SW000024', N'國一N288K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000117', N'67000-000117', N'新營區-正04', N'67000010', N'SW000024', N'新營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000118', N'67000-000118', N'新營區-正07', N'67000010', N'SW000024', N'新營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000119', N'67000-000119', N'新營區-正01', N'67000010', N'SW000001', N'新營(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000120', N'67000-000120', N'新營區-正10', N'67000010', N'SW000024', N'國一N288K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000121', N'67000-000121', N'鹽水區-正01', N'67000020', N'SW000024', N'舊營橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000122', N'67000-000122', N'鹽水區-正02', N'67000020', N'SW000024', N'舊營橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000123', N'67000-000123', N'鹽水區-正03', N'67000020', N'SW000001', N'舊營橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000124', N'67000-000124', N'學甲區-正06', N'67000130', N'SW000024', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000125', N'67000-000125', N'學甲區-正05', N'67000130', N'SW000024', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000126', N'67000-000126', N'學甲區-正07', N'67000130', N'SW000024', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000127', N'67000-000127', N'學甲區-正04', N'67000130', N'SW000024', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000128', N'67000-000128', N'楠西區-正01', N'67000240', N'SW000001', N'楠西(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000129', N'67000-000129', N'學甲區-正03', N'67000130', N'SW000024', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000130', N'67000-000130', N'學甲區-正08', N'67000130', N'SW000001', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000131', N'67000-000131', N'永康區-正18', N'67000310', N'SW000001', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000132', N'67000-000132', N'永康區-正10', N'67000310', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000133', N'67000-000133', N'安平區-正04', N'67000360', N'SW000024', N'安平(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000134', N'67000-000134', N'安平區-正05', N'67000360', N'SW000024', N'安平(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000135', N'67000-000135', N'楠西區-正02', N'67000240', N'SW000024', N'楠西(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000136', N'67000-000136', N'安平區-正07', N'67000360', N'SW000024', N'安平(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000137', N'67000-000137', N'北門區-正01', N'67000170', N'SW000024', N'鯤江橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000138', N'67000-000138', N'佳里區-正01', N'67000120', N'SW000001', N'北門橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000139', N'67000-000139', N'安南區-正05', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000140', N'67000-000140', N'北區-正02', N'67000340', N'SW000024', N'臺南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000141', N'67000-000141', N'東區-正03', N'67000320', N'SW000024', N'東區公所', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000142', N'67000-000142', N'東區-正02', N'67000320', N'SW000024', N'仁德(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000143', N'67000-000143', N'永康區-正3', N'67000310', N'SW000024', N'國一N323K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000144', N'67000-000144', N'永康區-正8', N'67000310', N'SW000024', N'永康(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000145', N'67000-000145', N'永康區-正20', N'67000310', N'SW000024', N'國一N323K(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000146', N'67000-000146', N'學甲區-正01', N'67000130', N'SW000001', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000147', N'67000-000147', N'安南區-正31', N'67000350', N'SW000024', N'安清城南路口', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000148', N'67000-000148', N'學甲區-正02', N'67000130', N'SW000024', N'學甲(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000149', N'67000-000149', N'南區-正08', N'67000330', N'SW000024', N'東區公所', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000150', N'67000-000150', N'安南區-正17', N'67000350', N'SW000024', N'總頭橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000151', N'67000-000151', N'北門區-正02', N'67000170', N'SW000024', N'鯤江橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000152', N'67000-000152', N'永康區-正07', N'67000310', N'SW000024', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000153', N'67000-000153', N'永康區-正04', N'67000310', N'SW000024', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000154', N'67000-000154', N'東區-正01', N'67000320', N'SW000024', N'東區公所', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000155', N'67000-000155', N'仁德區-正15', N'67000270', N'SW000024', N'文化站', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000156', N'67000-000156', N'仁德區-正14', N'67000270', N'SW000024', N'仁德(氣象)', CAST(0.01320000 AS Numeric(10, 8)), CAST(3.55830000 AS Numeric(10, 8)), CAST(0.68290000 AS Numeric(10, 8)))
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000157', N'67000-000157', N'中西區-正03', N'67000370', N'SW000024', N'臺南(氣象)', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000158', N'67000-000158', N'仁德區-正11', N'67000270', N'SW000024', N'仁德滯洪北池', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000159', N'67000-000159', N'南區-正10', N'67000330', N'SW000024', N'大甲橋', NULL, NULL, NULL)
GO
INSERT [dbo].[WaterLevelPrediction] ([dev_id], [stt_no], [stt_name], [county_code], [rain_st], [rain_st_name], [predict_mx], [predict_dy], [r2]) VALUES (N'SW000160', N'67000-000160', N'中西區-正01', N'67000370', N'SW000024', N'臺南(氣象)', NULL, NULL, NULL)
GO

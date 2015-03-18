/********************************************************************
 * Name:   ConfigConst
 * Creator:pxie 
 * Creation date:2013/5/28 10:45:46
 * clrversion : 4.0.30319.296
 * Description:   
 * 
 * Version History
 * Author		Description:				TaskID		Date
 * pxie			创建文件ConfigConst					2013/5/28 10:45:46
    ------------------------------------------------------------------
********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace DSInfo.Business
{
	/// <summary>
	/// ConfigConst
	/// </summary>
	public static class ConfigConst
	{
		public static readonly string SOAUSERID = ConfigurationManager.AppSettings["SOAUSERID"];

		public static readonly string AppID = ConfigurationManager.AppSettings["AppID"];

		public static readonly string LoggingServer_V2_IP = ConfigurationManager.AppSettings["LoggingServer.V2.IP"];

		public static readonly string LoggingServer_V2_Port = ConfigurationManager.AppSettings["LoggingServer.V2.Port"];


        public static readonly string CorpOnlineSiteDomain = ConfigurationManager.AppSettings["CorpOnlineSiteDomain"];

        public static readonly string CommentDomain = ConfigurationManager.AppSettings["CommentDomain"];

        public static readonly string HotelDomain = ConfigurationManager.AppSettings["HotelDomain"];

        public static readonly string CurrentPath = ConfigurationManager.AppSettings["CurrentPath"];

		private static readonly ConfigAppSetting appSetting = new ConfigAppSetting();

		public static ConfigAppSetting AppSettings
		{
			get
			{
				return appSetting;
			}
		}
	}
}

/********************************************************************
 * Name:   ConfigAppSetting
 * Creator:pxie 
 * Creation date:2013/5/28 11:17:08
 * clrversion : 4.0.30319.296
 * Description:   
 * 
 * Version History
 * Author		Description:				TaskID		Date
 * pxie			创建文件ConfigAppSetting					2013/5/28 11:17:08
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
	/// ConfigAppSetting
	/// </summary>
	public class ConfigAppSetting
	{
		/// <summary>
		/// ConfigAppSetting
		/// </summary>
		public ConfigAppSetting()
		{
		}

		private static IDictionary<string, string> dict = new Dictionary<string, string>();
		private static readonly object locker = new object();

		public static string GetAppSetting(string key)
		{
			if (!dict.ContainsKey(key))
			{
				lock (locker)
				{
					if (!dict.ContainsKey(key))
					{
						dict.Add(key, ConfigurationManager.AppSettings[key]);
					}
				}
			}
			return dict[key];
		}

		/// <summary>
		/// asdasd
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string this[string key]
		{
			get
			{
				return GetAppSetting(key);
			}
		}
	}
}

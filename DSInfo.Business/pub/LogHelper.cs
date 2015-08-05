// -----------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="Ctrip">
// 日志帮助类
// </copyright>
// -----------------------------------------------------------------------

namespace DSInfo.Business
{
    using System;
    using System.Collections.Generic;
    using Freeway.Logging;

    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 日志帮助类
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger("Corp.Car.WS");

        /// <summary>
        /// 记录Debug级别日志
        /// </summary>
        /// <param name="strTitle">日志标题</param>
        /// <param name="strMessage">日志内容</param>
        /// <param name="dicData">日志的AddInfo信息</param>
        public static void Debug(string strTitle, string strMessage, Dictionary<string, string> dicData = null)
        {
            CallAction(() => logger.Debug(strTitle, strMessage, dicData));
        }

        /// <summary>
        /// 记录Info级别日志
        /// </summary>
        /// <param name="strTitle">日志标题</param>
        /// <param name="strMessage">日志内容</param>
        /// <param name="dicData">日志的AddInfo信息</param>
        public static void Info(string strTitle, string strMessage, Dictionary<string, string> dicData = null)
        {
            CallAction(() => logger.Info(strTitle, strMessage, dicData));
        }

        /// <summary>
        /// 记录Warn级别日志
        /// </summary>
        /// <param name="strTitle">日志标题</param>
        /// <param name="strMessage">日志内容</param>
        /// <param name="dicData">日志的AddInfo信息</param>
        public static void Warn(string strTitle, string strMessage, Dictionary<string, string> dicData = null)
        {
            CallAction(() => logger.Warn(strTitle, strMessage, dicData));
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        /// <param name="strTitle">日志标题</param>
        /// <param name="ex">异常</param>
        /// <param name="dicData">日志的AddInfo信息</param>
        public static void Error(string strTitle, Exception ex, Dictionary<string, string> dicData = null)
        {
            CallAction(() => logger.Error(strTitle, ex, dicData));
        }

        /// <summary>
        /// 记录Error级别日志
        /// </summary>
        /// <param name="strTitle">日志标题</param>
        /// <param name="ex">异常</param>
        public static void Error(string strTitle, string ex)
        {
            CallAction(() => logger.Error(strTitle, ex));
        }

        /// <summary>
        /// Call back action
        /// </summary>
        /// <param name="actMethod">action method</param>
        private static void CallAction(Action actMethod)
        {
            try
            {
                actMethod();
            }
            catch (Exception ex)
            {
                logger.Error("日志错误", ex.ToString());
            }
        }
    }
}

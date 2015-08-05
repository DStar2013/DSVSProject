// -----------------------------------------------------------------------
// <copyright file="ServiceProxy.cs" company="CtripBiz">
// 服务代理类
// </copyright>
// -----------------------------------------------------------------------

namespace DSInfo.Business
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Ctrip.SOA.Comm;
    using Ctrip.SOA.Comm.Utility;

    /// <summary>
    /// 服务代理类
    /// </summary>
    public class ServiceProxy
    {
        /// <summary>
        /// 请求的用户IP
        /// </summary>
        private static readonly string ClientIp = Arch.Framework.Utility.IPHelper.GetClientIP();

        /// <summary>
        /// ESB总线地址
        /// </summary>
        private static readonly string EsbUrl = AppSetting.ESBUrl;

        /// <summary>
        /// 请求SOA
        /// </summary>
        /// <typeparam name="T">请求类型</typeparam>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="requestType">请求名</param>
        /// <param name="requestBody">请求体</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="referenceId">Reference Id</param>
        /// <returns>请求结果</returns>
        public static object Request<T, R>(
            string requestType, object requestBody, out string errorMsg, string referenceId = null) where T : new()
        {
            return Request<T, R>(requestType, requestBody, out errorMsg, false, referenceId);
        }

        /// <summary>
        /// 请求SOA
        /// </summary>
        /// <param name="requestType">RequestType</param>
        /// <param name="requestXml">请求的完整XML</param>
        /// <param name="isAnync">是否异步调用</param>
        /// <returns></returns>
        public static string Request(string requestType, string requestXml, bool isAnync)
        {
            string responseXml = null;
            if (!isAnync)
            {
                responseXml = WSAgent.Request(requestXml);
            }
            else
            {
                WSAgent.RequestAsync(requestXml, EsbUrl);
            }
           
            return responseXml;
        }

        /// <summary>
        /// 请求SOA
        /// </summary>
        /// <typeparam name="T">请求类型</typeparam>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="requestType">请求名</param>
        /// <param name="requestBody">请求体</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="isAsync">是否异步请求</param>
        /// <param name="referenceId">Reference Id</param>
        /// <returns>请求放回结果</returns>
        public static object Request<T, R>(string requestType, object requestBody, out string errorMsg, bool isAsync, string referenceId = null) where T : new()
        {
            errorMsg = string.Empty;
            object returnObj = null;

            var requestXml = GetRequestXml<T>(requestType, requestBody, referenceId);
            var responseXml = string.Empty;

            try
            {
                responseXml = Request(requestType, requestXml, isAsync);
                if (!string.IsNullOrWhiteSpace(responseXml))
                {
                    var response = (R)XMLSerializer.DeSerialize(responseXml, typeof(R));

                    // 有特殊字符时会反序列化失败，去掉特殊字符，再反序列化一次
                    if (response == null)
                    {
                        responseXml = ClearHtmlCode(responseXml);
                        response = (R)XMLSerializer.DeSerialize(responseXml, typeof(R));
                    }

                    var responseHead = (ResponseHead)response.GetType().GetField("Header").GetValue(response);

                    // 有些Response用的是字段，有的是属性
                    returnObj = response.GetType().GetField("ResponseBody") != null
                                    ? response.GetType().GetField("ResponseBody").GetValue(response)
                                    : response.GetType().GetProperty("ResponseBody").GetValue(response, null);

                    // 接口返回的非Success，将ErrorEntity抛出去
                    if (responseHead.ResultCode != ResultCode.Success)
                    {
                        // 将具体错误信息抛出去
                        errorMsg = responseHead.ResultMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SOA1.0服务调用异常", ex);
            }
            finally
            {
                LogHelper.Info(
                    "SOA1.0服务调用记录",
                    string.Format("Request:{0}{1}{0}Response:{0}{2}", Environment.NewLine, requestXml, responseXml),
                    new Dictionary<string, string> { { "Request", "Request" }, { "Response", "Response" } });
            }

            return returnObj;
        }

        /// <summary>
        /// 获取RequestHead
        /// </summary>
        /// <param name="requestType">请求类型</param>
        /// <param name="referenceId">应用ID</param>
        /// <returns>返回一个根据传入的RequestType生成的RequestHead</returns>
        public static RequestHead GetRequestHead(string requestType, string referenceId = null)
        {
            var strReferenceId = string.IsNullOrWhiteSpace(referenceId) ? WSAgent.GetReferenceID() : referenceId;
            return new RequestHead
            {
                Culture = "CN",
                UserID = AppSetting.UserID,
                ClientIP = ClientIp,
                ReferenceID = strReferenceId,
                RequestType = requestType
            };
        }

        /// <summary>
        /// 获取请求的RequestXml
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="requestType">请求类型</param>
        /// <param name="requestBody">请求body</param>
        /// <param name="referenceId">应用ID</param>
        /// <returns>序列化后可直接向WS发送的XML</returns>
        public static string GetRequestXml<T>(string requestType, object requestBody, string referenceId = null)
            where T : new()
        {
            var request = new T();
            request.GetType().GetField("Header").SetValue(request, GetRequestHead(requestType, referenceId));

            if (request.GetType().GetField("RequestBody") != null)
            {
                request.GetType().GetField("RequestBody").SetValue(request, requestBody);
            }
            else
            {
                request.GetType().GetProperty("RequestBody").SetValue(request, requestBody, null);
            }

            return XMLSerializer.Serialize(request, typeof(T));
        }

        /// <summary>
        /// 清除传入字符串中的UTF8转义标记（&#开头 ;号结尾，如&#2342;)
        /// </summary>
        /// <param name="strHtml">需要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string ClearHtmlCode(string strHtml)
        {
            const string StrHtmlTags = @"(&#){1}.+;{1}";
            return Regex.Replace(strHtml, StrHtmlTags, string.Empty);
        }
    }
}

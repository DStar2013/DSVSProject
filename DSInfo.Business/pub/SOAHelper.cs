using System;
using System.Collections.Generic;
using Ctrip.SOA.Comm;

namespace DSInfo.Business
{
    class SOAHelper
    {
        public static TResponse Get<TRequestWrapper, TResponseWrapper, TResponse>(string requestType, object request, int wsTimeOut = 10)
            where TRequestWrapper : RequestBase, new()
            where TResponseWrapper : ResponseBase, new()
            where TResponse : class
        {
            if (wsTimeOut < 1)
                wsTimeOut = 10;

            TRequestWrapper requestWrapper = new TRequestWrapper();
            requestWrapper.Header.Version = "1.0";
            requestWrapper.Header.UserID = ConfigConst.AppID;
            //requestWrapper.Header.ReferenceID = WSAgent.GetReferenceID();
            requestWrapper.Header.RequestType = requestType;

            var p = requestWrapper.GetType().GetField("RequestBody");
            if (p == null) throw new NotSupportedException("No RequestBody");
            p.SetValue(requestWrapper, request);

            string requestXML = null;
            try
            {
                //序列化请求参数
                requestXML = XMLSerializer.Serialize(requestWrapper, requestWrapper.GetType());
                //DebugSign.PrivatePush(1, string.Concat(requestType, " - Request"), requestXML);
                var responseXML = WSAgent.Request(requestXML, wsTimeOut);
                //DebugSign.PrivatePush(2, string.Concat(requestType, " - Response"), responseXML);

                //反序列化请求的结果
                var response = XMLSerializer.DeSerialize(responseXML, typeof(TResponseWrapper)) as TResponseWrapper;

                if (response != null && response.Header != null && response.Header.ResultCode == Ctrip.SOA.Comm.ResultCode.Success)
                {
                    return response.GetType().GetField("ResponseBody").GetValue(response) as TResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                //logger.Error(requestType, ex, new Dictionary<string, string> { { "request", requestXML } });
                return null;
            }
        }

        //private static Freeway.Logging.ICentralLoggingProxy logger = Freeway.Logging.CentralLoggingProxy.GetLogger("SOA");
    }
}

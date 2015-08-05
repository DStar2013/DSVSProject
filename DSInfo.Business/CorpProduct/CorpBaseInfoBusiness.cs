using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DSInfo.Business
{
    using CorpBaseInfo = Ctrip.Corp.CorpInfo.WS.Entity;

    public class CorpBaseInfoBusiness
    {
        public static CorpBaseInfo.GetCorpUserInfoResponse GetCorpUserInfoByUID(string uid)
        {
            string outErrMsg;
            var request = new CorpBaseInfo.GetCorpUserInfoRequest { UID = uid };

            return 
                (CorpBaseInfo.GetCorpUserInfoResponse)
                ServiceProxy.Request<CorpBaseInfo.Request, CorpBaseInfo.Response>(
                CorpBaseInfo.RequestType.GetCorpUserInfoRequest.Name,
                request,
                out outErrMsg);

        }


    }
}

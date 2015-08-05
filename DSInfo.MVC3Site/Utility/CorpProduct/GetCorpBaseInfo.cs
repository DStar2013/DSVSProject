using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSInfo.Business;

namespace DSInfo.MVC3Site.Utility
{
    public class GetCorpBaseInfo
    {

        public static void GetCorpInfo()
        {
            var test = CorpBaseInfoBusiness.GetCorpUserInfoByUID("hoteltest");


        }

    }
}

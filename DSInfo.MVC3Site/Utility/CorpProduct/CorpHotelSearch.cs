using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DSInfo.Business.CorpProduct;

namespace DSInfo.MVC3Site.Utility
{
    public class CorpHotelSearch
    {

        public static void getRecHotelList()
        {
            HotelSearchBusiness.GetRecHotel();
        }

    }
}
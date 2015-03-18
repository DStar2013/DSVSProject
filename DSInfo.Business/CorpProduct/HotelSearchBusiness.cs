using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corp.Product.CorpHotel.WS.Entity;

namespace DSInfo.Business.CorpProduct
{
    public class HotelSearchBusiness
    {

        //global::Corp.Product.CorpHotel.WS.Entity.AgreementPriceResponse

        public static RecommendHotelResponse GetRecHotel()
        {
            return SOAHelper.Get<global::Corp.Product.CorpHotel.WS.Entity.Request, global::Corp.Product.CorpHotel.WS.Entity.Response, RecommendHotelResponse>(
            global::Corp.Product.CorpHotel.WS.Entity.RequestType.GetRecommendHotel.Name, new RecommendHotelRequest
            {
                CorpID = "xiaomi",
                CityID = 2,
                RoomReturnCashType_Pay = "C",
                Culture = 0
            });

        }

    }
}

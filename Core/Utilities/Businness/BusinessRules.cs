using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Businness
{
    public class BusinessRules
    {
        //params istediğim kadar oarametre geçerim
        public static IResult Run(params IResult[] logics) //virgülle ayırdığımız parametreleri IResult dizisine atar
        {
            foreach (var logic in logics)
            {
                //parametre ile gönderdiğim iş kurallarını business'a gönder onu haberdar et
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}

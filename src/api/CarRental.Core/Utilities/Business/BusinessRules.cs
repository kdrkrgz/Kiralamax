using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Core.Utilities.Business
{
    public class BusinessRules
    {
        // parametreyle gönderilen iş kurallarından başarısız olanı business tarafına gönderiyoruz ve orada yakalıyoruz.
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.IsSuccess)
                {
                    return logic;
                }   

            }

            return null;
        }
    }
}

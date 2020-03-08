using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double _price;
        private int _nightsCount;
        private double _discount;
        private double _total;
        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0) throw new ArgumentException();
                _price = value;
                Notify(nameof(Price));
                _total = Price * NightsCount * (1 - Discount / 100);
                Notify(nameof(Total));
            }
        }
        public int NightsCount
        {
            get { return _nightsCount; }
            set
            {
                if (value <= 0) throw new ArgumentException();
                _nightsCount = value;
                Notify(nameof(NightsCount));
                _total = Price * NightsCount * (1 - Discount / 100);
                Notify(nameof(Total));
            }
        }
        public double Discount
        {
            get { return _discount; }
            set
            {
                if (value < -100 || value > 100) throw new ArgumentException();
                _discount = value;
                Notify(nameof(Discount));
                _total = Price * NightsCount * (1 - Discount / 100);
                Notify(nameof(Total));
            }
        }
        public double Total
        {
            get { return _total; }
            set
            {
                if (value < 0) throw new ArgumentException();
                _total = value;
                Notify(nameof(Total));
                _discount = Math.Abs((Total / (Price * NightsCount) - 1) * 100);
                Notify(nameof(Discount));
            }
        }
    }
}
using System.Collections.Generic;

namespace FeeForPub
{
    public interface ICheckInFee
    {
        decimal GetFee(Customer customer);
    }

    public class Customer
    {
        public bool IsMale { get; set; }

        public int Seq { get; set; }
    }

    public class Pub
    {
        private readonly ICheckInFee _checkInFee;
        private decimal _inCome = 0;

        public Pub(ICheckInFee checkInFee)
        {
            this._checkInFee = checkInFee;
        }

        /// <summary>
        /// 入場
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>收費的人數</returns>
        public int CheckIn(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var inCome = this._checkInFee.GetFee(customer);
                if (inCome <= 0) continue;
                _inCome += inCome;
                result++;

                /*
                var isFemale = !customer.IsMale;

                //女生免費入場
                if (isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    this._inCome += this._checkInFee.GetFee(customer);

                    result++;
                }
                */
            }

            //for stub, validate return value
            return result;
        }

        public decimal GetInCome()
        {
            return this._inCome;
        }
    }
}
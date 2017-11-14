/*
 * HL.YahooCurrency is a .NET API that uses YQL for retreiving data from Yahoo APIs!
 * It works by sending a request to Yahoo Services using YQL.
 * The service returns a XML response that is parsed by this API.
 * This API runs on .NET CORE 2.0.
 * 
 * You can modify this code as you want, but please, keep this header as it is!
 * All the rights for the Yahoo API's belongs to Yahoo Holdings, Inc.
 * 
 * This API has been developed by Herbert Lausmann!
 * 
 * Herbert Lausmann
 * November 2017
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HL.YahooCurrency
{
    /// <summary>
    /// Describes a currency and its price (a constant that can be used to convert between different currencies)
    /// </summary>
    public sealed class CurrencyUnit
    {
        #region Constructors
        /// <summary>
        /// For internal use only!
        /// Creates a CurrencyUnit from a XElement that came from the Currency XML table sent by yahoo
        /// </summary>
        /// <param name="Source">A valid XElement</param>
        internal CurrencyUnit(XElement Source)
        {
            _Name = Source.Elements().First(x => x.Attribute(XName.Get("name")).Value == "name").Value;
            if (_Name.Contains("/"))
            {
                _Name = _Name.Split('/')[1];
            }
            _Price = double.Parse(Source.Elements().First(x => x.Attribute(XName.Get("name")).Value == "price").Value);
            _Date = DateTime.Parse(Source.Elements().First(x => x.Attribute(XName.Get("name")).Value == "utctime").Value).ToLocalTime();
        }
        #endregion

        #region Fields
        private string _Name;
        private double _Price;
        private DateTime _Date;
        #endregion

        #region Properties
        /// <summary>
        /// Returns the name of this Currency. This name is, typically, a 3 character string like USD (United States Dollar)
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }
        /// <summary>
        /// Returns a constant that can be use to convert between this currency and other
        /// </summary>
        public double Price
        {
            get { return _Price; }
        }

        /// <summary>
        /// Returns the date when this object was lastest updated by Yahoo
        /// </summary>
        public DateTime Date
        {
            get { return _Date; }
        }
        #endregion

        #region Procedures
        /// <summary>
        /// Returns a string representation of this currency
        /// </summary>
        /// <returns>Returns a string like: United States - Dollar - USD</returns>
        public override string ToString()
        {
            try
            {
                CurrencyInfo info = CurrencyInfo.FromCurrencyUnit(this);
                string output = info.Country + " - " + info.Currency + " - " + info.Code;
                return output;
            }
            catch
            {
                return "UNKNOW";
            }
        }
        #endregion

        #region Static

        #endregion
    }
}

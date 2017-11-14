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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace HL.YahooCurrency
{
    /// <summary>
    /// Describes some useful informations about a CurrencyUnit like: Symbol, Country, Code and etc
    /// </summary>
    public sealed class CurrencyInfo
    {
        #region Constructors
        /// <summary>
        /// For internal use only
        /// </summary>
        /// <param name="Source">The source XElement from the InfoDataBase.xml internal resource file</param>
        private CurrencyInfo(XElement Source)
        {
            _Flag = Convert.FromBase64String(Source.Element(XName.Get("flag")).Value);
            _Country = Source.Element(XName.Get("country")).Value;
            _Currency = Source.Element(XName.Get("currency")).Value;
            _Code = Source.Element(XName.Get("code")).Value;

            byte[] symb = Convert.FromBase64String(Source.Element(XName.Get("symbol")).Value);
            _Symbol = System.Text.Encoding.Unicode.GetString(symb, 0, symb.Length);
        }
        #endregion

        #region Fields
        private byte[] _Flag;
        private string _Country;
        private string _Currency;
        private string _Code;
        private string _Symbol;
        #endregion

        #region Properties
        /// <summary>
        /// Returns a byte array that represents a flag picture;
        /// </summary>
        public byte[] Flag
        {
            get { return _Flag; }
        }
        /// <summary>
        /// Returns the name of the country that uses this currency
        /// </summary>
        public string Country
        {
            get { return _Country; }
        }
        /// <summary>
        /// Returns the name of the currency for each this object fits
        /// </summary>
        public string Currency
        {
            get { return _Currency; }
        }
        /// <summary>
        /// Returns the currency code
        /// </summary>
        public string Code
        {
            get { return _Code; }
        }
        /// <summary>
        /// Returns the currency symbol
        /// </summary>
        public string Symbol
        {
            get { return _Symbol; }
        }

        #endregion

        #region Procedures
        public Stream GetFlagStream()
        {
            return new MemoryStream(_Flag);
        }
        #endregion

        #region Static
        private static ICollection<CurrencyInfo> _Infos;

        /// <summary>
        /// Returns a collection with all CurrencyInfo objects avaiable in the database (more than 100!)
        /// </summary>
        public static IEnumerable<CurrencyInfo> Infos
        {
            get
            {
                if (_Infos == null)
                {
                    _Infos = new System.Collections.ObjectModel.Collection<CurrencyInfo>();
                    Stream databaseStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HL.YahooCurrency.InfoDataBase.xml");
                    StreamReader r = new StreamReader(databaseStream, Encoding.Unicode);
                    XDocument database = XDocument.Load(r);
                    foreach (XElement culture in database.Root.Elements(XName.Get("culture")))
                    {
                        _Infos.Add(new CurrencyInfo(culture));
                    }
                }
                return _Infos;
            }
        }

        /// <summary>
        /// Creates a CurrencyInfo from a given currency code
        /// </summary>
        /// <param name="code">The currency code</param>
        /// <returns>Returns a new CurrencyInfo object from its code</returns>
        public static CurrencyInfo FromCode(string code)
        {
            code = code.ToUpper();
            foreach (CurrencyInfo info in Infos)
            {
                if (info.Code == code)
                    return info;
            }
            return null;
        }

        /// <summary>
        /// Creates a CurrencyInfo object that provides information about the given CurrencyUnit
        /// </summary>
        /// <param name="unit">CurrencyUnit</param>
        /// <returns>Returns a new CurrencyInfo object</returns>
        public static CurrencyInfo FromCurrencyUnit(CurrencyUnit unit)
        {
            return FromCode(unit.Name);
        }
        #endregion
    }
}

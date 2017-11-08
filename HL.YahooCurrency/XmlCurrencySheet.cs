/*
 * HL.YahooCurrency is a .NET Wrapper for the Yahoo Finances WebService!
 * It working by sending a request to Yahoo Services using YQL.
 * The web service returns a XML response that is parsed by this wrapper.
 * This API runs on .NET CORE 2.0.
 * 
 * You can modify this code as you want, but please, keep this header as it is!
 * All the rights for the Yahoo Finances Services belongs to Yahoo Holdings, Inc.
 * 
 * This Wrapper has been developed by Herbert Lausmann!
 * 
 * Herbert Lausmann
 * November 2017
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HL.YahooCurrency
{
    /// <summary>
    /// Represents the XML Data response sent by yahoo webservices containing all the needed currency data.
    /// It's the database for Currency object
    /// </summary>
    public sealed class XmlCurrencySheet
    {
        #region Constructors
        private XmlCurrencySheet()
        {
        }
        #endregion

        #region Fields
        private System.Collections.ObjectModel.Collection<CurrencyUnit> _CurrencyUnits;
        private XDocument _CurrencySheet;
        #endregion

        #region Properties
        /// <summary>
        /// Returns all the CurrencyUnits within this XDocument
        /// </summary>
        public IEnumerable<CurrencyUnit> CurrencyUnits
        {
            get { return _CurrencyUnits; }
        }
        /// <summary>
        /// Returns the Xml Document sent by the yahoo web service
        /// </summary>
        public XDocument CurrencySheet
        {
            get { return _CurrencySheet; }
        }
        /// <summary>
        /// Returns the date when the XML data was received from the yahoo web service
        /// </summary>
        public DateTime Date
        {
            get
            {
                XElement dateEle = _CurrencySheet.Root.Element(XName.Get("date"));
                DateTime dt = DateTime.Parse(dateEle.Value);
                return dt.ToLocalTime();
            }
        }
        #endregion

        #region Procedures
        /// <summary>
        /// Saves this Database to a Stream
        /// </summary>
        /// <param name="Output">The output stream</param>
        public void Save(Stream Output)
        {
            _CurrencySheet.Save(Output);
        }

        /// <summary>
        /// Resend async a request and wait and loads any updates available
        /// </summary>
        /// <returns>Returns a task that can be awaited</returns>
        public Task UpdateAsync()
        {
            return Task.Run(async () =>
            {
                WebRequest request = WebRequest.Create("http://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote");
                WebResponse response = request.EndGetResponse(request.BeginGetResponse(null, null));

                Stream responseStream = response.GetResponseStream();
                _CurrencySheet = XDocument.Load(responseStream);
                responseStream.Dispose();
                response.Dispose();
                _CurrencySheet.Root.Add(new XElement("date", DateTime.Now.ToUniversalTime().ToString()));

                await InitializeCollectionAsync();
            });
        }

        /// <summary>
        /// For internal use only
        /// Generate a collection of CurrencyUnit objects from the XML file received
        /// </summary>
        /// <returns>Returns a awaitable task</returns>
        private Task InitializeCollectionAsync()
        {
            return Task.Run(() =>
            {
                _CurrencyUnits = new System.Collections.ObjectModel.Collection<CurrencyUnit>();
                foreach (XElement resource in CurrencySheet.Root.Descendants().Where(x => x.Name.LocalName == "resource"))
                {
                    CurrencyUnit unit = new CurrencyUnit(resource);
                    if (unit.ToString() != "UNKNOW")
                        _CurrencyUnits.Add(new CurrencyUnit(resource));
                }
            });
        }
        #endregion

        #region Static
        /// <summary>
        /// Creates and load, async, an updated XmlCurrencySheet
        /// </summary>
        /// <returns>Returns a Task<XmlCurrencySheet> that can be awaited in async contexts</returns>
        public static Task<XmlCurrencySheet> LoadAsync()
        {
            return Task.Run(async () =>
            {
                XmlCurrencySheet sheet = new XmlCurrencySheet();
                await sheet.UpdateAsync();
                return sheet;
            });
        }
        /// <summary>
        ///  Creates and load, async, a XmlCurrencySheet from a stream
        /// </summary>
        /// <param name="source">The stream containg the XML data</param>
        /// <returns>Returns a Task<XmlCurrencySheet> that can be awaited in async contexts</returns>
        public static Task<XmlCurrencySheet> LoadAsync(Stream source)
        {
            return Task.Run(async () =>
            {
                XmlCurrencySheet sheet = new XmlCurrencySheet();
                sheet._CurrencySheet = XDocument.Load(source);
                await sheet.InitializeCollectionAsync();
                return sheet;
            });
        }
        #endregion
    }
}

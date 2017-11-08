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
using System.Threading.Tasks;

namespace HL.YahooCurrency
{
    /// <summary>
    /// The main class of this Api.
    /// You can choose an input value, then choose the Source Currency and the Output Currency, and you will get the value converted
    /// </summary>
    public sealed class Currency : INotifyPropertyChanged
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of this class. 
        /// This istance will be initialized asynchronous. 
        /// See WaitLoad() method for more info.
        /// </summary>
        public Currency()
        {
            initialization = UpdateAsync();
        }

        private Task initialization;

        /// <summary>
        /// You can await the initialization of this class after instancing it or updating it. Use await ou .Wait();
        /// </summary>
        /// <returns>Returns a task that can be awaited ou waited synchronous</returns>
        public Task WaitLoad()
        {
            return initialization;
        }

        #endregion

        #region Fields
        private CurrencyUnit _SourceUnit;
        private CurrencyUnit _OutputUnit;

        private float _SourceValue;

        private XmlCurrencySheet DataBase;
        private bool _IsLoading;
        #endregion

        #region Properties
        /// <summary>
        /// Returns a value indicating if is downloading the Currency data in the background.
        /// DO NOT access any other property or method while this property is setted to true. Use WaitLoad();
        /// </summary>
        public bool IsLoading { get => _IsLoading; }

        /// <summary>
        /// Gets or Sets a source currency for using in conversions
        /// </summary>
        public CurrencyUnit SourceUnit
        {
            get { return _SourceUnit; }
            set
            {
                _SourceUnit = value;
                OnPropertyChanged("SourceUnit");
                OnPropertyChanged("OutputValue");
                OnPropertyChanged("SourceUnitInfo");
            }
        }

        /// <summary>
        /// Gets or Sets an output currency for using in conversions
        /// </summary>
        public CurrencyUnit OutputUnit
        {
            get { return _OutputUnit; }
            set
            {
                _OutputUnit = value;
                OnPropertyChanged("OutputUnit");
                OnPropertyChanged("OutputValue");
                OnPropertyChanged("OutputUnitInfo");
            }
        }

        /// <summary>
        /// Gets informations related to the current SourceUnit property
        /// </summary>
        public CurrencyInfo SourceUnitInfo
        {
            get
            {
                if (_SourceUnit != null)
                    return CurrencyInfo.FromCurrencyUnit(_SourceUnit);
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets informations related to the current OutputUnit property
        /// </summary>
        public CurrencyInfo OutputUnitInfo
        {
            get
            {
                if (_OutputUnit != null)
                    return CurrencyInfo.FromCurrencyUnit(_OutputUnit);
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets or Sets a value that will be converted from the given SourceUnit to the OutputUnit
        /// </summary>
        public float SourceValue
        {
            get { return _SourceValue; }
            set
            {
                _SourceValue = value;
                OnPropertyChanged("SourceValue");
                OnPropertyChanged("OutputValue");
            }
        }

        /// <summary>
        /// Gets the converted value
        /// </summary>
        public double OutputValue
        {
            get
            {
                if (SourceValue == 0) return 0;
                if (SourceUnit == null) return 0;
                if (OutputUnit == null) return 0;

                double input = SourceValue / SourceUnit.Price;
                double output = input * OutputUnit.Price;
                return output;
            }
        }

        /// <summary>
        /// Gets all the updated conversion units downloaded from Yahoo
        /// </summary>
        public IEnumerable<CurrencyUnit> Units
        {
            get { return DataBase?.CurrencyUnits; }
        }

        /// <summary>
        /// Gets all the available units informations about the downloaded conversion units. It uses a internal database
        /// </summary>
        public IEnumerable<CurrencyInfo> UnitInfos
        {
            get { return CurrencyInfo.Infos; }
        }
        #endregion

        #region Procedures
        /// <summary>
        /// Updates, asynchronous, the current Currency object to the lastest currency conversion data available
        /// </summary>
        /// <returns>Returns a task that can be awaited. You can use WaitLoad().Wait() for synchronous contexts</returns>
        public Task UpdateAsync()
        {
            var t = Task.Run(async () =>
            {
                _IsLoading = true;
                OnPropertyChanged("IsLoading");

                DataBase = await XmlCurrencySheet.LoadAsync();
                OnPropertyChanged("OutputUnit");
                OnPropertyChanged("OutputValue");
                OnPropertyChanged("SourceUnit");
                OnPropertyChanged("OutputValue");
                OnPropertyChanged("Units");
                OnPropertyChanged("UnitInfos");
                OnPropertyChanged("OutputUnitInfo");
                OnPropertyChanged("SourceUnitInfo");

                _IsLoading = false;
                OnPropertyChanged("IsLoading");
            });
            initialization = t;
            return t;
        }
        #endregion
    }
}
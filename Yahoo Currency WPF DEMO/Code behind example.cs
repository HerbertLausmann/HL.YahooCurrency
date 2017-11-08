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

namespace Yahoo_Currency_WPF_DEMO
{
    public static class Code_behind_example
    {
        /// <summary>
        /// The code bellow is simple example of how tu use the await keyword in the initialization
        /// </summary>
        /// <returns></returns>
        public static async Task AsyncExample()
        {
            HL.YahooCurrency.Currency currency = new HL.YahooCurrency.Currency();

            //YOU MUST NOT FORGET THE LINE BELLOW
            //It's very important that you wait the initialization of this object
            await currency.WaitLoad();

            //Select the source conversion unit(currency)
            currency.SourceUnit = currency.Units.ElementAt(0);
            //Select the output conversion unit(currency)
            currency.OutputUnit = currency.Units.ElementAt(1);
            //Gives a value for conversion
            currency.SourceValue = 2.32f;
            //Writes out the value converted to the output unit(currency)
            System.Diagnostics.Debug.WriteLine(currency.OutputValue);
        }

        /// <summary>
        /// The code bellow is a simple example of how you should code (Synchronous)
        /// </summary>
        public static void SynchronousExample()
        {
            HL.YahooCurrency.Currency currency = new HL.YahooCurrency.Currency();

            //YOU MUST NOT FORGET THE LINE BELLOW
            //It's very important that you wait the initialization of this object
            //Instead of using await and waiting asynchronous, you can use .Wait() and
            //it will block the current thread till the background data gets all downloaded and parsed
            //this usually takes 1 second in a good internet connection but, in a modest environment, it can take lik 6 seconds
            currency.WaitLoad().Wait();

            //Select the source conversion unit(currency)
            currency.SourceUnit = currency.Units.ElementAt(0);
            //Select the output conversion unit(currency)
            currency.OutputUnit = currency.Units.ElementAt(1);
            //Gives a value for conversion
            currency.SourceValue = 2.32f;
            //Writes out the value converted to the output unit(currency)
            System.Diagnostics.Debug.WriteLine(currency.OutputValue);
        }
    }
}

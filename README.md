# HL.YahooCurrency
## .NET C# Multiplatform wrapper for Yahoo Currency Web Service. Convert between over 100 different currencies.

With this API you can convert between over 100 different currencies.
This is just a nice wrapper around the Yahoo Currency web service. But I've added some interesting things like:

1. Conversion between one currency and another, given some value to convert!
2. XAML and Data binding support. You can declare an YahooCurrency object and bind to it directly in XAML without any code behind!
3. Asynchronous support. This library uses async and await. You can get a complete experience without any UI freezing.
4. This API comes with a data base that gives you some interesting information about each currency, like:
   -The currency's country name.
   -The currency country's flag icon.
   -The currency's unicode symbol.
   -The currency's universal code.
5. Multi platform support. Because this is a .NET CORE (2) library, you can use for XAMARIN, MONO, Win Forms, WPF and all platforms and frameworks supporting .NET CORE/STANDARD

### A little demo in WPF:

![alt WPF DEMO](https://herbertdotlausmann.files.wordpress.com/2017/11/yahoo-currency.gif)

### Using the code
Bellow I show you some example of how using this API in the code behind (you use directly in XAML as well, see WPF DEMO)
```csharp
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
```

### Fala português?? Abaixo vai um link para a postagem no meu blog:
[.NET CORE: API PARA CONVERSÃO DE MOEDAS – YAHOO CURRENCY](https://herbertdotlausmann.wordpress.com/2017/11/08/net-core-api-para-conversao-de-moedas-yahoo-currency/)

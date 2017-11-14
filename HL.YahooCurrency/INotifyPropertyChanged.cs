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

namespace HL.YahooCurrency
{
    public abstract class INotifyPropertyChanged : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string PropertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}

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

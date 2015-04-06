using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace WPFCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Value;

        public App()
        {
            Value = "INITITIIT";/*
            var id = AppDomain.CurrentDomain.Id;
            Debugger.Launch();*/
            // new Thread(() => { Debugger.Launch(); }).Start();
        }
    }
}

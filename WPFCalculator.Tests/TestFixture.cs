using System;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using Wpfa;
using Wpfa.Manipulation;

namespace WPFCalculator.Tests
{
    [ProcessIsolation("WPFCalculator.exe")]
    [TestFixture]
    public class TestFixture : ProcessIsolationTestBase
    {
        [Test]
        public void Test1()
        {
            Window mainWindow = Application.Current.MainWindow;
            
            Button b1 = (Button)VisualUtilities.FindByAutomationId(mainWindow, "B1_automation_id");

            VisualUtilities.Click(b1);

            VisualUtilities.Wait(TimeSpan.FromSeconds(10));
       }
    }
}

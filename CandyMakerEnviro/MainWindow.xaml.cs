using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICS;


namespace CandyMakerEnviro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

internal class TemperatureChecker
{
    Maker maker = new Maker();
    IsolationCoolingSystem isolation = new IsolationCoolingSystem();
    TurbineController turbines = new TurbineController();

    /// <summary>
    /// If the nougat temperature exceeds 160C it's too hot
    /// </summary>
    public bool IsNougatTooHot()
    {
        int temp = maker.CheckNougatTemperature();
        if (temp > 160)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Perform the Candy Isolation Cooling System vent procedure
    /// </summary>
    public void DoCICSVentProcedure()
    {
        turbines.CloseTripValve(2);
        isolation.Fill();
        isolation.Vent();
        maker.CheckAirSystem();
    }
    /// <summary>
    /// This code runs every 3 minutes (10sec) to check the temperature.
    /// If it exceeds 160c we need to vent the cooling system.
    /// </summary>
    public void ThreeMinuteCheck()
    {
        if (IsNougatTooHot() == true)
            DoCICSVentProcedure();
        else
            Console.WriteLine("Nougat is still below 160...");
    }
}

internal class TurbineController
{
    // method CloseTripValve with multiple valves 1-4 to close
    public void CloseTripValve(int valve)
    {
        Console.WriteLine("Closing valve " + valve);
    }
}

internal class IsolationCoolingSystem
{
    // two methods fill and vent that don't return anything
    public void Fill()
    {
        Console.WriteLine("Filling Isolation Cooling System . . .");
        Thread.Sleep(1000);
    }
    public void Vent()
    {
        Console.WriteLine("Venting Isolation Cooling System . . .");
        Thread.Sleep(1000);
    }
}

internal class Maker
{
    TemperatureChecker tcheck = new TemperatureChecker();
    Random random = new Random();

    public int CheckNougatTemperature()
    {
        int randomTemp = random.Next(155, 170 + 1);
        Console.WriteLine("Nougat temperature: " + randomTemp + " C");
        return randomTemp;
    }
    public void CheckAirSystem()
    {
        bool air = random.Next(2) == 0;
        Console.WriteLine("Checking Air System . . .");
        if (air == true)
            Console.WriteLine("Air Present in System");
        else
            Console.WriteLine("Pumping m3 Air into System");
    }
}

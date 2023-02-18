using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public bool air = false;
    Maker maker = new Maker();

    /// <summary>
    /// If the nougat temperature exceeds 160C it's too hot
    /// </summary>
    public static bool IsNougatTooHot()
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
        TurbineController turbines = new TurbineController();
        turbines.CloseTripValve(2);
        IsolationCoolingSystem.Fill();
        IsolationCoolingSystem.Vent();
        maker.CheckAirSystem();
    }
    /// <summary>
    /// This code runs every 3 minutes to check the temperature.
    /// If it exceeds 160c we need to vent the cooling system.
    /// </summary>
    public void ThreeMinuteCheck()
    {
        if (IsNougatTooHot() == true)
        {
            DoCICSVentProcedure();
        }
    }
}

internal class TurbineController
{

}

internal class IsolationCoolingSystem
{

}

internal class Maker
{
    TemperatureChecker tcheck = new TemperatureChecker();
    int t = 165;// temp in C

    public int CheckNougatTemperature()
    {
        Console.WriteLine("Nougat temperature: " + t + " C");
        return t;
    }
    public void CheckAirSystem()
    {
        Console.WriteLine("Checking Air System . . .");
        if (tcheck.air == true)
        {
            Console.WriteLine("Air Present in System");
        }
    }
}

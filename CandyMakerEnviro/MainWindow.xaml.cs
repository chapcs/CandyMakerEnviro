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

    public void DoCICSVentProcedure()
    {
        turbines.CloseTripValve(2);
        isolation.Fill();
        isolation.Vent();
        maker.CheckAirSystem();
    }

    public void ThreeMinuteCheck()
    {
        if (IsNougatTooHot() == true)
            DoCICSVentProcedure();
        else
            Console.WriteLine("Nougat is still at or below 160 C\n");
    }
}

internal class TurbineController
{
    public void CloseTripValve(int valve)
    {
        Console.WriteLine("Closing trip valve #" + valve);
        Thread.Sleep(2000);
    }
}

internal class IsolationCoolingSystem
{
    public void Fill()
    {
        Console.Write("Filling ICS : ");
        Thread.Sleep(2000);
    }
    public void Vent()
    {
        Console.WriteLine("Venting ICS");
        Thread.Sleep(2000);
    }
}

internal class Maker
{
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
        Console.WriteLine("Checking for air in system . . .");
        Thread.Sleep(2000);
        if (air == true)
            Console.WriteLine("Air present in system\n");
        else
            Console.WriteLine("10 m3 air added to system\n");
    }
}

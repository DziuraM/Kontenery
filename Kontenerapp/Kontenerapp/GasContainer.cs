namespace ContainerManagement;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }

    public GasContainer(double maxCapacity, double pressure) : base(maxCapacity, "G")
    {
        Pressure = pressure;
    }

    public override double ContainerWeight
    {
        get { return 3000; }
    }

    public override void UnloadCargo()
    {
        CurrentLoad = CurrentLoad * 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine("Notyfikacja niebezpieczeństwa (Kontener gazowy): " + message);
    }
}
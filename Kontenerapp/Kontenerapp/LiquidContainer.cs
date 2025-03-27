namespace ContainerManagement;

public class LiquidContainer : Container, IHazardNotifier
{
    public LiquidContainer(double maxCapacity) : base(maxCapacity, "L") { }
    public override double ContainerWeight
    {
        get { return 2000; }
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine("Notyfikacja niebezpieczeństwa (Kontener płynny): " + message);
    }
}
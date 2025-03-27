namespace ContainerManagement;

public abstract class Container
{
    public string SerialNumber { get; }
    public double MaxCapacity { get; protected set; }
    public double CurrentLoad { get; protected set; }

    public Container(double maxCapacity, string type)
    {
        MaxCapacity = maxCapacity;
        CurrentLoad = 0;
        SerialNumber = SerialNumberGenerator.GetNextSerial(type);
    }

    public virtual double ContainerWeight
    {
        get { return 0; }
    }

    public virtual void LoadCargo(double mass, bool isHazardous)
    {
        if (mass > MaxCapacity)
            throw new OverfillException("Masa ładunku przekracza pojemność kontenera.");

        double allowed = isHazardous ? 0.5 * MaxCapacity : 0.9 * MaxCapacity;
        if (mass > allowed)
        {
            if (this is IHazardNotifier notifier)
            {
                notifier.NotifyHazard($"Próba załadowania {mass} kg, co przekracza dozwolony poziom ({allowed} kg) w kontenerze {SerialNumber}.");
            }
            throw new InvalidOperationException("Ładowanie przekracza dozwolony poziom dla tego kontenera.");
        }
        CurrentLoad = mass;
    }

    public virtual void UnloadCargo()
    {
        CurrentLoad = 0;
    }

    public override string ToString()
    {
        return $"{SerialNumber} - Ładunek: {CurrentLoad} kg / Pojemność: {MaxCapacity} kg";
    }
}
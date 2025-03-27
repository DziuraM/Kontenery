namespace ContainerManagement;

public class ReeferContainer : Container, IHazardNotifier
    {
        public double Height { get; set; }
        public double Depth { get; set; }
        public double OwnWeight { get; set; }
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }

        public ReeferContainer(double maxCapacity, double height, double depth, double ownWeight)
            : base(maxCapacity, "C")
        {
            Height = height;
            Depth = depth;
            OwnWeight = ownWeight;
        }

        public override double ContainerWeight
        {
            get { return OwnWeight; }
        }

        public void LoadCargo(double mass, bool isHazardous, string productType, double temperature)
        {
            if (CurrentLoad > 0 && ProductType != productType)
            {
                throw new InvalidOperationException("Kontener może przechowywać jedynie produkty tego samego typu.");
            }
            if (CurrentLoad == 0)
            {
                ProductType = productType;
                Temperature = temperature;
            }
            base.LoadCargo(mass, isHazardous);
        }

        public override void LoadCargo(double mass, bool isHazardous)
        {
            throw new NotImplementedException("Dla kontenera chłodniczego użyj metody LoadCargo z dodatkowymi parametrami (produkt, temperatura).");
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine("Notyfikacja niebezpieczeństwa (Kontener chłodniczy): " + message);
        }

        public override string ToString()
        {
            return base.ToString() +
                   $" [Chłodniczy: Produkt={ProductType}, Temp={Temperature}°C, Wymiary: {Height}x{Depth} cm, Waga kontenera: {OwnWeight} kg]";
        }
    }
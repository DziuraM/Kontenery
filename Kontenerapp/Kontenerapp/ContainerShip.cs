namespace ContainerManagement;

    public class ContainerShip
    {
        public List<Container> Containers { get; private set; }
        public double MaxSpeed { get; set; }
        public int MaxContainerCount { get; set; }
        public double MaxTotalWeight { get; set; }

        public ContainerShip(double maxSpeed, int maxContainerCount, double maxTotalWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxTotalWeight = maxTotalWeight;
            Containers = new List<Container>();
        }

        public double CurrentTotalWeight()
        {
            double total = 0;
            foreach (var container in Containers)
            {
                total += container.ContainerWeight + container.CurrentLoad;
            }
            return total;
        }

        public void AddContainer(Container container)
        {
            if (Containers.Count >= MaxContainerCount)
            {
                throw new InvalidOperationException("Osiągnięto maksymalną liczbę kontenerów na statku.");
            }
            if (CurrentTotalWeight() + container.ContainerWeight + container.CurrentLoad > MaxTotalWeight * 1000)
            {
                throw new InvalidOperationException("Dodanie tego kontenera przekracza maksymalną dopuszczalną wagę statku.");
            }
            Containers.Add(container);
        }

        public void RemoveContainer(string serialNumber)
        {
            var container = Containers.Find(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                throw new InvalidOperationException("Kontener o podanym numerze nie został znaleziony na statku.");
            }
            Containers.Remove(container);
        }

        public void UnloadContainer(string serialNumber)
        {
            var container = Containers.Find(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                throw new InvalidOperationException("Kontener o podanym numerze nie został znaleziony na statku.");
            }
            container.UnloadCargo();
        }

        public void ReplaceContainer(string serialNumber, Container newContainer)
        {
            int index = Containers.FindIndex(c => c.SerialNumber == serialNumber);
            if (index == -1)
            {
                throw new InvalidOperationException("Kontener o podanym numerze nie został znaleziony na statku.");
            }
            double currentWeightWithout = CurrentTotalWeight() - (Containers[index].ContainerWeight + Containers[index].CurrentLoad);
            if (currentWeightWithout + newContainer.ContainerWeight + newContainer.CurrentLoad > MaxTotalWeight * 1000)
            {
                throw new InvalidOperationException("Zastąpienie kontenera nowym przekracza dopuszczalną wagę statku.");
            }
            Containers[index] = newContainer;
        }

        public void TransferContainerTo(ContainerShip targetShip, string serialNumber)
        {
            var container = Containers.Find(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                throw new InvalidOperationException("Kontener o podanym numerze nie został znaleziony na statku.");
            }
            this.RemoveContainer(serialNumber);
            targetShip.AddContainer(container);
        }

        public void PrintShipInfo()
        {
            Console.WriteLine($"Kontenerowiec - Prędkość: {MaxSpeed} węzłów, Maksymalna liczba kontenerów: {MaxContainerCount}, Maksymalna waga: {MaxTotalWeight} ton");
            Console.WriteLine("Aktualne kontenery na statku:");
            if (Containers.Count == 0)
            {
                Console.WriteLine("Brak");
            }
            else
            {
                foreach (var container in Containers)
                {
                    Console.WriteLine(container.ToString());
                }
            }
        }
    }
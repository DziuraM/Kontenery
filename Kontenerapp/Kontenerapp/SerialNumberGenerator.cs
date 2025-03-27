namespace ContainerManagement;

public static class SerialNumberGenerator
{
    private static int counter = 0;
    public static string GetNextSerial(string type)
    {
        return $"KON-{type}-{++counter}";
    }
}
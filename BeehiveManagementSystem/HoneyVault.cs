
namespace BeehiveManagementSystem;

internal static class HoneyVault
{
    private static decimal honey = Constants.INITIAL_HONEY;
    private static decimal nectar = Constants.INITIAL_NECTAR;

    public static void Reset()
    {
        honey = Constants.INITIAL_HONEY;
        nectar = Constants.INITIAL_NECTAR;
    }

    public static bool ConsumeHoney(decimal amount)
    {
        if (honey >= amount)
        {
            honey = honey - amount;
            return true;
        }
        else 
            return false;
    }

    public static void CollectNectar(decimal amount)
    {
        if (amount > 0)
            nectar = nectar + amount;
        return;
    }

    public static void ConvertNectarToHoney(decimal amount)
    {
        decimal actualAmount = amount;
        if (amount > nectar) actualAmount = nectar;

        nectar -= actualAmount;
        honey += actualAmount * Constants.NECTAR_CONVERSATION_RATIO;
    }

    public static string StatusReport
    {
        get
        {
            decimal edge = Constants.LOW_LEVEL_WARNING;
            string warning = string.Empty;
            if (honey<edge)
                warning = "LOW HONEY - ADD A HONEY MANUFACTURER";
            if (nectar<edge)
                warning = "LOW NECTAR - ADD A NECTAR COLLECTOR";
            if (honey < edge && nectar < edge)
                warning = $"LOW HONEY - ADD A HONEY MANUFACTURER \nLOW NECTAR - ADD A NECTAR COLLECTOR";

            return $"\nHoney amount: {honey} \nNectar amount: {nectar} \n{warning}";
        }
    }
    
}
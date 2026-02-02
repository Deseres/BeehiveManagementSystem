namespace BeehiveManagementSystem;

internal class HoneyVault
{
    private static decimal honey = Constants.INITIAL_HONEY;

    private static decimal nectar = Constants.INITIAL_NECTAR;

    public static bool ConsumeHoney(decimal amount)
    {
        if (honey >= amount)
        {
            honey -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CollectNectar(decimal amount)
    {
        if (amount > 0)
        {
            nectar += amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ConvertNectarToHoney(decimal amount)
    {
        nectar -= amount;
        honey += amount * Constants.NECTAR_CONVERSATION_RATIO;
        if (amount >= nectar)
        {
            nectar = amount;
            honey += amount * Constants.NECTAR_CONVERSATION_RATIO;
        }
        return true;
    }

    public string StatusReport
    {
        get
        {
            if (honey < Constants.LOW_LEVEL_WARNING && nectar < Constants.LOW_LEVEL_WARNING)
            {
                return $"LOW HONEY AND NECTAR: {honey}/n {nectar}";
            }
            if (honey < Constants.LOW_LEVEL_WARNING)
            {
                return $"LOW HONEY: {honey}/n {nectar}";
            }
            else if (nectar < Constants.LOW_LEVEL_WARNING)
            {
                return $"LOW NECTAR: {honey}/n {nectar}";
            }
            else
            {
                return $"{honey}/n {nectar}";
            }
        }
    }

    public static decimal Reset()
    {
        honey = Constants.INITIAL_HONEY;
        nectar = Constants.INITIAL_NECTAR;
        return honey;
    }
}

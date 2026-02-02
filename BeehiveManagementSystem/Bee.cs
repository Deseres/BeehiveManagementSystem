namespace BeehiveManagementSystem;

internal class Bee
{
    public virtual decimal CostPerShift { get; }

    public string Job { get; private set; }

    public Bee (string job)
    {
        Job = job;
    }

    public virtual bool WorkTheNextShift()
    {
        if (HoneyVault.ConsumeHoney(CostPerShift))
            return true;
        else
            return false;
    }

}

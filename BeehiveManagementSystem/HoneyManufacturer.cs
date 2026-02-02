namespace BeehiveManagementSystem;

internal class HoneyManufacturer : Bee
{
    public HoneyManufacturer() : base("Honey Manufacturer")
    {
    }

    public override decimal CostPerShift => Constants.HONEY_MANUFACTRORER_COST;

    public override bool WorkTheNextShift()
    {
        HoneyVault.ConvertNectarToHoney(Constants.NECTAR_PRODUCED_PER_SHIFT);
        return base.WorkTheNextShift();
    }
}

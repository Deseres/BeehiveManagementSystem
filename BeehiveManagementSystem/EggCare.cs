namespace BeehiveManagementSystem;

internal class EggCare : Bee
{
    private Queen queen;

    public EggCare(Queen queen) : base("Egg Care")
    {
        this.queen = queen;
    }

    public override decimal CostPerShift => Constants.EGG_CARE_COST;

    public override bool WorkTheNextShift()
    {
        queen.ReportEggConversion(Constants.CARE_PROGRESS_PER_SHIFT);
        return base.WorkTheNextShift();
    }
}
namespace BeehiveManagementSystem;

internal class Queen : Bee
{
    private Bee[] workers = new Bee[0];

    private decimal eggs = 0;
    private decimal unassignedWorkers = 3;

    public bool CanAssignWorkers {get { return unassignedWorkers >= 1; }}

    public string StatusReport { get; private set; } = "";

    public override decimal CostPerShift { get { return Constants.QEEN_COST_PER_SHIFT; } }

    public Queen() : base("Queen")
    {
        // Call the method 3 times to initialize the hive
        AssignBee("Nectar Collector");
        AssignBee("Honey Manufacturer");
        AssignBee("Egg Care");
    }

    private void AddWorker(Bee worker)
    {
        if (unassignedWorkers >= 1)
        {
            unassignedWorkers--;
            Array.Resize(ref workers, workers.Length + 1);
            workers[workers.Length - 1] = worker;
        }
    }

    public void AssignBee(string? job)
    {
            switch (job)
            {
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
                case "Honey Manufactorer":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
        }
    }


    private void UpdateStatusReeport(bool allWorkersDidTheirJob)
    { 
        StatusReport = $"VaultReport: \n {HoneyVault.StatusReport} \n" +
                       $"\nEggs count: {eggs:0:00}\nUnassigned workers: {unassignedWorkers:0:00)\n" +
                       $"{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufactorer")}" +
                       $"\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";

        if (!allWorkersDidTheirJob)
            {
            StatusReport = "Error: Not all workers completed their jobs successfully.\n" + StatusReport;
        }
    }

    private string WorkerStatus(string job)
    {
        int count = 0;
        foreach (Bee worker in workers)
        {
            if (worker.Job == job)
                count++;
        }
        string s = "s";
        if (count == 1)
            s = ""; 
        return $"{job}s: {count}";
    }

    public void ReportEggConversion(decimal eggsToConvert)
    {
        if (eggs >= eggsToConvert)
        {
            eggs -= eggsToConvert;
            unassignedWorkers += eggsToConvert;
        }
    }

    public override bool WorkTheNextShift()
    {
        eggs += Constants.EGGS_PER_SHIFT;
        bool allWorkersDidTheirJob = true;
        foreach (Bee worker in workers)
        {
            if (!worker.WorkTheNextShift())
            {
                allWorkersDidTheirJob = false;
            }
        }
        HoneyVault.ConsumeHoney(unassignedWorkers * Constants.NECTAR_PER_UNASSIGNED_WORKER);
        UpdateStatusReeport(allWorkersDidTheirJob);
        return base.WorkTheNextShift();
    }
   






}
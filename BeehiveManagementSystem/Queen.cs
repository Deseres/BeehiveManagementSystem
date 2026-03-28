using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BeehiveManagementSystem;

internal class Queen : Bee
{

    private IWorker[] workers = new IWorker[0];
    private decimal eggs  = 0;
    private decimal unassignedWorkers = 3;

    public bool CanAssignWorkers
    {
        get { return unassignedWorkers >= 1;  }
    }
    public string StatusReport { get; private set; } = "";

    public override decimal CostPerShift
    {
        get { return Constants.QUEEN_COST_PER_SHIFT; }
    }

    public Queen() : base("Queen")
    {
        AssignBee("Egg Care");
        AssignBee("Honey Manufacturer");
        AssignBee("Nectar Collector");
    }

    public void AssignBee(string job)
    {
        switch (job)
        {
            case "Egg Care":
                AddWorker(new EggCare(this));

                break;
            case "Honey Manufacturer":
                AddWorker(new HoneyManufacturer());

                break;
            case "Nectar Collector":
                AddWorker(new NectarCollector());
                
                break;
            default:
                return;
        }
        UpdateStatusReport(true);
    }

    private void AddWorker(IWorker worker)
    {
        if (unassignedWorkers >= 1)
        {
            unassignedWorkers--;
            Array.Resize(ref workers, workers.Length + 1);
            workers[workers.Length - 1] = worker;
        }
    }

    public void ReportEggConversion(decimal eggsToConvert)
    {
        if (eggs >= eggsToConvert)
        {
            eggs = eggs - eggsToConvert;
            unassignedWorkers = unassignedWorkers + eggsToConvert;
        }
    }

    public override bool WorkTheNextShift()
    {
        eggs = eggs + Constants.EGGS_PER_SHIFT;
        bool allWorkersDidTheirJobs = true;
        foreach (IWorker worker in workers)
        {
            if (!worker.WorkTheNextShift())
                allWorkersDidTheirJobs = false;

        }

        HoneyVault.ConsumeHoney(Constants.HONEY_PER_UNASSIGNED_WORKER * unassignedWorkers);
        UpdateStatusReport(allWorkersDidTheirJobs);
            return base.WorkTheNextShift();
    }

    private void UpdateStatusReport(bool allWorkersDidTheirJob)
    {
        StatusReport = $"Vault report: {HoneyVault.StatusReport}\n" +
                       $"Egg count: {eggs:0.00}\nUnassignedWorkers: {unassignedWorkers:0.00}\n" +
                       $"{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS:{workers.Length}";
        if (!allWorkersDidTheirJob)
        {
            StatusReport += "\nWARNING: NOT ALL WORKERS DID THEIR JOB";
        }
    }

    private string WorkerStatus(string job)
    {
        int count = 0;
        foreach (IWorker worker in workers)
        {
            if (worker.Job == job)
            {
                count += 1;
            }
        }
        string s = "s";
        if (count == 1) s = "";
        return $"{count} {job} bee{s}";
    }
}
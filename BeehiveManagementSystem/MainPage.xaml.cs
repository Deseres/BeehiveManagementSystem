namespace BeehiveManagementSystem;

public partial class MainPage : ContentPage
{
    private Queen queen = new Queen();
    public MainPage()
    {

        InitializeComponent();

        JobPicker.ItemsSource = new string[]
            {   "Nectar Collector", 
                "Honey Manufacturer",
                "Egg Care"
            };
        JobPicker.SelectedIndex = 0;

        UpdateStatusAndEnableAssignButton();
        
        Dispatcher.StartTimer(TimeSpan.FromSeconds(1.5), TimerTick);
    }

    private bool TimerTick()
    {
        if (!this.IsLoaded || !WorkShiftButton.IsVisible)
            return false;

        OnWorkTheNextShiftButtonClicked(this, new EventArgs());
        return true;
    }

    private void UpdateStatusAndEnableAssignButton()
    {
        StatusReport.Text = queen.StatusReport;
        AssignJobButton.IsEnabled = queen.CanAssignWorkers;
    }

    public void OnAssignJobButtonClicked(object sender, EventArgs e)
    {
        if (JobPicker.SelectedItem != null)
        {
            string selectedJob = JobPicker.SelectedItem.ToString();
            queen.AssignBee(selectedJob);
            UpdateStatusAndEnableAssignButton();
        }
    }

    public void OnWorkTheNextShiftButtonClicked(object sender, EventArgs e)
    {
        if (!queen.WorkTheNextShift())
        {
            WorkShiftButton.IsVisible = false;
            OutOfHoneyButton.IsVisible = true;
        }
        UpdateStatusAndEnableAssignButton();
    }

    public void OnOutOfHoneyButtonClicked(object sender, EventArgs e)
    {
        HoneyVault.Reset();
        queen = new Queen();
        WorkShiftButton.IsVisible = true;
        OutOfHoneyButton.IsVisible = false;
        UpdateStatusAndEnableAssignButton();
    }

    
}
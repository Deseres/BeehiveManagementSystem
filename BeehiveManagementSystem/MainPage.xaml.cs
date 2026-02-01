namespace BeehiveManagementSystem;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        // This method loads the XAML. Do not remove it.
        InitializeComponent();

        // 1. Define your data
        string[] jobs = { "Nectar Collector", "Honey Manufactorer", "Egg Care"};

        // 2. Assign it to the picker
        JobPicker.ItemsSource = jobs;
    }

    public void OnAssignJobButtonClicked(object sender, EventArgs e)
    {

    }

    public void OnWorkTheNextShiftButtonClicked(object sender, EventArgs e)
    {
    }

    public void onOutOfHoneyButtonClicked(object sender, EventArgs e)
    {
    }
}
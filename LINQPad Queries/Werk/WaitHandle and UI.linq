<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

async void Main()
{
  var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
  var data = new Data {Value = "No action"};
  var window = new Window();  
  window.Closed += (o, e) => waitHandle.Set();
  StackPanel panel = new StackPanel();
  Button button = new Button {Content= "Click"};
  button.Click += (o, e) => {
    data.Value ="Clicked";
    window.Close();    
    };   
  panel.Children.Add(button); 
  window.Content = panel;
  window.Show();
  await Task.Run(() => waitHandle.WaitOne());  
  $"Window closed: {data.Value}".Dump();
}

class Data 
{
  public string Value {get; set; } = "not set";
}

// Define other methods and classes here

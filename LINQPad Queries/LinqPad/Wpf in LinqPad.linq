<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\System.Windows.Presentation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
</Query>

var wrapper = new WrapPanel();
wrapper.Children.Add(new Label  { Content = "Hello" });
wrapper.Children.Add(new Label  { Content = "Hello" });
wrapper.Children.Add(new Button { Content = "Click" });
wrapper.Children.Add(new GroupBox { Header = "Data", Content="Wat voor weer zal het zijn in Den Haag?" });

PanelManager.StackWpfElement (wrapper, "MyWrapper");
PanelManager.StackWpfElement (new Label  { Content = "Hello" }, "WPF Control Gallery");
PanelManager.StackWpfElement (new Button { Content = "Click" }, "WPF Control Gallery");
PanelManager.StackWpfElement (new Expander { Header = "More" }, "WPF Control Gallery");
PanelManager.StackWpfElement (new GroupBox { Header = "Data" }, "WPF Control Gallery");
PanelManager.StackWpfElement (new Slider { }, "WPF Control Gallery");

var layoutContainer = new Grid();
var myStrings = new string[] {"Hello", "WPF", "in", "LinqPad"};
PanelManager.DisplayWpfElement(new DataGrid(){ItemsSource = myStrings}, "DisplayWpfElement");

var polyline = new Polyline();
polyline.Stroke = Brushes.Red;
polyline.StrokeThickness = 1d;
polyline.Points.Add(new Point (20, 30));
polyline.Points.Add(new Point (30, 250));
polyline.Points.Add(new Point (40, 260));
polyline.Points.Add(new Point (50, 282));
polyline.Points.Add(new Point (60, 363));
polyline.Points.Add(new Point (70, 385));
polyline.Points.Add(new Point (80, 181));
polyline.Points.Add(new Point (90, 50));
polyline.LayoutTransform = new ScaleTransform(){ScaleY= -1};
layoutContainer.Children.Add (polyline); 

polyline = new Polyline();
polyline.Stroke = Brushes.Green;
polyline.StrokeThickness = 1d;
polyline.Points.Add(new Point (20, 130));
polyline.Points.Add(new Point (30, 230));
polyline.Points.Add(new Point (40, 360));
polyline.Points.Add(new Point (50, 182));
polyline.Points.Add(new Point (60, 323));
polyline.Points.Add(new Point (70, 155));
polyline.Points.Add(new Point (80, 111));
polyline.Points.Add(new Point (90, 90));
polyline.LayoutTransform = new ScaleTransform(){ScaleY= -1};
layoutContainer.Children.Add (polyline); 

PanelManager.DisplayWpfElement (layoutContainer, "Graph");

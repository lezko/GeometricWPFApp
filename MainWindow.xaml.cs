using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Assembly loadedAssembly;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadLibrary_Click(object sender, RoutedEventArgs e)
        {
            string path = txtLibraryPath.Text;

            if (!File.Exists(path))
            {
                MessageBox.Show("File not found.");
                return;
            }

            try
            {
                loadedAssembly = Assembly.LoadFrom(path);
                var types = loadedAssembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(Lab3.library.IGeometricFigure))).ToList();
                listBoxClasses.Items.Clear();

                foreach (var type in types)
                {
                    listBoxClasses.Items.Add(type.FullName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assembly: {ex.Message}");
            }
        }

        private void listBoxClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelMethods.Children.Clear();

            if (listBoxClasses.SelectedItem == null) return;

            string className = listBoxClasses.SelectedItem.ToString();
            var type = loadedAssembly.GetType(className);
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                              .Where(m => !m.IsSpecialName).ToList();

            foreach (var method in methods)
            {
                var methodPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 5) };

                var lbl = new Label { Content = method.Name, Width = 150 };
                methodPanel.Children.Add(lbl);

                var paramsCount = method.GetParameters().Length;

                for (int i = 0; i < paramsCount; i++)
                {
                    var paramLabel = new Label { Content = method.GetParameters()[i].Name, Width = 100 };
                    methodPanel.Children.Add(paramLabel);
                    var paramTextBox = new TextBox { Width = 100 };
                    methodPanel.Children.Add(paramTextBox);
                }

                panelMethods.Children.Add(methodPanel);
            }

            var executeButton = new Button { Content = "Execute" };
            executeButton.Click += (s, ev) => ExecuteMethod(type, methods);
            panelMethods.Children.Add(executeButton);
        }

        private void ExecuteMethod(Type type, System.Collections.Generic.List<MethodInfo> methods)
        {
            if (methods.Count == 0) return;

            var constructor = type.GetConstructors().First();
            var parameters = constructor.GetParameters();
            var constructorArgs = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var textBox = panelMethods.Children.OfType<StackPanel>().ElementAt(i).Children.OfType<TextBox>().First();
                constructorArgs[i] = Convert.ChangeType(textBox.Text, parameters[i].ParameterType);
            }

            var instance = Activator.CreateInstance(type, constructorArgs);

            foreach (var method in methods)
            {
                var methodParams = method.GetParameters();
                var methodArgs = new object[methodParams.Length];

                for (int i = 0; i < methodParams.Length; i++)
                {
                    var stackPanel = panelMethods.Children.OfType<StackPanel>().ElementAt(i);
                    var textBox = stackPanel.Children.OfType<TextBox>().ElementAt(parameters.Length + i);
                    methodArgs[i] = Convert.ChangeType(textBox.Text, methodParams[i].ParameterType);
                }

                var result = method.Invoke(instance, methodArgs);
                MessageBox.Show($"Method {method.Name} executed. Result: {result}");
            }
        }
    }
}

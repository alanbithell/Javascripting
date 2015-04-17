using System;
using System.Collections.Generic;
using System.Linq;
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
using Noesis.Javascript;
using System.IO;


namespace Javascripting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class SystemConsole
        {
            public SystemConsole() { }

            public void Print(string iString)
            {
                Console.WriteLine(iString);
            }
        }

        public string[] LoadScript(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);

            string[] script = new string[3];

            do
            {
                string line = reader.ReadLine();
                

                if (line.Contains("//region"))
                {
                    string regionType = line.Split(' ')[1];
                    switch (regionType)
                    {
                        case "var_def":
                            while (!(line = reader.ReadLine()).Contains("//endregion"))
                            {
                                script[0] += line + System.Environment.NewLine;
                            }
                            break;
                        case "output_gen":
                            while (!(line = reader.ReadLine()).Contains("//endregion"))
                            {
                                script[1] += line + System.Environment.NewLine;
                            }
                            break;
                        case "input_proc":
                            while (!(line = reader.ReadLine()).Contains("//endregion"))
                            {
                                script[2] += line + System.Environment.NewLine;
                            }
                            break;
                    }
                }

            } while (reader.Peek() != -1);
            reader.Close();
            return script;
        }

        public MainWindow()
        {
            InitializeComponent();

            double[] InputStream = new double[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2};
            double[] OutputStream = new double[16];
            string[] scriptRegions = LoadScript(@"Y:\Documents\Sontia\DevelopementProjects\Ford\Measurement Tool\script.js");
            string script;

            // Initialize a context
            using (JavascriptContext context = new JavascriptContext())
            {

                // Setting external parameters for the context
                context.SetParameter("console", new SystemConsole());
                context.SetParameter("message", "Hello World !");
                context.SetParameter("number", 1);
                context.SetParameter("InputStream", InputStream);
                context.SetParameter("OutputStream", OutputStream);
                context.SetParameter("blockSize", 16);

                // Script
                script = scriptRegions[0] + scriptRegions[1];

                // Running the script
                context.Run(script);

                // Script
                script = scriptRegions[0] + scriptRegions[2];

                // Running the script
                context.Run(script);
            }


        }

        private void _switchPanel_ActiveLayoutChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}

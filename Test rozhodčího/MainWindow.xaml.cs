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

namespace Test_rozhodčího
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Questions data = new Questions("data.xml");

        int right = 0; // správné odpovědi
        double succes = 0; // úspěšnost v procentech
        bool evaluated = false;

        public MainWindow()
        {
            InitializeComponent();
            Setup();

            this.DataContext = data;            
        }

        private void Setup()
        {
            data.Select(20);

            // vyhledá všechny stackpanely, ve kterých jsou radio buttony
            IEnumerable<StackPanel> stackPanels = Playground.Children.OfType<StackPanel>();
            foreach (StackPanel s in stackPanels)
            {
                IEnumerable<RadioButton> rbuttons = s.Children.OfType<RadioButton>();
                foreach (RadioButton r in rbuttons)
                {
                    r.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC107"));
                }
            }
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            int tmp = 0; // pro iteraci

            // vyhledá všechny stackpanely, ve kterých jsou radio buttony
            IEnumerable<StackPanel> stackPanels = Playground.Children.OfType<StackPanel>();
            foreach (StackPanel s in stackPanels)
            {
                IEnumerable<RadioButton> rbuttons = s.Children.OfType<RadioButton>();
                foreach (RadioButton r in rbuttons)
                {
                    TextBlock t = (TextBlock)r.Content;
                    if (t.Text == data.Selection[tmp].True)
                    {
                        t.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2E7D32"));

                        if (r.IsChecked == true && r.Background != Brushes.DimGray && !evaluated)
                        {
                            right++;                          
                        }       
                    }

                    
                }
                tmp++;
            }

            // vyhodnocení
            if (evaluated == false)
            {
                succes = right * 100 / 20;
            }
            MessageBox.Show($"{right} otázek správných. Vaše úspěšnost je {succes}%.");
            evaluated = true;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // vyhledá všechny stackpanely, ve kterých jsou radio buttony
            IEnumerable<StackPanel> stackPanels = Playground.Children.OfType<StackPanel>();
            foreach (StackPanel s in stackPanels)
            {
                IEnumerable<RadioButton> rbuttons = s.Children.OfType<RadioButton>();
                foreach (RadioButton r in rbuttons)
                {
                    TextBlock t = (TextBlock)r.Content;
                    r.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC107"));
                    t.Foreground = Brushes.Black;
                    r.IsChecked = false;
                }
            }

            Scroll.ScrollToTop();

            evaluated = false;
            right = 0;
            Setup();
        }

        private void radioClick(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (evaluated)
            {
                rb.Background = Brushes.DimGray;
            }
        }
    }
}

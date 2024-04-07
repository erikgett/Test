using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Purple_Click(object sender, RoutedEventArgs e)
        {
            SetPrimaryColor(Colors.MediumPurple, BaseTheme.Dark);
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            SetPrimaryColor(Colors.LightGreen, BaseTheme.Light);
        }

        private static void SetPrimaryColor(Color color, BaseTheme themeType = BaseTheme.Light)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            //Apply primary color
            theme.SetPrimaryColor(color);

            theme.SetBaseTheme(themeType);

            //Apply theme to application
            paletteHelper.SetTheme(theme);
        }
    }
}
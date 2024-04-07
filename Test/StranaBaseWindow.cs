using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;

namespace Test
{
    public class StranaBaseWindow : Window
    {
        public StranaBaseWindow()
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("/Test;component/WindowResources.xaml", UriKind.Relative)
            };

            Resources.MergedDictionaries.Add(resourceDictionary);

            // Получение динамического ресурса MaterialDesignWindow из ресурсов приложения
            Style materialDesignWindowStyle = (Style)Application.Current.MainWindow.FindResource("MaterialDesignWindow");

            // Установка стиля окна
            this.Style = materialDesignWindowStyle;
        }

        public void DarkThemeClick(object sender, RoutedEventArgs e)
        {
            SetPrimaryColor(Colors.MediumPurple, BaseTheme.Dark);
        }

        public void LightThemeClick(object sender, RoutedEventArgs e)
        {
            SetPrimaryColor(Colors.MediumPurple, BaseTheme.Light);
        }

        private static void SetPrimaryColor(Color color, BaseTheme themeType = BaseTheme.Light)
        {
            PaletteHelper paletteHelper = new PaletteHelper();

            Theme theme = paletteHelper.GetTheme();
            //Apply primary color
            theme.SetPrimaryColor(color);

            theme.SetBaseTheme(themeType);

            //Apply theme to application
            paletteHelper.SetTheme(theme);
        }
    }
}

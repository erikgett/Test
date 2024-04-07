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

            Theme theme = paletteHelper.GetTheme();
            //Apply primary color
            theme.SetPrimaryColor(color);

            theme.SetBaseTheme(themeType);

            //Apply theme to application
            paletteHelper.SetTheme(theme);
        }
    }

    public class PaletteHelper
    {
        public virtual Theme GetTheme()
        {
            if (Application.Current is null)
                throw new InvalidOperationException($"Cannot get theme outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.GetTheme)} on the appropriate resource dictionary instead.");
            return GetResourceDictionary().GetTheme();
        }

        public virtual void SetTheme(Theme theme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));
            if (Application.Current.MainWindow is null)
                throw new InvalidOperationException($"Cannot set theme outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.SetTheme)} on the appropriate resource dictionary instead.");
            GetResourceDictionary().SetTheme(theme);
            RecreateThemeDictionaries();
        }

        public virtual IThemeManager? GetThemeManager()
        {
            if (Application.Current.MainWindow is null)
                throw new InvalidOperationException($"Cannot get ThemeManager outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.GetThemeManager)} on the appropriate resource dictionary instead.");
            return GetResourceDictionary().GetThemeManager();
        }

        private static ResourceDictionary GetResourceDictionary()
            => Application.Current.MainWindow.Resources.MergedDictionaries.FirstOrDefault(x => x is IMaterialDesignThemeDictionary) ??
                Application.Current.MainWindow.Resources;

        /// <summary>
        /// Removes and readds resource dictionaries with static resource that will be re-evaluated with new theme brushes.
        /// This is primarily here for the obsolete theme brushes.
        /// </summary>
        private void RecreateThemeDictionaries()
        {
            ResourceDictionary root = Application.Current.MainWindow.Resources;
            for (int i = 0; i < root.MergedDictionaries.Count; i++)
            {
                ResourceDictionary dictionary = root.MergedDictionaries[i];
                if (dictionary["MaterialDesign.Resources.RecreateOnThemeChange"] is bool recreateOnThemeChange && recreateOnThemeChange)
                {
                    root.MergedDictionaries.RemoveAt(i);
                    root.MergedDictionaries.Insert(i, new ResourceDictionary()
                    {
                        Source = dictionary.Source
                    });
                }
            }
        }
    }
}
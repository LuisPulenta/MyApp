using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MyApp.Common.Helpers
{
    public static class Settings
    {
        private const string _token = "token";
        private const string _technical = "technical";
        private const string _isRemembered = "IsRemembered";
        private const string _visit = "visit";

        private static readonly string _settingsDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;


        public static string Visit
        {
            get => AppSettings.GetValueOrDefault(_visit, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_visit, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string Technical
        {
            get => AppSettings.GetValueOrDefault(_technical, _settingsDefault);
            set => AppSettings.AddOrUpdateValue(_technical, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }


    }
}
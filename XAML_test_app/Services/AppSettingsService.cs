using Windows.Storage;
using XAML_test_app.Models;

namespace XAML_test_app.Services
{
    public abstract class AppSettingsService
    {
        #region Settings key
        public const string NUMBER_OF_IMAGE_LOAD_SETTINGS = "noil";
        public const string REGION_SETTINGS = "region";

        //default save location settings
        public const string DEFAULT_SAVE_SETTINGS = "defaultSaveSettings";
        public const string DEFAULT_SAVE_LOCATION_SETTINGS = "defaultSaveLocationSettings";

        //auto start background settings
        public const string AUTO_WALLPAPER_SETTINGS = "asb";
        public const string AUTO_WALLPAPER_WIFI_SETTINGS = "asbwf";
        public const string LAST_AUTO_START_IMAGE = "autoStartImage";

        //auto live tile settings
        public const string LIVE_TILE_SETTINGS = "lts";
        public const string LIVE_TILE_WIFI_SETTINGS = "ltswf";
        public const string LAST_LIVE_TILE_IMAGE = "liveTileImage";

        //auto save settings
        public const string AUTO_SAVE_SETTINGS = "ass";
        public const string AUTO_SAVE_LOCATION_SETTINGS = "assl";
        public const string AUTO_SAVE_WIFI_SETTINGS = "asswf";
        public const string LAST_AUTO_SAVE_IMAGE = "autoSaveImage";

        public const string THEME_SETTINGS = "ts"; //0: dark, 1: light
        public const string WELCOME_DIALOG_VERSION = "wdv";
        #endregion

        public static void Initialize()
        {
            if (!ContainsSetting(NUMBER_OF_IMAGE_LOAD_SETTINGS)) SaveSetting(NUMBER_OF_IMAGE_LOAD_SETTINGS, 8.0);
            if (!ContainsSetting(REGION_SETTINGS)) SaveSetting(REGION_SETTINGS, new RegionCollection().GetDefaultRegion().Value);
            if (!ContainsSetting(DEFAULT_SAVE_SETTINGS)) SaveSetting(DEFAULT_SAVE_SETTINGS, false);
            if (!ContainsSetting(DEFAULT_SAVE_LOCATION_SETTINGS))
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", KnownFolders.SavedPictures);
                SaveSetting(DEFAULT_SAVE_LOCATION_SETTINGS, KnownFolders.SavedPictures.Path);
            }
            if (!ContainsSetting(AUTO_WALLPAPER_SETTINGS)) SaveSetting(AUTO_WALLPAPER_SETTINGS, false);
            if (!ContainsSetting(AUTO_WALLPAPER_WIFI_SETTINGS)) SaveSetting(AUTO_WALLPAPER_WIFI_SETTINGS, true);
            if (!ContainsSetting(LAST_AUTO_START_IMAGE)) SaveSetting(LAST_AUTO_START_IMAGE, string.Empty);
            if (!ContainsSetting(LIVE_TILE_SETTINGS)) SaveSetting(LIVE_TILE_SETTINGS, false);
            if (!ContainsSetting(LIVE_TILE_WIFI_SETTINGS)) SaveSetting(LIVE_TILE_WIFI_SETTINGS, true);
            if (!ContainsSetting(LAST_LIVE_TILE_IMAGE)) SaveSetting(LAST_LIVE_TILE_IMAGE, string.Empty);
            if (!ContainsSetting(AUTO_SAVE_SETTINGS)) SaveSetting(AUTO_SAVE_SETTINGS, false);
            if (!ContainsSetting(AUTO_SAVE_LOCATION_SETTINGS))
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", KnownFolders.SavedPictures);
                SaveSetting(AUTO_SAVE_LOCATION_SETTINGS, KnownFolders.SavedPictures.Path);
            }
            if (!ContainsSetting(AUTO_SAVE_WIFI_SETTINGS)) SaveSetting(AUTO_SAVE_WIFI_SETTINGS, true);
            if (!ContainsSetting(LAST_AUTO_SAVE_IMAGE)) SaveSetting(LAST_AUTO_SAVE_IMAGE, string.Empty);
            if (!ContainsSetting(THEME_SETTINGS)) SaveSetting(THEME_SETTINGS, 0);
            if (!ContainsSetting(WELCOME_DIALOG_VERSION)) SaveSetting(WELCOME_DIALOG_VERSION, 0);
        }

        public static object GetSetting(string key) => ApplicationData.Current.LocalSettings.Values[key];

        public static void SaveSetting(string key, object value) => ApplicationData.Current.LocalSettings.Values[key] = value;

        public static void RemoveSetting(string key) => ApplicationData.Current.LocalSettings.Values.Remove(key);

        public static bool ContainsSetting(string key) => ApplicationData.Current.LocalSettings.Values.ContainsKey(key);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using XAML_test_app.Models;
using XAML_test_app.Services;

namespace XAML_test_app.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties
        public RegionCollection RegionCollection { get; set; }

        private Region _SelectedRegion;
        public Region SelectedRegion
        {
            get { return _SelectedRegion; }
            set
            {
                _SelectedRegion = value;
                //if selection change, save it
                if (value != null)
                {
                    AppSettingsService.SaveSetting(AppSettingsService.REGION_SETTINGS, value.Value);
                }
            }
        }

        private double _NumberOfImageLoad;
        public double NumberOfImageLoad
        {
            get { return _NumberOfImageLoad; }
            set
            {
                _NumberOfImageLoad = value;
                AppSettingsService.SaveSetting(AppSettingsService.NUMBER_OF_IMAGE_LOAD_SETTINGS, value);
            }
        }

        private bool _DefaultSave;
        public bool DefaultSave
        {
            get { return _DefaultSave; }
            set { Set(ref _DefaultSave, value); }
        }

        private string _DefaultSaveLocationPath;
        public string DefaultSaveLocationPath
        {
            get { return _DefaultSaveLocationPath; }
            set { Set(ref _DefaultSaveLocationPath, value); }
        }

        private bool _AutoStartBackground;
        public bool AutoWallpaper
        {
            get { return _AutoStartBackground; }
            set { Set(ref _AutoStartBackground, value); }
        }

        private bool _IsAutoStartBackgroundTaskLoading;
        public bool IsAutoWallpaperTaskLoading
        {
            get { return _IsAutoStartBackgroundTaskLoading; }
            set { Set(ref _IsAutoStartBackgroundTaskLoading, value); }
        }

        private bool _AutoStatBackground_Wifi;
        public bool AutoWallpaper_Wifi
        {
            get { return _AutoStatBackground_Wifi; }
            set
            {
                _AutoStatBackground_Wifi = value;
                AppSettingsService.SaveSetting(AppSettingsService.AUTO_WALLPAPER_WIFI_SETTINGS, value);
            }
        }

        private bool _LiveTile;
        public bool LiveTile
        {
            get { return _LiveTile; }
            set { Set(ref _LiveTile, value); }
        }

        private bool _LiveTile_Wifi;
        public bool LiveTile_Wifi
        {
            get { return _LiveTile_Wifi; }
            set
            {
                _LiveTile_Wifi = value;
                AppSettingsService.SaveSetting(AppSettingsService.LIVE_TILE_WIFI_SETTINGS, value);
            }
        }

        private bool _IsLiveTileBackgroundTaskLoading;
        public bool IsLiveTileBackgroundTaskLoading
        {
            get { return _IsLiveTileBackgroundTaskLoading; }
            set { Set(ref _IsLiveTileBackgroundTaskLoading, value); }
        }

        private bool _AutoSave;
        public bool AutoSave
        {
            get { return _AutoSave; }
            set { Set(ref _AutoSave, value); }
        }

        private string _AutoSaveLocationPath;
        public string AutoSaveLocationPath
        {
            get { return _AutoSaveLocationPath; }
            set { Set(ref _AutoSaveLocationPath, value); }
        }

        private bool _AutoSave_Wifi;
        public bool AutoSave_Wifi
        {
            get { return _AutoSave_Wifi; }
            set
            {
                _AutoSave_Wifi = value;
                AppSettingsService.SaveSetting(AppSettingsService.AUTO_SAVE_WIFI_SETTINGS, value);
            }
        }

        private bool _IsAutoSaveBackgroundTaskLoading;
        public bool IsAutoSaveBackgroundTaskLoading
        {
            get { return _IsAutoSaveBackgroundTaskLoading; }
            set { Set(ref _IsAutoSaveBackgroundTaskLoading, value); }
        }

        private int _Theme;
        public int Theme
        {
            get { return _Theme; }
            set
            {
                _Theme = value;
                AppSettingsService.SaveSetting(AppSettingsService.THEME_SETTINGS, value);
            }
        }
        #endregion

        public MainViewModel()
        {
            RegionCollection = new RegionCollection();
            _SelectedRegion = RegionCollection.GetRegion(AppSettingsService.GetSetting(AppSettingsService.REGION_SETTINGS).ToString());
            _NumberOfImageLoad = (double)AppSettingsService.GetSetting(AppSettingsService.NUMBER_OF_IMAGE_LOAD_SETTINGS);

            DefaultSave = (bool)AppSettingsService.GetSetting(AppSettingsService.DEFAULT_SAVE_SETTINGS);
            DefaultSaveLocationPath = AppSettingsService.GetSetting(AppSettingsService.DEFAULT_SAVE_LOCATION_SETTINGS).ToString();

            AutoWallpaper = (bool)AppSettingsService.GetSetting(AppSettingsService.AUTO_WALLPAPER_SETTINGS);
            AutoWallpaper_Wifi = (bool)AppSettingsService.GetSetting(AppSettingsService.AUTO_WALLPAPER_WIFI_SETTINGS);
            IsAutoWallpaperTaskLoading = false;

            LiveTile = (bool)AppSettingsService.GetSetting(AppSettingsService.LIVE_TILE_SETTINGS);
            LiveTile_Wifi = (bool)AppSettingsService.GetSetting(AppSettingsService.LIVE_TILE_WIFI_SETTINGS);
            IsLiveTileBackgroundTaskLoading = false;

            AutoSave = (bool)AppSettingsService.GetSetting(AppSettingsService.AUTO_SAVE_SETTINGS);
            AutoSaveLocationPath = AppSettingsService.GetSetting(AppSettingsService.AUTO_SAVE_LOCATION_SETTINGS).ToString();
            AutoSave_Wifi = (bool)AppSettingsService.GetSetting(AppSettingsService.AUTO_SAVE_WIFI_SETTINGS);
            IsAutoSaveBackgroundTaskLoading = false;

            Theme = (int)AppSettingsService.GetSetting(AppSettingsService.THEME_SETTINGS);
        }

        public void DefaultSaveSetup()
        {
            AppSettingsService.SaveSetting(AppSettingsService.DEFAULT_SAVE_SETTINGS, DefaultSave);
        }

        public async void ChangeAutoSaveLocation()
        {
            FolderPicker folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                AppSettingsService.SaveSetting(AppSettingsService.AUTO_SAVE_LOCATION_SETTINGS, folder.Path);
                AutoSaveLocationPath = folder.Path;
            }
        }

        public async void ChangeDefaultSaveLocation()
        {
            FolderPicker folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                AppSettingsService.SaveSetting(AppSettingsService.DEFAULT_SAVE_LOCATION_SETTINGS, folder.Path);
                DefaultSaveLocationPath = folder.Path;
            }
        }

        public async void AutoWallpaperSetup()
        {
            IsAutoWallpaperTaskLoading = true;

            if (AutoWallpaper)
            {
                //AutoWallpaper = await BackgroundTaskService.RegisterBackgroundTaskAsync(
                //    BackgroundTaskService.AUTO_START_BACKGROUND_TASK_NAME,
                //    typeof(Tasks.StartImageBackgroundTask).FullName);
            }
            else
            {
                if (BackgroundTaskService.UnregisterbackgroundTask(BackgroundTaskService.AUTO_START_BACKGROUND_TASK_NAME))
                {
                    AppSettingsService.SaveSetting(AppSettingsService.LAST_AUTO_START_IMAGE, string.Empty);
                }
            }

            AppSettingsService.SaveSetting(AppSettingsService.AUTO_WALLPAPER_SETTINGS, AutoWallpaper);

            IsAutoWallpaperTaskLoading = false;
        }

        public async void LiveTileSetup()
        {
            IsLiveTileBackgroundTaskLoading = true;

            if (LiveTile)
            {
                //LiveTile = await BackgroundTaskService.RegisterBackgroundTaskAsync(
                //    BackgroundTaskService.LIVE_TILE_BACKGROUND_TASK_NAME,
                //    typeof(Tasks.LiveTileBackgroundTask).FullName);
            }
            else
            {
                if (BackgroundTaskService.UnregisterbackgroundTask(BackgroundTaskService.LIVE_TILE_BACKGROUND_TASK_NAME))
                {
                    TileUpdateManager.CreateTileUpdaterForApplication().Clear();
                    AppSettingsService.SaveSetting(AppSettingsService.LAST_LIVE_TILE_IMAGE, string.Empty);
                }
            }

            AppSettingsService.SaveSetting(AppSettingsService.LIVE_TILE_SETTINGS, LiveTile);

            IsLiveTileBackgroundTaskLoading = false;
        }

        public async void AutoSaveSetup()
        {
            IsAutoSaveBackgroundTaskLoading = true;

            if (AutoSave)
            {
                //AutoSave = await BackgroundTaskService.RegisterBackgroundTaskAsync(
                //    BackgroundTaskService.AUTO_SAVE_BACKGROUND_TASK_NAME,
                //    typeof(Tasks.AutoSaveBackgroundTask).FullName);
            }
            else
            {
                if (BackgroundTaskService.UnregisterbackgroundTask(BackgroundTaskService.AUTO_SAVE_BACKGROUND_TASK_NAME))
                {
                    AppSettingsService.SaveSetting(AppSettingsService.LAST_AUTO_SAVE_IMAGE, string.Empty);
                }
            }

            AppSettingsService.SaveSetting(AppSettingsService.AUTO_SAVE_SETTINGS, AutoSave);

            IsAutoSaveBackgroundTaskLoading = false;
        }
    }
}

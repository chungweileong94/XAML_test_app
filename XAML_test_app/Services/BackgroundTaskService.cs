using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.System;
using Windows.UI.Popups;

namespace XAML_test_app.Services
{
    public class BackgroundTaskService
    {
        public const string AUTO_START_BACKGROUND_TASK_NAME = "AutoStartBackChangeBackgroundTask";
        public const string LIVE_TILE_BACKGROUND_TASK_NAME = "LiveTileBackgroundTask";
        public const string AUTO_SAVE_BACKGROUND_TASK_NAME = "AutoSaveBackgroundTask";

        //register background task
        public static async Task<bool> RegisterBackgroundTaskAsync(string taskName, string taskEntryPoint)
        {
            var status = true;
            var taskRegistered = false;

            //check whether the task registered yet
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == taskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            //if the task haven't register
            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder()
                {
                    Name = taskName,
                    TaskEntryPoint = taskEntryPoint
                };
                builder.SetTrigger(new TimeTrigger(60, false));

                var result = await BackgroundExecutionManager.RequestAccessAsync();

                if (result == BackgroundAccessStatus.AllowedSubjectToSystemPolicy
                || result == BackgroundAccessStatus.AlwaysAllowed)
                {
                    builder.Register();
                }
                else if (result == BackgroundAccessStatus.DeniedBySystemPolicy
                    || result == BackgroundAccessStatus.DeniedByUser
                    || result == BackgroundAccessStatus.Unspecified)
                {
                    MessageDialog message = new MessageDialog("You didn't allow this app to run in background.", "Permission Denied");
                    message.Commands.Add(new UICommand("Go to Settings", async (command) =>
                    {
                        await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-backgroundapps"));
                    }));
                    message.Commands.Add(new UICommand("Cancel"));
                    await message.ShowAsync();

                    status = false;
                }
            }

            return status;
        }

        //unregister background task
        public static bool UnregisterbackgroundTask(string taskName)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == taskName)
                {
                    task.Value.Unregister(true);
                    return true;
                }
            }

            return false;
        }
    }
}

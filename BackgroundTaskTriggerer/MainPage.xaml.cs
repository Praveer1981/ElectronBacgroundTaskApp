using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Background;
using System.Threading;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BackgroundTaskTriggerer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            BackgroundExecutionManager.RequestAccessAsync();

            // Register a TimeZoneTrigger background task with name and trigger
            RegisterBackgroundTask("TimeZoneTriggerTest", new SystemTrigger(SystemTriggerType.TimeZoneChange, false));
           // Console.WriteLine("Registration completed for Time Zone change system event.");

            // Register a background TimeTrigger task with name and trigger
            RegisterBackgroundTask("TimerTriggerTest", new TimeTrigger(15, false));
            //Console.WriteLine("Registration completed for Time trigger event. The backgrond task runs every 15 mins");
        }
        /// <summary>
        /// Register a background task with the taskEntryPoint, name and trigger
        /// </summary>
        public static void RegisterBackgroundTask(String triggerName, IBackgroundTrigger trigger)
        {
            // Check if the task is already registered
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == triggerName)
                {
                    // The task is already registered.
                    return;
                }
            }

            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
            builder.Name = triggerName;
            builder.SetTrigger(trigger);
            builder.TaskEntryPoint = "BackgroundTaskApp.BackgroundTask";
            builder.Register();
        }
    }
}

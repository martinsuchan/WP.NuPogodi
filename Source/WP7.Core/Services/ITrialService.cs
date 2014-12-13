using System;
using System.ComponentModel;
using Win8.Core.Helpers;

namespace Win8.Core.Services
{
    /// <summary>
    /// Abstraction of a service returning current business state of the app.
    /// </summary>
    public interface ITrialService : INotifyPropertyChanged
    {
        /// <summary>
        /// Current Trial state of the application
        /// </summary>
        AppTrialState AppState { get; }

        /// <summary>
        /// Simple wrapper of the AppState property returning if app is in full mode or not.
        /// </summary>
        bool IsTrial { get; }

        /// <summary>
        /// Total time available for the Trial version.
        /// </summary>
        TimeSpan TrialTime { get; }

        /// <summary>
        /// Reset the value in AppState after new application start/resuming.
        /// </summary>
        void Reset(AppLaunchType launchType);
    }
}
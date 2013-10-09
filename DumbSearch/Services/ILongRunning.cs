using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DumbSearch.Services
{
    public enum ProcessStatus
    {
        NotStarted
        , Running
        , Paused
        , Stopped
        , Completed
    }
    public class StatusChangedEventArgs : EventArgs
    {
        public ProcessStatus NewStatus { get; set; }
    }
    public interface ILongRunning
    {
        ProcessStatus Status { get; }
        bool IsRunning { get; }
        bool IsCompleted { get; }
        bool IsPaused { get; }

        bool CanRun { get; }
        bool CanPause { get; }
        bool CanStop { get; }
        void Pause();
        void Stop();
        void Resume();
        event EventHandler<StatusChangedEventArgs> StatusChanged;
    }
}

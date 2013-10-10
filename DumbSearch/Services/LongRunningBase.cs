using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DumbSearch.Services
{
    public class LongRunningBase : ILongRunning
    {

        private ProcessStatus _currentStatus = ProcessStatus.NotStarted;
        private bool _actionRequestedByUser { get { return _pauseRequestedByUser || _resumeRequestedByUser || _stopRequestedByUser; } }
        private bool _pauseRequestedByUser = false;
        private bool _stopRequestedByUser = false;
        private bool _resumeRequestedByUser = false;
        private event EventHandler<StatusChangedEventArgs> ILongRunningStatusChangedEvent;

        internal void Start()
        {
            _currentStatus = ProcessStatus.Running;
            notifyStatusChanged();
        }
        internal void Stop()
        {
            _currentStatus = ProcessStatus.Completed;
            notifyStatusChanged();
        }

        internal bool LongRunningCancelled()
        {
            while (_actionRequestedByUser || ILongRunningisPaused)
            {
                processAction();

                if (ILongRunningisPaused)
                    System.Threading.Thread.Sleep(1000);

            }

            return _currentStatus == ProcessStatus.Stopped || _currentStatus == ProcessStatus.Completed;
        }

        private void notifyStatusChanged()
        {
            if (ILongRunningStatusChangedEvent != null)
            {
                ILongRunningStatusChangedEvent(this, new StatusChangedEventArgs() { NewStatus = _currentStatus });
            }
        }

        private void processAction()
        {
            if (_stopRequestedByUser)
            {
                _stopRequestedByUser = false;
                _currentStatus = ProcessStatus.Stopped;
                notifyStatusChanged();
                return;
            }

            if (_resumeRequestedByUser)
            {
                _resumeRequestedByUser = false;
                _currentStatus = ProcessStatus.Running;
                notifyStatusChanged();
                return;
            }

            if (_pauseRequestedByUser)
            {
                _pauseRequestedByUser = false;
                _currentStatus = ProcessStatus.Paused;
                notifyStatusChanged();
                return;
            }
        }

        private ProcessStatus ILongRunningStatus
        {
            get { return _currentStatus; }
        }

        private bool ILongRunningisRunning
        {
            get
            {
                return
                    _currentStatus == ProcessStatus.Running
                    || _currentStatus == ProcessStatus.Paused;
            }
        }
        private bool ILongRunningisPaused
        {
            get
            {
                return
                    _currentStatus == ProcessStatus.Paused;
            }
        }
        private bool ILongRunningisCompleted
        {
            get
            {
                return
                  _currentStatus == ProcessStatus.Completed;
            }
        }

        private bool ILongRunningCanRun
        {
            get
            {
                return _currentStatus != ProcessStatus.Running;
            }
        }

        private bool ILongRunningCanPause
        {
            get
            {
                return _currentStatus == ProcessStatus.Running;
            }
        }
        private bool ILongRunningCanStop
        {
            get
            {
                return _currentStatus == ProcessStatus.Running
                    || _currentStatus == ProcessStatus.Paused;
            }
        }

        private void ILongRunningPause()
        {
            if (_currentStatus == ProcessStatus.Running)
                _pauseRequestedByUser = true;
        }
        private void ILongRunningStop()
        {
            if (_currentStatus == ProcessStatus.Running || _currentStatus == ProcessStatus.Paused)
                _stopRequestedByUser = true;
        }
        private void ILongRunningResume()
        {
            if (_currentStatus == ProcessStatus.Paused)
                _resumeRequestedByUser = true;
        }





        #region ILongRunning Members

        ProcessStatus ILongRunning.Status { get { return ILongRunningStatus; } }

        bool ILongRunning.IsRunning { get { return ILongRunningisRunning; } }
        bool ILongRunning.IsPaused { get { return ILongRunningisPaused; } }
        bool ILongRunning.IsCompleted { get { return ILongRunningisCompleted; } }

        bool ILongRunning.CanRun { get { return ILongRunningCanRun; } }
        bool ILongRunning.CanPause { get { return ILongRunningCanPause; } }
        bool ILongRunning.CanStop { get { return ILongRunningCanStop; } }

        void ILongRunning.Pause() { ILongRunningPause(); }
        void ILongRunning.Stop() { ILongRunningStop(); }
        void ILongRunning.Resume() { ILongRunningResume(); }

        event EventHandler<StatusChangedEventArgs> ILongRunning.StatusChanged
        {
            add { ILongRunningStatusChangedEvent += value; }
            remove { ILongRunningStatusChangedEvent -= value; }
        }
        #endregion

    }
}

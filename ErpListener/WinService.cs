using Common.Logging;
using System;
using Topshelf;
using System.Timers;
using System.Configuration;

namespace ErpConnector.Listener
{
    class WinService
    {
        private Timer _timer = new Timer();
        private DateTime _lastRun;

        public ILog Log { get; private set; }

        public WinService(ILog logger)
        {

            // IocModule.cs needs to be updated in case new paramteres are added to this constructor
            _lastRun = DateTime.Now;
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            Log = logger;

        }

        public bool Start(HostControl hostControl)
        {

            Log.Info($"{nameof(WinService)} Start command received.");
            // Set up a timer to trigger every minute.  
            _timer.Interval = 1 * 60 * 1000; // Check every minute
            _timer.Elapsed += new ElapsedEventHandler(ShouldSync);
            _timer.Start();
            //TODO: Implement your service start routine.
            return true;
        }

        private void ShouldSync(object sender, ElapsedEventArgs e)
        {
            double syncIntervalMs = 0;
            double.TryParse(ConfigurationManager.AppSettings["sync_time_interval_ms"], out syncIntervalMs);
            var timeSinceLastRunMs = DateTime.Now.Subtract(_lastRun).TotalMilliseconds;

            if (timeSinceLastRunMs >= syncIntervalMs)
            {
                _timer.Stop();
                try
                {
                    if (new DbService().Sync() == true)
                    {
                        Log.Info("Data transfer ran successfully");
                    }
                    else
                    {
                        Log.Trace("Data transfer not performed. Already Synced data.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Data transfer was not run. Reason: ", ex);
                }
                _lastRun = DateTime.Now;
                _timer.Start();
            }
        }

        public bool Stop(HostControl hostControl)
        {

            Log.Trace($"{nameof(WinService)} Stop command received.");

            //TODO: Implement your service stop routine.
            return true;

        }

        public bool Pause(HostControl hostControl)
        {

            Log.Trace($"{nameof(WinService)} Pause command received.");

            //TODO: Implement your service start routine.
            return true;

        }

        public bool Continue(HostControl hostControl)
        {

            Log.Trace($"{nameof(WinService)} Continue command received.");

            //TODO: Implement your service stop routine.
            return true;

        }

        public bool Shutdown(HostControl hostControl)
        {

            Log.Trace($"{nameof(WinService)} Shutdown command received.");

            //TODO: Implement your service stop routine.
            return true;

        }

    }
}

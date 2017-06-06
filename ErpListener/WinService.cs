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
        private DateTime _lastRun = DateTime.Now.AddDays(-1);

        public ILog Log { get; private set; }

        public WinService(ILog logger)
        {

            // IocModule.cs needs to be updated in case new paramteres are added to this constructor

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            Log = logger;

        }

        public bool Start(HostControl hostControl)
        {

            Log.Info($"{nameof(WinService)} Start command received.");
            // Set up a timer to trigger every minute.  
            double interval = 0;
            double.TryParse(ConfigurationManager.AppSettings["sync_time_interval_ms"], out interval);
            _timer.Interval = interval == 0 ? 60 * 60 * 1000 : interval; // Default to 60 minutes if nothing in config
            _timer.Elapsed += new ElapsedEventHandler(ShouldSync);
            _timer.Start();
            //TODO: Implement your service start routine.
            return true;
        }

        private void ShouldSync(object sender, ElapsedEventArgs e)
        {
            // If sync hasn't run today
            if (_lastRun.Date < DateTime.Now.Date)
            {
                _timer.Stop();
                var db = new DbService();
                db.Sync();
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

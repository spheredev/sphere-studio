using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Timers;

namespace SphereStudio.Utility
{
    /// <summary>
    /// The event handler used for batching files in the DeferredFileSystemWatcher
    /// </summary>
    /// <param name="sender">Usually the calling object.</param>
    /// <param name="eAll">The items.</param>
    /// <typeparam name="T">The type of the items.</typeparam>
    public delegate void BatchEventHandler<in T>(object sender, IEnumerable<T> eAll);
    
    /// <summary>
    /// A FileSystemWatcher that only triggers after so long of a delay.
    /// </summary>
    public class DeferredFileSystemWatcher : FileSystemWatcher
    {
        private Timer eventTimer;
        private LinkedList<FileSystemEventArgs> changeEvents = new LinkedList<FileSystemEventArgs>();
        private LinkedList<FileSystemEventArgs> createEvents = new LinkedList<FileSystemEventArgs>();
        private LinkedList<FileSystemEventArgs> deleteEvents = new LinkedList<FileSystemEventArgs>();
        private LinkedList<RenamedEventArgs> renameEvents = new LinkedList<RenamedEventArgs>();
        
        /// <summary>
        /// Initializes a new instance of the DeferredFileSystemWatcher.
        /// </summary>
        public DeferredFileSystemWatcher()
        {
            base.Changed += base_Changed;
            base.Created += base_Created;
            base.Deleted += base_Deleted;
            base.Renamed += base_Renamed;
            eventTimer = new Timer { AutoReset = false };
            eventTimer.Elapsed += eventTimer_Elapsed;
            Delay = 250;
        }

        /// <summary>
        /// The component whose thread the event delegates will be invoked on.
        /// </summary>
        public new ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                return base.SynchronizingObject;
            }
            set
            {
                base.SynchronizingObject = value;
                eventTimer.SynchronizingObject = value;
            }
        }

        /// <summary>
        /// Event handler for when a file has been changed.
        /// </summary>
        public new event BatchEventHandler<FileSystemEventArgs> Changed;

        /// <summary>
        /// Event handler for when a file is created.
        /// </summary>
        public new event BatchEventHandler<FileSystemEventArgs> Created;

        /// <summary>
        /// Event handler for when a file has been deleted.
        /// </summary>
        public new event BatchEventHandler<FileSystemEventArgs> Deleted;

        /// <summary>
        /// Event handler for when a file has been renamed.
        /// </summary>
        public new event BatchEventHandler<RenamedEventArgs> Renamed;

        /// <summary>
        /// Gets or sets how much time, in milliseconds, must pass after the last FileSystemWatcher event
        /// before batched event(s) are raised.
        /// </summary>
        public double Delay
        {
            get => eventTimer.Interval;
            set => eventTimer.Interval = value;
        }

        /// <summary>
        /// Overrides the default dispose method to dispose the timer.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                eventTimer.Dispose();
            base.Dispose(disposing);
        }

        private void base_Changed(object sender, FileSystemEventArgs e)
        {
            changeEvents.AddLast(e);
            eventTimer.Stop();
            eventTimer.Start();
        }

        private void base_Created(object sender, FileSystemEventArgs e)
        {
            createEvents.AddLast(e);
            eventTimer.Stop();
            eventTimer.Start();
        }

        private void base_Deleted(object sender, FileSystemEventArgs e)
        {
            deleteEvents.AddLast(e);
            eventTimer.Stop();
            eventTimer.Start();
        }

        private void base_Renamed(object sender, RenamedEventArgs e)
        {
            renameEvents.AddLast(e);
            eventTimer.Stop();
            eventTimer.Start();
        }

        private void eventTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Changed != null && changeEvents.Count > 0) Changed(this, changeEvents);
            if (Created != null && createEvents.Count > 0) Created(this, createEvents);
            if (Deleted != null && deleteEvents.Count > 0) Deleted(this, deleteEvents);
            if (Renamed != null && renameEvents.Count > 0) Renamed(this, renameEvents);

            // making new LinkedLists here is unfortunately necessary; events might be handled on a
            // different thread, so reusing the existing lists wouldn't be threadsafe
            changeEvents = new LinkedList<FileSystemEventArgs>();
            createEvents = new LinkedList<FileSystemEventArgs>();
            deleteEvents = new LinkedList<FileSystemEventArgs>();
            renameEvents = new LinkedList<RenamedEventArgs>();
        }
    }
}

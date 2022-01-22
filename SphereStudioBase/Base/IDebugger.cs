using System;
using System.Threading.Tasks;

namespace SphereStudio.Base
{
    /// <summary>
    /// Specifies the reason debug execution was paused.
    /// </summary>
    public enum PauseReason
    {
        /// <summary>
        /// A breakpoint was triggered.
        /// </summary>
        Breakpoint,

        /// <summary>
        /// An exception is about to be thrown.
        /// </summary>
        Exception,
    }

    /// <summary>
    /// Specifies data for a Paused debugger event.
    /// </summary>
    public class PausedEventArgs
    {
        /// <summary>
        /// Constructs data for a Paused debugger event.
        /// </summary>
        /// <param name="reason">Indicates why execution is being paused.</param>
        public PausedEventArgs(PauseReason reason)
        {
            Reason = reason;
        }

        /// <summary>
        /// Gets the reason execution was paused.
        /// </summary>
        public PauseReason Reason { get; private set; }
    }
    
    /// <summary>
    /// Specifies the interface for a single-step debugger.
    /// </summary>
    public interface IDebugger
    {
        /// <summary>
        /// Gets the fully qualified path of the source file currently being executed.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the line number of the next instruction to be executed.
        /// </summary>
        int LineNumber { get; }

        /// <summary>
        /// Gets a value indicating whether the engine is actively executing code.
        /// </summary>
        bool Running { get; }

        /// <summary>
        /// Fires when the debugger is successfully attached.
        /// </summary>
        event EventHandler Attached;

        /// <summary>
        /// Fires when the debugger is detached.
        /// </summary>
        event EventHandler Detached;

        /// <summary>
        /// Fires when execution pauses (e.g. at a breakpoint).
        /// </summary>
        event EventHandler<PausedEventArgs> Paused;

        /// <summary>
        /// Fires when execution resumes.
        /// </summary>
        event EventHandler Resumed;

        /// <summary>
        /// Attaches the debugger.
        /// </summary>
        /// <returns>A value indicating whether the debugger was successfully attached.</returns>
        Task<bool> Attach();

        /// <summary>
        /// Clears an existing breakpoint on a line of code.
        /// </summary>
        /// <param name="fileName">The name of the source file containing the breakpoint.</param>
        /// <param name="lineNumber">The line number of the breakpoint.</param>
        Task ClearBreakpoint(string fileName, int lineNumber);

        /// <summary>
        /// Detaches the debugger.
        /// </summary>
        Task Detach();

        /// <summary>
        /// Pauses execution and breaks into the debugger.
        /// </summary>
        Task Pause();

        /// <summary>
        /// Runs until the next breakpoint is hit or the target terminates, whichever comes first.
        /// </summary>
        Task Resume();

        /// <summary>
        /// Sets a breakpoint on a line of code.
        /// </summary>
        /// <param name="fileName">The name of the source file containing the breakpoint.</param>
        /// <param name="lineNumber">The line number of the breakpoint.</param>
        Task SetBreakpoint(string fileName, int lineNumber);

        /// <summary>
        /// Executes the next line of code, stepping into function calls.
        /// </summary>
        Task StepInto();

        /// <summary>
        /// Runs until the current function returns.
        /// </summary>
        Task StepOut();

        /// <summary>
        /// Executes the next line of code.
        /// </summary>
        Task StepOver();
    }
}

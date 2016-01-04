using System.Collections.Generic;
using NLog.Common;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace NLog.ManualFlush
{
    /// <summary>
    /// Wrapper target for manual controlling of flush behavior.
    /// </summary>
	[Target("ManualFlush")]
	public class ManualFlushWrapper : WrapperTargetBase
	{
		private readonly IList<AsyncLogEventInfo> logs = new List<AsyncLogEventInfo>();

        /// <summary>
        /// Write the event.
        /// </summary>
        /// <param name="logEvent">Event to be written.</param>
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			logs.Add(logEvent);
		}

        /// <summary>
        /// Flush and clear events.
        /// </summary>
        /// <param name="asyncContinuation"></param>
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			foreach (var log in logs)
			{
				WrappedTarget.WriteAsyncLogEvent(log);
			}

			logs.Clear();
			base.FlushAsync(asyncContinuation);
		}

        /// <summary>
        /// Clear the current events.
        /// </summary>
		public void EmptyLogs()
		{
			logs.Clear();
		}
	}
}

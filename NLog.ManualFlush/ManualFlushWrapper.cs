using System.Collections.Generic;
using NLog.Common;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace NLog.ManualFlush
{
	[Target("ManualFlush")]
	public class ManualFlushWrapper : WrapperTargetBase
	{
		private readonly IList<AsyncLogEventInfo> logs = new List<AsyncLogEventInfo>();

		protected override void Write(AsyncLogEventInfo logEvent)
		{
			logs.Add(logEvent);
		}

		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			foreach (var log in logs)
			{
				WrappedTarget.WriteAsyncLogEvent(log);
			}

			logs.Clear();
			base.FlushAsync(asyncContinuation);
		}

		public void EmptyLogs()
		{
			logs.Clear();
		}
	}
}

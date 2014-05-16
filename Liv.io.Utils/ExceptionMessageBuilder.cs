using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Liv.io.Utils
{
	public class ExceptionMessageBuilder
	{
		public void BuildDetailedExceptionText (Exception ex, TextWriter message, bool isInner)
		{
			// header
			if (!isInner) {
				message.WriteLine ("Exception");
				message.WriteLine ("====================================================");
				message.WriteLine ("EXCEPTION ({0}.{1})", ex.GetType ().Namespace, ex.GetType ().Name);
			} else {
				message.WriteLine ("----------------------------------------------------");
				message.WriteLine ("INNER EXCEPTION ({0}.{1}", ex.GetType ().Namespace, ex.GetType ().Name);
			}
			// main message
			message.WriteLine ("Message: {0}", ex.Message);

			// exception target
			if (null != ex.TargetSite) {
				MethodBase target = ex.TargetSite;
				message.WriteLine ("Target: {0}.{1}.{2}",
				                  target.DeclaringType.Namespace,
				                  target.DeclaringType.Name,
				                  target.Name);
			}

			// stack trace
			message.WriteLine ("Stack Trace:");
			StackTrace stackTrace = new StackTrace (ex, true);
			for (int i = 0; i < stackTrace.FrameCount; i++) {
				StackFrame frame = stackTrace.GetFrame (i);
				MethodBase method = frame.GetMethod ();
				Type type = method.DeclaringType;
				message.WriteLine ("[{0}] {1}.{2}.{3} ({4} line: {5})",
				                  i,
				                  type.Namespace,
				                  type.Name,
				                  method.Name,
				                  frame.GetFileName () ?? "Unknown File",
				                  frame.GetFileLineNumber ());
			}

			// inner exception
			if (ex.InnerException != null)
				BuildDetailedExceptionText (ex.InnerException, message, true);
			// footer
			if (!isInner)
				message.WriteLine ("====================================================");
		}
	}
}
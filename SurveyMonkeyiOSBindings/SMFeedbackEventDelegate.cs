using System;
using Foundation;

namespace SurveyMonkeyiOSBindings
{
	public class SMFeedbackEventArgs : EventArgs
	{
		public SMRespondent Respondent { get; set; }
		public NSError Error { get; set; }
	}

	public class SMFeedbackEventDelegate : SMFeedbackDelegate
	{

		public event EventHandler<SMFeedbackEventArgs> OnError;

		public SMFeedbackEventDelegate ()
		{
		}

		public override void Error (SMRespondent respondent, NSError error)
		{
			OnError?.Invoke (this, new SMFeedbackEventArgs { Respondent = respondent, Error = error });
		}
	}
}

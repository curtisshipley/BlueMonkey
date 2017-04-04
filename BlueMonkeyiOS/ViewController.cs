using System;
using Foundation;
using SurveyMonkeyiOSBindings;
using UIKit;

namespace BlueMonkeyiOS
{
	public partial class ViewController : UIViewController
	{
		public const string SM_RESPONDENT = "smRespondent";
		public const string SM_ERROR = "smError";
		public const string RESPONSES = "responses";
		public const string QUESTION_ID = "question_id";
		public const string FEEDBACK_QUESTION_ID = "813797519";
		public const string ANSWERS = "answers";
		public const string ROW_ID = "row_id";
		public const string FEEDBACK_FIVE_STARS_ROW_ID = "9082377273";
		public const string FEEDBACK_POSITIVE_ROW_ID_2 = "9082377274";
		public const string SAMPLE_APP = "Sample App";
		public const string SURVEY_HASH = "LBQK27G";

		protected SMFeedbackViewController feedbackController;
		protected SMFeedbackEventDelegate feedbackEventDelegate;

		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			feedbackController = new SMFeedbackViewController (SURVEY_HASH);

			feedbackEventDelegate = new SMFeedbackEventDelegate ();

			feedbackController.Delegate = feedbackEventDelegate;

			feedbackEventDelegate.OnError += FeedbackEventDelegate_OnFeedback;

			SurveyButton.TouchUpInside += (sender, e) => {

				PresentViewController (feedbackController, true, null);

			};

		}

		void FeedbackEventDelegate_OnFeedback (object sender, SMFeedbackEventArgs e)
		{
			SMRespondent respondent = e.Respondent;
			NSError error = e.Error;

			if (respondent != null) {
				SMQuestionResponse questionResponse = respondent.QuestionResponses [0] as SMQuestionResponse;
				string questionID = questionResponse.QuestionID;
				if (questionID.Equals (FEEDBACK_QUESTION_ID)) {
					SMAnswerResponse answerResponse = questionResponse.Answers [0] as SMAnswerResponse;
					string rowID = answerResponse.RowID;
					if (rowID.Equals (FEEDBACK_FIVE_STARS_ROW_ID) || rowID.Equals (FEEDBACK_FIVE_STARS_ROW_ID)) {
						new UIAlertView ("Rate!", "Please rate us in the app store!", null, "OK").Show ();
					} else {
						new UIAlertView ("Oops!", "We'll address the issues you encountered as quickly as possible!", null, "OK").Show ();
					}
				}
			} else {
				// handle error
			}
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

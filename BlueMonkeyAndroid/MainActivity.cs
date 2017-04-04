using Android.App;
using Android.Widget;
using Android.OS;
using Com.Surveymonkey.Surveymonkeyandroidsdk;
using Com.Surveymonkey.Surveymonkeyandroidsdk.Utils;

using Org.Json;
using Android.Util;

namespace BlueMonkeyAndroid
{
	[Activity (Label = "BlueMonkey Android", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		public const int SM_REQUEST_CODE = 0;
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


		protected SurveyMonkey monkey = new SurveyMonkey ();

		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += Button_Click;

			monkey.OnStart (this, SAMPLE_APP, SM_REQUEST_CODE, SURVEY_HASH);
		}

		void Button_Click (object sender, System.EventArgs e)
		{
			monkey.StartSMFeedbackActivityForResult (this, SM_REQUEST_CODE, SURVEY_HASH);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Android.Content.Intent intent)
		{
			base.OnActivityResult (requestCode, resultCode, intent);


			//In this example, we deserialize the user's response, check to see if they gave our app 4 or 5 stars, and then provide visual prompts to the user based on their response
			if (resultCode == Result.Ok) {
				bool isPromoter = false;
				try {
					string respondent = intent.GetStringExtra (SM_RESPONDENT);
					Log.Debug ("SM", respondent);
					JSONObject surveyResponse = new JSONObject (respondent);
					JSONArray responsesList = surveyResponse.GetJSONArray (RESPONSES);
					JSONObject response;
					JSONArray answers;
					JSONObject currentAnswer;
					for (int i = 0; i < responsesList.Length (); i++) {
						response = responsesList.GetJSONObject (i);
						if (response.GetString (QUESTION_ID).Equals (FEEDBACK_QUESTION_ID)) {
							answers = response.GetJSONArray (ANSWERS);
							for (int j = 0; j < answers.Length (); j++) {
								currentAnswer = answers.GetJSONObject (j);
								if (currentAnswer.GetString (ROW_ID).Equals (FEEDBACK_FIVE_STARS_ROW_ID) || currentAnswer.GetString (ROW_ID).Equals (FEEDBACK_POSITIVE_ROW_ID_2)) {
									isPromoter = true;
									break;
								}
							}
							if (isPromoter) {
								break;
							}
						}
					}
				} catch (JSONException e) {
					Log.GetStackTraceString (e);
				}
				if (isPromoter) {
					Toast t = Toast.MakeText (this, GetString (Resource.String.promoter_prompt), ToastLength.Long);
					t.Show ();
				} else {
					Toast t = Toast.MakeText (this, GetString (Resource.String.detractor_prompt), ToastLength.Long);
					t.Show ();
				}
			} else {
				Toast t = Toast.MakeText (this, GetString (Resource.String.error_prompt), ToastLength.Long);
				t.Show ();
				SMError e = (SMError)intent.GetSerializableExtra (SM_ERROR);
				Log.Debug ("SM-ERROR", e.Description );
			}
		}


	}
}


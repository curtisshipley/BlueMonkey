# BlueMonkey
Xamarin iOS and Android Binding Project for Survey Monkey iOS/Android SDKs

This project has binding projects for both iOS and Anroid.


## Android Survey Monkey Binding Project - SurveyMonkeyAndroidBindings

To use, add the android binding project to your solution and add as a dependent project to your Android Xamarin App. The usage is pretty much the same as the Survey Monkey Android SDK. The code follows what the Android Survey Monkey example does.


### Initialization

1. Create the SurveyMonkey class.
2. Call OnStart()

~~~
public const int SM_REQUEST_CODE = 0;
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
~~~

### Invoking the Survey

To invoke the survey screen, simply call ```StartSMFeedbackActivityForResult```.

~~~
void Button_Click (object sender, System.EventArgs e)
{
    monkey.StartSMFeedbackActivityForResult (this, SM_REQUEST_CODE, SURVEY_HASH);
}
~~~

### Consuming the Results

Survey Monkey calls the Activity's ```OnActivityResult``` method with the results of the survey. The results are returned in JSON. 

~~~
public const string SM_RESPONDENT = "smRespondent";


protected override void OnActivityResult (
    int requestCode, Result resultCode, Android.Content.Intent intent)
{
    base.OnActivityResult (requestCode, resultCode, intent);

    if (resultCode == Result.Ok) {
        bool isPromoter = false;
        try {
            // get json string
            string respondent = intent.GetStringExtra (SM_RESPONDENT);

            // do something with the json string

        }
    }
}
~~~

## Android Survey Monkey Binding Project - SurveyMonkeyAndroidBindings

To use, add the iOS binding project to your solution and add as a dependent project to your iOS Xamarin App. The usage is pretty much the same as the Survey Monkey iOS SDK. The code follows what the iOS Survey Monkey example does.


### Initialization

1. Create the ```SMFeedbackViewController``` class.
2. Create the ```SMFeedbackEventDelegate``` class.
3. Hook the OnError event handler for the result. OnError is the default name that Objective Sharpie created, but it is really where the result comes in.
4. Invoke the survey by presenting the view controller.

~~~
public const string SURVEY_HASH = "LBQK27G";

public override void ViewDidLoad ()
{
    base.ViewDidLoad ();
    // Perform any additional setup after loading the view, typically from a nib.
    feedbackController = new SMFeedbackViewController (SURVEY_HASH);

    feedbackEventDelegate = new SMFeedbackEventDelegate ();

    feedbackController.Delegate = feedbackEventDelegate;

    feedbackEventDelegate.OnError += FeedbackEventDelegate_OnFeedback;

    SurveyButton.TouchUpInside += (sender, e) => {

        // Present the view controller
        PresentViewController (feedbackController, true, null);

    };

}
~~~


### Consuming the Results

Survey Monkey will invoke the event handler with the results. Unlike Android, the results are pre-digested into an object hierarchy.

~~~
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
~~~

## Next Steps

Here are some things to be done. I will get around to this, or send a pull request. Also, post issues with questions.

1. Unify the response object hierachy objects. Right now it is json in android and objects iOS.
2. Create a Xamarin Forms wrapper for these.
3. Nuget package.

Thoughts, questions, comments -- post an issue or send an email to curtis@saltydogtechnology.com




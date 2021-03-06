﻿using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using SurveyMonkeyiOSBindings;

namespace SurveyMonkeyiOSBindings
{
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. VisualStudio auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     CGPoint Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_types
	//


	// @protocol SMJSONSerializableProtocol <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	public interface ISMJSONSerializableProtocol
	{
		// @required -(void)readFromJsonDictionary:(NSDictionary *)dictionary;
		[Abstract]
		[Export("readFromJsonDictionary:")]
		void ReadFromJsonDictionary(NSDictionary dictionary);

		// @optional -(NSDictionary *)toJson;
		[Export("toJson")]
		NSDictionary ToJson { get; }
	}

	// @interface SMRespondent : NSObject <SMJSONSerializableProtocol>
	[BaseType(typeof(NSObject))]
	public interface SMRespondent : ISMJSONSerializableProtocol
	{
		// @property (assign, nonatomic) SMCompletionStatus completionStatus;
		[Export("completionStatus", ArgumentSemantic.Assign)]
		SMCompletionStatus CompletionStatus { get; set; }

		// @property (nonatomic, strong) NSDate * dateModified;
		[Export("dateModified", ArgumentSemantic.Strong)]
		NSDate DateModified { get; set; }

		// @property (nonatomic, strong) NSDate * dateStarted;
		[Export("dateStarted", ArgumentSemantic.Strong)]
		NSDate DateStarted { get; set; }

		// @property (nonatomic, strong) NSString * respondentID;
		[Export("respondentID", ArgumentSemantic.Strong)]
		string RespondentID { get; set; }

		// @property (nonatomic, strong) NSArray * questionResponses;
		[Export("questionResponses", ArgumentSemantic.Strong)]
		NSObject[] QuestionResponses { get; set; }
	}

	// @protocol SMFeedbackDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface SMFeedbackDelegate
	{
		// @optional -(void)respondentDidEndSurvey:(SMRespondent *)respondent error:(NSError *)error;
		[Export("respondentDidEndSurvey:error:")]
		void Error(SMRespondent respondent, NSError error);
	}

	// @interface SMFeedbackViewController : UIViewController
	[BaseType(typeof(UIViewController))]
	interface SMFeedbackViewController
	{
		// -(id)initWithSurvey:(NSString *)collectorHash;
		[Export("initWithSurvey:")]
		IntPtr Constructor(string collectorHash);

		// -(id)initWithSurvey:(NSString *)collectorHash andCustomVariables:(NSDictionary *)customVariables;
		[Export("initWithSurvey:andCustomVariables:")]
		IntPtr Constructor(string collectorHash, NSDictionary customVariables);

		[Wrap("WeakDelegate")]
		SMFeedbackDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<SMFeedbackDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, strong) UIColor * cancelButtonTintColor;
		[Export("cancelButtonTintColor", ArgumentSemantic.Strong)]
		UIColor CancelButtonTintColor { get; set; }

		// -(void)scheduleInterceptFromViewController:(UIViewController *)viewController withAppTitle:(NSString *)appTitle;
		[Export("scheduleInterceptFromViewController:withAppTitle:")]
		void ScheduleInterceptFromViewController(UIViewController viewController, string appTitle);

		// -(void)scheduleInterceptFromViewController:(UIViewController *)viewController alertTitle:(NSString *)alertTitle alertBody:(NSString *)alertBody positiveActionTitle:(NSString *)positiveActionTitle cancelTitle:(NSString *)cancelTitle afterInstallInterval:(double)afterInstallInterval afterAcceptInterval:(double)afterAcceptInterval afterDeclineInterval:(double)afterDeclineInterval;
		[Export("scheduleInterceptFromViewController:alertTitle:alertBody:positiveActionTitle:cancelTitle:afterInstallInterval:afterAcceptInterval:afterDeclineInterval:")]
		void ScheduleInterceptFromViewController(UIViewController viewController, string alertTitle, string alertBody, string positiveActionTitle, string cancelTitle, double afterInstallInterval, double afterAcceptInterval, double afterDeclineInterval);

		// -(void)presentFromViewController:(UIViewController *)viewController animated:(BOOL)flag completion:(void (^)(void))completion;
		[Export("presentFromViewController:animated:completion:")]
		void PresentFromViewController(UIViewController viewController, bool flag, Action completion);
	}

	// @interface SMAnswerResponse : NSObject <SMJSONSerializableProtocol>
	[BaseType(typeof(NSObject))]
	interface SMAnswerResponse : ISMJSONSerializableProtocol
	{
		// @property (nonatomic, strong) NSString * textResponse;
		[Export("textResponse", ArgumentSemantic.Strong)]
		string TextResponse { get; set; }

		// @property (nonatomic, strong) NSString * rowID;
		[Export("rowID", ArgumentSemantic.Strong)]
		string RowID { get; set; }

		// @property (assign, nonatomic) NSUInteger rowIndex;
		[Export("rowIndex")]
		nuint RowIndex { get; set; }

		// @property (nonatomic, strong) NSString * rowValue;
		[Export("rowValue", ArgumentSemantic.Strong)]
		string RowValue { get; set; }

		// @property (nonatomic, strong) NSString * columnID;
		[Export("columnID", ArgumentSemantic.Strong)]
		string ColumnID { get; set; }

		// @property (assign, nonatomic) NSUInteger columnIndex;
		[Export("columnIndex")]
		nuint ColumnIndex { get; set; }

		// @property (nonatomic, strong) NSString * columnValue;
		[Export("columnValue", ArgumentSemantic.Strong)]
		string ColumnValue { get; set; }

		// @property (nonatomic, strong) NSString * columnDropdownID;
		[Export("columnDropdownID", ArgumentSemantic.Strong)]
		string ColumnDropdownID { get; set; }

		// @property (assign, nonatomic) NSUInteger columnDropdownIndex;
		[Export("columnDropdownIndex")]
		nuint ColumnDropdownIndex { get; set; }

		// @property (nonatomic, strong) NSString * columnDropdownValue;
		[Export("columnDropdownValue", ArgumentSemantic.Strong)]
		string ColumnDropdownValue { get; set; }
	}

	// @interface SMQuestionResponse : NSObject <SMJSONSerializableProtocol>
	[BaseType(typeof(NSObject))]
	interface SMQuestionResponse : ISMJSONSerializableProtocol
	{
		// @property (nonatomic, strong) NSString * pageID;
		[Export("pageID", ArgumentSemantic.Strong)]
		string PageID { get; set; }

		// @property (assign, nonatomic) NSUInteger pageIndex;
		[Export("pageIndex")]
		nuint PageIndex { get; set; }

		// @property (nonatomic, strong) NSString * questionID;
		[Export("questionID", ArgumentSemantic.Strong)]
		string QuestionID { get; set; }

		// @property (assign, nonatomic) NSUInteger questionSurveyIndex;
		[Export("questionSurveyIndex")]
		nuint QuestionSurveyIndex { get; set; }

		// @property (assign, nonatomic) NSUInteger questionPageIndex;
		[Export("questionPageIndex")]
		nuint QuestionPageIndex { get; set; }

		// @property (nonatomic, strong) NSString * questionValue;
		[Export("questionValue", ArgumentSemantic.Strong)]
		string QuestionValue { get; set; }

		// @property (nonatomic, strong) NSArray * answers;
		[Export("answers", ArgumentSemantic.Strong)]
		NSObject[] Answers { get; set; }
	}

	// @interface SurveyMonkeyiOSSDK : NSObject
	[BaseType(typeof(NSObject))]
	interface SurveyMonkeyiOSSDK
	{
	}
}

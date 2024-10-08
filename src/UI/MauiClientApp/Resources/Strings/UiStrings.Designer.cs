﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MauiClientApp.Resources.Strings {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class UiStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UiStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MauiClientApp.Resources.Strings.UiStrings", typeof(UiStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please try again..
        /// </summary>
        internal static string AppCommand_Restart {
            get {
                return ResourceManager.GetString("AppCommand_Restart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to My capabilities are only limited to reading or writing email messages..
        /// </summary>
        internal static string AppInfo_Capabilities {
            get {
                return ResourceManager.GetString("AppInfo_Capabilities", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Something went wrong, let&apos;s try again.
        /// </summary>
        internal static string AppInfo_GenericError {
            get {
                return ResourceManager.GetString("AppInfo_GenericError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hi there! I’m Umbrella, your friendly voice-operated email assistant..
        /// </summary>
        internal static string AppInfo_Introduction {
            get {
                return ResourceManager.GetString("AppInfo_Introduction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to How can I help you today?.
        /// </summary>
        internal static string AppQuery_Generic {
            get {
                return ResourceManager.GetString("AppQuery_Generic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current operation terminated..
        /// </summary>
        internal static string AppResponse_Cancel {
            get {
                return ResourceManager.GetString("AppResponse_Cancel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You chose not to send this email. I&apos;ll discard the draft and proceed..
        /// </summary>
        internal static string DraftInfo_EmailNotSend {
            get {
                return ResourceManager.GetString("DraftInfo_EmailNotSend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thank you! Your message to {0}, subject: {1}, reads, {2}.
        /// </summary>
        internal static string DraftInfo_EmailSummary {
            get {
                return ResourceManager.GetString("DraftInfo_EmailSummary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I&apos;ll guide you through writing and sending your email. Let&apos;s get started!.
        /// </summary>
        internal static string DraftInfo_Instructions {
            get {
                return ResourceManager.GetString("DraftInfo_Instructions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to the email drafting page!.
        /// </summary>
        internal static string DraftInfo_Introduction {
            get {
                return ResourceManager.GetString("DraftInfo_Introduction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You said: {0}. Is that correct?.
        /// </summary>
        internal static string DraftQuery_Confirmation {
            get {
                return ResourceManager.GetString("DraftQuery_Confirmation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Now, let&apos;s compose the body of your email. Please dictate your message clearly..
        /// </summary>
        internal static string DraftQuery_EmailBody {
            get {
                return ResourceManager.GetString("DraftQuery_EmailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to First, who would you like to send this email to? Please say the recipient&apos;s email address or contact name..
        /// </summary>
        internal static string DraftQuery_EmailRecipient {
            get {
                return ResourceManager.GetString("DraftQuery_EmailRecipient", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Got it. Next, what is the subject of your email? Please state the subject line..
        /// </summary>
        internal static string DraftQuery_EmailSubject {
            get {
                return ResourceManager.GetString("DraftQuery_EmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your email is ready; should I send it now?.
        /// </summary>
        internal static string DraftQuery_SendEmail {
            get {
                return ResourceManager.GetString("DraftQuery_SendEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ok, please dictate the email body text again..
        /// </summary>
        internal static string DraftResponse_EmailBody_Reject {
            get {
                return ResourceManager.GetString("DraftResponse_EmailBody_Reject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is an invalid email address and is not included in your contacts, please try again.
        /// </summary>
        internal static string DraftResponse_EmailRecipient_Invalid {
            get {
                return ResourceManager.GetString("DraftResponse_EmailRecipient_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ok, please try saying the email address again..
        /// </summary>
        internal static string DraftResponse_EmailRecipient_Reject {
            get {
                return ResourceManager.GetString("DraftResponse_EmailRecipient_Reject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ok, please dictate the subject line again..
        /// </summary>
        internal static string DraftResponse_EmailSubject_Reject {
            get {
                return ResourceManager.GetString("DraftResponse_EmailSubject_Reject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your email message to {0} has been sent!.
        /// </summary>
        internal static string DraftResponse_SendEmail {
            get {
                return ResourceManager.GetString("DraftResponse_SendEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email didn&apos;t go through. We&apos;re canceling the operation. Feel free to try again later..
        /// </summary>
        internal static string DraftResponse_SendEmail_Failed {
            get {
                return ResourceManager.GetString("DraftResponse_SendEmail_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message {0} is from {1}, subject {2}.
        /// </summary>
        internal static string InboxInfo_EmailSummary {
            get {
                return ResourceManager.GetString("InboxInfo_EmailSummary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want me to open this email?.
        /// </summary>
        internal static string InboxQuery_OpenEmail {
            get {
                return ResourceManager.GetString("InboxQuery_OpenEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certainly! You currently have {0} new messages..
        /// </summary>
        internal static string InputReponse_ReadEmails {
            get {
                return ResourceManager.GetString("InputReponse_ReadEmails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Got it! Let&apos;s head over to the email drafting screen and begin.
        /// </summary>
        internal static string InputReponse_WriteEmail {
            get {
                return ResourceManager.GetString("InputReponse_WriteEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hmm, I didn&apos;t quite get that. Could you repeat it for me?.
        /// </summary>
        internal static string InputResponse_Invalid {
            get {
                return ResourceManager.GetString("InputResponse_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Great! Let&apos;s move forward and review the email in detail..
        /// </summary>
        internal static string InputResponse_OpenEmail {
            get {
                return ResourceManager.GetString("InputResponse_OpenEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I&apos;m afraid I can&apos;t perform that task at the moment..
        /// </summary>
        internal static string InputResponse_Undefined {
            get {
                return ResourceManager.GetString("InputResponse_Undefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This message was sent by {0} at {1}.
        /// </summary>
        internal static string ReadingInfo_EmailSummary {
            get {
                return ResourceManager.GetString("ReadingInfo_EmailSummary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to the email reading page.
        /// </summary>
        internal static string ReadingInfo_Introduction {
            get {
                return ResourceManager.GetString("ReadingInfo_Introduction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email subject line: {0}.
        /// </summary>
        internal static string ReadingInfo_Subject {
            get {
                return ResourceManager.GetString("ReadingInfo_Subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Should I proceed reading this message?.
        /// </summary>
        internal static string ReadingQuery_ReadEmail {
            get {
                return ResourceManager.GetString("ReadingQuery_ReadEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want me to read it again?.
        /// </summary>
        internal static string ReadingQuery_RepeatRead {
            get {
                return ResourceManager.GetString("ReadingQuery_RepeatRead", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want to reply to this message?.
        /// </summary>
        internal static string ReadingQuery_Reply {
            get {
                return ResourceManager.GetString("ReadingQuery_Reply", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Absolutely, the email reads as follows.
        /// </summary>
        internal static string ReadingReponse_ReadEmail {
            get {
                return ResourceManager.GetString("ReadingReponse_ReadEmail", resourceCulture);
            }
        }
    }
}

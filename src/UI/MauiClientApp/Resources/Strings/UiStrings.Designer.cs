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
        ///   Looks up a localized string similar to Message {0} is from {1}, subject {2}.
        /// </summary>
        internal static string InboxInfo_EmailSummarry {
            get {
                return ResourceManager.GetString("InboxInfo_EmailSummarry", resourceCulture);
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
    }
}

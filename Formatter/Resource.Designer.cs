﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Formatter {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Formatter.Resource", typeof(Resource).Assembly);
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
        ///   Looks up a localized string similar to Output is not specified.
        /// </summary>
        internal static string FileNotSpecifiedMessage {
            get {
                return ResourceManager.GetString("FileNotSpecifiedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to : file or file directory not found.
        /// </summary>
        internal static string FilePathNotFoundMessage {
            get {
                return ResourceManager.GetString("FilePathNotFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to format is not available.
        /// </summary>
        internal static string FormatNotAvailableMessage {
            get {
                return ResourceManager.GetString("FormatNotAvailableMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Format is not specified.
        /// </summary>
        internal static string FormatNotSpecifiedMessage {
            get {
                return ResourceManager.GetString("FormatNotSpecifiedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Available formats:.
        /// </summary>
        internal static string FormatsHeader {
            get {
                return ResourceManager.GetString("FormatsHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Traced info succesfully saved.
        /// </summary>
        internal static string SuccessfullyTracedMessage {
            get {
                return ResourceManager.GetString("SuccessfullyTracedMessage", resourceCulture);
            }
        }
    }
}

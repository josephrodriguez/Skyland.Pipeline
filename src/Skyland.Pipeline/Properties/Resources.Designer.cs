﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Skyland.Pipeline.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Skyland.Pipeline.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to There is a JobComponent instance registered. Only one instance of this component can be registered..
        /// </summary>
        internal static string JobComponent_Registered_Error {
            get {
                return ResourceManager.GetString("JobComponent_Registered_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The output of last registered component missmatch with the input of current component..
        /// </summary>
        internal static string Missmatch_LastRegisteredComponent_Error {
            get {
                return ResourceManager.GetString("Missmatch_LastRegisteredComponent_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type input of registered stage missmatch with the input type of pipeline..
        /// </summary>
        internal static string Missmatch_TypeInput_Error {
            get {
                return ResourceManager.GetString("Missmatch_TypeInput_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type output of last registered stage missmatch with the type output of pipeline..
        /// </summary>
        internal static string Missmatch_TypeOutput_Error {
            get {
                return ResourceManager.GetString("Missmatch_TypeOutput_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is not job component registered instance for this stage..
        /// </summary>
        internal static string NoJob_Registered_Error {
            get {
                return ResourceManager.GetString("NoJob_Registered_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current pipeline must have at least one registered component..
        /// </summary>
        internal static string Pipeline_WithoutRegisteredComponent_Error {
            get {
                return ResourceManager.GetString("Pipeline_WithoutRegisteredComponent_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The filters must be registered before register the JobComponent instance..
        /// </summary>
        internal static string Register_Filter_Error {
            get {
                return ResourceManager.GetString("Register_Filter_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A job component instance must be registered before register a handler component..
        /// </summary>
        internal static string Register_Handler_Error {
            get {
                return ResourceManager.GetString("Register_Handler_Error", resourceCulture);
            }
        }
    }
}

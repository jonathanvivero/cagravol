﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGE.Cagravol.Presentation.Resources.Workflows {
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
    public class WorkflowStateResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WorkflowStateResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SGE.Cagravol.Presentation.Resources.Workflows.WorkflowStateResources", typeof(WorkflowStateResources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Trasladado a estado {0} por el sistema automáticamente..
        /// </summary>
        public static string AutomaticallyChangedToState_Format {
            get {
                return ResourceManager.GetString("AutomaticallyChangedToState_Format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No es posible cambiar de estado automaticamente, debido a que existen varias posibilidades..
        /// </summary>
        public static string CannotAutoMoveAheadWithSeveralTransitions {
            get {
                return ResourceManager.GetString("CannotAutoMoveAheadWithSeveralTransitions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Archivado.
        /// </summary>
        public static string CUSTOMER_FILE_FILED {
            get {
                return ResourceManager.GetString("CUSTOMER_FILE_FILED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to En Revisión.
        /// </summary>
        public static string CUSTOMER_FILE_IN_REVISION {
            get {
                return ResourceManager.GetString("CUSTOMER_FILE_IN_REVISION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to En Pre-Producción.
        /// </summary>
        public static string CUSTOMER_FILE_PRE_PRODUCTION {
            get {
                return ResourceManager.GetString("CUSTOMER_FILE_PRE_PRODUCTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to En Producción.
        /// </summary>
        public static string CUSTOMER_FILE_READY_PRODUCTION {
            get {
                return ResourceManager.GetString("CUSTOMER_FILE_READY_PRODUCTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rechazado.
        /// </summary>
        public static string CUSTOMER_FILE_REVISION_REJECTED {
            get {
                return ResourceManager.GetString("CUSTOMER_FILE_REVISION_REJECTED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Recibido con Éxito.
        /// </summary>
        public static string CUSTOMER_FILE_UPLOADED {
            get {
                return ResourceManager.GetString("CUSTOMER_FILE_UPLOADED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enviado Por Expositor.
        /// </summary>
        public static string CUSTOMER_INITIAL_SENT {
            get {
                return ResourceManager.GetString("CUSTOMER_INITIAL_SENT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Envío Fallido.
        /// </summary>
        public static string CUSTOMER_SENT_FAILED {
            get {
                return ResourceManager.GetString("CUSTOMER_SENT_FAILED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Estado inicial: {0}.
        /// </summary>
        public static string InitialStateForFileUploaded_Format {
            get {
                return ResourceManager.GetString("InitialStateForFileUploaded_Format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nuevo estado: {0}.
        /// </summary>
        public static string NewStateForItem_Format {
            get {
                return ResourceManager.GetString("NewStateForItem_Format", resourceCulture);
            }
        }
    }
}

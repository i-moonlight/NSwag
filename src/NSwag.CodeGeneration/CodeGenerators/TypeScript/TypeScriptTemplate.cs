//-----------------------------------------------------------------------
// <copyright file="TypeScriptAsyncType.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

namespace NSwag.CodeGeneration.CodeGenerators.TypeScript
{
    /// <summary>The TypeScript output templates.</summary>
    public enum TypeScriptTemplate
    {
        /// <summary>Uses JQuery with callbacks.</summary>
        JQueryCallbacks,

        /// <summary>Uses JQuery with Promises/A+.</summary>
        JQueryPromises,

        /// <summary>Uses $http from Angular 1.x.</summary>
        Angular,

        /// <summary>Uses the http service from Angular 2.x.</summary>
        Angular2
    }
}
﻿//-----------------------------------------------------------------------
// <copyright file="MainWindowModel.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.Commands;

namespace NSwagStudio.ViewModels.CodeGenerators
{
    public class SwaggerToCSharpClientGeneratorViewModel : ViewModelBase
    {
        private string _clientCode;
        private SwaggerToCSharpClientCommand _command = new SwaggerToCSharpClientCommand();

        /// <summary>Gets the settings.</summary>
        public SwaggerToCSharpClientCommand Command
        {
            get { return _command; }
            set
            {
                if (Set(ref _command, value))
                    RaiseAllPropertiesChanged();
            }
        }

        /// <summary>Gets the list of operation modes. </summary>
        public OperationGenerationMode[] OperationGenerationModes
        {
            get
            {
                return Enum.GetNames(typeof(OperationGenerationMode))
                    .Select(t => (OperationGenerationMode)Enum.Parse(typeof(OperationGenerationMode), t))
                    .ToArray();
            }
        }

        /// <summary>Gets the list of class styles. </summary>
        public CSharpClassStyle[] ClassStyles
        {
            get
            {
                return Enum.GetNames(typeof(CSharpClassStyle))
                    .Select(t => (CSharpClassStyle)Enum.Parse(typeof(CSharpClassStyle), t))
                    .ToArray();
            }
        }

        /// <summary>Gets or sets the namespace usages (comma separated). </summary>
        public string AdditionalNamespaceUsages
        {
            get
            {
                return Command.AdditionalNamespaceUsages != null ? string.Join(",", Command.AdditionalNamespaceUsages) : "";
            }
            set
            {
                if (value != null)
                    Command.AdditionalNamespaceUsages = value.Split(',').Select(n => n.Trim()).Where(n => !string.IsNullOrEmpty(n)).ToArray();
                else
                    Command.AdditionalNamespaceUsages = new string[] { };
                RaisePropertyChanged(() => AdditionalNamespaceUsages);
            }
        }

        /// <summary>Gets or sets the excluded type names (must be defined in an import or other namespace).</summary>
        public string ExcludedTypeNames
        {
            get
            {
                return Command.ExcludedTypeNames != null ? string.Join(",", Command.ExcludedTypeNames) : "";
            }
            set
            {
                if (value != null)
                    Command.ExcludedTypeNames = value.Split(',').Select(n => n.Trim()).Where(n => !string.IsNullOrEmpty(n)).ToArray();
                else
                    Command.ExcludedTypeNames = new string[] { };
                RaisePropertyChanged(() => ExcludedTypeNames);
            }
        }

        /// <summary>Gets or sets the client code. </summary>
        public string ClientCode
        {
            get { return _clientCode; }
            set { Set(ref _clientCode, value); }
        }

        public Task GenerateClientAsync(SwaggerDocument document, string documentPath)
        {
            return RunTaskAsync(async () =>
            {
                Dictionary<string, string> result = null;
                await Task.Run(async () =>
                {
                    if (document != null)
                    {
                        Command.Input = document;
                        result = await Command.RunAsync();
                        Command.Input = null;
                    }
                });

                ClientCode = result != null ? string.Join("\n\n", result.Values) : string.Empty;
            });
        }
    }
}

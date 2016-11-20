using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MyToolkit.Command;
using MyToolkit.Messaging;
using MyToolkit.Utilities;

namespace NSwagStudio.ViewModels
{
    public class DocumentViewModel : ViewModelBase
    {
        private static DocumentModel _document;

        /// <summary>Initializes a new instance of the <see cref="MainWindowModel"/> class.</summary>
        public DocumentViewModel()
        {
            GenerateCommand = new AsyncRelayCommand<string>(GenerateAsync);
        }

        /// <summary>Gets or sets the command to generate code from the selected Swagger generator.</summary>
        public AsyncRelayCommand<string> GenerateCommand { get; set; }

        public string SwaggerGenerator { get; set; }


        /// <summary>Gets or sets the settings. </summary>
        public DocumentModel Document
        {
            get { return _document; }
            set { Set(ref _document, value); }
        }

        /// <summary>Gets the application version with build time. </summary>
        public string ApplicationVersion => GetType().Assembly.GetVersionWithBuildTime();

        private async Task GenerateAsync(string type)
        {
            IsLoading = true;

            if (type == "files")
            {
                try
                {
                    await Document.Document.ExecuteAsync();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Error in DocumentViewModel.GenerateAsync: " + exception);
                }
            }
            else
            {
                var generator = Document.GetSwaggerGeneratorView();
                var swaggerCode = await generator.GenerateSwaggerAsync();

                if (!string.IsNullOrEmpty(swaggerCode))
                {
                    var documentPath = Document.GetDocumentPath(generator);
                    foreach (var codeGenerator in Document.CodeGenerators)
                        await codeGenerator.GenerateClientAsync(swaggerCode, documentPath);
                }
                else
                {
                    await Messenger.Default.SendAsync(new TextMessage("No Swagger specification",
                        "Could not generate code because the Swagger generator returned an empty document."));
                }
            }

            IsLoading = false;
        }
    }
}
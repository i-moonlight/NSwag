﻿//-----------------------------------------------------------------------
// <copyright file="MainWindowModel.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyToolkit.Command;
using MyToolkit.Storage;
using Newtonsoft.Json;
using NJsonSchema;
using NSwag;

namespace NSwagStudio.ViewModels
{
    /// <summary>The view model for the MainWindow.</summary>
    public class MainWindowModel : ViewModelBase
    {
        private NSwagDocument _selectedDocument;

        /// <summary>Initializes a new instance of the <see cref="MainWindowModel"/> class.</summary>
        public MainWindowModel()
        {
            CreateDocumentCommand = new RelayCommand(CreateDocument);
            OpenDocumentCommand = new AsyncRelayCommand(OpenDocumentAsync);

            CloseDocumentCommand = new AsyncRelayCommand<NSwagDocument>(async document => await CloseDocumentAsync(document), document => document != null);
            SaveDocumentCommand = new AsyncRelayCommand<NSwagDocument>(async document => await SaveDocumentAsync(document), document => document != null);
            SaveAsDocumentCommand = new AsyncRelayCommand<NSwagDocument>(async document => await SaveAsDocumentAsync(document), document => document != null);

            Documents = new ObservableCollection<NSwagDocument>();
        }

        public ObservableCollection<NSwagDocument> Documents { get; private set; }

        /// <summary>Gets or sets the selected document. </summary>
        public NSwagDocument SelectedDocument
        {
            get { return _selectedDocument; }
            set
            {
                if (Set(ref _selectedDocument, value))
                {
                    CloseDocumentCommand.RaiseCanExecuteChanged();
                    SaveDocumentCommand.RaiseCanExecuteChanged();
                    SaveAsDocumentCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand CreateDocumentCommand { get; private set; }

        public AsyncRelayCommand OpenDocumentCommand { get; private set; }

        public AsyncRelayCommand<NSwagDocument> CloseDocumentCommand { get; private set; }

        public AsyncRelayCommand<NSwagDocument> SaveDocumentCommand { get; private set; }

        public AsyncRelayCommand<NSwagDocument> SaveAsDocumentCommand { get; private set; }

        public string NSwagVersion => SwaggerService.ToolchainVersion;

        public string NJsonSchemaVersion => JsonSchema4.ToolchainVersion;

        protected override async void OnLoaded()
        {
            await Task.Delay(500);
            await LoadApplicationSettingsAsync();
        }

        private async Task LoadApplicationSettingsAsync()
        {
            try
            {
                var settings = ApplicationSettings.GetSetting("NSwagSettings", string.Empty);
                if (settings != string.Empty)
                {
                    var paths = JsonConvert.DeserializeObject<string[]>(settings)
                        .Where(File.Exists)
                        .ToArray();

                    if (paths.Length > 0)
                    {
                        foreach (var path in paths)
                            await OpenDocumentAsync(path);

                        SelectedDocument = Documents.Last();
                    }
                    else if (!Documents.Any())
                        CreateDocument();
                }
                else if (!Documents.Any())
                    CreateDocument();
            }
            catch
            {
                if (!Documents.Any())
                    CreateDocument();
            }

            SelectedDocument = Documents.First();
        }

        private void CreateDocument()
        {
            var document = NSwagDocument.Create();
            Documents.Add(document);
            SelectedDocument = document;
        }

        private async Task OpenDocumentAsync()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Open NSwag settings file";
            dlg.Filter = "NSwag settings (*.nswag)|*.nswag";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
                await OpenDocumentAsync(dlg.FileName);
        }

        public async Task OpenDocumentAsync(string filePath)
        {
            try
            {
                var currentDocument = Documents.SingleOrDefault(d => d.Path == filePath);
                if (currentDocument != null)
                    SelectedDocument = currentDocument;
                else
                {
                    var document = await NSwagDocument.LoadAsync(filePath);
                    Documents.Add(document);
                    SelectedDocument = document;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("File open failed: \n" + exception.Message, "Could not load the settings");
            }
        }

        public async Task<bool> CloseDocumentAsync(NSwagDocument document)
        {
            if (document.IsDirty)
            {
                var result = MessageBox.Show("Do you want to save the file " + document.Name + " ?",
                    "Save file", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var success = await SaveDocumentAsync(document);
                    if (!success)
                        return false;
                }
                else if (result == DialogResult.Cancel)
                    return false;
            }

            Documents.Remove(document);
            return true;
        }

        private async Task<bool> SaveDocumentAsync(NSwagDocument document)
        {
            try
            {
                if (File.Exists(document.Path))
                {
                    await document.SaveAsync();
                    MessageBox.Show("The file has been saved.", "File saved");
                    return true;
                }
                else
                {
                    if (await SaveAsDocumentAsync(document))
                        return true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("File save failed: \n" + exception.Message, "Could not save the settings");
            }

            return false;
        }

        private async Task<bool> SaveAsDocumentAsync(NSwagDocument document)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "NSwag settings (*.nswag)|*.nswag";
            dlg.RestoreDirectory = true;
            dlg.AddExtension = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                document.Path = dlg.FileName;
                await document.SaveAsync();
                return true;
            }
            return false;
        }
    }
}

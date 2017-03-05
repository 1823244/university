using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using SharpLZW;

namespace LZW
{
    public sealed partial class MainPage : Page
    {
        public StorageFile inputFile = null;
        public StorageFile outputFile = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SetInactive(Button button)
        {
            button.Foreground = new SolidColorBrush(Color.FromArgb(255, 154, 139, 131));
        }

        private void SetActive(Button button)
        {
            button.Foreground = new SolidColorBrush(Color.FromArgb(255, 200, 182, 153));
        }

        private async void inputButton_Click(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".txt");
            filePicker.ViewMode = PickerViewMode.List;
            filePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            filePicker.SettingsIdentifier = "TxtFilePicker";
            filePicker.CommitButtonText = "Выбрать файл";

            StorageFile selectedFile = await filePicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                inputLabel.Text = selectedFile.DisplayName + selectedFile.FileType;
                inputFile = selectedFile;
            }
        }

        private async void outputPicker_Click(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".txt");
            filePicker.ViewMode = PickerViewMode.List;
            filePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            filePicker.SettingsIdentifier = "TxtFilePicker";
            filePicker.CommitButtonText = "Выбрать файл";

            StorageFile selectedFile = await filePicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                outputLabel.Text = selectedFile.DisplayName + selectedFile.FileType;
                outputFile = selectedFile;
            }
        }

        private void encode_Click(object sender, RoutedEventArgs e)
        {
            if (inputFile == null)
            {
                inputLabel.Text = "Выберите файл!";
                return;
            }

            Runner start = new Runner();
            Runner.FileToCompress = inputFile.Path;
            //Runner.EncodedFile = outputFile.Path;
            start.Run();
        }
    }
}

﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TXT_FrameTimeHandler.Commands;
using TXT_FrameTimeHandler.DataProcessing;
using TXT_FrameTimeHandler.DataProcessing.Fraps;

namespace TXT_FrameTimeHandler.ViewModels
{
    public class StartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            this.SelectLogFilePathCommand?.RaiseCanExecuteChanged();
            this.SelectFrameTimingGraphFilePathCommand?.RaiseCanExecuteChanged();
            this.SelectProbabilityDensityGraphFilePathCommand?.RaiseCanExecuteChanged();
            this.SelectProbabilityDistributionGraphFilePathCommand?.RaiseCanExecuteChanged();

            this.OpenLogFileCommand?.RaiseCanExecuteChanged();
            this.SaveAsTxtFrameTimingGraphCommand?.RaiseCanExecuteChanged();
            this.SaveAsTxtProbabilityDensityGraphCommand?.RaiseCanExecuteChanged();
            this.SaveAsTxtProbabilityDistributionGraphCommand?.RaiseCanExecuteChanged();

            this.WriteFrameTimingGraphCommand?.RaiseCanExecuteChanged();
            this.WriteProbabilityDensityGraphCommand?.RaiseCanExecuteChanged();
            this.WriteProbabilityDistributionGraphCommand?.RaiseCanExecuteChanged();
        }

        private string _logFilePath;
        private string _frameTimingGraphFilePath;
        private string _probabilityDensityGraphFilePath;
        private string _probabilityDistributionGraphFilePath;

        public string LogFilePath
        {
            get => this._logFilePath;
            set
            {
                if (this._logFilePath == value)
                    return;

                this._logFilePath = value;
                this.OnPropertyChanged("LogFilePath");
            }
        }
        public string FrameTimingGraphFilePath
        {
            get => this._frameTimingGraphFilePath;
            set
            {
                if (this._frameTimingGraphFilePath == value)
                    return;

                this._frameTimingGraphFilePath = value;
                this.OnPropertyChanged("FrameTimingGraphFilePath");
            }
        }
        public string ProbabilityDensityGraphFilePath
        {
            get => this._probabilityDensityGraphFilePath;
            set
            {
                if (this._probabilityDensityGraphFilePath == value)
                    return;

                this._probabilityDensityGraphFilePath = value;
                this.OnPropertyChanged("ProbabilityDensityGraphFilePath");
            }
        }
        public string ProbabilityDistributionGraphFilePath
        {
            get => this._probabilityDistributionGraphFilePath;
            set
            {
                if (this._probabilityDistributionGraphFilePath == value)
                    return;

                this._probabilityDistributionGraphFilePath = value;
                this.OnPropertyChanged("ProbabilityDistributionGraphFilePath");
            }
        }

        public ClassicCommand SelectLogFilePathCommand { get; }
        public ClassicCommand SelectFrameTimingGraphFilePathCommand { get; }
        public ClassicCommand SelectProbabilityDensityGraphFilePathCommand { get; }
        public ClassicCommand SelectProbabilityDistributionGraphFilePathCommand { get; }

        public ClassicCommand OpenLogFileCommand { get; }
        public ClassicCommand SaveAsTxtFrameTimingGraphCommand { get; }
        public ClassicCommand SaveAsTxtProbabilityDensityGraphCommand { get; }
        public ClassicCommand SaveAsTxtProbabilityDistributionGraphCommand { get; }

        public ClassicCommand WriteFrameTimingGraphCommand { get; }
        public ClassicCommand WriteProbabilityDensityGraphCommand { get; }
        public ClassicCommand WriteProbabilityDistributionGraphCommand { get; }

        private Window ViewWindow => Application.Current.MainWindow;

        public StartViewModel()
        {
            this.SelectLogFilePathCommand = new ClassicCommand((arg) =>
            {
                var fileDialog = new OpenFileDialog
                {
                    Filter = "CSV Files|*.csv;|All Files|*.*"
                };

                if (Directory.GetFiles(Directory.GetCurrentDirectory()).Any(dirfile => Path.GetExtension(dirfile) == ".csv"))
                    fileDialog.InitialDirectory = Directory.GetCurrentDirectory();

                var result = fileDialog.ShowDialog(this.ViewWindow);

                if (result != true)
                    return;

                var file = fileDialog.FileName;

                this.LogFilePath = file;

            }, (arg) => true);

            this.SelectFrameTimingGraphFilePathCommand = new ClassicCommand((arg) =>
            {
                var fileDialog = new OpenFileDialog
                {
                    Filter = "GRG Files|*.grf;|All Files|*.*"
                };

                var result = fileDialog.ShowDialog(this.ViewWindow);

                if (result != true)
                    return;

                var file = fileDialog.FileName;

                this.FrameTimingGraphFilePath = file;

            }, (arg) => true);

            this.SelectProbabilityDensityGraphFilePathCommand = new ClassicCommand((arg) =>
            {
                var fileDialog = new OpenFileDialog
                {
                    Filter = "GRG Files|*.grf;|All Files|*.*"
                };

                var result = fileDialog.ShowDialog(this.ViewWindow);

                if (result != true)
                    return;

                var file = fileDialog.FileName;

                this.ProbabilityDensityGraphFilePath = file;

            }, (arg) => true);

            this.SelectProbabilityDistributionGraphFilePathCommand = new ClassicCommand((arg) =>
            {
                var fileDialog = new OpenFileDialog
                {
                    Filter = "GRG Files|*.grf;|All Files|*.*"
                };

                var result = fileDialog.ShowDialog(this.ViewWindow);

                if (result != true)
                    return;

                var file = fileDialog.FileName;

                this.ProbabilityDistributionGraphFilePath = file;

            }, (arg) => true);

            this.OpenLogFileCommand = new ClassicCommand((arg) =>
            {
                Maybe<FrapsData> result = FrapsDataProcessing.ProcessFrapsFile(this.LogFilePath);

                if(!result.HasValue)
                {

                }


            }, (arg) => true);
        }
    }
}
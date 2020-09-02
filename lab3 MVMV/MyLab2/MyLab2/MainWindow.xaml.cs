using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using Microsoft.Win32;
using System.Windows.Forms.DataVisualization.Charting;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public ModelDataView _modelDataView;
        public ModelDataView modelDataView 
        { 
            get 
            { 
                return _modelDataView; 
            } 
            set 
            {
                _modelDataView = value; 
            } 
        }
        public MainWindow()
        {
            modelDataView = new ModelDataView(new MyAppUIServices());
            this.DataContext = modelDataView;
            InitializeComponent();
            windowsformsHost.Child = modelDataView.chart;
            Closing += modelDataView.MainWindowClose;

        }
    } 
    /*public class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ModelData)value).p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }*/
    [Serializable]
    public class MyAppUIServices : IUISurvices
    {
        public bool ConfirmChanges()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                    break;
                case MessageBoxResult.No:
                    return false;
                    break;
            }
            return true;
        }
    }
}

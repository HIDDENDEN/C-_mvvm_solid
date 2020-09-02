using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;
using Model;
using System.Windows.Input;
using System.Windows.Controls;
using ViewModel.ViewModel;
using System.Windows.Data;

namespace ViewModel
{
    public interface IUISurvices
    {
        bool ConfirmChanges();
       
    }

    [Serializable]
    
    public partial class ModelDataView : IDataErrorInfo
    {
        public ObservableModelData obs_md { get; set; }
        public int Num_of_digits_after_comma_Min { get; set; } = 1;
        public int Num_of_digits_after_comma_Max { get; set; } = 5;
        public string Type_of_diagram { get; set; }
        public List<string> Types_of_graphs { get; set; }
        public string Property_for_bindings { get; set; }

        public int Num_of_digits_after_comma { get; set; }




        public ModelDataView(IUISurvices uiSurvices)
        {
            //UIServices
            this.uiSurvices = uiSurvices;

            //main part
            this.obs_md = new ObservableModelData();
            MyMD = new ModelData();
            Num_of_digits_after_comma = 2;
            Types_of_graphs = new List<string>() { "Line", "Spline" };

            //Moved from Window.cs
            
            //ADD_COMMAND
            addCommand = new RelayCommand(
                _ => NumOfGridNodesCheck(MyMD)&&pValueCheck(MyMD),
                _ => this.obs_md.Add_ModelData(new ModelData(MyMD.num_of_grid_nodes, MyMD.p))
                );

            //DELETE_COMMAND
            deleteCommand = new RelayCommand(
                _ => SelectedIndexCheck(ListBoxSelectedIndex),
                _ => this.obs_md.Remove_At(ListBoxSelectedIndex));

            //DRAW_COMMAND
            drawCommand = new RelayCommand(
                _ => SelectedIndexCheck(ListBoxSelectedIndex) && NumOfDigitsAfterCommaCheck(),
                _ => 
                {
                    IList<ModelData> temp = (
                        from item in this.obs_md
                         where (item.p <= ((ModelData)this.obs_md[ListBoxSelectedIndex]).p)
                         select item
                         ).ToList();
                    temp.Add(this.obs_md[ListBoxSelectedIndex] as ModelData);
                    this.Draw(chart, temp);
                }
                );

            //SAVE_COMMAND
            saveCommand = new RelayCommand(
                _ => this.obs_md.IsChanged,
                _ =>
                {
                    SaveFileDialog savefiledialog = new SaveFileDialog();
                    if (savefiledialog.ShowDialog() == DialogResult.OK)
                    {
                        Save(savefiledialog.FileName, this);
                    }
                }
                );

            //NEW_COMMAND
            newCommand = new RelayCommand(
                _=>  true,
                _=>
                {
                    if (this.obs_md.IsChanged)
                    {
                        bool wantToSave = IsChangesConfirmed;
                        
                        switch (wantToSave)
                        {
                            case true:
                                SaveFileDialog savefiledialog = new SaveFileDialog();
                                if (savefiledialog.ShowDialog() == DialogResult.OK)
                                {
                                    Save(savefiledialog.FileName, this);
                                }
                                WereChangesSaved = true;
                                break;
                            case false:
                                WereChangesSaved = false;
                                break;
                        }
                        
                    }
                    
                    
                    //instead of adding new DataContext - reset properties
                    this.obs_md.Remove_All();
                    this.obs_md.IsChanged = false;

                    //clear chart area
                    foreach (var series in chart.Series)
                        series.Points.Clear();
                        
                }
                );

            //OPEN_COMMAND
            openCommand = new RelayCommand(
                _=> true,
                _=>
                {
                    OpenFileDialog openfiledialog = new OpenFileDialog();
                    if (openfiledialog.ShowDialog() == DialogResult.OK)//right .OK ?
                    {
                        
                        if (this.obs_md.IsChanged)
                        {
                            bool wantToSave = IsChangesConfirmed;
                            switch(wantToSave)
                            {
                                case true:
                                    SaveFileDialog savefilefialog = new SaveFileDialog();
                                    if (savefilefialog.ShowDialog() == DialogResult.OK)
                                    {
                                        ModelDataView.Save(savefilefialog.FileName, this);
                                    }
                                    WereChangesSaved = true;
                                    break;
                                case false:
                                    WereChangesSaved = false;
                                    break;
                            }
                        }
                        ModelDataView _modelDataView = new ModelDataView(new FakeUIServices());
                        bool res = false;
                        res = ModelDataView.Load(openfiledialog.FileName, ref _modelDataView);
                        //instead of adding new DataContext - reset properties
                        this.obs_md.Remove_All();
                        this.obs_md.IsChanged = false;

                        //clear chart area
                        foreach (var series in chart.Series)
                            series.Points.Clear();
                        foreach (var md in _modelDataView.obs_md)
                            this.obs_md.Add_ModelData(md);
                    }
                }
                );
                
        }

        public void Draw(Chart chart, IList<ModelData> selectedModlel)
        {
            ModelData curModelData = selectedModlel[selectedModlel.Count() - 1];
            selectedModlel.RemoveAt(selectedModlel.Count() - 1);
            chart.ChartAreas.Clear(); chart.Series.Clear(); chart.Legends.Clear();
            chart.ChartAreas.Add(new ChartArea("first"));
            chart.Legends.Add("Main Legend");
            chart.Legends.Add("Extra Legend");
            chart.Legends[0].Position.Auto = true;
            chart.Legends[1].Position.Auto = true;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0." + new String('0', this.Num_of_digits_after_comma) + "}";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0." + new String('0', this.Num_of_digits_after_comma) + "}";
            for (int index = 0; index < selectedModlel.Count; index++)
            {
                chart.Series.Add("Series" + index);
                chart.Series[index].LegendText = selectedModlel[index].p.ToString();
                chart.Series[index].Points.DataBindXY(selectedModlel[index].grid_nodes, selectedModlel[index].values);
                if (this.Type_of_diagram == "Spline")
                    chart.Series[index].ChartType = SeriesChartType.Spline;
                else
                    if (this.Type_of_diagram == "Line")
                    chart.Series[index].ChartType = SeriesChartType.Line;
                chart.Series[index].ChartArea = "first";
                chart.Series[index].Legend = "Main Legend";
            }
            chart.ChartAreas.Add(new ChartArea("second"));
            chart.ChartAreas[1].AxisX.LabelStyle.Format = "{0:0." + new String('0', this.Num_of_digits_after_comma) + "}";
            chart.ChartAreas[1].AxisY.LabelStyle.Format = "{0:0." + new String('0', this.Num_of_digits_after_comma) + "}";
            double[] min = new double[curModelData.num_of_grid_nodes];
            double[] medium = new double[curModelData.num_of_grid_nodes];
            double[] max = new double[curModelData.num_of_grid_nodes];
            for (int index = 0; index < curModelData.num_of_grid_nodes; index++)
            {
                List<double> temp = new List<double>();
                foreach (var item in obs_md)
                {
                    temp.Add(item.F(curModelData.grid_nodes[index]));
                }
                min[index] = temp.Min();
                medium[index] = temp.Average();
                max[index] = temp.Max();
            }

            List<double[]> data = new List<double[]> { max, min, medium };
            List<string> legend_names = new List<string> { "max", "min", "medium" };

            for (int index = 0; index <= 2; index++)
            {
                chart.Series.Add("Series" + (index + selectedModlel.Count));
                chart.Series[index + selectedModlel.Count].LegendText = legend_names[index];
                chart.Series[index + selectedModlel.Count].Points.DataBindXY(curModelData.grid_nodes, data[index]);
                if (this.Type_of_diagram == "Spline")
                    chart.Series[index + selectedModlel.Count].ChartType = SeriesChartType.Spline;
                else
                    if (this.Type_of_diagram == "Line")
                    chart.Series[index + selectedModlel.Count].ChartType = SeriesChartType.Line;
                chart.Series[index + selectedModlel.Count].MarkerStyle = MarkerStyle.Circle;
                chart.Series[index + selectedModlel.Count].MarkerSize = 10;
                chart.Series[index + selectedModlel.Count].ChartArea = "second";
                chart.Series[index + selectedModlel.Count].Legend = "Extra Legend";
                for (int k = 0; k < chart.Series[index + selectedModlel.Count].Points.Count; k++)
                {
                    chart.Series[index + selectedModlel.Count].Points[k].ToolTip =
                    "x = " + chart.Series[index + selectedModlel.Count].Points[k].XValue.ToString("F" + Num_of_digits_after_comma) + "\ny = " + chart.Series[index + selectedModlel.Count].Points[k].YValues[0].ToString("F" + Num_of_digits_after_comma);
                }

            }
        }

        public static bool Save(string filename, ModelDataView obj)
        {
            if (obj == null) return false;
            Stream stream = null;
            BinaryFormatter bf = null;
            try
            {
                bf = new BinaryFormatter();
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            try
            {
                bf.Serialize(stream, obj);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                stream.Close();
            }
            obj.obs_md.IsChanged = false;
            return true;
        }

        public static bool Load(string filename, ref ModelDataView obj)
        {
            BinaryFormatter bf = null;
            Stream stream = null;
            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                bf = new BinaryFormatter();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            try
            {
                obj = (ModelDataView)bf.Deserialize(stream);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                stream.Close();
            }
            obj.obs_md.IsChanged = false;
            return true;
        }
        public string Error { get { return "Error in ModelDataView.cs"; } }
        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "Num_of_digits_after_comma":
                        if (Num_of_digits_after_comma > Num_of_digits_after_comma_Max || Num_of_digits_after_comma < Num_of_digits_after_comma_Min)
                            msg = "Num_of_digits_after_comma shuld be in range [ " + Num_of_digits_after_comma_Min + " ; " + Num_of_digits_after_comma_Max + "]";
                        break;
                }
                return msg;
            }
        }




        //MOVE LOGIC FROM WINDOW.CS
        //-------------------------
        //-------------------------
        public ModelData MyMD { get; set; }

        //AddCommand
        private readonly ICommand addCommand;
        public ICommand AddCommand { get { return addCommand; } }

        //DeleteCommand
        private readonly ICommand deleteCommand;
        public ICommand DeleteCommand { get { return deleteCommand; } }

        //DrawCommand
        private readonly ICommand drawCommand;
        public ICommand DrawCommand { get { return drawCommand; } }

       [field:NonSerializedAttribute()]
        public Chart chart { get; set; } = new Chart();

        //IUIServices
        private IUISurvices uiSurvices;

        //SaveCommand
        public bool WereChangesSaved { get; set; }
        private readonly ICommand saveCommand;
        public ICommand SaveCommand { get { return saveCommand; } }

        //NewCommand
        private readonly ICommand newCommand;
        public ICommand NewCommand { get { return newCommand; } }
        //[field:NonSerialized]
        public bool IsChangesConfirmed { get { return uiSurvices.ConfirmChanges(); } }

        //OpenCommand
        private readonly ICommand openCommand;
        public ICommand OpenCommand { get { return openCommand; } }

        //"Closing Window"
        public void MainWindowClose(object sender, EventArgs e)
        {
            if (this.obs_md.IsChanged)
            {
                bool wantToSave = IsChangesConfirmed;
                switch (wantToSave)
                {
                    case true:
                        SaveFileDialog savefilefialog = new SaveFileDialog();
                        if (savefilefialog.ShowDialog() == DialogResult.OK)
                        {
                            ModelDataView.Save(savefilefialog.FileName, this);
                        }
                        break;
                    case false:
                        WereChangesSaved = false;
                        break;
                }
            }
        }


        //Error logic 

        //ADD
        public static bool NumOfGridNodesCheck(ModelData newMD)
        {
            if ((newMD.num_of_grid_nodes < ModelData.nMin) || (newMD.num_of_grid_nodes > ModelData.nMax))
                return false;
            else
                return true;
        }
        public static bool pValueCheck(ModelData newMD)
        {
            if ((newMD.p < ModelData.pMin) || (newMD.p > ModelData.pMax))
                return false;
            else
                return true;
        }

        //DELETE
        public static int ListBoxSelectedIndex { get; set; } = -1;

        public static bool SelectedIndexCheck(int selectedIndex)
        {
            if (selectedIndex >= 0)
                return true;
            else
                return false;
        }

        //DRAW
        public  bool NumOfDigitsAfterCommaCheck()
        {
            if (Num_of_digits_after_comma > Num_of_digits_after_comma_Max || Num_of_digits_after_comma < Num_of_digits_after_comma_Min)
                return false;
            else
                return true;
        }
        
    }
    [Serializable]
    public class FakeUIServices : IUISurvices
    {
        public bool ConfirmChanges()
        {
            
            return true;
        }
    }
    public class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ModelData)value).p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Windows.DependencyProperty.UnsetValue;
        }
    }

    //Extra class for static resources to use in xaml instead of static res. from ModelData(cause ModData shouldn't be linked to View)
    public class MDStaticResClass
    {
        static public double pMin { get; set; } = ModelData.pMin;
        static public double pMax { get; set; } = ModelData.pMax;
        static public double nMin { get; set; } = ModelData.nMin;
        static public double nMax { get; set; } = ModelData.nMax;
        public MDStaticResClass()
        {
            
        }
    }

}   

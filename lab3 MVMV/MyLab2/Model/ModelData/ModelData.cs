using System;
using System.Linq;
using System.ComponentModel;


namespace Model
{
    //класс для данных
    [Serializable]
    public class ModelData : IDataErrorInfo,IDeepCopy
    {

        //класс хранит значения некоторой функции(зависящей от параметра р)
        //значения функции заданы на отрезке [0;1] э x   на равномерной сетке
        //p принимает значения [pMin;pMax]
        //число узлов сетки
        public int num_of_grid_nodes { get; set; }

        //значение для параметра р 
        public double p { get; set; }

        //массив узлов сетки
        public double[] grid_nodes { get; set; }

        //массив значений функции в узлах сетки
        public double[] values { get; set; }

        // нижнее\верхнее значение параметра р 
        static public double pMin { get; set; } = 1;
        static public double pMax { get; set; } = 6;

        // min\max количество числа точек в которых задана функция
        // (на сколько точек разбиваем область определения функции)
        static public double nMin { get; set; } = 4;
        static public double nMax { get; set; } = 10;
        
        // func Entity
        public Model.FunctionEntity funcForm = new Model.FunctionEntity();

        public ModelData() { }
        public ModelData(int num_of_grid_nodes, double p=1)
        {
            
            values = new double[num_of_grid_nodes];
            values[0] = funcForm.MyFunc(0.0, p);
            grid_nodes = new double[num_of_grid_nodes];
            grid_nodes[0] = 0.0;
            this.p = p;
            this.num_of_grid_nodes = num_of_grid_nodes;
            double step = 1.0 /(num_of_grid_nodes - 1);
            double cur_node = step;
            for (int i = 1; i <= num_of_grid_nodes - 2;i++)
            {
                grid_nodes[i] = cur_node;
                values[i] = funcForm.MyFunc(cur_node, p);
                cur_node += step;
            }
            grid_nodes[num_of_grid_nodes - 1] = 1.0;
            values[num_of_grid_nodes - 1] = funcForm.MyFunc(1.0, p);
        }


       
        public double F(double x)
        {
            if (grid_nodes.Contains(x))
                return funcForm.MyFunc(x, p);
            else
            {
                int i = FindLeftIndex(x);
                double xleft = grid_nodes[i];
                double xright = grid_nodes[i + 1];
                double Fleft = funcForm.MyFunc(xleft, p); //(xleft - p) * (xleft - p);
                double Fright = funcForm.MyFunc(xright, p);//(xright - p) * (xright - p);
                return ((Fleft + Fright) / 2);
            }
        }
        private int FindLeftIndex(double x)
        {
            int i = 0;
            for (i = 0; i < num_of_grid_nodes - 1; i++)
                if ((grid_nodes[i] < x) && (x < grid_nodes[i + 1]))
                    return i;
            
            return (num_of_grid_nodes - 1);
        }
        public string Error { get { return "Error in ModelData.cs"; } }
        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "num_of_grid_nodes":
                        if ((num_of_grid_nodes < nMin) || (num_of_grid_nodes > nMax))
                            msg = "num_of_grid_nodes should be in range [ " + nMin + " ; " + nMax + "]";
                        break;
                    case "p":
                        if ((p < pMin) || (p > pMax))
                            msg = "p should be in range [ " + pMin + " ; " + pMax + "]";
                        break;
                    default:
                        break;
                }
                return msg;
            }
        }

        public virtual object DeepCopy()
        {
            ModelData temp = new ModelData(num_of_grid_nodes, p);
            return temp;
        }

        public override string ToString()
        {
            return "Число узлов сетки = " + num_of_grid_nodes + "; значение параметра p = " + p;
        }
    }
   public interface IDeepCopy
{
    object DeepCopy();
}
}


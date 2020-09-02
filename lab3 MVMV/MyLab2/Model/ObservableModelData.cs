using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Model
{
    [Serializable]
    public class ObservableModelData : ObservableCollection<ModelData>, INotifyCollectionChanged,IDeepCopy
    {
        public bool IsChanged { get; set; } = false;

        public ObservableModelData(){}

        // добавление в коллекцию элемента ModelData
        public void Add_ModelData(ModelData modelData)
        {
            base.Add((ModelData)modelData.DeepCopy());
            this.IsChanged = true;
        }

        public void Remove_At(int index)
        {
            base.RemoveAt(index);
            this.IsChanged = true;
        }
        public void Remove_All()
        {
            int n = base.Count;
            for (int index = n - 1; index >=0; index--)
                base.RemoveAt(index);
            this.IsChanged = true;
        }

        public void AddDefaults()
        {
            base.Add(new ModelData(3, 5));
            this.IsChanged = true;
            base.Add(new ModelData(5, 5));
            base.Add(new ModelData(8, 5));
            base.Add(new ModelData(11, 5));
            
        }

        public double[] MyFForEveryParametr(double x)
        {

            double[] func_values = new double[base.Items.Count()];
            for (int i = 0; i < base.Items.Count(); i++)
            {
                func_values[i] = base[i].F(x);
            }

            return func_values;
        }

        public override string ToString()
        {
            string ans = "Changed= " + IsChanged;

            foreach (var item in base.Items)
            {
                ans += "\n" + item.ToString();
            }
            return ans;
        }

        public virtual object DeepCopy()
        {
            ObservableModelData temp = new ObservableModelData();
            int n = this.Count;
            for (int index=0;index<n;index++)
            {
                temp.Add_ModelData((ModelData)this[index].DeepCopy());
            }
            return temp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class FunctionEntity
    {
        private string _funcForm;
        public string funcForm
        {   get { return _funcForm;} 
            set { _funcForm = value; } }

        public FunctionEntity(string initForm="")
        {
            if ((initForm != ""))
                funcForm = initForm;
            else
                funcForm = "(p+x)*(p+x)";
        }

        public double MyFunc(double x, double p)
        {
            return (p + x) * (p + x);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseMVCv3
{
    public interface ISetTemp
    {
        int Temp { get; set; }
        void SetTemp(int input);
    }
}

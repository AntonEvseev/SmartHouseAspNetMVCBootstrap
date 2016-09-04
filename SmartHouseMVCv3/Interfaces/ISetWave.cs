using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseMVCv3
{
    public interface ISetWave
    {
        double Wave { get; set; }
        void SetWave(double input);
        void UpWave();
        void LessWave();
    }
}

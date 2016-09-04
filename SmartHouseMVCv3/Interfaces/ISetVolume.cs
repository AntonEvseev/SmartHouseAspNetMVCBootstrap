using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseMVCv3
{
    public interface ISetVolume
    {
        int Volume { get; set; }
        void SetVolume(int input);
        void UpVolume();
        void LessVolume();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouseMVCv3;

namespace SmartHouseMVCv3
{
    public interface ILampMode
    {
        BrightnessLevel Level
        {
            get;
        }
        void SetLowBrightness();
        void SetMediumBrightness();
        void SetHighBrightness();
    }
}

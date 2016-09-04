using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseMVCv3
{
    public interface IFridgeMode
    {
        void SetSuperFreezing();
        void SetFreezing();
        void SetDefrost();
    }
}

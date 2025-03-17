using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD3.src
{
    public interface IHazardNotifier
    {
        void SendNotification(string message);
    }
}

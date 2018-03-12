using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices
{
    public interface IToast
    {
        /// <summary>
        /// Zobrazí krátkou zprávu uživateli
        /// </summary>
        /// <param name="text"></param>
        void ShowToast(string text);
    }
}

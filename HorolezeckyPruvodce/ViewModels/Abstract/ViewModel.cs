using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UHK.MatejVlk.HorolezeckyPruvodce.ViewModels.Abstract
{
    /// <summary>
    /// Poskytuje základní nástroje pro práci s běžným ViewModelem
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        private bool isValid;

        /// <summary>
        /// Volá se pokud dojde ke změně nějaké vlastnosti
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Volá událost PropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Poskytuje základní nástroje pro práci s běžným ViewModelem
        /// </summary>
        public ViewModel()
        {
        }
    }
}

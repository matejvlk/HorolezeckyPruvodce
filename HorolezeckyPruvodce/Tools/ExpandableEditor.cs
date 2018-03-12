using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Tools
{
    /// <summary>
    /// Editor, který se při zadávání textu zvětšuje
    /// </summary>
    public class ExpandableEditor : Editor
    {
        /// <summary>
        /// Editor, který se při zadávání textu zvětšuje
        /// </summary>
        public ExpandableEditor()
        {
            this.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            this.InvalidateMeasure();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UHK.MatejVlk.HorolezeckyPruvodce.DependencyServices
{
    /// <summary>
    /// Vytváření dialogů
    /// </summary>
    public interface IDialogs
    {
        /// <summary>
        /// zobrazí dialog s nadpisem, zprávou a dvěmi tlačítky
        /// </summary>
        /// <param name="title">nadpis</param>
        /// <param name="message">zpráva</param>
        /// <param name="positiveButtonText">text pro levé tlačítko</param>
        /// <param name="negativeButtonText">text pro pravé tlačítko</param>
        /// <param name="positiveButton">akce pro levé tlačítko</param>
        /// <param name="negativeButton">ance pro pravé tlačítko</param>
        /// <param name="isSingleton">Určuje zda může být zobrazen v jednu chvíli pouze tento dialog či ještě nějaký jiný</param>
        void ShowDialog(string title, string message, string positiveButtonText, string negativeButtonText, EventHandler positiveButton, EventHandler negativeButton);
    }
}

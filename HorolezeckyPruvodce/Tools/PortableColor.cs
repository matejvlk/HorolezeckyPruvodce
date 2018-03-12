using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Backend.Models
{
    /// <summary>
    /// Třída pro práci s barvou ve sdílených projektech
    /// </summary>
    public class PortableColor
    {
        /// <summary>
        /// Vytvoří barvu ze zadaného hex kódu. Podorované formáty: #RRGGBB; RRGGBB; #AARRGGBB; AARRGGBB; rrr,ggg,bbb
        /// </summary>
        /// <param name="colorHexCode">Hex kód barvy.</param>
        public PortableColor(string colorHexCode)
        {
            if (colorHexCode.Contains(","))
                this.SetColorFromRgb(colorHexCode);
            else if (colorHexCode.Length <= 7)
                this.SetColorFromHex(colorHexCode);
            else if (colorHexCode.Length <= 9)
                this.SetColorWithAlpha(colorHexCode);
            else
                throw new ArgumentException($"Unsupported color format - {colorHexCode}");
        }

        /// <summary>
        /// Třída pro práci s barvou ve sdílených projektech
        /// </summary>
        public PortableColor()
        {
            Alpha = 255;
        }

        private void SetColorWithAlpha(string colorHexCode)
        {
            int start = (colorHexCode[0] == '#') ? 1 : 0;
            Alpha = Convert.ToByte(colorHexCode.Substring(start, 2), 16);
            start += 2;
            Red = Convert.ToByte(colorHexCode.Substring(start, 2), 16);
            start += 2;
            Green = Convert.ToByte(colorHexCode.Substring(start, 2), 16);
            start += 2;
            Blue = Convert.ToByte(colorHexCode.Substring(start, 2), 16);

        }

        private void SetColorFromHex(string colorHexCode)
        {
            int start = (colorHexCode[0] == '#') ? 1 : 0;
            Red = Convert.ToByte(colorHexCode.Substring(start, 2), 16);
            start += 2;
            Green = Convert.ToByte(colorHexCode.Substring(start, 2), 16);
            start += 2;
            Blue = Convert.ToByte(colorHexCode.Substring(start, 2), 16);

            Alpha = 255;
        }

        private void SetColorFromRgb(string colorRgbCode)
        {
            string[] rgb = colorRgbCode.Split(',');
            Red = Convert.ToByte(rgb[0]);
            Green = Convert.ToByte(rgb[1]);
            Blue = Convert.ToByte(rgb[2]);

            Alpha = 255;
        }

        /// <summary>
        /// Vypíše barvu hexadecimálně ve formátu #AARRGGBB
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}";
        }

        /// <summary>
        /// Průhlednost
        /// </summary>
        public byte Alpha { get; set; }

        /// <summary>
        /// Červená složka
        /// </summary>
        public byte Red { get; set; }

        /// <summary>
        /// Zelená složka
        /// </summary>
        public byte Green { get; set; }

        /// <summary>
        /// Modrá složka
        /// </summary>
        public byte Blue { get; set; }
    }
}

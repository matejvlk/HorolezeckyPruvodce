using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHK.MatejVlk.HorolezeckyPruvodce.Models;
using UHK.MatejVlk.HorolezeckyPruvodce.Resources;

namespace UHK.MatejVlk.HorolezeckyPruvodce.Tools
{
    /// <summary>
    /// Statická třída umožňující zobrazit obtížnost a stupnici a převádět mezi různými stupnicemi obtížností cest (naše virtuální - indexy, UIAA, Saská pískovcová, USA, Francouzská a Nová jizerská)
    /// </summary>
    public static class Difficulty
    {
        public const string UIAA = "UIAA";
        public const string Saxon = "Saxon";
        public const string USA = "USA";
        public const string French = "French";
        public const string Jizerska = "Jizerska";
        public const string Other = "Other";

        private static List<string> UIAAScale = new List<string>{"1","2","2+","3–","3","3+","4–","4","4+","5–","5","5+","5+/6-","6–","6","6+","6+/7-","7–","7-/7","7","7/7+","7+","7+/8-","8–","8-/8","8","8/8+","8+","8+/9-","9–","9-/9","9","9/9+","9+","9+/10-","9+/10-","10–","10-/10","10","10+","10+/11-","11–","11-/11","11","11+","12–","12"};

        private static List<string> SaxonScale = new List<string>{"I","I","II","II/III","III","III/IV","IV","IV/V","V","V/VI","VI","VIIa","VIIa/b","VIIb","VIIc","VIIc/VIIIa","VIIIa","VIIIa/b","VIIIb","VIIIb/c","VIIIc","VIIIc/Ixa","IXa","IXa/b","IXb","Ixb/c","IXc","IXc/Xa","Xa","Xa","Xa/b","Xb","Xb","Xb/c","Xc","Xc/XIa","Xc/XIa","XIa","XIb","XIc","XIc/XIIa","XIIa","XIIa/XIIb","XIIb","XIIc","XIIc/XIIIa","XIIIb"};

        private static List<string> USAScale = new List<string>{"1","2","3","4","5.0","5.1","5.2","5.3","5.4","5.5","5.6","5.7","5.7","5.8","5.9","5.9","5.10a","5.10b","5.10b","5.10c","5.10d","5.10d","5.11a","5.11b","5.11b","5.11c","5.11d","5.11d","5.12a","5.12b","5.12b","5.12c","5.12d","5.12d","5.13a","5.13a","5.13b","5.13c","5.13d","5.14a","5.14a","5.14b","5.14c","5.14d","5.15a","5.15b","5.15c"};

        private static List<string> FrenchScale = new List<string>{"1","2","2","3a","3b","3c","4a","4a+","4b","4c","4c+","5a","5a+","5b","5c","5c+","5c+/6a","6a","6a+","6a+/b","6b","6b+","6b+/c","6c","6c+","6c+/7a","7a","7a/b","7b","7b/+","7b+","7b+/c","7c","7c/+","7c+","7c+/8a","8a","8a+","8b","8b+","8b+/c","8c","8c+","9a","9a+","9b","9b+"};

        private static List<string> JizerskaScale = new List<string>{"I","I","I/II","II","II","III","III","IV","IV","IV","V","V","V","V","VI","VIb","VIb","VIc","VIc","VII","VII","VIIb","VIIc","VIII","VIII","VIIIb","VIIIb","VIIIc","I,","IX","IXb","IXc","IXc","X","Xb","Xb","Xc","Xc","XI","XIb","XIb","XIc","XIc","XII","XIIb","XIIc","XIIc"};

        public static string GetGrade(Route model)
        {
            if (model != null && model.Grade != null)
            {
                int virt;
                int.TryParse(model.Grade, out virt);
                if (virt != 0 && model.GradingSystem != null && model.GradingSystem != Difficulty.Other)
                {
                    switch (model.GradingSystem)
                    {
                        case Difficulty.UIAA:
                            return Difficulty.GetUIAAFromVirtual(virt);
                        case Difficulty.French:
                            return Difficulty.GetFrenchFromVirtual(virt);
                        case Difficulty.Jizerska:
                            return Difficulty.GetJizerskaFromVirtual(virt);
                        case Difficulty.Saxon:
                            return Difficulty.GetSaxonFromVirtual(virt);
                        case Difficulty.USA:
                            return Difficulty.GetUSAFromVirtual(virt);
                        default:
                            return model.Grade;
                    }
                }
                else
                    return model.Grade;
            }
            else
                return AppResources.Unknown;
        }

        public static string GetGradingSystem(Route model)
        {
            if (model != null && model.GradingSystem != null)
            {
                switch (model.GradingSystem)
                {
                    case Difficulty.UIAA:
                        return AppResources.GradingSystem_UIAA;
                    case Difficulty.French:
                        return AppResources.GradingSystem_French;
                    case Difficulty.Jizerska:
                        return AppResources.GradingSystem_Jizerska;
                    case Difficulty.Saxon:
                        return AppResources.GradingSystem_Saxon;
                    case Difficulty.USA:
                        return AppResources.GradingSystem_USA;
                    case Difficulty.Other:
                        return AppResources.GradingSystem_Other;
                    default:
                        return AppResources.Unknown;
                }
            }
            else
                return AppResources.Unknown;
        }

        public static string GetUIAAFromVirtual(int virt)
        {
            return UIAAScale[virt-1];
        }

        public static int GetVirualFromUIAA(string UIAA)
        {
            for (int i = 0; i < Difficulty.UIAAScale.Count; i++)
            {
                if (Difficulty.UIAAScale[i] == UIAA)
                {
                    return i+1;
                }
            }
            return -1;
        }

        public static string GetSaxonFromVirtual(int virt)
        {
            return SaxonScale[virt-1];
        }

        public static int GetVirualFromSaxon(string saxon)
        {
            for (int i = 0; i < Difficulty.SaxonScale.Count; i++)
            {
                if (Difficulty.SaxonScale[i] == saxon)
                {
                    return i+1;
                }
            }
            return -1;
        }

        public static string GetUSAFromVirtual(int virt)
        {
            return USAScale[virt-1];
        }

        public static int GetVirualFromUSA(string USA)
        {
            for (int i = 0; i < Difficulty.USAScale.Count; i++)
            {
                if (Difficulty.USAScale[i] == USA)
                {
                    return i+1;
                }
            }
            return -1;
        }

        public static string GetFrenchFromVirtual(int virt)
        {
            return FrenchScale[virt-1];
        }

        public static int GetVirualFromFrench(string french)
        {
            for (int i = 0; i < Difficulty.FrenchScale.Count; i++)
            {
                if (Difficulty.FrenchScale[i] == french)
                {
                    return i+1;
                }
            }
            return -1;
        }

        public static string GetJizerskaFromVirtual(int virt)
        {
            return JizerskaScale[virt-1];
        }

        public static int GetVirualFromJizerska(string jizerska)
        {
            for (int i = 0; i < Difficulty.JizerskaScale.Count; i++)
            {
                if (Difficulty.JizerskaScale[i] == jizerska)
                {
                    return i+1;
                }
            }
            return -1;
        }
    }
}

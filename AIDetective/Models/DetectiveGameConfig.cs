using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDetective.Models
{
    public class DetectiveGameConfig
    {
        public string IntroInstruction { get; set; } = "Geef een kort overzicht van het verhaal als verteller.";
        public string PlayerInputTemplate { get; set; } = "Spelersinput: {0} Einde spelersinput. Geef antwoord op de vraag. Als de speler iets gokt, zeg duidelijk of het goed of fout is. Stel geen vragen terug.";
    }
}

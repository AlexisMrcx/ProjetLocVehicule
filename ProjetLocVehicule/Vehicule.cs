using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetLocVehicule
{
    public class Vehicule
    {
        public string Immatriculation { get; }
        public string Marque { get; }
        public string Modele { get; }
        public string Couleur { get; }


        public Vehicule(string ma, string mo, string co, string imm)
        {
            Marque = ma;
            Modele = mo;
            Couleur = co;
            Immatriculation = imm;
        }
    }
}

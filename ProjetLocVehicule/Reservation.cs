using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetLocVehicule
{
    public class Reservation
    {
        public Client Client { get; }
        public Vehicule Vehicule { get; }
        public DateTime DateDebut { get; }
        public DateTime DateFin { get; }
        public int EstimationKm { get; }

        public Reservation(Client c, Vehicule v, DateTime dd, DateTime df, int eK)
        {
            Client = c;
            Vehicule = v;
            DateDebut = dd;
            DateFin = df;
            EstimationKm = eK;
        }
    }
}

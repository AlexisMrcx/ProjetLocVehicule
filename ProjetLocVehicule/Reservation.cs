﻿using System;
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

        public Reservation(Client c, Vehicule v, DateTime dd, DateTime df)
        {
            Client = c;
            Vehicule = v;
            DateDebut = dd;
            DateFin = df;
        }
    }
}

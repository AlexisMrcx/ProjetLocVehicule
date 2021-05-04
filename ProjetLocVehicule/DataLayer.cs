using System;
using System.Collections.Generic;

namespace ProjetLocVehicule
{
    internal class DataLayer : IDataLayer
    {
        public List<Client> Clients { get; }
        public List<Vehicule> Vehicules { get; }
        public List<Reservation> Reservations { get; }

        public DataLayer()
        {
            Clients = new List<Client>();
            Vehicules = new List<Vehicule>();
            Reservations = new List<Reservation>();
        }
    }
}
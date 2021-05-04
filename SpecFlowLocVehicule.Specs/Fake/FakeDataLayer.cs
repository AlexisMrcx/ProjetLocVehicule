using ProjetLocVehicule;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpecFlowLocVehicule.Specs.Fake
{
    public class FakeDataLayer : IDataLayer
    {
        public List<Client> Clients { get; }
        public List<Vehicule> Vehicules { get; }
        public List<Reservation> Reservations { get; }

        public FakeDataLayer()
        {
            Clients = new List<Client>();
            Vehicules = new List<Vehicule>();
            Reservations = new List<Reservation>();
        }
    }
}

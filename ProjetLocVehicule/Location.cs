using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProjetLocVehicule
{
    public class Location
    {
        private IDataLayer dataLayer;
        public bool UserConnected { get; private set; }
        public bool DateCorrect { get; private set; }
        public string messageReservation { get; private set; }


        public Location()
        {
            dataLayer = new DataLayer();
        }

        public Location(IDataLayer dL)
        {
            dataLayer = dL;
        }


        public string ConnectUser(string username, string password)
        {
            Client client = dataLayer.Clients.SingleOrDefault(_ => _.Login == username);

            if(client==null) 
            { 
                UserConnected = false;
                return "Login non reconnue";
            }
            else
            {
                if(client.Password==password)
                {
                    UserConnected = true;
                }
                else
                {
                    UserConnected = false;
                    return "Password incorrect";
                }
            }
            return "";
        }

        public List<Vehicule> SearchVehiculesDisponible(DateTime dateDebut, DateTime dateFin)
        {
            List<Vehicule> vDisponibles = dataLayer.Vehicules;
            IEnumerable<Reservation> reservationPeriode = new List<Reservation>();

            if(dateFin.CompareTo(dateDebut) > 0) 
            { 
                DateCorrect = true; 
            } else
            {
                DateCorrect = false;
                return null;
            }

            reservationPeriode = dataLayer.Reservations.Where(_ => (_.DateDebut >= dateDebut && _.DateDebut <= dateFin) || (_.DateFin >= dateDebut && _.DateFin <= dateFin));

            foreach(Reservation r in reservationPeriode)
            {
                vDisponibles.Remove(r.Vehicule);
            }

            return vDisponibles;
        }

        public bool Reservation(Client clientConnecte, Vehicule vSelectionne, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                dataLayer.Reservations.Add(new Reservation(clientConnecte, vSelectionne, dateDebut, dateFin));
                return true;
            }catch (Exception ex)
            {
                return false;
            }            
        }
    }
}

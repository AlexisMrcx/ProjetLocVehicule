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

            if (client == null)
            {
                UserConnected = false;
                return "Login non reconnue";
            }
            else
            {
                if (client.Password == password)
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

            if (dateFin.CompareTo(dateDebut) > 0)
            {
                DateCorrect = true;
            }
            else
            {
                DateCorrect = false;
                return null;
            }

            reservationPeriode = dataLayer.Reservations.Where(_ => (_.DateDebut >= dateDebut && _.DateDebut <= dateFin) || (_.DateFin >= dateDebut && _.DateFin <= dateFin));

            foreach (Reservation r in reservationPeriode)
            {
                vDisponibles.Remove(r.Vehicule);
            }

            return vDisponibles;
        }

        public bool CreateReservation(Client clientConnecte, Vehicule vSelectionne, DateTime dateDebut, DateTime dateFin, int estimationKm)
        {
            if (clientConnecte.Age < 18 || clientConnecte.Permis == false)
            {
                return false;
            }
            else if ((clientConnecte.Age < 21 && vSelectionne.NombreChevauxFiscaux >= 8) || (clientConnecte.Age >= 21 && clientConnecte.Age <= 25 && vSelectionne.NombreChevauxFiscaux >= 13))
            {
                return false;
            }
            else
            {
                foreach (Reservation r in dataLayer.Reservations)
                {
                    if (r.Client == clientConnecte)
                    {
                        //Si le client à déjà une réservation
                        return false;
                    }
                }

                dataLayer.Reservations.Add(new Reservation(clientConnecte, vSelectionne, dateDebut, dateFin, estimationKm));

                return true;
            }

        }

        public float CalculFacture(Reservation reservation, int ajustementKm = 0)
        {
            float total = 0;

            total += reservation.Vehicule.PrixKm * (reservation.EstimationKm + ajustementKm);
            total += reservation.Vehicule.PrixReservation;

            dataLayer.Reservations.Remove(reservation);

            return total;

        }
    }
}

using System;

namespace ProjetLocVehicule
{
    public class Client
    {
        public string Nom { get; }
        public string Prenom { get; }
        public DateTime DateNaissance { get; }
        public DateTime DateObtentionPermis { get; }
        public string NumeroPermis { get; }
        private string login;
        private string password;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="n">Nom</param>
        /// <param name="p">Prenom</param>
        /// <param name="l">Login</param>
        /// <param name="pw">Password</param>
        /// <param name="dn">Date Naissance</param>
        /// <param name="dp">Date Obtention permis</param>
        /// <param name="np">Numero de permis</param>
        public Client(string n, string p, string l, string pw, DateTime dn, DateTime dp, string np)
        {
            Nom = n;
            Prenom = p;
            DateNaissance = dn;
            login = l;
            password = pw;
            DateObtentionPermis = dp;
            NumeroPermis = np;
        }
    }
}

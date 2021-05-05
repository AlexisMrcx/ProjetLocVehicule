using System;

namespace ProjetLocVehicule
{
    public class Client
    {
        // Remplace le nom et prénom
        public string Login { get; }
        public string Password { get; }
        // --------------------------------
        private DateTime DateNaissance;
        private DateTime DateObtentionPermis;
        public string NumeroPermis { get; }


        public Client(string l, string pw, DateTime dn, DateTime dop, string nP)
        {
            Login = l;
            Password = pw;
            DateNaissance = dn;
            DateObtentionPermis = dop;
            NumeroPermis = nP;
        }


        public int Age
        {
            get
            {
                DateTime today = DateTime.Now;
                int age = today.Year - DateNaissance.Year; //Age théorique

                if (today.Month > DateNaissance.Month)
                {
                    age -= 1;
                }
                else if (today.Month == DateNaissance.Month)
                {
                    if (today.Day <= DateNaissance.Day)
                    {
                        age -= 1;
                    }
                }

                return age;
            }
        }

        public bool Permis
        {
            get
            {
                if (DateObtentionPermis.CompareTo(new DateTime(1, 1, 1)) == 0){
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}

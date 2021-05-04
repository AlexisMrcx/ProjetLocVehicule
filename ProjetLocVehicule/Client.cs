using System;

namespace ProjetLocVehicule
{
    public class Client
    {
        public string Login { get; }
        public string Password { get; }


        public Client(string l, string pw)
        {
            Login = l;
            Password = pw;
        }

    }
}

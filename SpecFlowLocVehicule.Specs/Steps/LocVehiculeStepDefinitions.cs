using TechTalk.SpecFlow;
using ProjetLocVehicule;
using System.Collections.Generic;
using FluentAssertions;
using System;
using System.Linq;

namespace SpecFlowLocVehicule.Specs.Steps
{
    [Binding]
    public sealed class LocVehiculeStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        private string _username;
        private string _password;
        private string _errorMessage;

        private Client clientConnecte;
        private DateTime _dateDebut;
        private DateTime _dateFin;
        private List<Vehicule> _vehiculesDisponibles;
        private Vehicule vSelectionne;
        private bool _reservationState;
        private int _estimationKm;
        private float _totalFacture;
        private int _ajustementKm;

        private ProjetLocVehicule.Location _loc;
        private Fake.FakeDataLayer _fakeDataLayer;

        public LocVehiculeStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _fakeDataLayer = new Fake.FakeDataLayer();
            _loc = new Location(_fakeDataLayer);
        }

        #region Background
        [Given(@"clients existants")]
        public void GivenClientsExistants(Table table)
        {
            foreach(TableRow row in table.Rows)
            {
                DateTime dateNaissance= new DateTime(int.Parse(row[2].Substring(6)),
                    int.Parse(row[2].Substring(3, 2)),
                    int.Parse(row[2].Substring(0, 2)));

                //Valeur 0 si il n'a pas le permis
                DateTime dateObtentionPermis = new DateTime(1,1,1);
                if (row[3].Length > 0)
                {
                    dateObtentionPermis = new DateTime(int.Parse(row[2].Substring(6)),
                    int.Parse(row[2].Substring(3, 2)),
                    int.Parse(row[2].Substring(0, 2)));
                }                

                _fakeDataLayer.Clients.Add(new Client(row[0], row[1], dateNaissance,dateObtentionPermis, row[4]));
            }
        }

        [Given(@"voitures existantes")]
        public void GivenVoituresExistantes(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _fakeDataLayer.Vehicules.Add(new Vehicule(row[0], row[1], row[2], row[3], int.Parse(row[4]),float.Parse(row[5]),float.Parse(row[6])));
            }
        }

        [Given(@"reservations existantes")]
        public void GivenReservationsExistantes(Table table)
        {
            Client c;
            Vehicule v;
            DateTime dd, df;
            int estimation;

            foreach(TableRow row in table.Rows)
            {
                v= _fakeDataLayer.Vehicules.SingleOrDefault(_ => _.Immatriculation == row[0]);
                c =_fakeDataLayer.Clients.SingleOrDefault(_ => _.Login == row[1]);
                dd = new DateTime(int.Parse( row[2].Substring(6)), int.Parse(row[2].Substring(3,2)), int.Parse(row[2].Substring(0, 2)));
                df = new DateTime(int.Parse(row[3].Substring(6)), int.Parse(row[3].Substring(3,2)), int.Parse(row[3].Substring(0, 2)));
                estimation = int.Parse(row[4]);

                _fakeDataLayer.Reservations.Add(new Reservation(c, v, dd, df, estimation));
            }
            
        }
        #endregion


        #region Given
        [Given(@"login est ""(.*)""")]
        public void GivenLoginEst(string username)
        {
            _username = username;
        }

        [Given(@"password est ""(.*)""")]
        public void GivenPasswordEst(string password)
        {
            _password = password;
        }

        [Given(@"client connecté est")]
        public void GivenClientConnecteEst(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                clientConnecte = _fakeDataLayer.Clients.SingleOrDefault(_ => _.Login == row[0] && _.Password == row[1]);
            }
        }


        [Given(@"date de début est jour:(.*) mois:(.*) annee:(.*)")]
        public void GivenDateDeDebutEstJourMoisAnnee(int jour, int mois, int annee)
        {
            _dateDebut = new DateTime(annee, mois, jour);
        }

        [Given(@"date de fin est jour:(.*) mois:(.*) annee:(.*)")]
        public void GivenDateDeFinEstJourMoisAnnee(int jour, int mois, int annee)
        {
            _dateFin = new DateTime(annee, mois, jour);
        }

        [Given(@"voiture sélectionné est ""(.*)""")]
        public void GivenVoitureSelectionneEst(string immatriculation)
        {
            vSelectionne = _fakeDataLayer.Vehicules.SingleOrDefault(_ => _.Immatriculation == immatriculation);
        }

        [Given(@"pour une estimation de (.*) km")]
        public void GivenPourUneEstimationDeKm(int km)
        {
            _estimationKm = km;
        }

        [Given(@"client a fait (.*) km de difference avec l'estimation")]
        public void GivenClientAFaitKmDeLEstimation(int ajustement)
        {
            _ajustementKm = ajustement;
        }

        #endregion


        #region When
        [When(@"essaie de se connecter")]
        public void WhenEssaieDeSeConnecter()
        {
            _errorMessage = _loc.ConnectUser(_username, _password);
        }

        [When(@"valide son choix")]
        public void WhenValideSonChoix()
        {
            _vehiculesDisponibles = _loc.SearchVehiculesDisponible(_dateDebut, _dateFin);
        }

        [When(@"client reserve")]
        public void WhenClientReserve()
        {
            _reservationState = _loc.CreateReservation(clientConnecte, vSelectionne, _dateDebut, _dateFin,_estimationKm);
        }

        [When(@"client ramène véhicule")]
        public void WhenClientRameneVehicule()
        {
            Reservation r = _fakeDataLayer.Reservations.SingleOrDefault(_ => _.Client.Login == clientConnecte.Login &&
            _.Vehicule.Immatriculation == vSelectionne.Immatriculation);

            _totalFacture = _loc.CalculFacture(r, _ajustementKm);
        }

        #endregion


        #region Then
        [Then(@"connexion est refusé")]
        public void ThenConnexionEstRefuse()
        {
            _loc.UserConnected.Should().BeFalse();
        }

        [Then(@"connexion est réussie")]
        public void ThenConnexionEstReussie()
        {
            _loc.UserConnected.Should().BeTrue();
        }

        [Then(@"le message d'erreur est ""(.*)""")]
        public void ThenLeMessageDErreurEst(string errorMessage)
        {
            _errorMessage.Should().Be(errorMessage);
        }             


        [Then(@"dates sont corrects")]
        public void ThenDatesSontCorrects()
        {
            _loc.DateCorrect.Should().BeTrue();
        }

        [Then(@"dates ne sont pas corrects")]
        public void ThenDatesNeSontPasCorrects()
        {
            _loc.DateCorrect.Should().BeFalse();
        }


        [Then(@"les voitures disponibles sont")]
        public void ThenVoituresDisponiblesSont(Table table)
        {
            List<Vehicule> vAttendue = new List<Vehicule>();

            foreach (TableRow row in table.Rows)
            {
                Vehicule v = _fakeDataLayer.Vehicules.SingleOrDefault(_ => _.Immatriculation == row[3]);
                vAttendue.Add(v); 
            }

            _vehiculesDisponibles.Should().BeEquivalentTo(vAttendue);
        }

        [Then(@"reservation est créée")]
        public void ThenReservationEstCreee()
        {
            _reservationState.Should().BeTrue();
        }

        [Then(@"reservation n'est pas créée")]
        public void ThenReservationNEstPasCreee()
        {
            _reservationState.Should().BeFalse();
        }

        [Then(@"reservation de ""(.*)"" a une estimation de (.*) km")]
        public void ThenReservationDeAUneEstimationDeKm(string nomReservation, int km)
        {
            Reservation r = _fakeDataLayer.Reservations.SingleOrDefault(_ => _.Client.Login == nomReservation);

            _estimationKm.Should().Be(km);
        }

        [Then(@"la facture a un total de (.*) €")]
        public void ThenLaFactureAUnTotalDe(int prix)
        {
            _totalFacture.Should().Be(prix);
        }

        #endregion 



    }
}

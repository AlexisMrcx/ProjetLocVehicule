using TechTalk.SpecFlow;
using ProjetLocVehicule;
using System.Collections.Generic;

namespace SpecFlowLocVehicule.Specs.Steps
{
    [Binding]
    public sealed class LocVehiculeStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        private List<Client> clients = new List<Client>();
        private Client client;

        public LocVehiculeStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        #region Given
        [Given("données utilisateur suivantes")]
        public void GivenDonneesUtilisateurSuivantes(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        #endregion


        #region When
        [When("le client s'inscrit")]
        public void WhenLeClientSInscrit()
        {
            ScenarioContext.Current.Pending();
        }

        #endregion


        #region Then
        [Then("Le client est ajouté dans la base de donnée")]
        public void ThenLeClientEstAjouteDansLaBaseDeDonnee()
        {
            ScenarioContext.Current.Pending();
        }

        #endregion
    }
}

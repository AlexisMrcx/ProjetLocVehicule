Feature: LocVehicule

Scenario: Un nouveau client s'inscrit sur le site
	Given données utilisateur suivantes
		| nom   | prenom | login | password | naissance  | datePermis | numeroPermis |
		| Speck | Didier | Did   | ier      | 20/06/1997 | 28/06/2015 | 1685467      |
	When le client s'inscrit
	Then Le client est ajouté dans la base de donnée
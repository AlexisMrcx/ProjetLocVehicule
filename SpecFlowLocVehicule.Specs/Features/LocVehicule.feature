Feature: LocVehicule

Background:
	Given clients existants
		| login  | password |
		| alexis | sixela   |
		| didier | reidid   |
	Given voitures existantes
		| marque  | modele | couleur | immatriculation |
		| Renault | Clio   | Grise   | AA-123-BB       |
		| Peugeot | 207    | Grise   | CC-456-DD       |
	Given reservations existantes
		| immatricualtion | nom client | date debut | date fin   |
		| CC-456-DD       | didier     | 02-01-2022 | 10-01-2022 |

#Connexion
Scenario: Connexion du client avec login non reconnue
	Given login est "Nalexis"
	And password est "Nsixela"
	When essaie de se connecter
	Then connexion est refusé
	And le message d'erreur est "Login non reconnue"

Scenario: Connexion du client avec login reconnue
	Given login est "alexis"
	And password est "sixela"
	When essaie de se connecter
	Then connexion est réussie

Scenario: Connexion du client avec login reconnue et password non reconnue
	Given login est "alexis"
	And password est "sila"
	When essaie de se connecter
	Then connexion est refusé
	And le message d'erreur est "Password incorrect"

#------------------------------------------------
#Reservation Date
Scenario: Client rentre les dates qu'ils souhaitent réserver
	Given client connecté est
		| login  | password |
		| alexis | sixela   |
	And date de début est jour:4 mois:5 annee:2022
	And date de fin est jour:6 mois:5 annee:2022
	When valide son choix
	Then dates sont corrects
	And les voitures disponibles sont
		| marque  | modele | couleur | immatriculation |
		| Renault | Clio   | Grise   | AA-123-BB       |
		| Peugeot | 207    | Grise   | CC-456-DD       |

Scenario: Client rentre les dates qu'ils souhaitent réserver mais elles ne sont pas corrects
	Given client connecté est
		| login  | password |
		| alexis | sixela   |
	And date de début est jour:4 mois:5 annee:2022
	And date de fin est jour:1 mois:5 annee:2022
	When valide son choix
	Then dates ne sont pas corrects

Scenario: Client rentre les dates qu'ils souhaitent réserver et un véhicule n'est pas disponible
	Given client connecté est
		| login  | password |
		| alexis | sixela   |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	When valide son choix
	Then dates sont corrects
	And les voitures disponibles sont
		| marque  | modele | couleur | immatriculation |
		| Renault | Clio   | Grise   | AA-123-BB       |

#------------------------------------------------
#Choix voiture
Scenario:  Client choisis de réserver une voiture disponible
	Given client connecté est
		| login  | password |
		| alexis | sixela   |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "AA-123-BB"
	When client reserve
	Then reservation est créée
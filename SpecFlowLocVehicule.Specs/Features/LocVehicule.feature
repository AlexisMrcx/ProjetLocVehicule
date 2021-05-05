Feature: LocVehicule

Background:
	Given clients existants
		| login  | password | date naissance | date obtention permis | numero permis |
		| alexis | sixela   | 20-05-1999     | 21-05-2017            | 121556        |
		| didier | reidid   | 19-09-1984     | 20-08-2010            | 6923243       |
		| robin  | nibor    | 06-05-2005     |                       |               |
		| emile  | elime    | 19-03-2003     |                       |               |
		| lucas  | sacul    | 16-06-2002     | 17-07-2020            | 46538         |
		| lea    | ael      | 03-08-1998     | 20-08-2017            | 65333         |
	Given voitures existantes
		| marque  | modele | couleur | immatriculation | cv fiscaux | prix reservation | prix au km |
		| Renault | Clio   | Grise   | AA-123-BB       | 4          | 100              | 1          |
		| Peugeot | 207    | Grise   | CC-456-DD       | 5          | 150              | 1.2        |
		| Bmw     | M3     | Noire   | EE-789-FF       | 8          | 300              | 2.5        |
		| Audi    | Rs3    | Noire   | GG-369-HH       | 13         | 800              | 5          |
		| Citroen | Ds3    | Blanche | II-258-JJ       | 6          | 200              | 1.5        |
	Given reservations existantes
		| immatricualtion | nom client | date debut | date fin   | estimation |
		| CC-456-DD       | didier     | 02-01-2022 | 10-01-2022 | 1000       |
		| II-258-JJ       | lea        | 02-03-2021 | 15-03-2021 | 800        |

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
		| Bmw     | M3     | Noire   | EE-789-FF       |
		| Audi    | Rs3    | Noire   | GG-369-HH       |
		| Citroen | Ds3    | Blanche | II-258-JJ       |

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
		| Bmw     | M3     | Noire   | EE-789-FF       |
		| Audi    | Rs3    | Noire   | GG-369-HH       |
		| Citroen | Ds3    | Blanche | II-258-JJ       |

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

Scenario:  Client choisis de réserver une voiture disponible mais à déjà une réservation
	Given client connecté est
		| login  | password |
		| didier | reidid   |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "AA-123-BB"
	When client reserve
	Then reservation n'est pas créée

Scenario:  Client choisis de réserver une voiture disponible et n'est pas majeur
	Given client connecté est
		| login | password |
		| robin | nibor    |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "AA-123-BB"
	When client reserve
	Then reservation n'est pas créée

Scenario:  Client choisis de réserver une voiture disponible et n'est pas majeur et n'a pas le permis
	Given client connecté est
		| login | password |
		| robin | nibor    |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "AA-123-BB"
	When client reserve
	Then reservation n'est pas créée

Scenario:  Client choisis de réserver une voiture disponible et n'a pas le permis
	Given client connecté est
		| login | password |
		| emile | elime    |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "AA-123-BB"
	When client reserve
	Then reservation n'est pas créée

Scenario:  Client choisis de réserver une voiture de 8 chevaux fiscaux et a moins de 21 ans
	Given client connecté est
		| login | password |
		| lucas | sacul    |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "EE-789-FF"
	When client reserve
	Then reservation n'est pas créée

Scenario:  Client choisis de réserver une voiture de 13 chevaux fiscaux et a entre 21 et 25 ans
	Given client connecté est
		| login  | password |
		| alexis | sixela   |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "GG-369-HH"
	When client reserve
	Then reservation n'est pas créée

#------------------------------------------------
#Calcul prix
Scenario:  Client choisis de réserver une voiture disponible pour 500km
	Given client connecté est
		| login  | password |
		| alexis | sixela   |
	And date de début est jour:3 mois:1 annee:2022
	And date de fin est jour:11 mois:1 annee:2022
	And voiture sélectionné est "AA-123-BB"
	And pour une estimation de 500 km
	When client reserve
	Then reservation est créée
	And reservation de "alexis" a une estimation de 500 km

Scenario: Client ramène la voiture qu'il a loué
	Given client connecté est
		| login | password |
		| lea   | ael      |
	And voiture sélectionné est "II-258-JJ"
	When client ramène véhicule
	Then la facture a un total de 1400 €

Scenario: Client ramène la voiture qu'il a loué avec un réajustement négatif
	Given client connecté est
		| login | password |
		| lea   | ael      |
	And voiture sélectionné est "II-258-JJ"
	And client a fait -200 km de difference avec l'estimation
	When client ramène véhicule
	Then la facture a un total de 1100 €

Scenario: Client ramène la voiture qu'il a loué avec un réajustement positif
	Given client connecté est
		| login | password |
		| lea   | ael      |
	And voiture sélectionné est "II-258-JJ"
	And client a fait 200 km de difference avec l'estimation
	When client ramène véhicule
	Then la facture a un total de 1700 €
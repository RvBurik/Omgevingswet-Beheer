# Omgevingswet-Beheer
Beheerapplicatie betreft de opdracht Omgevingswet.
Push nooit van je local branch naar de master branch, je commits worden gerevert. 
Altijd minimaal 2 reviewers op elke push naar development.
Commit local bij elke toegevoegde functionaliteit, commit naar development als alle functionaliteiten voltooid en getest zijn. 
Development branch mag nooit gebroken worden. 
Aan het eind van elke iteratie wordt development overgebracht naar de master.

BELANGRIJK:
Nadat git push naar development is uitgevoerd, moet je ZELF een pull request aanmaken in Github. Als je dit niet doet zijn je commits nog niet final en zijn ze ook niet beschikbaar voor je teamgenoten.

****COMMANDS****

Clone repos:
git clone <clone link ssh(te vinden in Github)>

Bekijk status:
git status (toont welke files gewijzigd zijn)

Bekijk huidige branches:
git branch

Maak een nieuwe local branch:
git checkout -b <naam van de branch>

Voeg bestanden toe aan een (toekomstige commit):
git add <naam van je bestand> OF git add . (voegt alle bestanden toe)

Maak een nieuwe commit (bij elke functionaliteit)
git commit -m '<Leg uit wat je hebt gedaan>'

Pushen naar een branch
git push origin <naam van branch> 

Op het moment dat teamleden nieuwe branch hebben aangemaakt:
git fetch (haalt nieuwe branches op)

Haal wijzigingen van remote branches op:
git pull origin <naam van de branch>



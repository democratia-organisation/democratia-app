# Test

- [Test](#test)
  - [ViewModel](#viewmodel)
    - [HomeViewModel](#homeviewmodel)

## ViewModel

### HomeViewModel

- attributs :
  - internaute : internaute connecté
  - navigationService : service permettant la navigation entre les pages
  - localizationService : service permettant d'internationaliser le texte
  - _internautePret : attribut pour attendre la fin de la récupération des données
  - Groupes : listes de groupe où l'internaute est membre affiché dans l'écran
  - listeRecu : listes des groupes recu de la base de donnée
- méthodes :
  1. `HomeViewModel(INavigationService , IEnumerable<IClient>, ILocalizationService)` : constructeur adapté pour recevoir les services externes
  2. `ApplyQueryAttributes(IDictionnary<string, object>) : void` : méthode pour récupérer les paramètres d'une navigations
  3. `InitializeAsync() : void`
  4. `NavigationTapped(string) : Task` méthode pour naviguer vers une autre page
- spécifications des tests
| fonctions | tests abstraits | tests concrets | résultat attendu |
| :------ | :-----: | :------: | ---: |
| 3 | rempli Groupes des groupes de l'utilistauers | listesRecu : 3 groupe;  internaute avec le mail : <vincent.leclerc@example.com> | Grouepes : 3 groupe |
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" |  |

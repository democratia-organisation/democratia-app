# Test

- [Test](#test)
  - [ViewModel](#viewmodel)
    - [HomeViewModel](#homeviewmodel)
    - [ConnectableViewModel](#connectableviewmodel)
    - [MainViewModel](#mainviewmodel)
    - [PreferenceViewModel](#preferenceviewmodel)
    - [ModifierGestionViewModel](#modifiergestionviewmodel)
    - [HomeGestionViewModel](#homegestionviewmodel)
    - [CreationViewModel](#creationviewmodel)
    - [PremiereCreationViewModel](#premierecreationviewmodel)
    - [DeuxiemeCreationViewModel](#deuxiemecreationviewmodel)
    - [TroisiemeCreationViewModel](#troisiemecreationviewmodel)
  - [Model](#model)
    - [Internaute](#internaute)
    - [Groupe](#groupe)
    - [Thematique](#thematique)
    - [Proposition](#proposition)
  - [View](#view)
    - [HomePage](#homepage)
    - [ConnectablePage](#connectablepage)
    - [MainPage](#mainpage)
    - [ModifierGestionPage](#modifiergestionpage)
    - [HomeGestionPage](#homegestionpage)
    - [CreationPage](#creationpage)
    - [PremiereCreationPage](#premierecreationpage)
    - [DeuxiemeCreationPage](#deuxiemecreationpage)
    - [TroisiemeCreationPage](#troisiemecreationpage)
  - [Service](#service)
    - [InternauteClient](#internauteclient)
    - [GroupeClient](#groupeclient)
    - [ThematiqueClient](#thematiqueclient)
    - [PropositionClient](#propositionclient)
  - [Utils](#utils)

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

### ConnectableViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

### MainViewModel

- attributs :
  - Client et client : connexion http avec le serveur
  - LocalizationService : service pour accéder à la traduction d'une page
- méthodes :  
  1. `RecupererInformationConnexion(string) : List<object>` : récupère les informations récupérer depuis le serveur
  2. `EnregistrerModele<T>(T) : void where T : IModel` met en cache le modèle en parammètre
  3. `RetrouverModele<T>() : Task<T>` retrouve le modèle mit en cache
- spécifications des tests

| fonctions | tests abstraits | tests concrets | résultat attendu |
| :------ | :-----: | :------: | ---: |
| 2 | renvoie une liste contenant les objets donnée en paramètre | string : "[{id_internaute : 1}]" | Liste de taille 1 |
| 2 | jette une erreur si le string est mal formaté | string : "[{id_internaute : 1]" | une erreur |

### PreferenceViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

### ModifierGestionViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

### HomeGestionViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

### CreationViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

### PremiereCreationViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

### DeuxiemeCreationViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

### TroisiemeCreationViewModel

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
| 4 | navigue vers une page donnée | paramètre 1 : "CreerPage" | void |

## Model

### Internaute

- attributs :

### Groupe

- attributs :
  
### Thematique

- attributs :
  
### Proposition

- attributs :
  
## View

### HomePage

- attributs :
  
### ConnectablePage

- attributs :
  
### MainPage

- attributs :
  
### ModifierGestionPage

- attributs :
  
### HomeGestionPage

- attributs :
  
### CreationPage

- attributs :
  
### PremiereCreationPage

- attributs :
  
### DeuxiemeCreationPage

- attributs :
  
### TroisiemeCreationPage

- attributs :
  
## Service

### InternauteClient

- attributs :

### GroupeClient

- attributs :
  
### ThematiqueClient

- attributs :
  
### PropositionClient

- attributs :

## Utils

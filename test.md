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
    - [DécideurViewModel](#décideurviewmodel)
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
    - [DécideurPage](#décideurpage)
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
  - Shell.Current : service permettant la navigation entre les pages
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

### ConnectableViewModel

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

### MainViewModel

- attributs :
  
- méthodes :  
  1. `RecupererInformationConnexion(string) : List<object>` : récupère les informations récupérer depuis le serveur
  2. `EnregistrerModele<T>(T) : void where T : IModel` met en cache le modèle en parammètre
  3. `RetrouverModele<T>() : Task<T>` retrouve le modèle mit en cache
- spécifications des tests

| fonctions | tests abstraits | tests concrets | résultat attendu |
| :------ | :-----: | :------: | ---: |
| 2 | renvoie une liste contenant les objets donnée en paramètre | string : "[{id_internaute : 1}]" | Liste de taille 1 |

### PreferenceViewModel

- attributs :
  - internaute : internaute connecté
  - Shell.Current : service permettant la navigation entre les pages
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
  - Shell.Current : service permettant la navigation entre les pages
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
  - Shell.Current : service permettant la navigation entre les pages
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
  - Shell.Current : service permettant la navigation entre les pages
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
  - Shell.Current : service permettant la navigation entre les pages
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
  - Shell.Current : service permettant la navigation entre les pages
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
  - Shell.Current : service permettant la navigation entre les pages
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

### DécideurViewModel

## Model

### Internaute

### Groupe

### Thematique

### Proposition

## View

### HomePage

### ConnectablePage

### MainPage

### ModifierGestionPage

### HomeGestionPage

### CreationPage

### PremiereCreationPage

### DeuxiemeCreationPage

### TroisiemeCreationPage

### DécideurPage

- Choisir une proposition
  - accepter cette proposition
  - refuser une proposition
- Afficher le prix de chacune des propositions
- Modifier chacune des propositions
- Limiter le budget
  - Définir un budget annuel maximal
  - Définir un budget par thème
  - La somme des budget par thème ne doit pas dépasser le budget annuel
- classer les propositions
  - classer selon le critère de popularité
  - classer selon le prix
  - classer selon les intéractions sur la propositions
- cas limites :
  - un groupe n'a pas de thématique : texte pour le préciser
  - une thématique n'a pas de proposition : texte pour le préciser
  - une ou des propositions n'a pas de donnée pour un critère: rendre le classement impossible pour ce critère

## Service

### InternauteClient

- attirbuts :
- méthodes :
- spécifications des tests

### GroupeClient

- attributs :
  
### ThematiqueClient

- attributs :
  
### PropositionClient

- attributs :

## Utils

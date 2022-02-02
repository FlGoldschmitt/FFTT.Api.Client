# Client C# pour l'API de la Fédération Française de Tennis de Table

Cette librairie C# permet d'appeler l'ensemble des méthodes exposées par l'API de la FFTT (plus connue sous le nom d'interfaces Smartping).  
Elle vous permettra entre autres de récupérer des informations sur les clubs, les joueurs (classements, statistiques), les poules ou encore les résultats des différentes épreuves (par équipes et individuelles).

Vous trouverez la spécification technique détaillée dans le projet à cet emplacement : https://github.com/FlGoldschmitt/FFTT.Api.Client/blob/develop/Documentations/Sp%C3%A9cifications%20techniques%20de%20API%20Smartping%202.0.pdf.

## Comment utiliser les interfaces Smartping
Toute personne désirant utiliser ces APIs doit au préalable en faire la demande à la Fédération Française de Tennis de Table.  
Vous trouverez toutes les informations nécessaires à l'adresse ci-dessous, ainsi qu'un formulaire à remplir dans lequel vous présenterez votre projet : https://www.fftt.com/site/mediatheque/autres-medias/api.

Après validation de votre projet, vous recevrez un e-mail avec vos codes d'accès.

## Installation du package NuGet

### Package Manager

```
    Install-Package FFTT.Api.Client -Version 1.0.0
```
### .NET CLI

```
    dotnet add package FFTT.Api.Client --version 1.0.0
```
### PackageReference

```
    <PackageReference Include="FFTT.Api.Client" Version="1.0.0" />
```
## Utilisation de la librairie

### Initialisation des paramètres d'accès

Pour chaque requête à l'API, il est nécessaire de passer 4 paramètres dans les entêtes (id, serie, tm, tmc).  
* id : code d'application reçu par e-mail  
* serie : numéro de série aléatoire de 15 chiffres/lettres [0-9][A-Z] permettant d'identifier un utilisateur    
* tm : timestamp  
* tmc : timestamp encodé à partir de votre mot de passe reçu par e-mail  

La librairie fait le travail pour vous. Il vous suffit d'instancier un nouvel objet "FfftCredentials" en lui passant en paramètre votre id, votre mot de passe et en option, votre numéro de série aléatoire (si rien n'est précisé, celui-ci sera généré automatiquement. Il vous faudra ensuite le sauvegarder pour éviter d'en générer un nouveau à chaque fois).  

```c#
  FfttCredentials _credentials = new FfttCredentials("[CODE_APPLI]", "[PASSWORD]", "[SERIAL_NUMBER]");
```

### Instanciation d'un client

Pour appeler les différentes méthodes de l'API, il vous faut instancier un client en lui donnant en paramètres vos "credentials":  

```c#
  using (var client = new FfttClient(_credentials))
```

A noter qu'avant de pouvoir appeler l'ensemble des méthodes, il est nécessaire d'appeler la méthode 'UserInitializationAsync()', afin de valider vos accès avec votre numéro de série. **Cette méthode n'est à appeler qu'une seule fois**.

```c#
  UserInitialization user;
  using (var client = new FfttClient(_credentials))
  {
      user = await client.UserInitializationAsync();
    
      // Le résultat doit retourner "true"
      Assert.IsTrue(user.IsAuthorized);
  }
```

### Exemple d'utilisation

Récupération de l'ensemble des joueurs du club de Montfort L'Amaury TT US

```c#
  IEnumerable<Player> players;
  using (var client = new FfttClient(_credentials))
      players = await client.GetPlayersByClubAsync("08781094");
```

## Liste complète des méthodes de l'API

### Initialisation  
* UserInitializationAsync : vérification et validation d'un nouvel utilisateur d'API  

### Clubs  
* GetClubByNumberAsync : retourne un club à partir de son numéro  
* GetClubByPostalCodeAsync : retourne un club à partir d'un code postal  
* GetClubsByDepartmentAsync : retourne les clubs à partir d'un numéro de département
* GetClubsByCityAsync : retourne les clubs à partir d'un nom de ville
* GetClubDetailsAsync : retourne les informations détaillées d'un club à partir de son numéro  

### Organisations  
* GetOrganizationsAsync : retourne les organisations à partir d'un type (F=Fédération, Z=Zone, L=Ligue, D=Département)  

### Epreuves
* GetEventsTypeAsync : retourne les épreuves pour une organisation donnée  

### Divisions
* GetDivisionsAsync : retourne les divisions pour une organisation et une épreuve données

### Poules
* GetGroupsAsync : retourne les poules d'une division  
* GetGroupRankingAsync : retourne le classement d'une poule dans une division donnée  
* GetInitialGroupRankingAsync : retourne le classement initial d'une poule dans une division donnée  

### Rencontres
* GetGamesResultAsync : retourne les résultats d'une poule
* GetGamesResultAsync : retourne les résultats d'une ou plusieurs poules
* GetGameInformationsAsync : retourne le détail d'une rencontre

### Equipes
*  GetTeamsAsync : retourne les équipes d'un club

### Joueurs
* GetPlayerByLicenceAsync : retourne les informations d'un joueur de la base Spid à partir de sa licence
* GetPlayersByClubAsync : retourne les informations des joueurs de la base Spid d'un club
* GetPlayersByNameAsync : retourne les informations des joueurs de la base Spid à partir d'un nom
* GetPlayersRankingByClubAsync : retourne les informations des joueurs de la base classement d'un club
* GetPlayersRankingByNameAsync : retourne les informations des joueurs de la base classement à partir d'un nom
* GetPlayerRankingAsync : retourne les informations de classement d'un joueur
* GetPlayerLicenceAsync : retourne les informations de licence d'un joueur
* GetPlayerLicenceAndMonthlyProgressAsync : retourne les informations de licence d'un joueur et sa progression mensuelle
* GetPlayerGamesMySqlAsync : retourne la liste des rencontres de la base MySql d'un joueur
* GetPlayerGamesSpidAsync : retourne la liste des rencontres de la base Spid d'un joueur
* GetPlayerHistoryAsync : retourne l'historique du classement du joueur

### Actualités FFTT
* GetNewsAsync : retourne les dernières actualités publiées sur le site de la FFTT

# Star Commander

> Fun showcase of software architecture presented as a game yet inspired by domain driven design.

![GitHub](https://img.shields.io/github/license/RyanNieuwoudt/StarCommander?style=flat-square)
![GitHub Release Date](https://img.shields.io/github/release-date/RyanNieuwoudt/StarCommander?style=flat-square)

---

## Example

```csharp
public class WelcomeCaptainAboard : IWhen<PlayerSignedIn>
{
	readonly IPlayerService playerService;

	public WelcomeCaptainAboard(IPlayerService playerService)
	{
		this.playerService = playerService;
	}

	public async Task Handle(PlayerSignedIn @event)
	{
		await playerService.BoardShip(@event.Player);
	}
}
```

---

## Installation

### Prerequisites

-   Install [.NET Core SDK](https://dotnet.microsoft.com/download).
-   Install [Node.js](https://nodejs.org/en/).
-   Install [yarn](https://legacy.yarnpkg.com/en/docs/install).

### Clone

-   Clone this repo to your local machine.

### Dependencies

Open a terminal in the folder where the repository is cloned.

```
dotnet restore
```

Then type:

```
cd StarCommander
cd ClientApp
yarn install
cd ..
```

### Run

In the 'StarCommander' sub-folder, type:

```
dotnet watch run
```

Open [https://localhost:5001](https://localhost:5001;http://localhost:5000) in a web browser.

---

## FAQ

> Is Star Commander a game?

No, the game element provides an interesting and well known domain that has no bearing on any real projects I have ever worked on. Here, the problem space is...space!

---

## License

[MIT License](https://github.com/RyanNieuwoudt/StarCommander/blob/master/LICENSE)

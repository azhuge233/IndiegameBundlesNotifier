# IndiegameBundlesNotifier

> EpicBundle has not been updated for a while, so I made this as an alternative.

A CLI tool fetches free games info from IndiegameBundles, sends notification through Telegram, Bark, Email, QQ, PushPlus, DingTalk, PushDeer, Discord and MeoW.

Demo Telegram Channel [@azhuge233_FreeGames](https://telegram.me/azhuge233_FreeGames)

## Build

Install dotnet 10.0 SDK first, you can find installation packages/guides [here](https://dotnet.microsoft.com/download).

Follow commands will publish project as a executable file.

```shell
git clone https://github.com/azhuge233/IndiegameBundlesNotifier.git
cd IndiegameBundlesNotifier
dotnet publish -c Release -p:PublishDir=/your/path/here -r [win-x64/osx-x64/linux-x64/...]
```

## Usage

Set your telegram bot token and chat ID in config.json.

Check [wiki](https://github.com/azhuge233/IndiegameBundlesNotifier/wiki/Config-Description) for more explanations, only notify varaibles are available for this project.

### Repeatedly running

The program will not add while/for loop, it's a scraper. To schedule the program, use cron.d in Linux(macOS) or Task Scheduler in Windows.

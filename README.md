# README.md

## How It Works

I've created a simple mock scene that serves as a starting point for testing the task functionality. The core of the implementation revolves around:

- **Setup**: Quickly created `DemoEntryPoint` that substitutes for a proper UI system, with a quick Zenject installer setup for popup manager and my other planned services. Put together a simple button layout: one for opening a popup, and another to clear image cache.


- **Popup System**: Created and designed a popup prefab according to my vision of the task, hooked it up to an addressable group. Wrote `BasePopup` that covers basic common functionality, then implemented `LeaderboardPopup` and `LeaderboardEntryView` to populate a scrollable list of entries with specific designs according to players' positions (diamond, gold, silver, bronze, default).


- **Data & Services**: Wrote a `LeaderboardEntry` data model and `LeaderboardService` for retrieving and parsing JSON from a local file. It's written in a way that would make it easy to hypothetically replace part of its functionality for getting JSON with a WebRequest or something.


- **Avatar Handling**: Created `AvatarProviderService` with caching functionality that generates a hash for file names based on a link and checks if the file is already cached.


- **Polish**: Wrote summaries, created and configured asmdefs, and tested on different resolutions.

## Design Choices & Assumptions

- I had to update `SimplePopupManager` to use my persistent canvas. The solution is not ideal, but it was done for speed.

- I thought about adding a loading icon for when the JSON is parsing (assuming a hypothetical WebRequest) and about having a fallback image for when the avatar fails to load, but I also wanted to avoid overengineering and get it done quicker.

- I didn't want to change `PopupManagerService` a lot, but if I had more time and reason, I would actually rewrite it, at least because it breaks if there's a duplicate `OpenPopup` call in quick succession. It happens because the key is added asynchronously after loading an addressable.

- Drawn an extremely ugly but not repulsive "loading icon" png.
![Alt text](SpeechGram.png?raw=true "Title")

A Microsoft Hololens application that aids users with hearing disabilities have conversations in everyday life. Using Microsoft Azure and the Hololens, our app performs three main functions:

* Translates conversation speech into text, which is displayed to the disabled user
* Allows the disabled user select one of many pre-defined responses, which is automatically spoken out loud
* Identifies a single speaker (in a group of speakers) and tags each chat message with the speaker's name

This application was developed in a 3-week timeframe for the 2017's Microsoft Imagine Cup.

## Build and Installation

We have packaged the app into a UWP appx package, but it requires a lot of work in order to sideload onto the HoloLens. We recommend that users just use the latest build found in the `Build` folder of the [latest release](https://github.com/ndass6/Speechgram/releases) and continue on with the instructions. Otherwise, users have two other options:

1. You can find the UWP appx package in the `Speechgram_1.0.1.0_Test` folder of the latest release [found here](https://github.com/ndass6/Speechgram/releases). You will need to follow [this guide](https://docs.microsoft.com/en-us/windows/uwp/packaging/packaging-uwp-apps#sideload-your-app-package) to sideload the package onto the HoloLens
2. You can download the source code on this repo and follow the `Build source code` instructions below

Common instructions for building the source code or running the app from the latest build:

1. Install **[Visual Studio 2015 Update 3](https://www.visualstudio.com/downloads/)**
2. Ensure that **Tools** and **Windows 10 SDK** is selected when installing Visual Studio
3. Install **[Unity 5.5.1f1](https://unity3d.com/get-unity/download/archive)**
4. Insure that the HoloLens is connected to the Internet. This is not needed for building the app, but is needed during application use

### Run the App from the Build 
1. Download the latest build release of this app [found here](https://github.com/ndass6/Speechgram/releases)
2. Extract the zip to an empty folder
3. Open up the solution file (`Speechgram.sln` in the `Build` folder) in Microsoft Visual Studio
4. Connect the HoloLens to your computer. Make sure your laptop is authorized to access your device (you might be prompted to enter a pin on your computer, follow the instructions on the pop-up display)
5. Select `Release, x86, Device` for the three dropdowns in the top to deploy to the Microsoft Hololens

If the build fails, make sure to follow the messages in the console to fix the issues. Otherwise, just try building again :)

![Alt text](Deploy.png?raw=true "Title")

### Build source code

These instructions are not needed if you use the latest build of the app (above).

1. Clone this repo to an empty folder
2. Create a folder named `Build`
3. Open up the project in Unity
4. Click on `File -> Build & Run`
5. Configure the following build settings:
   * SDK = Windows 10
   * Target Device = HoloLens
   * UWP Build Type = D3D
   * Build and Run on = Local Machine 
   * Check Copy References
   * Check Unity C# Projects
6. Click `Build and Run`
7. Follow the instructions above in the `Run the App from the Build File` from step 3

![Alt text](Build Settings.png?raw=true "Title")

If the instructions above don't work:
* [Here](https://developer.microsoft.com/en-us/windows/holographic/install_the_tools) are general instructions on how to install the tools.
* [Here](https://developer.microsoft.com/en-us/windows/holographic/holograms_100) are general instructions on how to build and run the project.

## Usage Instructions

Before starting, make sure you understand what an **[Air-tap](https://developer.microsoft.com/en-us/windows/holographic/gestures#press_and_release)** is.

As soon as it's launched, a chatroom opens up, and the application will start listening via microphone. It will wait for a speaker to start talking, and will record the entire time (max 30 seconds). As soon as the speaker is done talking, speech-to-text and Azure Voice Recognition is started. In about 5 seconds, both processes should finish. The message should appear in the chatroom (with the message tagged with the speaker's id).

A user can respond to messages by going to the quick-response menu. Air-tap anywhere on the chat room and the menu should open up. Some buttons will respond automatically, others are folders that contain similiar responses. The menu is color-coded like the following:
* Red: App navigation buttons
* Blue: Folders for other reply buttons
* Green: Quick-response buttons that are said out loud using text-to-speech

To add a new speaker, have the speaker start talking. While talking, the user should go to the response menu and click on the `Register New User` button. Wait until the speaker is done talking, then registration should take place automatically.

For now, be wary of the following time limits:
* A speaker must talk for at least 3 seconds to be identified
* A speaker must talk for at least 10 seconds to be registered
* Between speaker transitions, wait for at least 2 seconds

## Technologies Used

* **Microsoft HoloLens**: Platform for the application to run on
* **Unity Game Engine**: Used to develop the HoloLens application
* **Microsoft Azure Cognitive Services Speaker Recognition API**: Used to identify a single speaker for each message recorded
* **Microsoft Azure App Service**: A broker-layer to connect to our database from the HoloLens
* **Microsoft Azure SQL Database**: Stores persistent information about speakers, such as names and speaker Ids

## Contact

Contact any one of the contributors for additional information!
* Teju Nareddy: tnareddy3@gatech.edu
* Nathan Dass: ndass6@gatech.edu
* Jonathan Chen: jonathanchen@gatech.edu

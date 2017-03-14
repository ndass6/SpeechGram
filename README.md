![Alt text](SpeechGram.png?raw=true "Title")

A Microsoft Hololens application that aids users with hearing disabilities have conversations in everyday life. Using Microsoft Azure and the Hololens, our app performs three main functions:

* Translates conversation speech into text, which is displayed to the disabled user
* Allows the disabled user select a response, which is automatically sent back in speech
* Identifies a single speaker out of a group and tags each chat message with the speaker's name

This application was developed in a 3-week timeframe for the 2017's Microsoft Imagine Cup.

## Build and Installation

1. Install **[Visual Studio 2015 Update 3](https://www.visualstudio.com/downloads/)**
2. Ensure that **Tools** and **Windows 10 SDK** is selected when installing Visual Studio.
3. Install **[Unity 5.5.1f1](https://unity3d.com/get-unity/download/archive)**
4. Download the latest release of this code ([found here](https://github.com/ndass6/Speechgram/releases))
5. Create a folder name `Build`
6. Open up the project in Unity
7. Click on `File -> Build & Run`
8. Configure the following build settings: 
   * SDK = Windows 10
   * Target Device = HoloLens
   * UWP Build Type = D3D
   * Build and Run on = Local Machine 
   * Check Copy References
   * Check Unity C# Projects
9. Click `Build and Run`
10. Open up the solution file (found in the `Build` folder) in Microsoft Visual Studio
11. Connect the HoloLens to your computer. Make sure your laptop is authorized to access your device. 
12. Select `Release, x86, Device` to deploy to the Microsoft Hololens

If the above instructions do not make sense:
* [Here](https://developer.microsoft.com/en-us/windows/holographic/install_the_tools) are general instructions on how to install the tools.
* [Here](https://developer.microsoft.com/en-us/windows/holographic/holograms_100) are general instructions on how to build and run the project.

## Usage Instructions

Before starting, make sure you understand what an **[Air-tap](https://developer.microsoft.com/en-us/windows/holographic/gestures#press_and_release)** is.

As soon as it's launched, a chatroom opens up, and the application will start listening via microphone. It will wait for a speaker to start talking, and will record the entire time (max 30 seconds). As soon as the speaker is done talking, speech-to-text and Azure Voice Recognition is started. In about 5 seconds, both processes should finish. The message should appear in the chatroom (with the message tagged with the speaker's name).

A user can respond to messages by going to the quick-response menu. Air-tap anywhere on the chat room and the menu should open up. Some buttons will respond automatically, others are folders that contain similiar responses. As soon as one is clicked, it is said out loud.

To add a new speaker, have the speaker start talking. While talking, the user should go to the response menu and click on the `Register New User` button. Wait until the speaker is done talking, then registration should take place automatically.

For now, be wary of the following time limits:
* A speaker must talk for at least 3 seconds to be identified
* A speaker must talk for at least 10 seconds to be registered
* Between speaker transitions, wait for at least 3 seconds

## Technologies Used

* **Microsoft HoloLens**: Platform for the application to run on
* **Unity Game Engine**: Used to develop the HoloLens application
* **Microsoft Azure Speaker Recognition API**: Used to identify a single speaker for each message recorded
* **Microsoft Azure App Service**: A broker-layer to connect to our database from the HoloLens
* **Microsoft Azure SQL Database**: Stores persistent information about speakers, such as names and speaker-ids

## Contact

Contact any one of the contributors for additional information!

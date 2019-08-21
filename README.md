# ElectronBacgroundTaskApp

The objective of this application is to run in the background and send a toast notification in max 15 mins upone any change in the file.

Later this exe will be consumed by the electron application,which eventually will be available to download from the window store.[Yet to be done]

Tools used:
	I have used VS2017 IDE
How to build and run IT.

1. open BackgroundTaskApp.sln file in the vs2017 IDE.
2. build BackgroundTaskApp [right click on this project and select build]
3. build BackgroundTaskTriggerer [right click on this project and select build]
4. make BackgroundTaskTriggerer as an active project
5. Right click on BackgroundTaskTriggerer project and click on  Deploy
6. you will see BackgroundTaskTriggerer  in the start up list

How to Debug ?
1. Right click on the area where you see all the tools.
2. From the POP up menu click on the Debug Location
3. Press F5
4. you will notice a `Lifecycle Events` in the drop down.
5. Click on TimeZoneTriggerTest.[Make sure you have a break point in Run() function in BackgroundTask.cs


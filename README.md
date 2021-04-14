# Flight Inspection App


*Video Demo:* https://www.youtube.com/watch?v=Vkt-fiWrFTI&feature=youtu.be

This project is a WPF Desktop Application that allows to inspect a flight simulation.
Using our Flight Inspection App, you will be able to inspect the flight simulation provided by [*Flight Gear*](https://www.flightgear.org/).  
Flight Gear is an open-source program which is free for download for anyone onto their computer Mac, Windows, and Linux. The program simulates a plane in different modes.

The Flight Gear will provide the video and the simulation and our app will provide controls to interact with, and means to inspect the ongoing flight.

<img src="https://github.com/ApplicationFlight/FlightSimulatorApp/blob/master/ReadmeMedia/inAction.PNG" width="600" height="300"/>


## Getting Started

### Prerequisities

Before starting using our app, there are a few steps you need to follow:
1. Download the Flight Gear for your OS, at: https://www.flightgear.org/download/.
    We reccomend version 2020.3.6.
2. Open FG, go into `Settings > Additional Settings`. Paste there:

--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm=null


![instruction](https://user-images.githubusercontent.com/58266146/114233455-03010d00-9986-11eb-99c7-0de94bbd0447.png)

3. Place the `play_small.xaml` file inside appropriate folder (see video).
4. Make sure to have a correct CSV file to upload at start. We provide with an example under: `> Resources > Documents > anomaly_flight.csv`


### Compiling and Running

1. Download the project and open it in Visual Studio (we used Visual Studio 2019  16.3.4).

2. Open FG and click on the Fly button.

3. Start the App from Visual Studio


## Deployment
### Technologies
- .Net framework to create a GUI with WPF
- MVVM architecture in a multi-threaded environment in C#
- TCP Connection to send data to Flight-Gear
- Dll provided by the user for Anomaly Inspection in C#

### MVVM & App Architechture
The UML diagram is accessible in the `> UML` folder.

The app has 3 main folders:
#### Model
Responsible for the communication with Flight Gear via TCP Socket. It sends the data inside the provided CSV file, so that the FG can display a video representation of it. The model notifies the View Model when data is changed.
#### View Model
Responsible for the connection between Model and View. Here you will find one ViewModel for each UserControl, in addition to one dedicated to Page2, Page3.
#### View
Responsible for the View. In this folder you will find all the  `XAML` files UserControls.
We advise you to move the window of the App and the window of Flight Gear in a way that half of the screen shows the FG and the other half shows the App:
Note: the app is indendent for a screen of 15.60-inch and more.

## Other Folders:
- Plug-ins: here we provide two examples of ready dlls to use
- ReadmeMedia: here we inserted the images used for this readme
- Resources: here we provide images and background for for the view
    - Documents: here we provide example of all CSV files in the project + `playback_small.xaml`
- UML: here you will find the UML diagram

### Functionalities

At the start, you will be prompted with a request to upload a CSV file.  Each line in the CSV describes one 0.1s of the flight simulation (see at Getting Started).

On the first page, we provide the following controls:
- VideoPlayer - for the video visualization of the flight
- DashBoard - shows selected data types changing in realtime during flight
- Joystick - simulates a real joystick based on the real-time position of the plane
- Anomaly Inspection - it allows you to inspect anomalies occurring during the flight in real-time. Clicking on a specif anomaly will link to that time in the video

<img src="https://github.com/ApplicationFlight/FlightSimulatorApp/blob/master/ReadmeMedia/page2.PNG" width="400" height="450" />


On second page, we provide the following inspect tools:

- List of features - clicking on the list will update with relavant data on graphs
- Graph 1 (Selected) - shows values of the data selected over time
- Graph 2 (Most Correlative)- shows values of most correaltive data selected over time
- Graph 3 (Regression)- shows linear regression among values selected and most correlative, in addition to points added in the last 30 seconds

<img src="https://github.com/ApplicationFlight/FlightSimulatorApp/blob/master/ReadmeMedia/graphsimageapp.PNG" width="350" height="400" />

### DLL
The anomaly detection algorithm is provided by the user. During runtime, you have an option to add a dll, which will serve as algorithm to detced anomalies.   

*Format*   
The DLL inserted needs to be in `C#`. The name of the DLL needs to be: `DLL_{algorithm name}.dll`.      
Inside, there needs to be a `Class` called: `DLL_{algorithm name}`, within a `namespace` also called `DLL_{algorithm name}`.      
Inside the `Class`, there needs to be a function called: `{algorithm name}`, receveing 3 path string parameters.  
Such as: `void regression(string CSVreg, string CSVanomalt, CSVoutput)`.
The `{algorithm name}` function is void, but the dll will create a new file with a list of the anomalies found.

*Functionality*     
The `{algorithm name}` function the path of the 3 following files:
- the train file: to learn from and find CorrelatedFeatures
- the anomaly flight: to detect Anomalies against CorrelatedFearures
- output file: where the dll needs to output the Anomalies found

All of these fields will be analized by *our* application and showed for the user to further inspect.     
The description is purely subjective, each algorithm might have different representation, is up to the user to chose what description/additional info fits the best. Keep in mind this description will appear on screen, (see video demo).

*Bonus*
As said above, the dll provided must be in `C#`. But, if you wish to use `C++`, it's also possible! In fact, we provide two examples of dlls written in `C++`. ( `> DLLResources`).
In order to use `C++` dlls, the main function in the `C++` dll will still be: `{algorithm name}`.
You can then create a simple `C#`, composed by one `Class` and `namespace` (as described above), add a reference to the `C++` algorithm, and import there *only the {algorithm name} function*.   
Here's a video on how write and use a `C++` dll: https://www.youtube.com/watch?v=3efOjwKb9p4.

Our app will then received the dll in `C#` of the simple project created.


## Authors
- [Sara Spagnoletto](https://github.com/saraspagno)
- Eva Hallermeier
- Samuel Memmi
- Gali Seregin

Hope you enjoy the experience and the product!
# Flight Inspection App

Our application serves as a client for Flight Gear. (See more at: https://www.flightgear.org/).
Flight Gear is a free, open-source program which is free for download for anyone onto their computer Mac, Windows, and Linux. The program simulates a plane in different modes. 

Through our Flight Inspection App, you will be able to inspect the flight simulation provided by Flight Gear. 
The Flight Gear will provide the video and the simulation. Our app will provide controls to interact with, and means to inspect the ongoing flight. 

## Functionalites

At the start, you will be prompted with a request to upload a CSV file.  Each line on the CSV describes one 0.1s of the flight simulation (see at Getting Started). 

On the first page, we provide the following controls:
- VideoPlayer - for the video visualization of the flight
- DashBoard - shows selected data types changing in realtime during flight
- Joystick - simulates a real joystick based on the real-time position of the plane
- Anomaly Inspection - it allows you to inspect anomalies occurring during the flight in real-time. Clicking on a specif anomaly will link to that time in the video

On second page, we provide the following inspect tools:

- List of data - clicking on the list will update with relavant data on graphs
- Graph1 - shows values of the data selected over time
- Graph2 - shows values of most correaltive data selected over time
- Graph3 - shows linear regression among values selected and most correlative, in addition to points added in the last 30 seconds

### Getting Started

Before you can start using our app, there are a few steps you need to follow. 
1. Download the Flight Gear for your OS, at: https://www.flightgear.org/download/
2. Go into `Properties > Additional Properties`. Paste there:
```
--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm=null
```
3. Place the `play_small.xaml` file inside appropriate folder (see video).
4. Make sure to have a correct CSV file to upload at start. We provide with an example under: `> Resources > Document > anomaly_flight.csv`

### DLL
The anomaly detection algorithm is provided by the user. During runtime, you have an option to add a dll, which will serve as algorithm to detced anomalies.
**Format:**
The DLL inserted needs to be in `C#`. The name of the DLL needs to be: DLL_{algorithm name}.dll.
Inside, there needs to be a class called: DLL_{algorithm name}, within a namspace also called DLL_{algorithm name}.
Inside the Class, there needs to be a function called: {algorithm name}, receveing 3 string parameters.
**Functionality**
The  {algorithm name} function takes the input a train file, the anomaly flight, and the output file. It needs to learn form the train file, check against the anomaly flight for anomalies, and return in the output file a list of anomalies, in the following format: {feature1 name}, {feature2 name}, {line of anomaly in file}, {algorithm description/addition info}.
All of these fields will be analized by our application and showed for the user to further inspect.
Note: the description if purely subjective, each algorithm might have different representation, is up to the user to chose what descriptio/additional info fits the best.
**Bonus**
As said above, the dll provided must be in `C#`. But, f you wish to use `C++` code it's also possible. In fact, we provide two examples of dlls written in `C++`. ( `> DLL_Resources`). 



## Built With:

During the work on this project we worked with big number of platforms ,some of them :
Net framework to create a GUI App for Flight-Gear , MVVM architecture in a multi-threaded environment, TCP Client to send data to Flight-Gear.

## Our app  has 3 main parts: 

### Model :
responsible for communication with Flight-Gear via a TCP Socket : it pass the data of a flight and FG can show the flight with the Simulator, the model also notify the View-Model when data changed.
### View Model:
responsible to change data and notify different views about all the changes. According to the view model, the different view can show to the user different data that change all over the time
This App permits to analyses data of the flight during all the flight time.
### Veiw :
The user has possibility to control the speed of the video and to read it depends on his needs. 
In real-time, the  user has access to the Dashboard that notify in real time principles data of the flight.
The user can see changes of data also with a joystick that show values of aileron, elevator with the button of the middle and values of throttle and rudder via sliders.

## Compiling and Running:
First, you need to download Flight Gear on your computer and configure several settings:


![instruction](https://user-images.githubusercontent.com/58266146/114233455-03010d00-9986-11eb-99c7-0de94bbd0447.png)




In additional settings you need to write:

--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm==null

We believe that in this uneasy period each of us wanted to fly to another place and for that the app is here .Some interesting feature we have here:
1.	Please give a CSV file to the app. The XML file should contains relevant info about the CSV file (such as columns names …). 
After the CSV upload, the app will show you the plane in each moment. The plane image is done using an outside app called flight Gear, it’s a simulator used by amateur pilots to learn how to control an airplane.

2.	You are  able to move in video/showing of the flight simulation with commands like YouTube. When moving the bar also the data showing will  change.


3.	Here  please pay attention to the steering wheel it is just like a joystick and will move with the plane control. The joystick cannot be used, it shows what’s happening in real-time.

4.	If you want you can increase the velocity of the video to a point where nothing special is happening, and vice versa So you can control the velocity of the video with a dedicated control option. 

5.	See next to the flight image and the joystick also parameters like:

a.	The height of the flight 

b.	The velocity of the flight (airspeed)

c.	The directions of the flight

d.	The indices of yaw, roll, pitch    

![plane](https://user-images.githubusercontent.com/58266146/114234061-ec0eea80-9986-11eb-909f-11782bcf9a46.png)

6.	You can choose one single data from the CSV file and analyses it. Once you choose a specific data there will an updated graph of that data. Where the X-axes is time, and the Y-axes is the one data.

7.	Once you choose to analyses one specific data, it will appear a graph of another one specific data, the most correlative one. Also, this graph will be updated with time.

8.	Once you choose to analyses one specific data, and also see the second correlative one, you will see the regression line between them.

FOR EXAMPLE:

 ![graph](https://user-images.githubusercontent.com/58266146/114234215-24162d80-9987-11eb-97e2-9f3b8b39b6a7.png)
                                          

9. you can see clearly shown exception during the flight and when and where the error happened which will be achieved using 2 algorithms both you can see in the first course of advanced programming 1 which is taught by one of the best lectors  professor  Eliahu Khalastchi :
11.	Based on regression
12.	Based on the circle points 





This algorithm will be used separately like plug-ins, like DLLs that load dynamically in the app after it is uploaded. As a user you will get the DLL file external to the app and load it dynamically during the run.
Before we take of don’t you forget to listen carefully to the flight instructions below:

https://www.youtube.com/watch?v=mcKGCS4rSl8 
 
We wish you all a pleasant flight 
 The crew 
Sara ,Eva ,Gali and Shmuel

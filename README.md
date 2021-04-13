# Flight Simulator App

Flight Gear is a free, open-source program which is free for download for anyone  onto their computer  Mac, Windows, and Linux. The program simulates a plane in different modes. It has many features such as viewing the different graphs and even the remote of our plane!
you can look at the website: https://www.flightgear.org/

Our application serves as a client to the Flight Gear. Our program has two main functionalities
yaw , roll and pitch for more information about them please look here:

https://www.youtube.com/watch?v=pQ24NtnaLl8 

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

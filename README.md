# VivoosVR

## License

[**BSD 3-Clause**](https://opensource.org/licenses/BSD-3-Clause)

See [LICENSE](https://github.com/Ideasis/VivoosVR/blob/master/LICENSE)

## About

VivoosVR is a virtual treatment tool to assist in exposure therapy where participants are placed in a computer-generated 3D virtual world and guided through the selected environments, situations and conditions. 

Physiological values of the participants are constantly monitored via GSR and pulse sensors. The system is designed to be used by qualified therapists and our target participants are young people suffering from phobias and social anxiety. 

Simulation worlds have been created using Unreal 4 game engine and we support popular VR HMDs such as Oculus Rift and HTC Vive. 
We also have plans to optimize the system for mobile VR solutions in the near future. 

VivoosVR is being developed in cooperation with a team of psychology / psychiatry experts.  

## Prerequisites

* [SQL Server 2014 (or above) Express](https://download.microsoft.com/download/5/E/9/5E9B18CC-8FD5-467E-B5BF-BADE39C51F73/SQLServer2017-SSEI-Expr.exe)
    * If you have problems while installing, this [link](http://help.dugeo.com/m/Insight4-0/l/438911-downloading-and-installing-sql-server) can guide you through installation

* [SQL Server Management Studio 2017 (or above)](https://go.microsoft.com/fwlink/?linkid=2043154)

* [Neulog API](https://neulog.com/Downloads/neulog_api_ver_002b.exe)

* Acquire and install either or both of the following products:
    * [HTC Vive](https://support.steampowered.com/steamvr/HTC_Vive/)
    * [Oculus Rift](https://www.oculus.com/download_app/?id=1582076955407037)

* [Visual Studio](https://visualstudio.microsoft.com/tr/downloads/?rr=https%3A%2F%2Fwww.google.com%2F)     

## Git initialisation 

### GitHub Desktop

1. Download and install [GitHub Desktop](https://central.github.com/deployments/desktop/desktop/latest/win32)

2. Run GitHub Desktop Application and click on File -> Clone Repository:
    ![Screenshot of Clone Repository](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/GitHubDesktop%20_Installation_SS/SS1_CloneRepository.png)

3. Click on "URL" tab and copy the following link of the VivoosVR Repo and paste it on the URL part:
    ![Screenshot of PasteRepoLink](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/GitHubDesktop%20_Installation_SS/SS2_PasteRepoLink.png)

4. Set local path of the repo

5. Click on "Clone" to complete connecting to VivoosVR Repo using GitHub Desktop.

## Installation

### SQL Server Management Studio

1. Locate "Database" folder in your local repo path and copy "Vivoos.bak" file in this folder to "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\Backup"

2. Open the SQL Server Management Studio and login. In the Object Explorer tab, right click on "Databases" and select "Restore Database".

    ![2](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/2.jpg)
    
3. Select Device part, click add and find "Vivoos.bak" file
    
    ![3](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/3.JPG)
    
    ![4](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/4.JPG)

    ![5](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/5.JPG)

4. 	Close the SQL Server Management Studio Application.

### Scenario Installation 

If you wish to use the scenarios below in VivoosVR, you need to execute following steps:

1. Click on the link which belongs to the scenario you would like to add.
2. Download the zip in the link
3. Extract the zip into the VivoosVR Scenario folder (Default Path: C:\\Scenarios) (If the folder does not exist, you need to create the folder manually)

Links to the latest version of the scenarios can be found below:

1. [Fear of Dogs - Park Scenario - version 0.9.2](https://www.dropbox.com/sh/it30lye2e79l9qg/AACcIpCw0AaHZSBn_ESCSKrba?dl=0) 
2. [Fear of Height - Balcony Scenario - version 0.9.6](https://www.dropbox.com/sh/ahwumxzlpyncv2m/AABXWAHCyCWYKH5lOI8HhYAga?dl=0)
3. [Fear of Spiders - Living Room Scenario - version 0.7.1](https://www.dropbox.com/sh/vgy91s4kx15g0xc/AAAEaEp6MuMg_eBYXzafwghUa?dl=0)
4. [Fear of Dark - Childs Room Scenario - version 0.9.2](https://www.dropbox.com/sh/calap5uvcd1z4l3/AAAhWt5SSCt4mcN-nVcvmOyxa?dl=0)
5. [Social Anxiety - Classroom - version 0.9.6](https://www.dropbox.com/sh/rsmu6jaa30qjiqm/AAAd4YyPPkPOPpAJ6RVTFxj2a?dl=0)
6. [Fear of Height - Elevator Scenario - version 0.7.5](https://www.dropbox.com/sh/vv9ui87iqyh2dee/AAAZNtMJeHXpVsgBa-Kx4aYna?dl=0)

WebVR related links can be found below:

1. Fear of Height - Balcony Scenario - WebVR:
	1. Build: [Fear of Height - Balcony Scenario - WebVR version 0.8.0](https://www.dropbox.com/sh/lkpk96ujhm44h6e/AABFP2dSAdalOsYrVswlkaTfa?dl=0) 
	1. Try: [Fear of Height - Balcony Scenario](http://www.ideasis.com.tr/Content/Balkon/index.html)

## How to Use 

### VivoosVR

1.  If you click the exe of the program (VivoosVR\VivoosVR\bin\debug), login form welcomes you. 
    Default user: Username: user Password:123 
    Default admin: Username: admin Password:123
    You can add new users in admin page.
    
![6](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/6.JPG)

2.	For using the scenarios, you can download them in the github. Default path is C:/Scenarios. You can create a folder named Scenarios and copy the scenarios in this folder. If you want to copy another path, please login with the admin password and change the scenarios path. (Login -> Find the scenario and click edit -> Change the path)  

![7](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/7.JPG)

![8](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/8.JPG)

3.	If you login with the default user password, the first screen you will see is the patient page. You can see a list of your patients in this page. You can also add new patients or you can edit existing patient information. 

![9](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/9.JPG)

4.	You can click the “Session List” of one of your patients. In this page, you can see the sessions of the selected patient. You can also download the data (GSR and Pulse data due to the time) of the session or delete the session. 

![10](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/10.JPG)

5.	By clicking the “New Session” button in the “Sessions Page”, you can choose and begin any of the existing exposure scenarios. 

![11](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/11.JPG)

6.	In the “New Session Controls” page, you can trigger and control scenario events. Moreover, you can track the anxiety level of the patient through GSR and pulse graphs. 

![12](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/13.JPG)

![13](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/12.JPG)

### Neulog

Currently VivoosVR communicates with the following modules and sensors of the Neulog: 

* [Heart Rate & Pulse Logger Sensor](https://neulog.com/heart-rate-pulse)

* [GSR Logger Sensor](https://neulog.com/gsr)

You need to perform following tasks to make Neulog modules and sensors work correctly with VivoosVR:

1.	Plug USB module, Heart Rate & Pulse Logger Sensor and GSR Logger Sensor to each other and plug them to your computer via USB Port (Order of the modules are not important!)

![Neulog_PlugModules](https://github.com/Ideasis/VivoosVR/blob/master/Neulog_SS/Neulog_PlugModules_SS.jpeg)

2.	Attach handle of the heart rate and pulse sensor to little finger of your right hand in a way that glassy side of the handle touches the bottom side of your finger

![Neulog_HeartRate](https://github.com/Ideasis/VivoosVR/blob/master/Neulog_SS/Neulog_HeartRate_SS.jpeg)

3.	Attach the handles of the gsr module to ring finger and index finger of your right hand in a way that metal part of the handles touches the bottom side of your finger

![Neulog_GSR](https://github.com/Ideasis/VivoosVR/blob/master/Neulog_SS/Neulog_GSR_SS.jpeg)

## Testing

### How To Examine UE4 Project Stats

You need Unreal Engine 4 in order to examine the given stat files in the folder (Recommended version: 4.18 or above).

Assuming you have the stated Unreal Engine 4 version, you need to perform the following steps:

1. Open any compatible Unreal Engine 4 project

2. Open "Session Frontend" panel from Window > Developer Tools > Session Frontend
![1](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/UE4ProjectStats_SS/SS_SessionFrontEndOpening.png)

3. Click on "Profiler" tab
![2](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/UE4ProjectStats_SS/SS_ClickOnProfiler.png)

4. Click on "Load" button and locate the desired stat file
![3](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/UE4ProjectStats_SS/SS_ClickOnLoad.png)

[Click here for the project stat files](https://www.dropbox.com/sh/dqk3hrw7geeqrhw/AADFIQme4_-vGRnH9F0rV_Lfa?dl=0)
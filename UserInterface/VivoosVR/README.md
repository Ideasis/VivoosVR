# VivoosVR

## License

[**BSD 3-Clause**](https://opensource.org/licenses/BSD-3-Clause)

See [LICENSE](https://github.com/Ideasis/VivoosVR/blob/master/UserInterface/LocalVersion/VivoosVR/LICENSE)

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

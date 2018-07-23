## Getting Started

In order to use VivoosVr User Interface, you must do some steps:

1.	Firstly, you can download Microsoft SQL Server Express which is free in the [link](https://www.microsoft.com/en-us/download/details.aspx?id=55994)
2.	Then, you can download SQL Server Management Studio which is also free in the [link](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017)

    Note: SQL Server and management studio setup is bit complicated so you can watch helpful videos on youtube. 

3.	Then, you can download VivoosVR User Interface Local Version in github. In the setup folder, there is a database folder. In this folder, there is a Vivoos.bak file. Copy it to C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\Backup
![1](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/1.JPG)

4.	Open the SQL Server Management Studio and login. In the Object Explorer tab, right click the Databases and select Restore Database.

![2](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/2.jpg)

5.	Select Device part, click add and find the Vivoos.bak file (previously do in the third step).
![3](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/3.JPG)

![4](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/4.JPG)

![5](https://github.com/Ideasis/VivoosVR/blob/master/Readme%20Images/Guideline_SS/5.JPG)

6.	If there is no error, Vivoos VR User Interface database is ready to use. Please close the Management Studio.
7.	Finally, download Neulog Api in the [link](https://neulog.com/software/)

## VivoosVR User Interface
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

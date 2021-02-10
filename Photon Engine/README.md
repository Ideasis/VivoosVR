VivooVR Photon/Oculus Integration

Make a copy of the project which is going to be modified.
Step 1: Delete all unnecessary Unity packages from Unity package manager. Especially Multiplayer HLAPI
Step 2: Download Photon PUN 2 Free version if it does not exist in the project.
Step 3: Setup Photon ID (App ID) inside Unity. Photon setup setting can be found on Window -> Photon Unity Networking -> PUN Wizard. Important Photon user account mail: vivoosvr@ideasis.com.tr and password: Vivoos2020
Step 4: Download Photon Voice 2 and setup voice ID. Voice ID is different from PUN2 ID.
Step 5: Add Photon View/Animator View/Transform View to the non-static, animated objects that needs to be synched. Examples Spider/Dog/Cat/NPCs. Photon View IDs must be equal for both server and client side if it is set by developer.
Step 6: Put the prefabs that will be spawned after the scene starts to the Resources folder and photon spawn function not Unity’s.
Step 7: If every step so far is done, separate the project into server and client side. (Copy and paste the project and give name accordingly)
Server-Side Preparations
Step 8: Server does NOT need VR setting so remove Oculus/Open VR. If SteamVR exists in the project remove it first then remove Oculus/OpenVR to avoid package importing loop.
Step 9: Import server “photonserver.unitypackage” to the project.
Step 10: There is a scene titled Room, add this scene to Scenes in Build and make its index 0. It must be the first scene that Unity to load.
Important: Do not touch Voice Manager and Connector scripts if you do not know Photon.
Connector has game version field that must be same for client and server.
Connector has nickname field that must be different. (On the period of writing of this document, we are using the same nicknames for all rooms/lobbies, this may have to be changed in the future )
Step 11: Remove Unity Socket Connector script and add Gam(Game) Controller to Scene/Game Master/Controller and assign a spawn point in Gam(Game) controller for camera to spawn location.
Step 12: Remove Main Camera from server. There is a PlayerGO prefab in Resource folder. Its camera must be tagged as “Main Camera”.
Client-Side Preparations 
Step 13: Remove Oculus/Open/SteamVR from the project and untick Virtual Reality support in Player > XR Settings.
Step 14: Install XR Plugin Management and tick the Oculus option for Android.
Step 15: Install “photonclient.unitypackage ”
Step 16: There is a scene titled Room, add this scene to Scenes in Build and make its index 0. It must be the first scene that Unity to load.
Step 17: Remove camera in the scene.
Step 18: Delete Unity Socket Connector from the project and add Gam(Game) Controller to Scene/Game Master/Controller gameobject. Assign a spawn point for camera spawn location on Gam(Game) Controller.
Step 19: Change Build Setting to Android. After that change Color Space to Gama from Linear if Unity asks.
Step 20: Change Android API Level to Minimum 23
Important Photon Things
IPunObservable: An interface class that helps synch classes on network
Photoview ownership can be changed. EXP: Server can “Take Over” PlayerGO to initiate Reset Camera function or change players’ position. Look at Germofobia for more examples.
MonoBehaviourPUN => Allows coders to use PhotonNetwork.RaiseEvent() function. Look at FearOfCats for examples.
OnPhotonSerializeView() function that resides in IPunObservable allows to make Custom Views.






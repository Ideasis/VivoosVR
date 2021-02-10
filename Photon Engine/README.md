# VivooVR Photon/Oculus Integration

Make a copy of the project which is going to be modified.<br>
<b>Step 1:</b> Delete all unnecessary Unity packages from Unity package manager. Especially Multiplayer HLAPI <br>
<b>Step 2:</b> Download Photon PUN 2 Free version if it does not exist in the project.<br>
<b>Step 3:</b> Setup Photon ID (App ID) inside Unity. Photon setup setting can be found on Window -> Photon Unity Networking -> PUN Wizard.<br>
<b>Step 4:</b> Download Photon Voice 2 and setup voice ID. Voice ID is different from PUN2 ID.<br>
<b>Step 5:</b> Add Photon View/Animator View/Transform View to the non-static, animated objects that needs to be synched. Examples Spider/Dog/Cat/NPCs. Photon View IDs must be equal for both server and client side if it is set by developer.<br>
<b>Step 6:</b> Put the prefabs that will be spawned after the scene starts to the Resources folder and photon spawn function not Unity’s.<br>
<b>Step 7:</b> If every step so far is done, separate the project into server and client side. (Copy and paste the project and give name accordingly)<br>
<br>
<b>Server-Side Preparations</b><br>
<br>
<b>Step 8:</b> Server does NOT need VR setting so remove Oculus/Open VR. If SteamVR exists in the project remove it first then remove Oculus/OpenVR to avoid package importing loop.<br>
<b>Step 9:</b> Import server “photonserver.unitypackage” to the project.<br>
<b>Step 10:</b> There is a scene titled Room, add this scene to Scenes in Build and make its index 0. It must be the first scene that Unity to load.<br>
<b>Important:</b> Do not touch Voice Manager and Connector scripts if you do not know Photon.<br>
Connector has game version field that must be same for client and server.<br>
Connector has nickname field that must be different. (On the period of writing of this document, we are using the same nicknames for all rooms/lobbies, this may have to be changed in the future )<br>
Step 11: Remove Unity Socket Connector script and add Gam(Game) Controller to Scene/Game Master/Controller and assign a spawn point in Gam(Game) controller for camera to spawn location.<br>
Step 12: Remove Main Camera from server. There is a PlayerGO prefab in Resource folder. Its camera must be tagged as “Main Camera”.<br>
<br>
Client-Side Preparations <br>
<br>
<b>Step 13:</b> Remove Oculus/Open/SteamVR from the project and untick Virtual Reality support in Player > XR Settings.<br>
<b>Step 14:</b> Install XR Plugin Management and tick the Oculus option for Android.<br>
<b>Step 15:</b> Install “photonclient.unitypackage ”<br>
<b>Step 16:</b> There is a scene titled Room, add this scene to Scenes in Build and make its index 0. It must be the first scene that Unity to load.<br>
<b>Step 17:</b> Remove camera in the scene.<br>
<b>Step 18:</b> Delete Unity Socket Connector from the project and add Gam(Game) Controller to Scene/Game Master/Controller gameobject. Assign a spawn point for camera spawn location on Gam(Game) Controller.<br>
<b>Step 19:</b> Change Build Setting to Android. After that change Color Space to Gama from Linear if Unity asks.<br>
<b>Step 20:</b> Change Android API Level to Minimum 23<br>
<br>
<b>Important Photon Concepts</b><br>
<br>
<b>IPunObservable:</b> An interface class that helps synch classes on network<br>
Photoview ownership can be changed. EXP: Server can “Take Over” PlayerGO to initiate Reset Camera function or change players’ position. Look at Germofobia for more examples.<br>
<b>MonoBehaviourPUN:</b> Allows coders to use PhotonNetwork.RaiseEvent() function. Look at FearOfCats for examples.<br>
OnPhotonSerializeView() function that resides in IPunObservable allows to make Custom Views.<br>






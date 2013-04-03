geoauth client for WP7
==================

This is the GeoAuth WP7 client. It is designed for use on WP7 (but should would on WP8). Background location services are only available in WP8, so it is not implemented in this client.

Instructions
==================
1. Install Windows Phone SDK 7.1 or higher
2. Open the GeoAuthApp.sln file with Visual Studio.
3. Press F5 to build and then run the app in the windows phone emulator
4. The lower buttons (up and down arrow) select the GPS accuracy and the X cancels GPS tracking.
5a. When "check-in" or "create region" are clicked and there is no device id (the device is not registered) it will take you to the registration page. To register the device you can use any group member's x500 and the password "porkchop" and name the device.
5b. You can disable registration for testing by going to GeoAppStorage and uncommenting the deviceId in the object constructor.
6. To change the device location you follow the instructions here http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202933(v=vs.105).aspx

Additional Features
==================
Local database stores location region information. Currently the database is populated whenever a region is successfully added, and is checked to prevent duplicate location requests of the same name. A future enhancement would be to query the local database and display if the user is in defined region. A UI to manage regions could also be created.

On location change it checks if the GPS distance (3200 meters from last check in) or the GPS last check in time (15 minutes since check in) have been exceeded, if so it will automatically checkin to the server. There is currently no UI for a user to modify these settings, but it could be created in the future.

Third Party Libraries/code
=====================
The summary tags of the classes denote the source of some code if any. Below is listing of code used:
PostClient.cs: http://www.codeproject.com/Articles/198651/An-HTTP-POST-client-for-Windows-Phone-7
Images and pages/mainpage:  http://code.msdn.microsoft.com/wpapps/Location-Service-Sample-6b9ef410

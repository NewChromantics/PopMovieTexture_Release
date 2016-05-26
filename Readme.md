PopMovie
=============================================

High performance, cross-platform, Movie-Texture plugin for Unity.
---------------------------------------------
+ [popmovie.xyz](http://www.popmovie.xyz)
+ We welcome all questions, bug reports &amp; feature requests (some features may already exist and be hidden), simply [email us at help@popmovie.xyz](mailto:help@popmovie.xyz), send me a tweet to [@soylentgraham](http://www.twitter.com/soylentgraham)
+ We have [public bug tracking & feature requests](https://github.com/NewChromantics/PopMovieTexture_Release/issues) on our github issues page
+ The [Github releases page](https://github.com/NewChromantics/PopMovieTexture_Release/releases) has detailed release notes for all public releases dating back to December 2015
+ Unity example projects for the latest release (sometimes more examples than in the store) are [on the github page](https://github.com/NewChromantics/PopMovieTexture_Release/tree/master/Example%20Projects). We welcome pull requests and demo submissions!
+ Supports iOS, Android, Windows & OSX. Virtual Reality, common movie files, subtitle(.srt) files, images, Kinects, webcams & cameras, window streaming.

Downloads & Release Notes
---------------------------------------------
[Download from the Unity asset store!](https://www.assetstore.unity3d.com/#!/content/59097). This may not be the very latest version as submissions to the Asset store take time to approve.

Below is a list of recent releases and their release notes. (The [complete history of public watermarked releases and their release notes](https://github.com/NewChromantics/PopMovieTexture_Release/releases) are on the github releases page.

+ v[0.1.7](https://github.com/NewChromantics/PopMovieTexture_Release/releases/tag/v0.1.7.15a71c8)
+ v[0.1.6](https://github.com/NewChromantics/PopMovieTexture_Release/releases/tag/v0.1.6.a711d51) Current [asset store](https://www.assetstore.unity3d.com/#!/content/59097) version
+ v[0.1.5](https://github.com/NewChromantics/PopMovieTexture_Release/releases/tag/v0.1.5.b8702b8)
+ v[0.1.4](https://github.com/NewChromantics/PopMovieTexture_Release/releases/tag/v0.1.4.9697319) 
+ v[0.1.3](https://github.com/NewChromantics/PopMovieTexture_Release/releases/tag/v0.1.3.16f6156)


Free Watermarked Demo package
---------------------------------------------
+ A free watermarked demo is [availible to download on Github](https://github.com/NewChromantics/PopMovieTexture_Release/releases). This also serves as a Beta release as the releases take longer to make it to the asset store.
+ [Direct link to the latest watermarked .UnityPackage](https://github.com/NewChromantics/PopMovieTexture_Release/releases/download/v0.1.7.15a71c8/PopMovieTexture.unitypackage)

Cross-platform Features
---------------------------------------------
+ Does as little work in render thread as possible so all platforms can achieve >=60fps
+ Multi track support
+ Streams audio to AudioSource to allow customisation/positional audio
+ No platform specific C# code (Same code in unity for all platforms)
+ Very precise sync to allow synchronisation with external audio (as well as sync with audio in movies)
+ Performance graph textures which show lag in decoding & aid debugging
+ Audio visualisation to aid audio debugging
+ NO additional DLL's/runtimes required. Edit: Windows now bundles Directx shader compiler for 32 & 64bit.
+ Works in editor!
+ Lots of options for tweaking performance & specific video problems
+ Not limited to one video at a time
+ Synchronised .srt(subtitle file) parser.
+ Can Enumerate sources to list all videos, cameras, devices, windows that can be used with the plugin
+ Can be used independently of unity with the C interface or as an osx framework (enquire for details)
+ Various image format support (GIF, TGA, PNG, BMP, PSD)
+ Realtime Window capture on windows & osx

Android Features
---------------------------------------------
+ OpenGL ES 2 & 3.
+ Video decoding with or without opengl surface backing.
+ Load files from APK (streaming assets), OBB files(patches), non-compressed zip/jars, persistent data, or anywhere accessible by file system (eg. sd card)
+ Multithread rendering support
+ Multiple track support (except mpegts files, see issues)

iOS Features
---------------------------------------------
+ OpenGL ES 2 & 3 (Metal coming soon)
+ Video decoding with or without opengl surface backing.
+ File seeking forwards & backwards where supported

OSX Features
---------------------------------------------
+ OpenGL (legacy and 5.3 GLCore) support (metal coming soon)
+ Hardware video decoding
+ Multiple video & audio track support
+ Audio support
+ KinectV1 support
+ Video camera support
+ window: protocol allows capturing contents of other windows
+ File seeking forwards & backwards where supported

Windows Features
---------------------------------------------
+ OpenGL (legacy and 5.3 GLCore) support
+ DirectX 11 support
+ Hardware video decoding
+ window: protocol allows capturing contents of other windows
+ video camera/webcam/microphone support
+ File seeking forwards & backwards where supported
+ File-streaming from HTTP urls

Supported Platform versions & devices
---------------------------------------------
This is not a definitive list as the requirements and limitations can vary between devices & os, graphics card, but quite comprehensive. Corrections welcome!

+ Windows 7 32 & 64 bit
	+ Limitations: https://msdn.microsoft.com/en-us/library/windows/desktop/dd797815(v=vs.85).aspx
+ Windows 8, 8.1, 10 32 & 64 bit
+ Android API level 16 and above (4.1 Jelly-bean)
	+ Limitations: http://developer.android.com/guide/appendix/media-formats.html
+ OSX 10.7+ 32bit, 64bit & universal builds
+ iPhone 5s+, iPad Air2/Retina, iPad Mini
	+ iOS6+
	+ h264: 4k / 60fps / High Profile level 4.1
+ iPhone 5C, 4S
	+ iOS6+
	+ h264: 1280x720 / 30fps / High Profile level 4.1
+ iPhone 3GS, iPad 1,2
	+ iOS6+
	+ h264: 1280x720 / 30fps / Baseline Profile level 3.0
+ iPad 1,2
	+ iOS6+
	+ h264: 1280x720 / 30fps / Main Profile level 3.0
+ iPad 1, 2 & iPhone 4,4s,5,5c
	+ iOS6+
	+ h264: 1280x720, Baseline 3.1, 30fps



Open source! (ish)
---------------------------------------------
While PopMovie itself is not entirely open source, a huge amount of the code uses [SoyLib](https://github.com/SoylentGraham/SoyLib), an open source C++ library for a variety of platforms.
We endeavour to release more and more of the code as open source as time goes on.

Planned todo list
---------------------------------------------
This is a list of our high-priority upcoming features

+ Metal support for iOS & OSX
+ HLS streaming across all platforms
+ HAP codec support
+ Kinect & Kinect2 for windows
+ Full animated gif support
+ Stream simple files (images, subtitles) directly from HTTP
+ Stream movie files from HTTP (windows already supported)


Quick start guide
=============================================
We have provided some simple movie playback components with full power, but lots of common code written for you. If you want much more refined control, or want to handle multiple video & audio streams, consider writing your own (even using `PopMovieSimple` as a base), the interface is still quite simple!

The simple way
---------------------------------------------
+ Create a new game object in the scene and attach a `PopMovie/PopMovieSimple.cs` component
+ Create a `RenderTexture` texture to draw the movie to, or select a normal `texture2D` texture and set it on the `Target Texture` field
	+ If using a `Texture2D` you must set this to be `writable` in the advanced settings.
	+ If using a `RenderTexture` you must set the Depth Buffer setting to `No Depth Buffer`.
+ Set the `Filename` field to...
	+ To your filename `Yourfile.mp4`, and the c# code will try and resolve it to a fully qualified path, look for it in [streaming assets](http://docs.unity3d.com/Manual/StreamingAssets.html), or your [persistent data path](http://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html) for files [downloaded with WWW](http://docs.unity3d.com/ScriptReference/WWW.html).
	+ Specifically to a special folder; `streamingassets:YourFile.mp4` or `persistentdata:YourFile.mp4`
	+ For Android you can use `apk:YourFile.mp4` which will explicitly load from the assets (streaming assets) in the APK, or if you have a downloaded/store updated OBB file it will look in there for the latest version
	+ Again for android, `sdcard:YourFile.mp4` looks specifically in the external storage for your file
	+ A window-capture name `window:Notepad` or `window:*`. This is only for windows or OSX
	+ A webcam or other device (microphone, phone camera etc) `device:isight` or `device:mic` or `device:*`
+ Play! Your file should load, (An exception will be thrown if it fails), auto play, and be drawn to your target texture. You can check this in the inspector. Then assign your texture to a material, cube draw to the UI, or do whatever you want with it!
+ If you want to adjust any elements (for example change aspect ratio of a UI element). Use the `GetMeta()` function to find width & height information.


Capturing Cameras & Devices
---------------------------------------------
PopMovie also captures from OS camera & capture devices. This includes microphones, phone cameras, web cameras and other devices

+ Prefix the `Filename` field with `device:` to capture.
+ `device:*` will capture from the first device it finds
+ `device:MMP` for the HTC Vive camera
+ `device:isight` for iMac monitor Cameras
+ `device:mic` commonly gets a microphone for most platforms


Capturing Window contents
---------------------------------------------
On windows and OSX PopMovie has the ability to capture window contents and display them to your texture in realtime.

+ Prefix the `Filename` field with `window:` to capture
+ `window:*` will capture the first window with a name it finds (good for testing)
+ `window:notepad` will capture notepad if it's running
+ Window names don't have to be exact, `window:Unity` should capture unity's display.
+ OSX's window capture is currently occluded by other windows. If you want to capture a window, whilst another window sits on top, you will see the other window's contents. This does not apply to Windows
+ Window capture on Windows7 can sometimes be slow. This doesn't affect Unity's framerate, but can make applications or the contents seem laggy.
+ On Windows, there is an option named `Include Window Borders` which allows you to capture the whole window (Title bars, Minimise, close buttons etc), or just the client area
+ On Windows, you can get the full title and HWND value from `PopMovie.GetMeta()`


Problems and solutions
=============================================
In many situations enabling debug logging with `PopMovie.EnableDebugLog` will reveal problems, or hints as to any unexpected behaviour. Please check this first!

It may have unintelligible information though, so check the common problems below, or... submit an issue to [our GitHub issues page](http://www.github.com/NewChromantics/PopMovieTexture_release/issues/) (or check if it's a known problem)

You can also [send us an email directly](mailto:help@popmovie.xyz). You may have a file format or weird movie encoding that we haven't come across yet.  If nothing below solves your problem, let us know and we can fix it, and update this documentation.

Any relevant debug output will be extremely useful.
+ editor.log & console output
+ `adb logcat -s "Pop"` output for android
+ OS and Unity crash dumps
+ crash and console output from Xcode or devices on iOS & OS X.

My texture is a big gradient colour
---------------------------------------------
+ This usually indicates a runtime error. Internal shaders may not compile on your platform, or there was a problem decoding or copying the texture, turn on debug logging and you should see a message. This is quite rare now, so please email us if you cannot fix the problem!


An exception is thrown when I create my `PopMovie` class/when `PopMovieSimple` starts
---------------------------------------------
+ There was an error opening your file, or creating a decoder. Check the exception's `.message` as to what the problem is. Often the message will have OS specific errors, if these don't make sense, email us and we will try to make the errors more clear.
+ The most common cause of this is the wrong filename! Double check your spelling (remember, all files on platforms other than windows are case sensitive)
+ Streaming assets can be a special case for android. If your files are going to be in an OBB or APK, try using `apk:yourfile.mp4`.
+ see other filename prefixes in the `PopMovie` class for special handling.


My build crashes on startup or: how I learned to love DllNotFoundException
---------------------------------------------
+ On OS X this is a known issue when building a 32 bit (x86) app. Although PopMovie is built as a universal binary, it's not working. You should really be building a 64bit application anyway! We understand some other restrictions could be in place though (other plugins which HAVE to be 32 bit), let us know if this is the case and we can fix this as a priority
+ Windows is also currently 64 bit only, but this will be less trouble to correct, same as above, contact us if this is a priority for you. This should yield DllNotFoundException. If we have linked PopMovie with a 32 bit Dll, or one missing from your system you may also get this error when building 64 bit, we want to fix this!
+ Android is currently only built for Armv7. If you have an x86 device, let us know and we can work with you to get it working.
+ The Android based Epsom BT moverio is a known Armv7 device that mysteriously cannot find our library at bootup.


My texture is quite visible, but the colours are all messed up or everything is misaligned
---------------------------------------------
+ This suggests we are not merging the raw decoded image format correctly. A few options may fix, or help debug this by toggling the following options, turn off `MergeYuv`, use `Force Non-planar output`, turn off `Use Hardware decoder`, turn off `Use pixel transform`. Let us know if any of these fix, or don't fix your problem and we can look into it.

I cannot hear any audio
---------------------------------------------
+ OnAudioFilterRead in unity 5.0 on OS X in-editor sometimes doesn't output any sound, even though data is there. Some things restore it like `Debug.log(AudioData[0])` or reloading scripts. Newer versions of unity don't seem to have this issue

Audio doesn't sound correct
---------------------------------------------
+ As of `0.1.7` this should no longer be an issue on any platform. Please [contact us](mailto:help@popmovie.xyz) or [report a bug](https://github.com/NewChromantics/PopMovieTexture_Release/issues) if this is the case!
+ `Previous answer:` If the source sample rate or channel count is different from the project's audio settings, the sound won't be remuxed and will sound slow, fast or echo'y. (some platforms try to do this automatically in the hardware AAC/MP3/etc decoder) Enable debug logging to confirm the mix-match. To fix, change the your projects audio settings or re-encode the video. (Future versions will remix the audio at runtime, but the quality may not be as good)


My problem isn't listed!
---------------------------------------------
+ send us an email to [help@popmovie.xyz](mailto:help@popmovie.xyz), or [submit an issue](https://github.com/NewChromantics/PopMovieTexture_Release/issues) to the github issue tracker.



Additional thanks
=============================================
+ [@JonathonForder](https://twitter.com/jonathanforder) & [@Dave Johnson](https://twitter.com/the_real_dave_j) for art and support
+ [Benji](https://twitter.com/rhodesiamonique) for the majority of the work on the new [Demo_Movie player example](https://github.com/NewChromantics/PopMovieTexture_Release/tree/master/Example%20Projects/Assets/Demo_Movie)

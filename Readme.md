Release notes for PopMovieTexture.

Email help@popmovie.xyz for questions, feature requests, bug reports. Or submit to the issues page https://github.com/NewChromantics/PopMovieTexture_Release/issues

Features coming in/completed in next release;
=====================
 - https://github.com/NewChromantics/PopMovieTexture_Release/issues?q=is%3Aissue+is%3Aopen+label%3A%22NEXT+RELEASE+FEATURE%22

Cross-platform Features;
=====================
- Does as little work in render thread as possible so all platforms can achieve >=60fps
- Multi track support
- Streams audio to AudioSource to allow customisation/positional audio
- No platform specific C# code (Same code in unity for all platforms)
- Very precise sync to allow synchronisation with external audio (as well as sync with audio in movies)
- Performance graph textures which show lag in decoding & aid debugging
- Audio visualisation to aid audio debugging
- NO additional DLL's required.
- Works in editor!
- Lots of options for tweaking performance & specific video problems
- Not limited to one video at a time
- Synchronised .srt(subtitle file) parser.
- Can Enumerate sources to list all videos, cameras, devices, windows that can be used with the plugin
- Can be used independently of unity with the C interface or as an osx framework (enquire within for details)
- Various image format support
- Realtime Window capture on windows & osx

Specific OS Features;
======================
- Android
	- OpenGL ES 2 & 3.
	- Video decoding with or without opengl surface backing.
	- Load files from APK (streaming assets), OBB files(patches), non-compressed zip/jars, persistent data, or anywhere accessible by file system (eg. sd card)
	- Multithread rendering support
	- Multiple track support (except mpegts files, see issues)

- Ios
	- OpenGL ES 2 & 3
	- Video decoding with or without opengl surface backing.

- OSX
	- Hardware video decoding
	- Multiple video & audio track support
	- Initial audio streaming support
	- KinectV1 support
	- Video camera support
	- window: protocol allows capturing contents of other windows

- Windows
	- OpenGL support
	- DirectX 11 support
	- Hardware video decoding (currently only via MediaFoundation)
	- window: protocol allows capturing contents of other windows
	- video camera/webcam support
	- Robust seeking forwards & backwards

Demo Projects
======================
![Demo_Movie Screenshot](/Docs/Demo_Movie.png)
##Demo_Movie
Demo_Movie plays the clip from Dr.Strangelove, from the streaming assets folder to an image which is displayed in a GUI. There is an additional script which creates another PopMovie instance which attempts to load a .srt(subtitle) file in the same path. If it succeeds, it synchronises (in the demo's case it's 3 and a half minutes out, as the video has been clipped but the subtitles are still for the full movie) the subtitle "movie" with the playing movie and displays the current frame's subtitle to a text GUI element.
There is also a slider in the gui which controls the MovieTimeScalar to allow you to speed-up & slow down the movie whilst it's playing.

Known issues; (see issue tracker for most recent bugs/fixes)
======================
- Android
	- Audio needs resampling from android project-default of 24000hz (Set your project audio settings to match the audio rate!)
	- Files in APK limited to ~250mb (reports "file not found")
	- To split with ffmpeg 
		- __ffmpeg -i long.mp4 -vcodec copy -acodec copy -ss 0 -t 50 short_0_50.mp4__
		- __ffmpeg -i long.mp4 -vcodec copy -acodec copy -ss 50 -t 100 short_50_100.mp4__


- Ios
	- Video camera support experimental
	- No metal support
	- iphone 5/4 not currently decoding (Though may just need very specific resolutions)
	- Unity doesn't warn about H264 profiles/levels that are too high (just fails)
	- Full 4K videos render green (or black if not using hardware backing), with no error. Even slightly smaller and it's fine.
	- Pausing & resuming app sometimes breaks asset reader (if closed for > 5secs)
	- 60FPS video decodes FASTER with non-opengl backing

- OSX
	- No metal support
	- 60FPS video decodes FASTER with non-opengl backing
	- WIP stream-from-shared-memfile code (ask for details)
	
- Windows
	- DX12 untested
	- No DX9 support
	- Performance Graphs currently don't render
	- Audio visualisation currently doesn't render
	- WIP stream-from-shared-memfile code (ask for details)

Todo/Work in progress;
=====================
- All platforms
	- HTTP/Websocket file streaming (WIP)
	- HLS streaming (parser done, working on cross platform mpeg2ts decoder)
	- DASH streaming
	- Explicit animated .gif support (Experimental)
	- Audio re-sampling
	- More modular C# interface
	- Rewind/reverse seeking (Currently windows only)
	- Gapless playlisting
	- Frame-level-control (instead of time)
	- More stats on decoding/downloading/playback speeds/rates
	- Expose more stream meta
	
- Windows & OSX
	- Shared-mem-file reading for working with drivers/3rd party apps
	- Stream kinect skeletons to unity (as text/json stream)
	- Built in kinect auto-alignment (detect floor plane and stream camera extrinsics to unity)
	- Hardware(pixel shader) kinect depth alignment
	- Depth map cleanup (noise reduction, hole filling) for kinect
	- Window interaction (cross platform method to send mouse clicks, keyboard input etc)
	
- IOS & OSX
	- Metal support (half finished)

- Windows
	- Full DX12 support
	- Add Nvidia/Cuda/AMD specific hardware decoder support (NVDec etc)
	
- Android
	- Camera support

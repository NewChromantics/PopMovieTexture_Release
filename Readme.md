Release notes for PopMovieTexture.

Email graham@newchromantics.com for questions, feature requests, bug reports. (Or use  github's issues https://github.com/NewChromantics/PopMovieTexture_Release)


Cross-platform Features;
=====================
- Does as little work in render thread as possible so all platforms can achieve >=60fps
- Multi track support
- Streams audio to AudioSource to allow customisation/positional audio
- No platform specific C# code (Same code in unity for all platforms)
- Very precise sync to allow synchronisation with external audio (decoding audio not yet synchronised on all platforms)
- Performance graph textures which show lag in decoding & aid debugging
- NO additional DLL's required.
- Works in editor!
- Lots of options for tweaking performance/working around issues
- Not limited to one video at a time
- Synchronised .srt(subtitle file) parser.


Specific OS Features;
======================
- Android
	- OpenGL ES 2 & 3.
	- Video decoding with or without opengl surface backing.
	- Load files from APK (streaming assets), persistent data, or anywhere accessible by file system
	- Multithread rendering support
	- Multiple track support

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


Known issues; (see issue tracker for most recent bugs/fixes)
======================
- Android
	- Audio decoding experimental (needs resampling for android defaults, not yet synchronised)
	- Files in APK limited to ~250mb (reports "file not found")

- Ios
	- Video camera support experimental
	- Audio decoding experimental (not yet synchronised)
	- No metal support
	- iphone 5/4 not currently decoding
	- Unity doesn't warn about H264 profiles/levels that are too high (just fails)
	- Full 4K videos render green (or black if not using hardware backing), with no error
	- Pausing & resuming app sometimes breaks asset reader (if closed for > 5secs)
	- 60FPS video decodes FASTER with non-opengl backing

- OSX
	- No metal support
	- 60FPS video decodes FASTER with non-opengl backing
	- Client-storage can be tempramental
	- Audio decoding experimental (not yet synchronised)
	- WIP stream-from-shared-memfile code (ask for details)
	
- Windows
	- Some win7 setups won't load DLL (see issues for details)
	- Reports that DLL won't load in built apps
	- Audio decoding experimental (not yet synchronised)
	- Win10 untested
	- DX12 untested
	- No DX9 support [yet]
	- Performance Graphs currently don't render
	- WIP stream-from-shared-memfile code (ask for details)

Todo/Work in progress;
=====================
- All platforms
	- HTTP/Websocket file streaming (WIP)
	- HLS streaming (parser done, working on cross platform mpeg2ts decoder)
	- DASH streaming
	- Explicit .gif support
	- Explicit support of image(non-video) formats
	- Audio visualisation (for debugging)
	- Audio sync & re-sampling
	- More modular C# interface
	- Rewind/reverse seeking
	- Gapless playlisting
	- Frame-level-control (instead of time)
	- More stats on decoding/downloading/playback speeds/rates
	- Expose more stream meta
	
- Windows & OSX
	- Shared-mem-file reading for working with drivers/3rd party apps
	- Stream kinect skeletons to unity (as text/json stream)
	- Built in kinect auto-alignment (detect floor plane and stream camera extrinsics to unity)
	- Hardware(pixel shader) kinect depth alignment
	- Depth map cleanup (noise reduction, hole filling)
	- Window interaction (cross platform method to send mouse clicks, keyboard input etc)
	
- IOS & OSX
	- Metal support (half finished)

- Windows
	- Full win10 support
	- Full DX12 support
	- Add Nvidia/Cuda/AMD hardware decoder support
	
- Android
	- Camera support
	- Stream from OBB/ZIP files

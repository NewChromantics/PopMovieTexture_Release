Release notes for PopMovieTexture.

Email graham@newchromantics.com for questions, feature requests, bug reports. (Or use  github's issues https://github.com/NewChromantics/PopMovieTexture_Release)


Cross-platform Features;
=====================
- Does as little work in render thread as possible so all platforms can achieve >=60fps
- Multi track support
- Streams audio to AudioSource to allow customisation/positional audio
- No platform specific C# code
- Very precise sync to allow sync with external audio
- Performance graph textures which show lag in decoding & aid debugging
- NO additional DLL's required.


Specific OS Features;
======================
- Android
	- OpenGL ES 2 & 3.
	- Video decoding with or without opengl surface backing.
	- Load files from APK (streaming assets), persistent data, or anywhere accessible by file system
	- Multithread rendering support

- Ios
	- OpenGL ES 2 & 3
	- Video decoding with or without opengl surface backing.

- OSX
	- Hardware video decoding
	- Multiple video & audio track support
	- Initial audio streaming support
	- KinectV1 support
	- Video camera support

- Windows
	- OpenGL support
	- DirectX 11 support
	- Hardware video decoding 


Known issues;
======================
- Android
	- No audio decoding
	- Files in APK limited to ~250mb (reports "file not found")

- Ios
	- Video camera support experimental
	- Audio decoding experimental
	- No metal support
	- iphone 5/4 not currently decoding
	- Unity doesn't warn about H264 profiles/levels that are too high (just fails)
	- Full 3K videos render green (or black if not using hardware backing), with no error
	- Pausing & resuming app sometimes breaks asset reader (if closed for > 5secs)
	- 60FPS video decodes FASTER with non-opengl backing

- OSX
	- No metal support
	- 60FPS video decodes FASTER with non-opengl backing
	- Client-storage can be tempramental

- Windows
	- Some win7 setups won't load DLL
	- Reports that DLL won't load in built apps
	- No audio decoding
	- Win10 untested
	- DX12 untested
	- No DX9 support
	- Performance Graphs currently don't render


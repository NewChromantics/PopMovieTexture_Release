Release notes for PopMovieTexture.

Email graham@newchromantics.com for questions, feature requests, bug reports. (Or use  github's issues https://github.com/NewChromantics/PopMovieTexture_Release)



Specific OS Features;
======================
- Android
	- OpenGL ES 2 & 3.
	- Video decoding with or without opengl surface backing.
	- Load files from APK (streaming assets), persistent data, or anywhere accessible by file system

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

- OSX
	- No metal support

- Windows
	- Some win7 setups won't load DLL
	- Reports that DLL won't load in built apps
	- No audio decoding
	- Win10 untested
	- DX12 untested
	- No DX9 support
	- Performance Graphs currently don't render


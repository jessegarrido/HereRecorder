A "smart" audio application targeting single-board computers (and named for Tascam's legacy "Portacam" home recording line), SmartaCam captures and processes audio for for review and collaboration. SmartaCam creates new recordings in the lossless .wav format, and converts them to the more portable .mp3 format for sharing to the cloud.

SmartaCam's Blazor-based web interface includes:
- A home page where users can create new recordings, and playback or delete existing ones.
- A tag editor page where users can create or activate "Title, "Artist," and "Album" tags applied to recordings. "Title" will also be used for .wav file name, and must include the text "[#]" (replaced by a count to ensure unique file names). 
- A configuration page where users elect to normalize (maximize volume without clipping), copy to usb, and/or upload to DropBox after audio is captured. 

SmartaCam's Dropbox integration works, but does not yet have a configuration interface. To observe its function:
	1. Copy (separately provided) App API key and App API secret to Settings.settings in the SmartaCam.API project directory
	1. Visit Dropbox.com and Log In using account credentials (separately provided)
	2. Launch SmartaCam with a removable drive inserted
	3. Visit the url written to DropBoxCode.txt, on the root of the removable drive
	4. Follow prompts to give SmartaCam permission to create and access a dedicated folder in the DropBox account
	5. Overwrite the url in DropBoxCode.txt with the resultant "Access Code" 
	6. SmartaCam detects the modified text and completes authentication (visible in Console)
	
Many features (Dropbox UI interface, audio device selection, logging, etc.) remain pending, as SmartaCam is planned for development beyond Capstone submission.
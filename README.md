A "smart" audio application targeting single-board computers (and named for Tascam's legacy "Portacam" home recording line), SmartaCam captures and processes audio for for review and collaboration. SmartaCam creates new audio recordings in the lossless .wav format, converts them to the portable .mp3 format, and shares them to the cloud. SmartaCam is comprised of ASP.NET (localhost:7152) and Blazor Webassembly (localhost:7140) applications.  App API key and App API secret (separately provided) must be copied to fields in Settings.settings for DropBox integration.<br/>

SmartaCam's web interface includes:
- A home page where users can create new recordings, and playback or delete existing ones.
- A tag editor page where users can create or activate "Title, "Artist," and "Album" fields and file name. "Title" must include the text "[#]" (programmatically  replaced by a count to ensure unique file names). 
- A configuration page where users elect to normalize (maximize volume without clipping), copy to usb, and/or upload to DropBox after audio is captured, and link a DropBox account.
To authenticate SmartaCam for use in headless, un-networked environment:
	1. Launch SmartaCam on headless device with removable drive inserted<br/>
	2. Visit Dropbox.com and Log In using account credentials on a remote, networked machine(separately provided)<br/>
	3. Insert the removable drive in the remote machine and visit the url written to DropBoxCode.txt in the drive's root<br/>
	4. Follow prompts to give SmartaCam permission to create and access a dedicated folder in the DropBox account<br/>
	5. Overwrite the url in DropBoxCode.txt with the resultant "Access Code"<br/>
	6. Return the USB drive to the headless machine. SmartaCam detects the modified text and completes authentication (viewable in Console)<br/>
	
https://youtube.com/shorts/p5d0PktDPz8?si=YAI-a2Kp9jGuEtHj<br/>
SmartaCam API on Raspberry Pi with breadboard IO circuit
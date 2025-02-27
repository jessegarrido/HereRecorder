H E R E records audio and shares it to the cloud. 

Cross-platform but targeting headless operation on the Raspberry Pi, HERE, with one button push, footpedal press, or mouse click captures new audio, normalizes and converts it to mp3, and hands it off for review and collaboration.

HERE is comprised of ASP.NET (127.0.0.1:7152) and Blazor Webassembly (127.0.0.1:7140) applications. 
WebUI includes:
- A home page to create new recordings and play back or delete existing ones.
- A tag editor page to create or activate file name scheme and ID3 tags. 
	*Title" must include the text "[#]" (programmatically  replaced by a count to ensure unique file names). 
- A settings page of options to normalize, convert stereo recordings to mono, copy files to usb, link/unlink a DropBox account, and upload recordings.

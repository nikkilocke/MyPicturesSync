# MyPicturesSync
Copy all the folders under My Pictures to Flickr, and place them in sets according to folder.

# Running the program

If you just want to use the program, unzip MyPicturesSync.zip anywhere, and run it. You can download it from the release tab on GitHub at https://github.com/nikkilocke/MyPicturesSync/releases 

The first time you use it, it will open a web page on Flickr, asking if you give it permission to upload pictures. 
If you give permission, you will be given a Verifier code - copy this into the Verifier Code box in the
Settings dialog.

You can also change the pictures folder to sync, and set start and stop times (this is useful if you have
free Internet bandwidth between certain times, or if you want the uploads to take place overnight).

Once you have pressed OK on the Settings dialog, MyPicturesSync will find all the folders under the pictures folder, and list them on the left. If you click on one of them, you will see the pictures in that folder on the right. Note that it will ignore any folders which have a date-based name (e.g. 2015-01-01) - if you add a more descriptive name to the folder, MyPicturesSync will recognise it. This is deliberate, because if you just accept the defaults when you load pictures in from your camera, the folder name will just be the date, whereas on Flickr you want your set names to be descriptive.

MyPicturesSync will check each folder corresponds to a set on Flickr (creating a set if not), and upload any pictures which are in a folder but not in the corresponding set. Pictures which are on Fickr are shown with a small tick in the bottom left hand corner.

The toolbar at the top lets you change the way the pictures are shown, change the picture details which are listed, and go back to the Settings dialog to change things. Note that, when you open the Settings dialog, Flickr upload will stop, and restart from the beginning when you close it.

# Compiling

The program is compiled using Visual Studio Express C# 2012. The solution should compile straight out of the box.


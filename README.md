# CSC476HW3
Unity project files and assets for CSC 476 HW3

## Purpose
The assignment was to follow chapter 29 of text and implement the "Mission Demolition" Prototype, along with a few extra features.
Luckily I didn't run into too many issues implementing the base prototypes, it wasn't until I began implementing extra features that I saw some trouble.

### Slingshot Band
This was mainly playing with legacy shaders and the line renderer plus some scripting logic to make it work and actually kind of look like a rubber band.
* Follows your mouse in the launch zone.
* Snaps back to resting on mouse exit.
* Keeps following your mouse if your are aiming and exit the launch mode.

### Showing Previous Shot Trails
Just saving the old line when a new projectile is fired and chaning the color to grey.
I experimented with the `LineRenderer.BakeMesh()` method but I couldn't quite get it to behave how I wanted.
It seems like an interesting and efficient way to save and store an object created by the line renderer.
I just wasn't quite sure how to properly display the mesh in the scene from the object returned by that method.

### New Castle Element: Window
Similar to the stone wall and slab elements from the tutorial the window is another game object that could make up a castle.
* On fast enough projectile collision the window will "shatter"
* Windows will not shatter against other parts of the castle
* Shatter effect is to just spawn a large amount of tiny cubes made out of the same material and disable the original game object.
* After several seconds the "shards" created from the shatter effect despawn.

### Level Navigation
Super simple, just added 2 buttons that navigate between levels so they all can be played much easier.

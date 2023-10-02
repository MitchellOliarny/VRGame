# How it works

1) Attach one SeveringBlade script instance and one UvMapper script instance (WorldUvMapper or TargetUvMapper), located in Users main, to a game object that you want to shatter, 
split or sever something of. When shattering or cutting a game object the pieces will be instantiated as clones of the original game object and the original game object will be destroyed.

The Mesh severing package script requires that the game object has a MeshFilter attached, as it needs some geometry to work with. 
Any other component you attach will be carried over without modification to the pieces when the game object is shattered or split. 
However, the SeveringBlade script takes special care to itself, MeshCollider and Rigidbody components respectively. 
If attached, any of these will be updated to act realistically when the game object is shattered or split by for example modifying the Rigidbody's mass and velocity.

2) The TargetUvMapper let you set a target area within that objects material texture to fill the hole and its new created cap.
The WorldUvMapper uses the whole texture to uv wrap the object, 
like if you cut a stone block (see UvMapping example) or a watermelon, the watermelos is diferent inside but hte stone is still stone inside.

IMPORTANT! Is highly recomended to work with square texturex, exponents of 2. for example 2^10 = 1024 a 1024 x 1024 texture, or 2^8 = 256 a 256 x 256 texture like the uv test.
other thing to remember is that the tiling on the material have an effect on the target area.

IMPORTANT! there are 2 checker style textures use them to see where your target area begins and if the size is right, also if the square and centerMeshOrigo options are needed, 
just play arround with them and eventually you will se how it works...!! XD.
There is important to know that the texture uv test shows how the cordinates are being mapped like in the uv test texture the (0,0) 
is the same (0,0) the TargetUvMapper would have if you set the target start in those coordinates, for example go to uvMapping scene and slice the cube with the cheker texture vertically.
then pause and separate the severed cube pices, as you could see the target start was (0,0) and the size was (1,1) meaning that the target was the whole texture, one thing to remember 
is that the uv test texture each square has a 32 X 32 pixel size so this texture is for example only but if your are using other texture the same size as uv test the atrget are will be the same,
so play with this cube for learning how to work properly your uv mapping area.

Other things to try is to apply the square and centerMeshOrigo options, for example if you apply centerMeshOrigo what happens 
is that the hole will fill the cut with the same target area as before but the center of the newly created cap to fill the hole will be the center of your target area,
use full if you want to have centered you fill texture like cutting a orange. 

try this unapply the square and centerMeshOrigo options set the target start to (0,0) and the size to (0.25,0.25), the play and sever the cube vertically, 
pause and separete the pieces on X axis now the see how the fill texture only uses the 4 first squares how to get this result?? easy just see there are 8 squares by 8 squares on the uv test texture
the max size will be 1 on X and Y because the texture is sqaure so just 1/8 = 0.125 this is the size of one square in uv coordinates 
so we want to show 4 squares we use (0.125 x 2) = 0.25 on the size in X and Y and thats it.


3) The ShatterOnCollision script plus  both of the scripts metioned in 1) and 2) steps (see Parent objects scene in examples). and it de


IMPORTANT! If you attach a MeshCollider and a Rigidbody, always keep the MeshCollider Convex in order to avoid errors while updating the mass properties of the Rigidbody.

IMPORTANT! If SeveringBlade.FillCut is enabled (default) every edge of the mesh needs to belong to exactly two triangles, ie. the mesh needs to be closed.

Here are a number of ways you can shatter or split a game object:

No scripting required:

- Attach the ShatterOnCollision script (located in Helpers/Game Object) and specify the required force needed to shatter the game object (requires an attached rigidbody and convex collider)

- Attach any of the mouse scripts (located in Helpers/Mouse Controlls) to an empty game object in the same scene

Scripting:

- In a script, send a "Shatter" message to the game object and specify a world-space point (Vector3) where the game object should be shattered; for example

SendMessage("Shatter", Vector3.zero);

- In a script, send a "Split" message to the game object and specify an array of world-space planes (Plane[]) with unit-length normals where the game object should be split; for example

SendMessage("Split", new Plane[] { new Plane(Vector3.right, Vector3.zero) });

# Example scenes:

Check out the example scenes to see how the Severing Blade package is used in practice.

# Good to know

1. The SeveringBlade properties have tooltips in the editor, mouse over to read them

2. You can make the SeveringBlade instance send pre- and post-split messages by toggling the "Pre Split msg" and the "Post Split msg" properties in the editor. 
These may be useful if you need to do something before and/or after a split occurs.

3. When shattering or cutting parented game objects, make sure you always handle the children/parent carefully to avoid duplicating too many game objects. see Parent objects scene in examples.

4. The scripts EdgeBuilder and sliceControll (located in Helpers/Extra scripts) where used for debuging snd showing how the triangulator works, and where the new vertices are located, 
but feel free to use it anyway you want, at first i think of using both scripts to cut the meshes but you know the things change as i continue to develop the package.
So the Edgeduilder get al mesh edges and each mesh is a pair of vertices and the face(triangle they belog to), the sliceControl make use of edgebuilder to look where the new vertices 
should be within the plane that is used to cut the mesh. But how i repeat feel free to use them, hope you find this fund and usseful.
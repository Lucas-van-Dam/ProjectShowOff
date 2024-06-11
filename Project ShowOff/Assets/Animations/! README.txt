IMPORTANT: While importing these FBX Animations, make sure you go click on the Prefab in the outliner and under Model turn off 'Convert Units', then apply. 
That way the size should be correct and not 100x smaller.

To activate: Click the Prefab in the Outliner and Drag the Take001 (Animation Clip) onto the character. 
[Note: You can rename the Animation Clip from the Prefab>Animation or just pressing Edit on the Animation Clip]
This creates an Animator Controller. The Animation Clip should now be in the Animator & the Controller is selected in the Animator on the model's Inspector.

You only really have to drag in 1 model and can drag more of the other models animation clips onto it to have them in the animator
Note: With Cutyzilo he introduces a new mesh into the idle animation that isn't in the other prefabs so I'm assuming the actual main prefab we should use for his character controller should be the prefab with the leaf mesh in it? then drag the other animation clips onto that prefab?


Notes:
With some animations, esp standing animations, it's good to select the  'Loop Time' checkbox under prefab>animation tab
These models should already have materials but there might be issues w the shaders, you might have to reapply them????
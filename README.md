# Made with Unity 2019.2.9f1 and ARcore 1.18.0

# Demo:-
![](Sample_2.gif)

# How to build :-
1. Clone or Download the project.
2. Switch the platform to Android.
3. Now Click on Build.

# Apk usage instruction:-
1. After successful installation open the application.
2. Scan the ground. A horizontal plane will be generated while scanning the ground.
3. Tap on the screen using two fingers to hide the plane.
4. Now tap on the ground to break.

# Description:-
Augmented Reality Floor breaking tech demo (Floor/ground texture capture script included)

Few months ago we were given up with an interesting problem to solve. We have to break the floor using AR core ground plane detection but the tricky part is that the broken floor pieces should have the same real world texture as the floor/ground which is the critical part of realism.

We have managed to extract the ground plane bounding /floor region from the camera texture  and applied it back to the 3d floor pieces to bring the sense of realism (blend with environment) that the actual floor is broken into pieces. So the broken pieces will have tile texture if you were testing this in tiles floor or cement texture if you were testing in cement floor. So dynamically it can extract texture and apply back to models.

We were happy to open source and share this technical implementation with the community.

# **Space Invaders Original**

# Authorship
* __Leandro Brás__ > __a21801026__ 

* __Sara Gama__ > __a21705494__ 

## Who did what
__Leandro Brás__
-> Created most of the code, and the documentation (Doxygen).

__Sara Gama__
-> Made the report, UML and flowchart.

## Contact

* [GitLab](https://gitlab.com/xShadoWalkeR/Space-Invaders)

# Project architecture

## Our Solution
We choose to make the game `Space Invaders` which has 25 classes and 2 enumeration.

The `Program` class appoints the window size, hides the cursor and calls 
the `Menu` class. 

The `Menu` class shows the base menu of the game in which the player needs to
choose `Play` to play or `Quit` get out of the console; and calls the `Game` 
class.

The `Sprites` class has the sprites used by the Menu Game and for the game it self.

We user 2 Patterns Iterator and Facede.

The `Game` class has 3 constants, 13 global variables; they make 
work and diplay the score, lifes, ship, enemies, barriers, bullets and the 
ovni in the console. The `GetReady()` method put on display the header (in the
`DisplayRender()` method) with score, level and lifes)); start the animation of 
the enemys and "Get Ready!". The `LifeLost()` method controls how many lives 
the player have during the game. If the enemy kills the player he loses 1 live, 
if the player loses the 3 lives then its game over; while the `LevelCompleted()` 
method only works when the player passes the level, initializing the animations 
to start the next level.
These 3 methods (`EnemyDestroyCheck()`, `OvniDestroyCheck()`, `ShipDestrooyCheck()`) 
are used to verify the collision between the bullets and the game
objects destroying them on hit and deleting the colliding bullets.

The `BufferEditor` is a static class that serves has a type of `Facade` to the 
class `DoubleBuffer2d` to ease up the interection between other classes and the 
`DoubleBuffer`; being able  to write, delete and changes the color of the 
charaters in the buffer.

## UML
![UML](UML.png)

## Flowchart
![Flowchart](Flowchart.png)

## Conclusions

__Leandro Brás__
Gained a better understanding of how the console works, 

__Sara Gama__
Hunderstood how to writing report about code work. Remenbering how to make 
flowchart and UML.

# References
* [LP2_Aulas](https://github.com/VideojogosLusofona/lp2_2019_aulas.git): 
Powerpoints provided by the teacher

## Thanks
We are grateful to Fálvio and the theacher Nuno Fachada who help us in 
console render.
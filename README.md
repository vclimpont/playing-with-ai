# Pac-man like game

This project is a scoring based game made in Unity in order to work around **A*** and **Dijkstra** algorithms.

In this small game, players maximize their score by chaining levels.
To complete a level, the player has to collect every gems in order to open a chest without being hit by the enemies looking for him.
When the player complete a level, the score is increased depending on the difficulty.

Levels are procedurally generated and their difficulty increase over time :
* The size of the level is increased by 1x1 per level.
* The obstacles frequency is increased by 1% per level (maximum 50%).
* The number of enemies is increased by 20% per level rounded to the closest integer.
* The number of gems to collect is increased by 1 per level.
* The score gained by collecting a gem and completing a level is increase by 10 and 100 respectively.

![game view](https://github.com/vclimpont/playing-with-ai/blob/master/Image/gameview.PNG)

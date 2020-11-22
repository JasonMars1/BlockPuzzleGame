# BlockPuzzleGame

A block placing puzzle game I worked on in the summer of 2019.\
The game has been built for Windows, Android and IOS.

## Overview
The core gameplay loop is placing blocks onto a grid. Placing the blocks on the grid awards points. Once a line has been completed, either vertically or horizontally, the blocks making up that line are removed, and points are also awarded. Once all three blocks are placed, a new set of three blocks are spawned. For every X amount of points, bonus points are awarded which can be spent on bonus buttons which effect the blocks you have available to place. The game ends when you cannot place any of the blocks on the grid and have no bonus points to help you.

## Bonus Buttons

* **Hold Block:** Holds the block so that you don’t have to use it this round.
* **New Block:** Replaces the block in the slot with another block
* **Delete Block:** Removes the block from the slot
* **Block Bomb:** Replaces the block with a block bomb which sets all blocks in a 5x5 radius to a certain block, or removes the all.

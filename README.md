# BattleShipStateTracker

Application to track the different states of the Battleships board game

# The task
The task is to implement a Battleship state-tracker for a single player that must support the
following logic:

*Create a board

*Add a battleship to the board

*Take an “attack” at a given position, and report back whether the attack resulted in a
hit or a miss

*Return whether the player has lost the game yet (i.e. all battleships are sunk)
The application should not implement the entire game, just the state tracker. No UI or
persistence layer is required.

# Task Notes

* I have implemented a version of the GoF State Design Pattern to track the states of the Cells and the Game itself. For this cut down version
I have not extended this to the Ship object but this would be an idea for further extension of the functionality.

* The brief above stated no UI however, after speaking to Jordan we agreed to include a basic console app. Here you can perform the 4 main
functions highlighted above.

* I have included Unit Tests for the main classes modelling all the different states.

* As this is just a simulation with a single User I have not included a User object to model the interaction between two Users playing each other. 
Again this is a good idea for further extension of the game.

# Unity2D Extrapolation & Interpolation sample
> Instructions

Add the "client" script to the follower object

Add the "moveServer" script to the moving target

The client script is hardcoded to follow the moveServer component.

> Usage

The client script will try to keep up to the moving target with randomizing values resembling the current ping. The biggest issue is fluctuating velocities which the server might not be able to send in time to the client, which this project aims to reduce. 

When the moving target reaches a static velocity, the client has no issue keeping up.

> Todo

This project is heavily unfinished and does not support anything on the Y or Z axis. The purpose is currently only to get an idea on how to implement this.

When working on multiplayer games, it is important to be careful. In the event that the math is wrong, the players would get a worse experience than if there was no interpolation / extrapolation going on. The same thing applies for players who have fluctuating pings. Interpolation would be better in this case, excluding the extrapolation as it would result in a visual bug where it seems like players are running outside of bounds or that the players are moving at unnatural speeds.

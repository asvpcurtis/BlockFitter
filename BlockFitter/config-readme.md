# Configuration


## Heuristic 
Options include:
* `"SpaceCohesion"`
* `"SpaceUncovered"`
* `"IntersectingPieces"`

## HillClimbStrategy
Options include:
* `"SimulatedAnnealing"`
* `"GeneticAlgorithm"`
* `"SimpleHillClimbing"`

## TimeoutMillis
This is the amount of time in milliseconds you wish for the algorithm to attempt to solve the problem.

## Problem
Consists of container and a list of pieces to fit into the container
### Container
A large blockshape container made of units.
### Pieces
A list of blockshape pieces each made of units.
### Unit
A `{"x": 0, "y": 0}` co-ordinate that represents a block
### Blockshape
A list of units/blocks that when combined form a blocky shape



``` json
{
    "Heuristic": "SpaceCohesion",
    "HillClimbStrategy": "SimulatedAnnealing",
    "TimeoutMillis": 10000,
    "Problem": {
        "Container": {
            "Units": [
                {"x": 0, "y": 0}, {"x": 1, "y": 0}, {"x": 2, "y": 0},
                {"x": 0, "y": 1}, {"x": 1, "y": 1}, {"x": 2, "y": 1},
                {"x": 0, "y": 2}, {"x": 1, "y": 2}, {"x": 2, "y": 2}
            ]
        },
        "Pieces": [
            {
                "Units": [
                    {"x": 0, "y": 0}, {"x": 1, "y": 0}, 
                    {"x": 0, "y": 1}, {"x": 0, "y": 2}, 
                    {"x": 1, "y": 2}
                ]
            },
            {
                "Units": [
                    {"x": 1, "y": 0}, {"x": 0, "y": 1}, 
                    {"x": 1, "y": 1}, {"x": 1, "y": 2}
                ]
            }
        ]
    }
}
```
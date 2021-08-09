# Code Samples for my YouTube video
These are code samples I created for my bideo https://youtu.be/3BMaE0mKrk8 

## Python Code
This code was created in Jupyter Notebook and then tested and saved in PyCharm. 
Most of the code is repeating in all three files, except changes made for each version of the algorithm.

TraverseWithGraph-SingleLevel.py

TraverseWithGraph-WithStartingCoordinates.py

TraverseWithGraph-MultiLevelArray.py


## C# code 
TraverseWithGraph.cs is a C# implementation of Python's version of algorithm - TraverseWithGraph-MultiLevelArray.py

To use:

var myMaze = Mazes.Maze3D;  // Create an array

var result = new TraverseMap3D().Traverse(myMaze); // Traverse the array

// To print the result, iterate over the collection of nodes in the graph

foreach (var node in result.GetAllNodes())

{

Console.WriteLine($"Node {node.Key.PadLeft(7)} | value: {node.Value.GetValue().PadLeft(3)} | Connected to: {string.Join(", ", node.Value.GetConnections())}");

}

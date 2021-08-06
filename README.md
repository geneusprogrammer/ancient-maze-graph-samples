# Code Samples for my YouTube video
The video will be uploaded by the end of Sunday, 2021-08-08

## C# code 
TraverseWithGraph.cs

To use:


var myMaze = Mazes.Maze3D;  // Create an array

var result = new TraverseMap3D().Traverse(myMaze); // Traverse the array

// To print the result, iterate over the collection of nodes in the graph
foreach (var node in result.GetAllNodes())
{
    Console.WriteLine($"Node {node.Key.PadLeft(7)} | value: {node.Value.GetValue().PadLeft(3)} | Connected to: {string.Join(", ", node.Value.GetConnections())}");
}

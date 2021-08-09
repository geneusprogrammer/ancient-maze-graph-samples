import numpy as np
import time
import sys
from abc import ABC
import json
from typing import List, Tuple, Type, Optional


maze_single_level = [[1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
                     [0,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0],
                     [0,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0],
                     [0,1,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,1,0],
                     [0,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,0,1,0],
                     [0,1,0,1,0,0,1,0,0,1,0,0,1,0,1,0,1,0,0,0],
                     [0,0,0,1,0,0,1,0,0,1,0,0,1,0,1,0,1,1,1,0],
                     [0,0,0,1,0,0,1,0,0,1,1,1,1,0,1,0,0,0,1,0],
                     [0,0,0,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0],
                     [0,0,0,1,0,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0],
                     [0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0],
                     [0,0,0,1,1,0,1,1,1,1,0,1,0,0,0,0,1,0,0,0],
                     [0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,0,1,0,0,0],
                     [0,0,0,1,0,0,0,0,0,1,0,1,0,0,1,0,1,0,0,0],
                     [0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,1,0,0,0],
                     [0,0,0,1,1,1,1,1,0,8,1,1,1,0,0,0,1,0,0,0],
                     [0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0],
                     [0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0],
                     [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
                     [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]]


class Edge:
    def __init__(self) -> None:
        self.steps = []

    def add_step(self, step) -> None:
        self.steps.append(step)

    def __str__(self) -> str:
        return ''.join(self.steps)

    def path(self) -> str:
        return str(self)


class Node:
    def __init__(self, key: str, value: str = '') -> None:
        self.id = key
        self.value = value
        self.connections = {}

    def add_connection(self, connection_key: str, edge: str) -> None:
        self.connections[connection_key] = edge

    def get_connections(self) -> List[str]:
        return self.connections.keys()

    def get_id(self) -> str:
        return self.id


class Graph:
    def __init__(self) -> None:
        self.nodes = {}

    def add_node(self, key: str, value: str = '') -> None:
        if not self.get_node(key):
            node = Node(key, value)
            self.nodes[key] = node

    def get_node(self, key: str) -> Optional[Node]:
        if key in self.nodes:
            return self.nodes[key]
        else:
            return None

    def __contains__(self, key: str):
        return key in self.nodes

    def add_edge(self, fr_node: str, to_node: str, edge: str):
        if fr_node not in self.nodes:
            self.add_node(fr_node)
        if to_node not in self.nodes:
            self.add_node(to_node)
        self.nodes[fr_node].add_connection(to_node, edge)

    def get_nodes(self) -> List[str]:
        return self.nodes.keys()

    def toJson(self, formatted=False):
        indent = 4 if formatted else 0
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=formatted, indent=indent)


class TraverseMap:
    def __init__(self) -> None:
        self.data = None
        self.graph = Graph()

    def traverse(self, map_data) -> Type[Graph]:
        self.data = np.array(map_data)
        val = 'G' if self.data[0][0] == 8 else 'B'
        self.graph.add_node('0-0', val)             # Add beginning node
        node = self.graph.get_node('0-0')
        edge = Edge()                               # Define beginning edge
        point = (0, 0, val)                         # Define starting point
        self._traverse(point, edge, node)
        return self.graph

    def _traverse(self, point, current_edge, from_node):
        row, col, direction = point
        current_key = f'{row}-{col}'
        from_key = from_node.get_id()

        # Is this a coridor or a fork?
        available_points = self._get_child_points(row, col)
        child_points = [x for x in available_points if x[2] != self._reverse_of(direction)]

        def mark_spot():
            current_path = current_edge.path()
            self.graph.add_edge(from_key, current_key, current_path)
            self.graph.add_edge(current_key, from_key, self._reverse_path(current_path))

        if current_key in self.graph:
            mark_spot()
            if direction != 'B': return

        # Check if we reached the GOAL
        if self.data[row][col] == 8:
            self.graph.add_node(key=current_key, value='G')
            mark_spot()
            return

        if len(child_points) == 0:
            # dead end, nowhere to go
            self.graph.add_node(key=current_key, value='DE')
            mark_spot()
            return
        elif len(child_points) == 1:
            single_point = child_points[0]
            _, _, this_direction = single_point
            current_edge.add_step(this_direction)
            self._traverse(single_point, current_edge, from_node)
        else:
            mark_spot()

            for child_point in child_points:
                child_edge = Edge()
                child_edge.add_step(child_point[2])
                self._traverse(child_point, child_edge, self.graph.get_node(current_key))

    def _is_open(self, row: int, col: int) -> bool:
        return row >= 0 and col >= 0 and \
               row < len(self.data) and col < len(self.data[0]) and \
               self.data[row][col] != 0

    def _get_child_points(self, row: int, col: int) -> List[Tuple[int, int, str]]:
        child_points = []
        if self._is_open(row - 1, col): child_points.append((row - 1, col, 'N'))
        if self._is_open(row + 1, col): child_points.append((row + 1, col, 'S'))
        if self._is_open(row, col + 1): child_points.append((row, col + 1, 'E'))
        if self._is_open(row, col - 1): child_points.append((row, col - 1, 'W'))
        return child_points

    def _reverse_of(self, direction: str) -> str:
        if direction == 'N': return 'S'
        if direction == 'S': return 'N'
        if direction == 'E': return 'W'
        if direction == 'W':
            return 'E'
        else:
            return direction

    def _reverse_path(self, path: str) -> str:
        lst = [self._reverse_of(x) for x in list(path)]
        return ''.join(list(reversed(lst)))



if __name__ == '__main__':
    g = TraverseMap().traverse(maze_single_level)
    for node in g.nodes.values():
        print(f'Node {node.id.ljust(5)} | value: {node.value.ljust(2)} | Connected to: {node.connections}')

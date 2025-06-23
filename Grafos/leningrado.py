import sys
import heapq
from collections import defaultdict

def dijkstra_snipers(graph, snipers_count, start, end):
    """
    Dijkstra algorithm to find path with minimum snipers
    """
    # Initialize distances
    distances = defaultdict(lambda: float('inf'))
    distances[start] = snipers_count.get(start, 0)
    
    # Priority queue: (distance, vertex)
    pq = [(distances[start], start)]
    
    # Previous vertex for path reconstruction
    previous = {}
    visited = set()
    
    while pq:
        current_dist, u = heapq.heappop(pq)
        
        if u in visited:
            continue
            
        visited.add(u)
        
        if u == end:
            break
            
        # Explore neighbors
        for v in graph[u]:
            if v in visited:
                continue
                
            # Cost to reach vertex v is the snipers at v
            sniper_cost = snipers_count.get(v, 0)
            new_dist = distances[u] + sniper_cost
            
            if new_dist < distances[v]:
                distances[v] = new_dist
                previous[v] = u
                heapq.heappush(pq, (new_dist, v))
    
    # Reconstruct path
    if distances[end] == float('inf'):
        return float('inf'), []
    
    path = []
    current = end
    while current is not None:
        path.append(current)
        current = previous.get(current)
    path.reverse()
    
    return distances[end], path

def calculate_probability(snipers, bullets, hit_probability):
    """
    Calculate survival probability using simple exponential formula
    """
    return hit_probability ** snipers

def solve():
    """
    Main solution function
    """
    while True:
        try:
            line = input().strip()
            if not line:
                break
                
            # Parse header: V A K P
            parts = line.split()
            V = int(parts[0])  # vertices
            A = int(parts[1])  # edges  
            K = int(parts[2])  # bullets
            P = float(parts[3])  # hit probability
            
            # Create bidirectional graph
            graph = defaultdict(list)
            
            # Read edges
            for _ in range(A):
                u, v = map(int, input().split())
                graph[u].append(v)
                graph[v].append(u)  # bidirectional
            
            # Read snipers
            sniper_line = list(map(int, input().split()))
            num_snipers = sniper_line[0]
            sniper_positions = sniper_line[1:num_snipers+1]
            
            # Count snipers per vertex
            snipers_count = defaultdict(int)
            for pos in sniper_positions:
                snipers_count[pos] += 1
            
            # Read start and end
            start, end = map(int, input().split())
            
            # Find shortest path with minimum snipers
            min_snipers, path = dijkstra_snipers(graph, snipers_count, start, end)
            
            if min_snipers == float('inf'):
                print("0.000")
            else:
                # Calculate probability using exponential formula
                probability = calculate_probability(min_snipers, K, P)
                print(f"{probability:.3f}")
                
        except EOFError:
            break

if __name__ == "__main__":
    solve()
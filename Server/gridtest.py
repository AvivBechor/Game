from pathfinding.core.diagonal_movement import DiagonalMovement
from pathfinding.core.grid import Grid
from pathfinding.finder.a_star import AStarFinder
import time
import math
startime=time.time()

hmap = [
    [1, 1, 1, 1, 1],
    [1, 1, 1, 1, 1],
    [1, 1, 1, 1, 1],
    [1, 1, 0, 1, 1],
    [1, 1, 0, 1, 1]
]
grid = Grid(matrix=hmap)
finder = AStarFinder(diagonal_movement=DiagonalMovement.always)

enemy=(3,4)
player=(1,4)
enemySpeed=1

#path, runs = finder.find_path(enemy, player, grid)
#print(str(path))
previousTime=time.time()
starttime=time.time()
while True:
    time.sleep(0.001)
    currentTime = time.time()
    deltaTime = currentTime - previousTime
    ztep = enemySpeed * deltaTime
    print("step " + str(ztep))
    
    grid.cleanup()
    
    roundedEnemy=grid.node(math.floor(enemy[0]), math.floor(enemy[1]))
    roundedPlayer=grid.node(math.floor(player[0]), math.floor(player[1]))
    
    path, runs = finder.find_path(roundedEnemy, roundedPlayer, grid)

    if(len(path)==1):
        print("End time: " + str(time.time() - starttime))
        break;

    targetCell=path[1]

    change=(targetCell[0]-roundedEnemy.x, targetCell[1]-roundedEnemy.y)

    enemy=(enemy[0] + ztep * change[0], enemy[1] + ztep * change[1])

    
    print("enemy " + str(enemy[0]) + ", " + str(enemy[1]))

    
    previousTime=currentTime


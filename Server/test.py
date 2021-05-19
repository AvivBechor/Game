import math
import time
from queue import Queue
starttime = time.time()
hmap=[
    "**********",
    "*GGGGGGGG*",
    "*GGGGGGGG*",
    "*GGGGGGGG*",
    "*GGG*GGGG*",
    "*GGPGEGGG*",
    "**********"
    ];
def WorldToIndex(pos):
    return (pos[0], len(hmap)-1-pos[1]);
def FindNextCell(e,player, used=[]):
    avilableCells=[]
    bestCell=(e[0], e[1], 10000)
    for i in range(-1, 2):
        for j in range(-1, 2):
            currentX = i+e[0]
            currentY = j+e[1]
            currentCell=hmap[currentX][currentY]            
            if(currentCell != "*" and currentCell not in used):
                print("GOING TO " + currentCell + " AT " + str((currentX, currentY)))
                toAppend=(currentX, currentY, math.dist((currentX, currentY), player))
                if(toAppend[2] < bestCell[2]):
                    bestCell = toAppend
    return (bestCell[0], bestCell[1])

def FindPath(e, player):
    path=Queue()
    used=[]
    curr=e
    while(curr!=player):
        curr = FindNextCell(curr,player,used)
        path.put(curr)
    return path

enemy=(5,5)
player=(5,3)
enemySpeed=1
enemyTarget=FindNextCell(enemy,player)
avilableCells=[]
previousTime=time.time()
accum=0
enemypath=FindPath(enemy, player)

'''
while True:
    time.sleep(0.001)
    deltaTime=time.time() - previousTime
    accum+=deltaTime
    step = enemySpeed * deltaTime
    roundedEnemy = (math.ceil(enemy[0]), math.ceil(enemy[1]))
    if(accum >= 1):
        enemypath=FindPath(enemy, player)
    
    #if(roundedEnemy == enemyTarget):   
    #    enemyTarget = FindNextCell(roundedEnemy, player)
    #enemyChange = (enemyTarget[0]-roundedEnemy[0], enemyTarget[1]-roundedEnemy[1])
    #enemy = (enemy[0] + step * enemyChange[0], enemy[1] + step * enemyChange[1])
    #diff=(enemy[0]-player[0], enemy[1]-player[1])
    #if(abs(diff[0])+abs(diff[1]) < 0.02):
    #    print("END TIME" + str(time.time()-starttime))
    #    break;
    previousTime = time.time()
'''

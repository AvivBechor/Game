import math
import time
starttime = time.time()
hmap=[
    "**********",
    "*GGGGGGGG*",
    "*GGPGGGGG*",
    "*GGGGGGGG*",
    "*GGGGGGGG*",
    "*GGGGEGGG*",
    "**********"
    ];
def WorldToIndex(pos):
    return (pos[0], len(hmap)-1-pos[1]);
def FindNextCell(e,player):
    avilableCells=[]
    bestCell=(e[0], e[1], 10000)
    for i in range(-1, 2):
        for j in range(-1, 2):
            currentX = i+e[0]
            currentY = j+e[1]
            currentCell=hmap[currentX][currentY]            
            if(currentCell != "*"):
                toAppend=(currentX, currentY, math.dist((currentX, currentY), player))
                if(toAppend[2] < bestCell[2]):
                    bestCell = toAppend
    return (bestCell[0], bestCell[1])


enemy=(5,5)
player=(3,3)
enemySpeed=1.5
enemyTarget=FindNextCell(enemy,player)
avilableCells=[]
previousTime=time.time()


while True:
    time.sleep(0.001)
    deltaTime=time.time() - previousTime        
    step = enemySpeed * deltaTime
    #print("step" + str(step))
    roundedEnemy = (round(enemy[0]), round(enemy[1]))
    #print("roundedEnemy:" + str(roundedEnemy))
    #print("enemyTarget:" + str(enemyTarget))
    if(roundedEnemy == enemyTarget):   
        enemyTarget = FindNextCell(roundedEnemy, player)
        #print("new enemyTarget:" + str(enemyTarget))
    enemyChange = (enemyTarget[0]-roundedEnemy[0], enemyTarget[1]-roundedEnemy[1])
    #print("Change " + str(enemyChange))
    enemy = (enemy[0] + step * enemyChange[0], enemy[1] + step * enemyChange[1])
    #print(str(enemy));
    #print("____________")  
    if(roundedEnemy == player):
        print("END TIME" + str(time.time()-starttime))
        break;
    previousTime = time.time()

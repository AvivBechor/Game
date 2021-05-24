#gameProperties.py
from socket import *
import time
from datetime import datetime
import math
from pathfinding.core.diagonal_movement import DiagonalMovement
from pathfinding.core.grid import Grid
from pathfinding.finder.a_star import AStarFinder


class client:
    def __init__(self, ID, socket):
        self.groupID=ID
        self.socket=socket

        
class game:
    def __init__(self,ID):
        self.pending=False
        self.ID=ID
        self.previousTime=time.time()
        self.players=[]
        self.attacks=[]
        self.enemies=[]
        self.map=self.generateMap()
        self.grid = Grid(matrix=self.map)
        self.timeAccumulator=0
        self.finder = AStarFinder(diagonal_movement=DiagonalMovement.always)
    def generateMap(self):
        return [[1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1],
                [1, 1, 0, 1, 1],
                [1, 1, 0, 1, 1]
                ]
    def generateWave(self):
        self.enemies=[skeleton(100,"4,0","UP",{"speed":1},10,1),
                      skeleton(100,"0,0","UP",{"speed":1},11,1),
                      skeleton(100,"0,2","UP",{"speed":1},12,1)
                      ]
                      #generated list
        self.sendEnemyPos()
    def addPlayer(self,player):
        self.players.append(player)
    def addAttack(self,attack):
        self.attacks.append(attack)
    def removePlayer(self,player):
        try:
            self.players.remove(player)
        except:
            pass
    def sendEnemyPos(self):
        HEADER=4
        for p in self.players:
            for e in self.enemies:
                sendMessage("enm", "{ID}:{name}/{pos}/{level}".format(ID=e.ID,name=type(e).__name__,pos=str(self.WorldToIndex(e.pos))[1:-1].replace(" ",""),level=e.level),p.client.socket,HEADER)
    def WorldToIndex(self,pos):
        return (pos[0], len(self.map)-pos[1]-1)
    
    def run(self):
        #print("im running")
        #initialize
        time.sleep(0.01)
        HEADER=4
        atkAlive=True
        deltaTime=time.time() - self.previousTime
        self.timeAccumulator += deltaTime
        #spawn wave 
        if len(self.enemies)==0:
            self.generateWave()
        #kill attack
        for p in self.players:
            step = deltaTime*p.moveSpeed
            p.pos = (p.pos[0] + step*p.change[0], p.pos[1] + step*p.change[1])
        for atk in self.attacks:
            atk.timeLived += deltaTime
            if(atk.timeLived >= atk.lifespan):
                self.attacks.remove(atk)
                atkAlive=False
                continue
            if(self.timeAccumulator>=0.2):
                self.timeAccumulator=0
            #check collision
                for e in self.enemies:
                    x1=atk.pos[0] - atk.hitbox[0]/2
                    w1=atk.hitbox[0]
                    x2=e.pos[0] - e.hitbox[0]/2
                    w2=e.hitbox[0]
                    y1=atk.pos[1] - atk.hitbox[1]/2
                    h1=atk.hitbox[1]
                    y2=e.pos[1] - e.hitbox[1]/2
                    h2=e.hitbox[1]              
                    if( x1 + w1 > x2 and x1 + w1 < x2 + w2):
                        if(y1 + h1 > y2 and y1 + h1 < y2 + h2):
                            print("collide")
                            e.HP-=atk.dmg
                            if(e.HP<=0):
                                for p in self.players:
                                    sendMessage("kil","{ID}:Enemy".format(ID=e.ID),p.client.socket,HEADER)
                                    if(atk.isRanged==True):
                                        sendMessage("kil","{ID}:Attack".format(ID=atk.ID),p.client.socket,HEADER)
                                self.enemies.remove(e)
                            self.attacks.remove(atk)
                            atkAlive=False

            #move attacks          
            if (atkAlive):           
                step = atk.speed * deltaTime
                if(atk.direction == "RIGHT"):
                    atk.pos = (atk.pos[0] + step, atk.pos[1])
                elif(atk.direction == "LEFT"):
                    atk.pos = (atk.pos[0] - step, atk.pos[1])
                elif(atk.direction == "UP"):
                    atk.pos = (atk.pos[0], atk.pos[1] - step)
                elif(atk.direction == "DOWN"):
                    atk.pos = (atk.pos[0], atk.pos[1] + step)
                else:
                    print("INVALID DIRECTION FOR ATTACK " + str(atk.ID))
        #move enemies
        
        for e in self.enemies:
            change=(0,0)
            self.grid.cleanup()
            closestPlayer = e.findClosestPlayer(self.players)
            step=e.stats["speed"]*deltaTime
            path, roundedEnemy = e.find(closestPlayer, self.finder, self.grid)
            if(len(path) > 0):
                targetCell = path[0]
                change = (targetCell[0]-roundedEnemy.x, targetCell[1]-roundedEnemy.y)
                        #print("change is {c}\n path is {p}\n pos is {pos}\n ID is {id}\n and going to {player}\n-----".format(player=closestPlayer.pos, c=str(change), pos=str(e.pos), p=str(path), id=str(e.ID)));
                e.pos = (e.pos[0] + step * change[0], e.pos[1] + step * change[1])
                        #send change vector sendMessage("mov",change/e.name)
                                  
            for p in self.players:
                sendMessage("mov","{ID}:{pos}/Enemy/{c}".format(ID=e.ID,c=str((change[0],change[1]*-1))[1:-1].replace(" ",""),pos=str(self.WorldToIndex(e.pos))[1:-1].replace(" ","")),p.client.socket,HEADER)
        self.previousTime=time.time()

    
    
class player:
    def __init__(self,client,pos,ID,title,gender):
        self.client=client
        self.pos=pos
        self.ID=ID
        self.title=title 
        self.gender=gender
        self.change=(0,0)
        self.moveSpeed=4
    def setPos(self,pos):
        self.pos=pos
class enemy:
    def __init__(self,HP,pos,rotation,stats,ID,level):
        self.HP=HP
        self.pos=(float(pos.split(',')[0]),float(pos.split(',')[1]))
        self.rotation=rotation
        self.stats=stats
        self.ID=ID
        self.level=level
        self.change=(0,0)
        
    def find(self, player, finder, grid):
        
        
        roundedEnemy = grid.node(math.floor(self.pos[0]), math.floor(self.pos[1]))
        roundedPlayer = grid.node(math.floor(player.pos[0]), math.floor(player.pos[1]))
        
        path, runs = finder.find_path(roundedEnemy, roundedPlayer, grid)
        #print("I am at " + str(self.pos))
        #print("roundedEnemy is " + str((roundedEnemy.x, roundedEnemy.y)))
        #print("Player is at " + str(player.pos))
        #print("roundedPlayer is " + str((roundedPlayer.x, roundedPlayer.y)))
        #print("path is " + str(path))
        #print("-------")
        return path[1:], roundedEnemy
    
    def findClosestPlayer(self, players):
        #print("{ID}: ".format(ID=players[0]) + str(math.dist(players[0].pos, self.pos)) + " " + "{ID}: ".format(ID=players[1])+str(math.dist(players[1].pos, self.pos)))
        if(math.dist(players[0].pos, self.pos) < math.dist(players[1].pos, self.pos)):
            
            return players[0]
        return players[1]

class skeleton(enemy):
    def __init__(self,HP,pos,rotation,stats,ID,level):
        enemy.__init__(self,HP,pos,rotation,stats,ID,level)
        self.hitbox=self.createHitbox()
    def createHitbox(self):
        return(1,1)
class attack:
    def __init__(self,pos,dmg,playerID,speed,direction,lifespan,name,ID):
        self.pos=(float(pos.split(',')[0]),float(pos.split(',')[1]))
        self.dmg=dmg
        self.playerID=playerID
        self.speed=speed
        self.direction=direction
        self.lifespan=lifespan
        self.name=name
        self.ID=ID
        self.timeLived=0
        self.hitbox=self.createHitbox(name)
        self.isRanged=self.checkIsRanged(self.name)
    def createHitbox(self,name):
        if name=="strike":
            return (1,1)
    def checkIsRanged(self,name):
        if name=="strike":
            return True
        return False
        
        
            


def sendMessage(cmd,msg,s,HEADER):
        msg=cmd+":"+msg
        msg=f'{len(msg):<{HEADER}}'+msg
        if(len(msg)%2!=0):
            msg+="~"
        s.send(msg.encode("UTF-8"))

def recvMessage(s,HEADER):
    new_msg=True
    full_msg=''
    msg_len=0
    s.settimeout(0.01)
    try:
        while True:
            msg=s.recv(2)
            if new_msg:
                msg_len=int(msg[:HEADER])
                new_msg=False
            full_msg+=msg.decode("UTF-8")
        
            if (len(full_msg.replace("~",""))-HEADER==msg_len):
                new_msg=True
                return full_msg[HEADER:]
    except:
        print("error")
def findGame(ID,games):
    for g in games:
         if g.ID==ID:
             return g 
def deleteGame(games,gameID,playerID):
    
    g=findGame(gameID,games)
    if (g):
        
        for p in g.players:
            if(p.ID==playerID):
                g.removePlayer(p)
                
               
        if(len(g.players)==2):
            games.remove(g)
    else:
        print("game doesnt exist")
        
    

def handleData(data,s,games):
    cmd=data.split(':')[0]
    groupID=data.split(':')[1]
    playerID=data.split(':')[2]
    val=data.split(':')[3]
    g=findGame(groupID,games)
    val=val.replace("~","")
    HEADER=4
    if(cmd=="crt"):
        values=val.split("/")
        p=player(client(groupID,s),(3,4),playerID,values[0],values[1])
        if g:
            g.addPlayer(p)
            sendMessage("add","player added",s,HEADER)
            
        else:  
            games.append(game(groupID))
            games[-1].addPlayer(p)
            games[-1].pending=True            
            sendMessage("hlt","waiting for players",s,HEADER)
        if (g is not None and len(g.players)==2):
            g.generateWave()
            g.pending=False
            for p in g.players:
                for j in g.players:
                    if p is not j:
                        sendMessage("srt", "{ID}:{title}/{gender}/{pos}".format(pos=str(g.WorldToIndex(p.pos))[1:-1].replace(" ",""),title=p.title,gender=p.gender,ID=p.ID), j.client.socket, HEADER)
                
            games[-1].previousTime=time.time()


    elif(cmd=="mov"):
        for p in g.players:
            if(p.client.socket is s):
                p.change=(int(val.split(",")[0]),int(val.split(",")[1])*-1)
                sendMessage("rcv","0:recieved",p.client.socket,HEADER)
                continue
            sendMessage("mov","{ID}:{value}/Player".format(ID=playerID,value=val),p.client.socket,HEADER)
                
    elif(cmd=="atk"):
        values=val.split('/')
        pos=values[0]
        dmg=values[1]
        speed=values[2]
        direction=values[3]
        lifespan=values[4]
        name=values[5]
        ID=0
        atk=attack(pos,float(dmg),int(playerID),float(speed),direction,float(lifespan),name,ID)
        atk.pos=g.WorldToIndex(atk.pos)
        g.addAttack(atk)
        for p in g.players:
           sendMessage("atk", "{ID}:{position}/{AtkName}/{dir}".format(position=pos,AtkName=name,dir=direction,ID=atk.ID), p.client.socket, HEADER)

    elif(cmd=="pos"):
        for p in g.players:
            if(p.ID==playerID):
                p.setPos(g.WorldToIndex((float(val.split(",")[0]),float(val.split(",")[1]))))
            else:
                sendMessage("pos","{ID}:{value}".format(ID=p.ID,value=val),p.client.socket,HEADER)
    #elif(cmd=="nul"):
        #sendMessage("nul","-1:",s,HEADER)
        





#list of commands:
'''
crt: creates a new game 
mov: moves the player 
atk: attack 
pos: hard set player position 
swp: request to change room 
end: finish game
'''



def remove_client(client, clients):
    clients.remove(client)

def close(server_socket):# closes the server safely to avoid trouble.   
    try:
        server_socket.close()
        return False
    except OSError:
        pass

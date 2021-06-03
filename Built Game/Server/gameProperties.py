#gameProperties.py
from socket import *
import time
import random
from datetime import datetime
import math
from pathfinding.core.diagonal_movement import DiagonalMovement
from pathfinding.core.grid import Grid
from pathfinding.finder.a_star import AStarFinder


class client:
    def __init__(self, ID, socket):
        self.groupID=ID
        self.socket=socket
        self.isConnected=True

        
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
        self.waves=0
        self.defeteBoss=False
    def generateMap(self):
        return [[1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1]
                ]
    def generateWave(self):
        possibleEnemies=["skeleton","sorcerer","vampire"]
        self.waves+=1
        x=0
        y=0
        for i in range(3):
            while True:
                exists=False
                ID=random.randint(100,999)
                for e in self.enemies:
                    if(ID==e.ID):
                        exists=True
                        break
                if(exists==False):
                    break
                
            if i==0:
                x=0
            if i==1:
                x=2
            if i==2:
                x=4
            enemy=random.choice(possibleEnemies)
            if(enemy=="skeleton"):
                self.enemies.append(skeleton(100,"{posX},{posY}".format(posX=x,posY=y),"DOWN",{"speed":1},ID))
            elif(enemy=="sorcerer"):
                self.enemies.append(sorcerer(100,"{posX},{posY}".format(posX=x,posY=y),"DOWN",{"speed":1},ID))
                possibleEnemies.remove("sorcerer")
            elif(enemy=="vampire"):  
                self.enemies.append(vampire(100,"{posX},{posY}".format(posX=x,posY=y),"DOWN",{"speed":1},ID))
                                    
                

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
                sendMessage("enm", "{ID}:{name}/{pos}".format(ID=e.ID,name=type(e).__name__,pos=str(self.WorldToIndex(e.pos))[1:-1].replace(" ","")),p,HEADER)
    def WorldToIndex(self,pos):
        return (pos[0], len(self.map)-pos[1]-1)
    def summonBoss(self):
        self.enemies.append(boss(200,"0,4","LEFT",{"speed":2},100))
        self.sendEnemyPos()
        
    def run(self,games):
        #initialize
        time.sleep(0.01)
        HEADER=4
        atkAlive=True
        deltaTime=time.time() - self.previousTime
        self.timeAccumulator += deltaTime
        if(self.players[0].client.isConnected==False and self.players[1].client.isConnected==False):
            games.remove(self)
            return
        if(self.defeteBoss):
            #send a message says game is over and that the players won.
            for p in self.players:
                sendMessage("win","0:0",p,HEADER)
            games.remove(self)
            return 
            
        if(self.players[0].isDead and self.players[1].isDead):
            #send a message says game is over and that the players lost.
            for p in self.players:
                sendMessage("los","0:0",p,HEADER)
            games.remove(self)
            return
        
        
        #spawn wave
        if len(self.enemies)==0:
            if(self.waves<3):
                self.generateWave()
            else:
                self.defeteBoss=True  
            
        #move players 
        for p in self.players:
            step = deltaTime*p.moveSpeed
            newx, newy = (p.pos[0] + step*p.change[0]), (p.pos[1] + step*p.change[1])
            if(newx < 0):
                newx = 0
            elif(newx > len(self.map[0])-1):
                newx = len(self.map[0])-1
            if(newy < 0):
                newy = 0
            elif(newy > len(self.map)-1):
                newy = len(self.map)-1
            p.pos = (newx, newy)
            
            
        #kill attack
        for atk in self.attacks:
            atk.timeLived += deltaTime
            if(atk.timeLived >= atk.lifespan):
                try:
                    self.attacks.remove(atk)
                except:
                    pass
                if len(self.attacks)==0:
                    atkAlive=False
                continue
            if(self.timeAccumulator>=0.2):
                self.timeAccumulator=0
            #check collision
                for e in self.enemies:
                    x1=atk.pos[0]
                    w1=atk.hitbox[0]
                    x2=e.pos[0]
                    w2=e.hitbox[0]
                    y1=atk.pos[1]
                    h1=atk.hitbox[1]
                    y2=e.pos[1]
                    h2=e.hitbox[1]              
                    if( x1 + w1 > x2 and x1 < x2 + w2):
                        if(y1 + h1 > y2 and y1 < y2 + h2):
                            e.HP-=atk.dmg
                            for p in self.players:
                                sendMessage("ehp","{ID}:{HP}".format(ID=e.ID,HP=e.HP),p,HEADER)
                            if(atk.isRanged==True):
                                for p in self.players:
                                    sendMessage("kil","{ID}:Attack".format(ID=atk.ID),p,HEADER)
                            if(e.HP<=0):
                                for p in self.players:
                                    sendMessage("kil","{ID}:Enemy".format(ID=e.ID),p,HEADER)
                                '''    
                                if(e.isBoss):
                                    #send message that says boss defeted
                                    self.defeteBoss=True
                                '''    
                                self.enemies.remove(e)
                                
                            try:
                                self.attacks.remove(atk)
                            except:
                                pass
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
            e.act(deltaTime, self.players, self)
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
        self.isDead=False
        
    def setPos(self,pos):
        self.pos=pos
class enemy:
    def __init__(self,HP,pos,rotation,stats,ID):
        self.HP=HP
        self.poscum=0
        self.state="move"
        self.pos=(float(pos.split(',')[0]),float(pos.split(',')[1]))
        self.rotation=rotation
        self.stats=stats
        self.ID=ID
        self.change=(0,0)
        self.isBoss=False
    def find(self, player, finder, grid):      
        try:
            roundedEnemy = grid.node(math.floor(self.pos[0]), math.floor(self.pos[1]))
            roundedPlayer = grid.node(math.floor(player.pos[0]), math.floor(player.pos[1]))
        except:
            pass
        path, runs = finder.find_path(roundedEnemy, roundedPlayer, grid)
        return path[1:], roundedEnemy
    
    def findClosestPlayer(self, players):
        if(players[0].isDead):
            return players[1]
        elif(players[1].isDead):
            return players[0]
        else:
            if(math.dist(players[0].pos, self.pos) < math.dist(players[1].pos, self.pos)):
                return players[0]
            return players[1]
class boss(enemy):
    def __init__(self,HP,pos,rotation,stats,ID):
        enemy.__init__(self,HP,pos,rotation,stats,ID)
        self.hitbox=self.createHitbox()
        self.isBoss=True
        self.strength=20
        self.atkcum=0
        self.cooldown=1
    def createHitbox(self):
        return(1,1)
    def act(self, deltaTime, playerList, game):
        HEADER=4
        closestPlayer = self.findClosestPlayer(playerList)
        if self.state == "move":
            self.atkcum += deltaTime
            change=(0,0)
            game.grid.cleanup()
            step=self.stats["speed"]*deltaTime
            path,roundedEnemy = self.find(closestPlayer, game.finder, game.grid)
            if(len(path) > 0):
                targetCell = path[0]
                change = (targetCell[0]-roundedEnemy.x, targetCell[1]-roundedEnemy.y)
                        
                self.pos = (self.pos[0] + step * change[0], self.pos[1] + step * change[1])
                        #send change vector sendMessage("mov",change/e.name)
            elif(len(path) == 0):
                self.state = "attack";
            if (change != self.change):
                self.change=change
                for p in playerList:
                    sendMessage("mov","{ID}:{c}/Enemy".format(ID=self.ID,c=str((change[0],change[1]*-1))[1:-1].replace(" ","")),p,HEADER)
                    
        elif(self.state == "attack"):
            if(self.atkcum >= self.cooldown):
               for p in playerList:
                   sendMessage("atk", "{ID}:{position}/{AtkName}/{dir}/Skeleton/{dmg}/{player}".format(player=closestPlayer.ID,dmg=self.strength, ID=self.ID,position=str(game.WorldToIndex(self.pos))[1:-1].replace(" ",""),AtkName="attack",dir="UP"),p,HEADER)
               self.atkcum = 0
            self.state = "move"
class skeleton(enemy):
    
    def __init__(self,HP,pos,rotation,stats,ID):
        enemy.__init__(self,HP,pos,rotation,stats,ID)
        self.hitbox=self.createHitbox()
        self.cooldown = 1
        self.atkcum = 0
        self.strength=8
    def createHitbox(self):
        return(1,1)
    
    def act(self, deltaTime, playerList, game):
        HEADER=4
        closestPlayer = self.findClosestPlayer(playerList)
        if self.state == "move":
            self.atkcum += deltaTime
            change=(0,0)
            game.grid.cleanup()
            step=self.stats["speed"]*deltaTime
            path,roundedEnemy = self.find(closestPlayer, game.finder, game.grid)
            if(len(path) > 0):
                targetCell = path[0]
                change = (targetCell[0]-roundedEnemy.x, targetCell[1]-roundedEnemy.y)
                        
                self.pos = (self.pos[0] + step * change[0], self.pos[1] + step * change[1])
                        #send change vector sendMessage("mov",change/e.name)
            elif(len(path) == 0):
                self.state = "attack";
            if (change != self.change):
                self.change=change
                for p in playerList:
                    sendMessage("mov","{ID}:{c}/Enemy".format(ID=self.ID,c=str((change[0],change[1]*-1))[1:-1].replace(" ","")),p,HEADER)
                    
        elif(self.state == "attack"):
            if(self.atkcum >= self.cooldown):
               for p in playerList:
                   sendMessage("atk", "{ID}:{position}/{AtkName}/{dir}/Skeleton/{dmg}/{player}".format(player=closestPlayer.ID,dmg=self.strength, ID=self.ID,position=str(game.WorldToIndex(self.pos))[1:-1].replace(" ",""),AtkName="attack",dir="UP"),p,HEADER)
               self.atkcum = 0
            self.state = "move"
            
        
class sorcerer(enemy): 
    def __init__(self,HP,pos,rotation,stats,ID):
        enemy.__init__(self,HP,pos,rotation,stats,ID)
        self.hitbox=self.createHitbox()
        self.cooldown = 5
        self.atkcum = 0
        self.state = "heal"
        self.magicPower=5
    def createHitbox(self):
        return(1,1)
    def act(self, deltaTime, playerList, game):
        HEADER=4
        self.atkcum += deltaTime
        if(self.atkcum >= self.cooldown):
            
            for e in game.enemies:
                e.HP += self.magicPower
                if e.HP>100:
                    e.HP=100
                for p in playerList:
                    sendMessage("ehp","{ID}:{HP}".format(ID=e.ID,HP=e.HP),p,HEADER)

            for p in playerList:
                sendMessage("atk", "{ID}:{position}/{AtkName}/{dir}/Sorcerer/0/0".format(ID=self.ID,position=str(game.WorldToIndex(self.pos))[1:-1].replace(" ",""),AtkName="attack",dir="UP"),p,HEADER)
            self.atkcum = 0
        
class vampire(enemy):
    def __init__(self,HP,pos,rotation,stats,ID):
        enemy.__init__(self,HP,pos,rotation,stats,ID)
        self.hitbox=self.createHitbox()
        self.atkcum = 0
        self.cooldown = 1.5
        self.strength=5
    def createHitbox(self):
        return(1,1)
    def act(self, deltaTime, playerList, game):
        HEADER=4
        closestPlayer = self.findClosestPlayer(playerList)
        if self.state == "move":
            self.atkcum += deltaTime
            change=(0,0)
            game.grid.cleanup()
            step=self.stats["speed"]*deltaTime
            path,roundedEnemy = self.find(closestPlayer, game.finder, game.grid)
            if(len(path) > 0):
                targetCell = path[0]
                change = (targetCell[0]-roundedEnemy.x, targetCell[1]-roundedEnemy.y)
                        #print("change is {c}\n path is {p}\n pos is {pos}\n ID is {id}\n and going to {player}\n-----".format(player=closestPlayer.pos, c=str(change), pos=str(e.pos), p=str(path), id=str(e.ID)));
                self.pos = (self.pos[0] + step * change[0], self.pos[1] + step * change[1])
                        #send change vector sendMessage("mov",change/e.name)
            elif(len(path) == 0):
                self.state = "attack"
            if (change != self.change):
                self.change=change
                for p in playerList:
                    sendMessage("mov","{ID}:{c}/Enemy".format(ID=self.ID,c=str((change[0],change[1]*-1))[1:-1].replace(" ","")),p,HEADER)
                    #pos=str(self.WorldToIndex(e.pos))[1:-1].replace(" ","")
        elif(self.state == "attack"):
            if(self.atkcum >= self.cooldown):
                self.HP += 2
                if self.HP>100:
                    self.HP=100
                for p in playerList:
                   sendMessage("atk", "{ID}:{position}/{AtkName}/{dir}/Vampire/{dmg}/{player}".format(player=closestPlayer.ID,dmg=self.strength, ID=self.ID,position=str(game.WorldToIndex(self.pos))[1:-1].replace(" ",""),AtkName="attack",dir="UP"),p,HEADER)
                   sendMessage("ehp","{ID}:{HP}".format(ID=self.ID,HP=self.HP),p,HEADER)

                self.atkcum = 0
            self.state = "move"
    
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
        if name=="vroom":
            return (1,1)
        elif name=="strike":
            return (1,1)
    def checkIsRanged(self,name):
        if name=="vroom":
            return True
        return False
        
        
            


def sendMessage(cmd,msg,player,HEADER):
    try:
        msg=cmd+":"+msg
        msg=f'{len(msg):<{HEADER}}'+msg
        if(len(msg)%2!=0):
            msg+="~"
        player.client.socket.send(msg.encode("UTF-8"))
    except:
        player.client.isDead=False

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
            sendMessage("add","player added",p,HEADER)
            
        else:  
            games.append(game(groupID))
            games[-1].addPlayer(p)
            games[-1].pending=True            
            sendMessage("hlt","waiting for players",p,HEADER)
        if (g is not None and len(g.players)==2):
            g.generateWave()
            g.pending=False
            for p in g.players:
                for j in g.players:
                    if p is not j:
                        sendMessage("srt", "{ID}:{title}/{gender}/{pos}".format(pos=str(g.WorldToIndex(p.pos))[1:-1].replace(" ",""),title=p.title,gender=p.gender,ID=p.ID), j, HEADER)
                
            games[-1].previousTime=time.time()

    if (g is None):
        return 0
    elif(cmd=="mov"):
        for p in g.players:
            if(p.client.socket is s):
                p.change=(int(val.split(",")[0]),int(val.split(",")[1])*-1)
                sendMessage("rcv","0:recieved",p,HEADER)
                continue
            sendMessage("mov","{ID}:{value}/Player".format(ID=playerID,value=val),p,HEADER)
                
    elif(cmd=="atk"):
        values=val.split('/')
        pos=values[0]
        dmg=values[1]
        speed=values[2]
        direction=values[3]
        lifespan=values[4]
        name=values[5]
        ID=random.randint(0,1000)
        atk=attack(pos,float(dmg),int(playerID),float(speed),direction,float(lifespan),name,ID)
        atk.pos=g.WorldToIndex(atk.pos)
        g.addAttack(atk)
        for p in g.players:
           sendMessage("atk", "{ID}:{position}/{AtkName}/{dir}/Player/{dmg}/0".format(dmg=atk.dmg,position=str(g.WorldToIndex(atk.pos))[1:-1].replace(" ",""),AtkName=name,dir=direction,ID=atk.ID), p, HEADER)

    elif(cmd=="pos"):
        for p in g.players:
            if(p.ID==playerID):
                p.setPos(g.WorldToIndex((float(val.split(",")[0]),float(val.split(",")[1]))))
            else:
                sendMessage("pos","{ID}:{value}/Player".format(ID=p.ID,value=val),p,HEADER)
    elif(cmd=="kil"):
        for p in g.players:
            if str(p.ID)==playerID:
                p.isDead=True
                p.change=(0,0)
                continue
            sendMessage("kil","{ID}:Player".format(ID=playerID), p,HEADER)
            
            
                
        





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

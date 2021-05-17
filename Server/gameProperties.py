from socket import *
import time
from datetime import datetime

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
        self.map=[]
        self.timeAccumulator=0
    def generate(self):
        self.map=self.generateMap()
        self.enemies=self.generateWave()
    def generateMap(self):
        pass
    def generateWave(self):
        return [skeleton(100,"5,3","UP","stats",10)]
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
        pass
        #send the position of all the enemies to all players
    def run(self):
        
        HEADER=4
        atkAlive=True
        deltaTime=time.time() - self.previousTime
        self.timeAccumulator += deltaTime
        if len(self.enemies)==0:
            self.enemies=self.generateWave()
        for atk in self.attacks:            
            atk.timeLived += deltaTime
            if(atk.timeLived >= atk.lifespan):
                self.attacks.remove(atk)
                continue
            if(self.timeAccumulator>=0.2):
                self.timeAccumulator=0
                for e in self.enemies:
                    x1=atk.pos[0] - atk.hitbox[0]/2
                    w1=atk.hitbox[0]
                    x2=e.pos[0] - e.hitbox[0]/2
                    w2=e.hitbox[0]
                    y1=atk.pos[1] - atk.hitbox[1]/2
                    h1=atk.hitbox[1]
                    y2=e.pos[1] - e.hitbox[1]/2
                    h2=e.hitbox[1]              
                    if( x1 + w1 >= x2 and x1 + w1 <= x2 + w2):
                        if(y1 + h1 >= y2 and y1 + h1 <= y2 + h2):
                            e.HP-=atk.dmg
                            if(e.HP<=0):
                                print("COLLISION " + str(datetime.now()))
                                for p in self.players:
                                
                                    sendMessage("kil","{ID}:Enemy".format(ID=e.ID),p.client.socket,HEADER)
                                    if(atk.isRanged==True):
                                        sendMessage("kil","{ID}:Attack".format(ID=atk.ID),p.client.socket,HEADER)
                                self.enemies.remove(e)
                            self.attacks.remove(atk)
                            atkAlive=False
                            
                        
                        
            if (atkAlive):           
                step = atk.speed * deltaTime
                if(atk.direction == "RIGHT"):
                    atk.pos = (atk.pos[0] + step, atk.pos[1])
                elif(atk.direction == "LEFT"):
                    atk.pos = (atk.pos[0] - step, atk.pos[1])
                elif(atk.direction == "UP"):
                    atk.pos = (atk.pos[0], atk.pos[1] + step)
                elif(atk.direction == "DOWN"):
                    atk.pos = (atk.pos[0], atk.pos[1] - step)
                else:
                    print("INVALID DIRECTION FOR ATTACK " + str(atk.ID))
            
        self.previousTime=time.time()

    
    
class player:
    def __init__(self,client,pos,ID,title,gender):
        self.client=client
        self.pos=pos
        self.ID=ID
        self.title=title 
        self.gender=gender
    def setPos(self,x,y):
        self.pos=(x,y)
class enemy:
    def __init__(self,HP,pos,rotation,stats,ID):
        self.HP=HP
        self.pos=(float(pos.split(',')[0]),float(pos.split(',')[1]))
        self.rotation=rotation
        self.stats=stats
        self.ID=ID
        
    def find(self):
        #pathfinding
        pass
        

class skeleton(enemy):
    def __init__(self,HP,pos,rotation,stats,ID):
        enemy.__init__(self,HP,pos,rotation,stats,ID)
        self.hitbox=self.createHitbox()
    def find(self):
        #pathfidning
        pass
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
    #s.settimeout(0.01)
    while True:
        msg=s.recv(2)
        if new_msg:
            msg_len=int(msg[:HEADER])
            new_msg=False
        full_msg+=msg.decode("UTF-8")
        
        if (len(full_msg.replace("~",""))-HEADER==msg_len):
            new_msg=True
            return full_msg[HEADER:]
def findGame(ID,games):
    for g in games:
         if g.ID==ID:
             return g 
def deleteGame(games,gameID,playerID,HEADER):
    
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
        p=player(client(groupID,s),(-5,0),playerID,values[0],values[1])
        if g:
            g.addPlayer(p)
            sendMessage("add","player added",s,HEADER)
            if len(g.players)==2:
                for p in g.players:
                    sendMessage("pos","{ID}:-5,0".format(ID=p.ID),p.client.socket,HEADER)
                
        else:  
            games.append(game(groupID))
            games[-1].addPlayer(p)
            games[-1].pending=True            
            sendMessage("hlt","waiting for players",s,HEADER)
        if (g is not None and len(g.players)==2):
            g.pending=False
            g.generate()
            for p in g.players:
                for j in g.players:
                    if p is not j:
                        sendMessage("srt", "{ID}:{title}/{gender}".format(title=p.title,gender=p.gender,ID=p.ID), j.client.socket, HEADER)
                
            games[-1].previousTime=time.time()


    elif(cmd=="mov"):
        for p in g.players:
            if(p.client.socket is s):
                sendMessage("rcv","0:recieved",p.client.socket,HEADER)
                continue
            sendMessage("mov","{ID}:{value}".format(ID=playerID,value=val),p.client.socket,HEADER)
                
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
        g.addAttack(atk)
        print(atk.ID)
        for p in g.players:
           sendMessage("atk", "{ID}:{position}/{AtkName}/{dir}".format(position=pos,AtkName=name,dir=direction,ID=atk.ID), p.client.socket, HEADER)

    elif(cmd=="pos"):
        for p in g.players:
            if(p.ID==playerID):
                p.setPos(int(val.split(",")[0]),int(val.split(",")[1]))
            else:
                sendMessage("pos","{ID}:{value}".format(ID=p.ID,value=val),p.client.socket,HEADER)
    elif(cmd=="nul"):
        sendMessage("nul","-1:",s,HEADER)
        
    if (g):
        if(g.pending!=True):
            g.run()       





#list of commands:
'''
crt: creates a new game 
mov: moves the player 
atk: attack 
pos: hard set player position 
swp: request to change room 
end: finish game

    
def send_data(w_list):#sends data to every client (you can always exclude someone with a simple if)
    while not data_to_send.empty():
        data = data_to_send.get()
        for c in clients:
            if c in w_list:
                try:
                    c.send(data.encode())
                except ConnectionError:
                    remove_client(c)
'''

def recv_all(sock):# receives all data (in case it's bigger than the buffer).
    BUFF_SIZE = 4096  # 4 KiB
    data = b''
    while True:
        part = sock.recv(BUFF_SIZE)
        data += part
        if len(part) < BUFF_SIZE:# either 0 or end of data   
            break
    return data

def remove_client(client, clients):
    clients.remove(client)

def close(server_socket):# closes the server safely to avoid trouble.   
    try:
        server_socket.close()
        return False
    except OSError:
        pass

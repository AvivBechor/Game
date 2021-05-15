from queue import Queue
from socket import *
import time

class client:
    def __init__(self, ID, socket):
        self.groupID=ID
        self.socket=socket

        
class game:
    def __init__(self,ID):
        self.pending=False
        self.data_to_send=Queue()
        self.ID=ID
        self.previousTime=time.time()
        self.players=[]
        self.attacks=[]
        self.enemies=[]
    def addPlayer(self,player):
        self.players.append(player)
    def addAttack(self,attack):
        self.attacks.append(attack)
    def removePlayer(self,player):
        try:
            self.players.remove(player)
        except:
            pass
    def run(self):
        HEADER=4
        for atk in self.attacks:
            deltaTime=time.time() - self.previousTime
            atk.timeLived += deltaTime
            if(atk.timeLived >= atk.lifespan):
                self.attacks.remove(atk)
                continue
            print("DELTA TIME IS " + str(deltaTime))
            step = atk.speed * deltaTime
            print("ATTACK POSITION IS " + str(atk.pos))
            if(atk.direction == "RIGHT"):
                atk.pos = (atk.pos[0] + step, atk.pos[1])
            elif(atk.direction == "LEFT"):
                atk.pos = (atk.pos[0] - step, atk.pos[1])
            elif(atk.direction == "UP"):
                atk.pos = (atk.pos[0], atk.pos[1] + step)
            elif(atk.direction == "DOWN"):
                atk.pos = (atk.pos[0], atk.pos[1] - step)
            else:
                print("INVALID DIRECTION FOR ATTACK " + str(atk.ID));
            self.previousTime=time.time()
        
        pass

    
    
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
    def __init__(self,HP,pos,rotation,stats,change):
        self.HP=HP
        self.pos=(pos.split(',')[0],pos.split(',')[1])
        self.rotation=rotation
        self.stats=stats
        self.change=change
class attack:
    def __init__(self,pos,dmg,playerID,speed,direction,lifespan,name,ID):
        self.pos=(int(pos.split(',')[0]),int(pos.split(',')[1]))
        self.dmg=dmg
        self.playerID=playerID
        self.speed=speed
        self.direction=direction
        self.lifespan=lifespan
        self.name=name
        self.ID=ID
        self.timeLived=0

 
def sendMessage(cmd,msg,s,HEADER):
    try:
        msg=cmd+":"+msg
        msg=f'{len(msg):<{HEADER}}'+msg
        if(len(msg)%2!=0):
            msg+="~"
        s.send(msg.encode("UTF-8"))
    except:
        print("not connected")

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

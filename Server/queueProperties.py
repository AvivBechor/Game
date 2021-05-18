#queueProperies.py
import random
global pendingGames
global gamesInPlay
global HEADER
HEADER=4
def sendMessage(cmd,msg,s,HEADER):
    msg=cmd+":"+msg
    msg=f'{len(msg):<{HEADER}}'+msg
    if(len(msg)%2!=0):
        msg+="~"
    s.send(msg.encode("UTF-8"))
    
        
def recvMessage(s,HEADER):
    try:
        new_msg=True
        full_msg=''
        msg_len=0
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
        print("not connected")
        return ""
def generateID(s,gamesInPlay,pendingGames,playerID):
    newGameID=random.randint(100, 999)
    for g in gamesInPlay:
        if g==newGameID:
            return generateID(s,pendingGames,playerID)
    pendingGames.append(newGameID)
    sendMessage("uid", "{gameID}:{pID}".format(gameID=str(pendingGames[0]),pID=playerID), s, HEADER)
                
def handleData(data,s,gamesInPlay,pendingGames):
    playerID=1
    cmd=data.split(':')[0]
    val=data.split(':')[1]
    val=val.replace("~","")
    
    if cmd=="crt":
        sendMessage("rcv","",s,HEADER)
        if len(pendingGames)!=0:
            playerID=2
            sendMessage("uid", "{gameID}:{pID}".format(gameID=str(pendingGames[0]),pID=playerID), s, HEADER)
            gamesInPlay.append(pendingGames[0])
            pendingGames.remove(pendingGames[0])
        else:
            generateID(s,gamesInPlay,pendingGames,playerID)
    if cmd=="del":
        gamesInPlay.remove(int(val))

        
        
        
                

       
        

    





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

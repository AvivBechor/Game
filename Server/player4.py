import socket
import time
import threading
global HEADER
s=socket.socket()
s.connect(("127.0.0.1",5555))
s.setblocking(1)
new_msg=True
HEADER=4
full_msg=''
msg_len=0
inGame=False
ID="178"
#s.send(b"hello")
count=0
print("im player 4" + str(s))


def sendMessage(cmd,gameID,userID,msg,s,HEADER):
    try:
        msg=cmd+":"+gameID+":"+userID+":"+msg
        msg=f'{len(msg):<{HEADER}}'+msg
        if(len(msg)%2!=0):
            msg+="~"
        s.send(msg.encode())
    except e:
        print("error is: " + str(e))

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
            full_msg+=msg.decode()
        
            if (len(full_msg.replace("~",""))-HEADER==msg_len):
                new_msg=True
                return full_msg[HEADER:]
    except:
        print("not connected")
        return ""

while True:
    
    if count<1:
        sendMessage("crt",ID,"2","mage/0",s,HEADER)
    data=recvMessage(s,HEADER)
    data=data.replace("~","")
    print(data)
    if data.split(":")[0]=='srt':
        inGame=True
    while inGame:
        data=recvMessage(s,HEADER)
        data=data.replace("~","")
        if(data.split(':')[0]=="mov"):
            print(data)
        
        if (count==1):
            sendMessage("atk", ID, "2", "3,1/100/3/UP/2/strike", s, HEADER)
        if (count==2):
            sendMessage("mov",ID,"2","0,1",s,HEADER)
        elif (count==20): 
            sendMessage("mov",ID,"2","0,0",s,HEADER)
        elif(count==40):
            sendMessage("mov",ID,"2","1,0",s,HEADER)
        elif(count==60):
            sendMessage("mov",ID,"2","0,0",s,HEADER)
        
        count+=1
    if data=="":
        break
    count+=1


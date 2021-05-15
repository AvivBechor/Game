import socket
import time
import random
s=socket.socket()
s.connect(("127.0.0.1",5556))
s.setblocking(1)
new_msg=True
HEADER=4
full_msg=''
msg_len=0
ID=""
#s.send(b"hello")
count=0
def sendMessage(cmd,msg,s,HEADER):
    try:
        msg=cmd+":"+msg
        msg=f'{len(msg):<{HEADER}}'+msg
        if(len(msg)%2!=0):
            msg+="~"
        s.send(msg.encode())
    except:
        print("not connected")

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
        sendMessage("crt","",s,HEADER)
    data=recvMessage(s,HEADER)
    data=data.replace("~","")
    if data.split(":")[0]=="uid":
        ID=int(data.split(":")[1])
        playerID=int(data.split(":")[2])
    print("Your game ID is " + str(ID) + " and your player ID is " + str(playerID))
    count+=1



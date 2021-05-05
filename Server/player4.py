import socket
import time 
s=socket.socket()
s.connect(("127.0.0.1",5555))
s.setblocking(1)
new_msg=True
HEADER=4
full_msg=''
msg_len=0
inGame=False
ID="111"
#s.send(b"hello")
count=0
print("im player 4")
def sendMessage(cmd,gameID,userID,msg,s,HEADER):
    try:
        msg=cmd+":"+gameID+":"+userID+":"+msg
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
    data=""
    if count<1:
        sendMessage("crt",ID,"4","mage/0",s,HEADER)
    data=recvMessage(s,HEADER)
    data=data.replace("~","")
    print(data)
    if data.split(":")[0]=='pos':
        inGame=True
    
    while inGame:
        if (count==1):
            sendMessage("atk", ID, "4", "5,-3/30/3/LEFT/1/bomb", s, HEADER)
        elif (count<10):
            sendMessage("mov",ID,"4","-10,10",s,HEADER)
        else: 
            sendMessage("end",ID,"4","",s,HEADER)
            break
        count+=1
    if data=="":
        break
    count+=1

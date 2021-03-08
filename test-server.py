import socket
import time
s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind(("",5555))
s.listen(1)
conn,addr=s.accept()
count=0
while True:
    #data=conn.recv(2048).decode()
    if count%2==0:
        conn.send("t:1,0".encode())
    else:
        conn.send("e:1,0".encode())
    count+=1
    #print(data)
#84.108.93.229

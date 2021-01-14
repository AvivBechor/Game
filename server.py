#this is the game server
import socket
import threading
import time 

s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
ip="0.0.0.0"
port=5555

s.bind((ip,port))
s.listen(1)
print("Waiting for a connection, Server Started")
conn,addr=s.accept()
print("Connected to:", addr)
while True:
    data=conn.recv(2048)
    if data.decode()=="exit":
        s.close()
        break

#this is the game server
import socket
import threading
from datetime import datetime
 

s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
ip="0.0.0.0"
port=5555

s.bind((ip,port))
s.listen(1)
print("Waiting for a connection, Server Started")
conn,addr=s.accept()
print("Connected to:", addr)
def exit(conn):
    conn.close()
def time():
    now = datetime.now()
    current_time = now.strftime("%H:%M:%S")
    return current_time
while True:
    data=conn.recv(2048)
    if data.decode()=="exit":
        exit(conn)
        break     
    try:
        result=eval("{}()".format(data.decode()))
        conn.send(result.encode())
    except:
        conn.send("please enter a valid request".encode())
        
    
s.close()

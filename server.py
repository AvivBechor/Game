#this is the game server
import socket
import threading
from datetime import datetime
import pandas as pd
import csv
global usernames
global passwords
global conn,addr 
df= pd.read_csv("usernames and passwords.csv")
usernames=df["Username"].tolist()
passwords=df["Password"].tolist()
usernames.append("test")

s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
ip="0.0.0.0"
port=5555
s.bind((ip,port))
s.listen(1)
print("Waiting for a connection, Server Started")
conn,addr=s.accept()
print("Connected to:", addr)
def exit():
    conn.close()
def login():
    #1:get username
    #2:get password
    #3:succesful login
    #01:username not found
    #02:password not found 
    conn.send(1.encode())
    user=conn.revc(2048)
    while user:
        if user.decode() in usernames:
            conn.send(2.encode())
            pas=conn.revc(2048)
            while pas:
                if pas.decode in passwords:
                    conn.send(3.encode())
                    break
                else:
                    conn.send("02".encode())
                    pas=conn.revc(2048)
                    
           
        else
            conn.send("01".encode())
            user=conn.revc(2048)
        
  
while True:
    data=conn.recv(2048)
    print(data.decode())
    if data.decode()=="exit":
        exit(conn)
        break
    eval("{}()".format(data.decode()))
    '''
    try:
        eval("{}()".format(data.decode()))
    except:
        conn.send("please enter a valid request".encode())
 '''       

s.close()


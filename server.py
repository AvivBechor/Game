#this is the game server
import socket
import threading
from datetime import datetime
import pandas as pd
import csv
import time
global usernames
global passwords
global conn,addr 
df= pd.read_csv("usernames and passwords.csv")
usernames=df["Username"].tolist()
passwords=df["Password"].tolist()
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
def signup():#this is NOT good, this is just temporary. To do: instead of 2 lists work straight on the df and update it each time u make a new user.
    #1.1:get username-signup
    #2.1:get password-signup
    #3.1:signup succesful
    #01.1:username already exists
    #02.1:password already exists 
    conn.send(b'1.1')
    user=conn.recv(2048)
    while user:
        if user.decode() in usernames:
            conn.send(b'01.1')
            user=conn.recv(2048)
        else:
            conn.send(b'2.1')
            pas=conn.recv(2048)
            while pas:
                if pas in passwords:
                    conn.send(b'02.1')
                    user=conn.recv(2048)
                else:
                    uasernames.append(user)
                    passwords.append(pas)
                    new=(usernames[-1],passwords[-1])
                    df.append(new)
                    
            
def login():
    #1:get username
    #2:get password
    #3:succesful login
    #01:username not found
    #02:password not found 
    conn.send(b'1')
    user=conn.recv(2048)
    while user:
        if user.decode() in usernames:
            i=usernames.index(user.decode())
            print(usernames[i])
            conn.send(b'2')
            pas=conn.recv(2048)
            while pas:
                if passwords[i]==pas.decode():
                    conn.send(b'3')
                    user=None
                    break
                else:
                    conn.send("02".encode())
                    pas=conn.recv(2048)
                    
           
        else:
            conn.send("01".encode())
            user=conn.recv(2048)
            continue
    
  
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


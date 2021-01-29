#this is the game server
import socket
import threading
from datetime import datetime
import pandas as pd
import csv
import time
global conn,addr
global df
global lock

df=pd.read_csv("usernames and passwords.csv")
lock=threading.Lock()

def exit():
    conn.close()
def login():
    #1:get username
    #2:get password
    #3:succesful login
    #01:username not found
    #02:password not found 
    conn.send(b'1')
    user=conn.recv(2048)
    while user:
        if user.decode() in list(df["Username"]):
            i=list(df["Username"]).index(user.decode())
            conn.send(b'2')
            pas=conn.recv(2048)
            while pas:
                if list(df["Password"])[i]==pas.decode():
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
def signup():#make it a locked action so 2 connections couldnt write to the database at the same time 
    #1.1:get username-signup
    #2.1:get password-signup
    #3.1:signup succesful
    #01.1:username already exists
    df=pd.read_csv("usernames and passwords.csv")
    if lock.locked(): #lock only at data base write, not all the fuction 
        return False
    else:
        lock.acquire()
        conn.send(b'1.1')
        user=conn.recv(2048)
        while user:
            if user.decode() in list(df["Username"]):
                conn.send(b'01.1')
                user=conn.recv(2048)
            else:
                conn.send(b'2.1')
                pas=conn.recv(2048)
                s=pd.Series({"Username":user.decode(),"Password":pas.decode()})
                df=df.append(s,ignore_index=True)
                df.to_csv("usernames and passwords.csv",index=False)
                conn.send(b'3.1')
                user=None
                print(df)
                lock.release()
                return True 
                    
#server
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
    print(data.decode())
    if data.decode()=="exit":
        exit(conn)
        break
    while not eval("{}()".format(data.decode())):
        conn.send(b'please try again')
        data=conn.recv(2048)
    '''
    try:
        eval("{}()".format(data.decode()))
    except:
        conn.send("please enter a valid request".encode())
 '''       

s.close()


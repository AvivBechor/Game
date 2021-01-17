import socket
#create an idex of reply to command for client server 
s=socket.socket()
s.connect(("127.0.0.1",5555))
s.send("login".encode())
while True:
    data=s.recv(2048)
    if data.decode()=="1":
        username=input("please insert your username")
        s.send(username.encode())
        
    elif data.decode()=="2":
        pas=input("please insert your password")
        s.send(pas.encode())
        
    elif data.decode()=="01":
        username=input("invalid username, please try again")
        s.send(username.encode())

    elif data.decode()=="02":
        pas=input("invalid password, please try again")
        s.send(pas.encode())
    elif data.decode()=="3":
        print("You have succesfully logged in")
        
    



import socket
s=socket.socket()
s.connect(("127.0.0.1",5555))
s.send("login".encode())
while True:
    data=s.recv(2048)
    if data.decode()=1:
        username=input("please insert your username")
        s.send(username.encode())
        
    



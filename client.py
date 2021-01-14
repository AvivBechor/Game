import socket
s=socket.socket()
s.connect(("127.0.0.1",5555))
while True:
    s.send("time".encode())
    time=s.recv(2048)
    print(time.decode())
    break


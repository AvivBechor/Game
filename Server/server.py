#server.py
import socket
import select
import gameProperties
from gameProperties import game, client, player
global RUN
global games
HEADER=4
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
clients = []
games=[]
data=""
RUN =True
ip="localhost"#will cahnge to the ip of the virtual machine
server_socket.bind((ip,5555))

server_socket.listen(4)
server_socket.setblocking(False)
count=0
while RUN:
    read_list, write_list, exception_list = select.select([server_socket] + clients, clients, [])
    
    for s in read_list:
        if s is server_socket:
            try:
                new_client, addr = server_socket.accept()
                new_client.settimeout(0.01)
                clients.append(new_client)
            except OSError:
                    pass
        else:
            
            try:
                
                data = gameProperties.recvMessage(s,HEADER)
                #print(data)
                if not data:
                    gameProperties.remove_client(s,clients)
                    continue
                if len(data) == 0:
                    gameProperties.remove_client(s,clients)
                    continue
                if data.split(":")[0]=="end":
                    if len(write_list) != 0:
                        gameProperties.deleteGame(games,data.split(":")[1],data.split(":")[2],HEADER)
                        gameProperties.remove_client(s,clients)
                        s.close()   
                else:
                    if len(write_list) != 0:
                        
                        gameProperties.handleData(data,s,games)
                
                
            except ConnectionError:
                print("the problamatic socket is" + str(s))
                gameProperties.remove_client(s,clients)
    if(data):
        g=gameProperties.findGame(data.split(":")[1],games)
        if(g):
            if(g.pending!=True):
                        g.run()


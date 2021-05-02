import socket
import select
import threading
from queue import Queue
import gameProperties
from gameProperties import game, client, player
global RUN
global games
HEADER=4

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# To avoid bind() exception – 'OSError: [Errno 48] Address already in use.'
#server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
clients = []
games=[]
RUN =True
server_socket.bind(("localhost",5555))
server_socket.listen(4)
server_socket.setblocking(False)
count=0

def run(games):
    while RUN:
        for g in games:
            g.run()
t=threading.Thread(target=run,args=(games,))
t.start()

while RUN:

    read_list, write_list, exception_list = select.select([server_socket] + clients, clients, [])
    for s in read_list:
        
        if s is server_socket:
            try:
                new_client, addr = server_socket.accept()
                new_client.settimeout(5)
                clients.append(new_client)
            except OSError:
                    pass
        else:
            
            try:
                
                data = gameProperties.recvMessage(s,HEADER)
                print(data)
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
                gameProperties.remove_client(s,clients)
'''
        if (count==10):
            RUN=False
        count+=1
        '''
        

    
'''
                    
                    #send_data(write_list)#send what is in data_to_send to all clients.
                elif data.decode()=='ID/1':
                    print("ggg")
                    p=player(client(data.decode().split('/')[1],s))
                    for g in games:
                        if g.ID==data.decode().split('/')[1]:
                            g.players.append(p)
'''
                            
                            
                            
            
        
    
            
'''
    def proceed_data(self, client, byte_data):
        # the client parameter is in case you want specifically to know who sent you what.
        if byte_data is not None:
            try:
                print(byte_data.decode())
                # master should have a receive_data method.
                #self.master.receive_data(data)
            except EOFError:
                pass
'''


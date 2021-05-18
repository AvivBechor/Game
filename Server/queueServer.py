#queueServer.py
import socket
import select
import threading
import queueProperties
global RUN
global pendingGames 
global gamesInPlay
HEADER=4

gamesInPlay=[] #an int array with all the game IDs in play 
pendingGames=[] #an int array with all the game IDs waiting fro players 
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)games=[]
RUN =True
ip="localhost"#will cahnge to the ip of the virtual machine
server_socket.bind((ip,5556))
server_socket.listen(4)
server_socket.setblocking(False)
count=0

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
                
                data = queueProperties.recvMessage(s,HEADER)
                print(data)
                if len(data) == 0:
                    queueProperties.remove_client(s,clients)
                    continue
                if data.split(":")[0]=="end":
                    if len(write_list) != 0:
                        
                        queueProperties.remove_client(s,clients)
                        s.close()   
                else:
                    if len(write_list) != 0:
                        
                        queueProperties.handleData(data,s,gamesInPlay,pendingGames)
                
            except ConnectionError:
                queueProperties.remove_client(s,clients)
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


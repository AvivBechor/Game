import socket
import select
import threading
import pickle
from queue import Queue


class Server:
    """
    Multi-client select based server class.
    """
    def __init__(self):
        # master is the object that should handle the data.
        #self.master = master
        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        # To avoid bind() exception – 'OSError: [Errno 48] Address already in use.'
        self.server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

        self.clients = []
        self.data_to_send = Queue()

        self.RUN = True

        self.main_thread = threading.Thread(target=self.run, daemon=True)
        self.main_thread.start()

    def run(self):
        self.server_socket.bind(('0.0.0.0', 5555))
        self.server_socket.listen()
        self.server_socket.setblocking(False)

        while self.RUN:
            read_list, write_list, exception_list = select.select([self.server_socket] + self.clients, self.clients, [])
            

            for s in read_list:
                if s is self.server_socket:
                    try:
                        new_client, addr = self.server_socket.accept()
                        self.clients.append(new_client)
                    except OSError:
                        pass
                else:
                    try:
                        data = self.recv_all(s)
                        if len(data) == 0:
                            self.remove_client(s)
                        else:
                            self.proceed_data(s, data)
                    except ConnectionError:
                        self.remove_client(s)

            if len(write_list) != 0:
                self.send_data(write_list)
                self.send("test")

    def proceed_data(self, client, byte_data):
        # the client parameter is in case you want specifically to know who sent you what.
        if byte_data is not None:
            try:
                print(byte_data.decode())
                # master should have a receive_data method.
                #self.master.receive_data(data)
            except EOFError:
                pass

    def send_data(self, w_list):
        # sends data to every client (you can always exclude someone with a simple if)
        while not self.data_to_send.empty():
            data = self.data_to_send.get()
            
            for c in self.clients:
                if c in w_list:
                    try:
                        c.send(data.encode())
                    except ConnectionError:
                        self.remove_client(c)

    def send(self, data):
        # outside method to call from master.
        self.data_to_send.put(data)

    def recv_all(self, sock):
        # receives all data (in case it's bigger than the buffer).
        BUFF_SIZE = 4096  # 4 KiB
        data = b''
        while True:
            part = sock.recv(BUFF_SIZE)
            data += part
            if len(part) < BUFF_SIZE:
                # either 0 or end of data
                break
        return data

    def remove_client(self, client):
        self.clients.remove(client)
        # in case the client connection is important to the master
        # self.master.receive_data(data)

    def close(self):
        # closes the server safely to avoid trouble.
        self.RUN = False
        try:
            self.server_socket.close()
        except OSError:
            pass
def Main():
    s=Server()

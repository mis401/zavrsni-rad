#include "tcpserver.h"



TCPServer::TCPServer(std::string ip_address, int port): ip_address(ip_address), port(port), serverAddressLength(sizeof(serverAddress)) {
    start();
}


TCPServer::~TCPServer(){
    stop();
}

void TCPServer::start(){
    std::cout << "Starting server" << '\n';
    m_socket = socket(AF_INET, SOCK_STREAM, 0);
    if (m_socket < 0){
        std::cout<< "Cannot create socket";
        exit(1);
    }
    serverAddress.sin_family = AF_INET;
    serverAddress.sin_port = htons(port);
    serverAddress.sin_addr.s_addr = inet_addr(ip_address.c_str());
    std::cout<<ip_address.c_str();

    if (bind(m_socket, (sockaddr*)&serverAddress, serverAddressLength) < 0){
        std::cout << "Cannot bind socket";
        exit(1);
    }
}

void TCPServer::startListen(){
    std::cout << "Starting to listen\n";
    if (listen(m_socket, 50)){
        std::cout << "Error listening\n";
    }
    bool run = true;
    mutex mutexRun;
    while( [&run, &mutexRun] {
        unique_lock<mutex> lock(mutexRun);
         return run;
    }()){
            const int bufferSize = 32000;
            int new_socket = accept(m_socket, (sockaddr*)&serverAddress, &serverAddressLength);
            if(new_socket < 0)
                return;
            threadpool.addTask([this, new_socket, &run, &mutexRun] {
                {
                    unique_lock<mutex> lock(mutexRun);
                    if (!run)
                        return;
                }
                char *buffer = new char[bufferSize];
                int bytesReceived = read(new_socket, buffer, bufferSize);
                if (bytesReceived < 0 || bytesReceived > bufferSize)
                    return;
                HttpRequest req(buffer, bytesReceived);
                if (req.getMethod() == "POST"){
                    int receivedNumber = atoi(req.getContent().c_str()); 
                    this->bst.push(receivedNumber);
                }
                else if(req.getMethod() == "GET"){
                    string bst_string = this->bst.toString();
                    write(new_socket, bst_string.c_str(), bst_string.size());
                }
                delete[] buffer;
                close(new_socket);
                if (req.getMethod() == "GET"){
                    unique_lock<mutex> lock(mutexRun);
                    run = false;
                    int socketState = shutdown(this->m_socket, SHUT_RDWR);
                    if (socketState < 0){
                        cout << "Error on shutdown" << endl;
                        close(this->m_socket);
                    }
                }
        });
    }
    cout<<"Stopping run" << endl;
}



void TCPServer::stop(){
    close(m_socket);
    exit(0);
}





#ifndef TCPSERVER_H
#define TCPSERVER_H
#include <sys/socket.h>
#include <string>
#include <iostream>
#include <stdio.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include "threadpool.h"
#include "httprequest.h"
#include "concurrent_bst.h"

using namespace std;
class TCPServer{
private:
    ThreadPool threadpool;
    void start();
    void stop();
    static void* consume(void* args);
    int m_socket;
    string ip_address;
    int port;
    struct sockaddr_in serverAddress;
    unsigned int serverAddressLength;
    string welcomeMessage = "Welcome to test server";
    ConcurrentBST bst;

public:
    void startListen();
    TCPServer(std::string ip_address, int port);
    ~TCPServer();
};




#endif
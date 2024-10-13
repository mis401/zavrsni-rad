#ifndef HTTPREQUEST_H
#define HTTPREQUEST_H
#include <iostream>
#include <sstream>
#include <vector>
#include <string.h>

using namespace std;
class HttpRequest{
    string request;
    string method;
    string host;
    string userAgent;
    string accepts;
    vector<string> headers;

    string content;
    void parseMethod(string &methodLine);
    void parseHost(string &hostLine);
    void parseUserAgent(string &userAgentLine);
    void parseAccepts(string &acceptLine);
    void parseHeaders(vector<string> &headers);
    void parseContent(string &content);
public:
    HttpRequest(char* request, int n);
    void parse();
    string getMethod() {return method;}
    string getHost() {return host;}
    string getUserAgent() {return userAgent;}
    string getAccepts() {return accepts; }
    vector<string> getHeaders() {return headers;}
    string getContent() {return content;}
};
#endif
#include "httprequest.h"

using namespace std;
HttpRequest::HttpRequest(char* req, int n){
    char* requestArray = new char[n+1];
    strncpy(requestArray, req, n);
    requestArray[n]='\0';
    request = string(requestArray);
    delete[] requestArray;
    parse();
}

void HttpRequest::parse(){
    stringstream ss(request);
    string line;
    vector<string> lines;
    while(getline(ss, line)){
        if(line.empty()){
            break;
        }
        lines.emplace_back(line);
    }
    parseMethod(lines[0]);
    parseHost(lines[1]);
    parseUserAgent(lines[2]);
    parseAccepts(lines[3]);
    vector<string> headers;
    int i;
    for (i = 4; lines[i][0] != '\n' && lines[i][0] != '\r' ; ++i){
        headers.emplace_back(lines[i]);
    }
    parseHeaders(headers);
    ++i;
    parseContent(lines[i]);
}

void HttpRequest::parseMethod(string &methodLine){
    int whitespace = methodLine.find(' ');
    if (whitespace == methodLine.npos) {
        method = "Error";
        return;
    }
    method = methodLine.substr(0, whitespace);
    return;
}

void HttpRequest::parseHost(string &hostLine){
    host = hostLine;
}

void HttpRequest::parseUserAgent(string &agentLine){
    userAgent = agentLine;
}

void HttpRequest::parseAccepts(string &acceptLine){
    accepts = acceptLine;
}

void HttpRequest::parseHeaders(vector<string> &headers){
    this->headers = headers;
}

void HttpRequest::parseContent(string &content){
    this->content = content;
}
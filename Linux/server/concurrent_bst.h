#ifndef CONCURRENT_BST_H
#define CONCURRENT_BST_H
#include <iostream>
#include <mutex>
#include <cmath>
#include <vector>
#include "node.h"



using namespace std;
class ConcurrentBST{
private:
    mutex mutexLock;
    Node* root;
    int size;
    std::vector<Node*> getNodes();
    void traverse(Node* node, std::vector<Node*> &nodes);
    void swap(Node* a, Node* b);
public:
    ConcurrentBST();
    ~ConcurrentBST();
    void push(int a);
    int pop();
    bool isEmpty();
    string toString();
};

#endif
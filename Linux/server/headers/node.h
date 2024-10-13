#ifndef NODE_H
#define NODE_H

class Node{
private:
    int value;
public:
    Node* left;
    Node* right;
    int getValue() {return value;}
    Node(int value): value(value), left(nullptr), right(nullptr){}
    ~Node() {};
};
#endif
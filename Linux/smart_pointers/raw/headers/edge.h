#ifndef EDGE_H
#define EDGE_H
#include "node.h"

class Edge{
private:
    int weight;
    Node* start;
    Node* end;
public:
    int getWeight();
    Node* getStart();
    Node* getEnd();
    Edge(int w, Node* s, Node* e);
};

#endif
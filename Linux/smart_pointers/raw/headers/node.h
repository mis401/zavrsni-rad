#ifndef NODE_H
#define NODE_H
#include "satellite.h"
#include "edge.h"

class Node{
    private:
    Satellite* data;
    Edge** edges;
};

#endif
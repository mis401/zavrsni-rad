#include "concurrent_bst.h"

ConcurrentBST::ConcurrentBST(){
    size = 0;
    root = nullptr;
}

void ConcurrentBST::traverse(Node* root, std::vector<Node*> &nodes){
    if(root == nullptr){
        return;
    }
    if (root->left != nullptr)
        traverse(root->left, nodes);
    nodes.emplace_back(root);
    if (root->right != nullptr)
        traverse(root->right, nodes);
}

std::vector<Node*> ConcurrentBST::getNodes(){
    std::vector<Node*> nodes;
    traverse(root, nodes);
    return nodes;
}

ConcurrentBST::~ConcurrentBST(){
    std::vector<Node*> nodes = getNodes();
    for(auto node: nodes){
        delete node;
    }
    
}


void ConcurrentBST::push(int value){
    Node* newNode = new Node(value);
    {
        unique_lock<mutex> lock(mutexLock);
        if (root == nullptr){
            root = newNode;
            ++size;
            return;
        }
        Node* tmp = root;
        Node* tmp2 = nullptr;
        while (tmp != nullptr){
            tmp2 = tmp;
            if (value < tmp->getValue())
                tmp = tmp->left;
            else
                tmp = tmp->right;
        }
        if (value < tmp2->getValue())
            tmp2->left = newNode;
        else
            tmp2->right = newNode;
        ++size;
    }
}

int ConcurrentBST::pop(){
    {
        unique_lock<mutex> lock(mutexLock);
        int value = 0;
        if (isEmpty()){
            return value;
        }
        if (size == 1){
            value = root->getValue();
            delete root;
            root = nullptr;
            return value;
        }
        value = root->getValue();
        Node* oldRoot = root;
        if (root->left == nullptr)
            root = root->right;
        else if (root->right == nullptr)
            root = root->left;
        else {
            Node* tmp = root->right;
            while(tmp != nullptr && tmp->left != nullptr)
                tmp = tmp->left;
            root = tmp;
            root->left = oldRoot->left;
            if(tmp->right != nullptr){
                Node* tmp2 = tmp;
                while(tmp2->right != nullptr)
                    tmp2 = tmp2->right;
                tmp2->right = oldRoot->right;
            }
        }
        delete oldRoot;
        --size;
        return value;
    }
}



bool ConcurrentBST::isEmpty(){
    {
    unique_lock<mutex> lock(mutexLock);
    if (size == 0)
        return true;
    else
        return false;
    }
}

string ConcurrentBST::toString(){
    vector<Node*> nodes = getNodes();
    string result = "Inorder BST: ";
    for (auto &node: nodes){
        string value = to_string(node->getValue());
        result.append(value);
        result.append(" -> ");
    }
    result.erase(result.size() - 4);
    return result;
}
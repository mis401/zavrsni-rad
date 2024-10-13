#ifndef THREADPOOL_H
#define THREADPOOL_H
#include <iostream>
#include <thread>
#include <vector>
#include <mutex>
#include <condition_variable>
#include <queue>
#include <functional>

using namespace std;
class ThreadPool{
private:
    vector<thread> threads;
    mutex mutexLock;
    condition_variable conditional;
    queue<function<void()>> tasks;
    bool exit;
public:
    void addTask(function<void()> task);
    int getNumberOfThreads() {return threads.size();}
    int getNumberOfTasks() {return tasks.size();}
    ThreadPool(int numberOfThreads = thread::hardware_concurrency());
    ~ThreadPool();
};

#endif 
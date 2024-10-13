#include "threadpool.h"

ThreadPool::ThreadPool(int num) {
    exit = false;
    for (int i = 0; i < num; ++i){
        threads.emplace_back([this, i] {
            while(true){
                function<void()> task;
                {
                    //  cout<<"Emplacing task, thread " << threads[i].get_id() << '\n';
                    unique_lock<mutex> lock(mutexLock);
                    conditional.wait(lock, [this] {
                        return !tasks.empty() || exit;
                    });
                    if (exit && tasks.empty()){
                        return;
                    }
                    task = move(tasks.front());
                    tasks.pop();
                }
                task();
            }
        });
    }
}

void ThreadPool::addTask(function<void()> task){
    {
        unique_lock<mutex> lock(mutexLock);
        tasks.emplace(move(task));
    }
    conditional.notify_one();
}

ThreadPool::~ThreadPool() {
    {
        unique_lock<mutex> lock(mutexLock);
        exit = true;

        conditional.notify_all();
        for(auto& thread: threads){
            thread.join();
        }
    }
}
#include <stdio.h>
#include <stdlib.h>

void func(){
    int* y = (int*)malloc(sizeof(int));
    *y = 5;
    printf("%d", *y);
}

int main(int argc, char** argv){

    int* x = (int*)malloc(sizeof(int));
    *x = 10;
    func();
    func();
    printf("%d", *x);
    printf("Hello World");
}
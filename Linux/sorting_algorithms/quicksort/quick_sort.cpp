#include <iostream>
#include <ctime>

#define NUM_ELEMENTS 20
#define MAX_ELEMENT 500
int partition(int* array, int p, int r);
void quicksort(int* array, int p, int r);
void printArray(int* array, int n);
int* allocateAndPopulateArray(int n);


int main(int argc, char** argv){
    int size = NUM_ELEMENTS;
    int* test_array = allocateAndPopulateArray(size);

    printArray(test_array, size);
    quicksort(test_array, 0, size-1);
    printArray(test_array, size);
    delete[] test_array;

    for (int i = 1; i <= 10001; i+=1000){
        int* array = allocateAndPopulateArray(i);
        quicksort(array, 0, i-1);
        delete[] array;
    }
    return 0;
}

void quicksort(int* array, int p, int r){
    if (p < r){
        int pivot = partition(array, p, r);
        quicksort(array, p, pivot-1);
        quicksort(array, pivot+1, r);
    }
}

int partition(int* array, int p, int r){
    int pivot = array[r];
    int i = p - 1;
    int tmp;
    for (int j = p; j < r; j++){
        if (array[j] < pivot){
            i++;
            tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }
    tmp = array[i+1];
    array[i+1] = array[r];
    array[r] = tmp; 
    return i+1;
}
int* allocateAndPopulateArray(int n){
    std::srand(std::time(nullptr));
    int* array = new int[n];
    for (int i = 0; i < n; i++) {
            array[i] = std::rand() % MAX_ELEMENT;
    }
    return array;
}

void printArray(int* array, int n){
    for (int i = 0; i < n; i++){
        std::cout << array[i] << ' ';
    }
    std::cout << '\n';
}
#include <iostream>
#include <ctime>
#define NUM_ELEMENTS 20
#define MAX_ELEMENT 500

void mergeSort(int* array, int l, int r);
void merge(int* array, int l, int mid, int r);
int* allocateAndPopulateArray(int n);
void printArray(int* array, int n);

int main(int argc, char** argv){
    int* testArray = allocateAndPopulateArray(NUM_ELEMENTS);
    printArray(testArray, NUM_ELEMENTS);
    mergeSort(testArray, 0, NUM_ELEMENTS-1);
    printArray(testArray, NUM_ELEMENTS);
    delete[] testArray;

    for (int i = 1; i <= 10001; i+=1000){
        int* array = allocateAndPopulateArray(i);
        mergeSort(array, 0, i-1);
        delete[] array;
    }
    return 0;
}

void mergeSort(int* array, int l, int r){
    if (l >= r)
        return;
    int mid = (l + r) / 2;
    mergeSort(array, l, mid);
    mergeSort(array, mid+1, r);
    merge(array, l, mid, r);
}

void merge(int* array, int l, int mid, int r){
    int sizeLeft = mid - l + 1;
    int sizeRight = r - mid;
    int* leftArray = new int[sizeLeft];
    int* rightArray = new int[sizeRight];
    for (int i = 0; i < mid-l+1; i++){
        leftArray[i] = array[l+i];
    }
    for (int j = 0; j < r-mid; j++){
        rightArray[j] = array[mid+1+j];
    }
    int i = 0;
    int j = 0;
    int k = l;
    while(i < sizeLeft && j < sizeRight){
        if (leftArray[i] < rightArray[j]){
            array[k] = leftArray[i];
            i++;
        }
        else{
            array[k] = rightArray[j];
            j++;
        }
        k++;
    }
    while(i<sizeLeft){
        array[k] = leftArray[i];
        i++;
        k++;
    }
    while(j<sizeRight){
        array[k] = rightArray[j];
        j++;
        k++;
    }
    delete[] leftArray;
    delete[] rightArray;
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
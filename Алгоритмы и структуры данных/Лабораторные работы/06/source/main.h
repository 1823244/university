#include <cstdio>
#include <cstdlib>
#include <ctime>
#include <random>

#include "StackArray.h"
#include "QueueList.h"
#include "TInquiry.h"

void generateName(char* string);
TInquiry generator();
void dump(Queue *F0, Queue *F1, Queue *F2, Stack *S, TInquiry currentTask, int number);

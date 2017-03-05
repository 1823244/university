#include "main.h"

int main(int argc, char const *argv[]) {
	Queue F0, F1, F2;
	Stack S;
	TInquiry currentTask, tmpTask;
	int isFree = 1;

	InitQueue(&F0);
	InitQueue(&F1);
	InitQueue(&F2);
	InitStack(&S);

	for (int i = 0; i < 15; i++) {
		tmpTask = generator();

		switch (tmpTask.P) {
		case 0: PutQueue(&F0, tmpTask);
			break;
		case 1: PutQueue(&F1, tmpTask);
			break;
		case 2: PutQueue(&F2, tmpTask);
			break;
		}

		if (isFree == 0) {
			if (currentTask.P > tmpTask.P) {
				PutStack(&S, currentTask);

				switch (tmpTask.P) {
				case 0: GetQueue(&F0, &currentTask);
					break;
				case 1: GetQueue(&F1, &currentTask);
					break;
				case 2: GetQueue(&F2, &currentTask);
					break;
				}
			}
		}
		else {
			if (!EmptyQueue(&F0)) {
				GetQueue(&F0, &currentTask);
			}
			else {
				if (!EmptyStack(&S)) {
					ReadStack(&S, &tmpTask);

					if (tmpTask.P == 1) {
						GetStack(&S, &currentTask);
					}
					else if (!EmptyQueue(&F1)) {
						GetQueue(&F1, &currentTask);
					}
					else {
						GetStack(&S, &currentTask);
					}
				}
				else if (!EmptyQueue(&F1)) {
					GetQueue(&F1, &currentTask);
				}
				else if (!EmptyQueue(&F2)) {
					GetQueue(&F2, &currentTask);
				}
			}
		}

		dump(&F0, &F1, &F2, &S, currentTask, i);
		if (--currentTask.Time <= 0) {
			isFree = 1;
		}
		else {
			isFree = 0;
		}
	}

	getchar();
	return 0;
}

void dump(Queue *F0, Queue *F1, Queue *F2, Stack *S, TInquiry currentTask, int number) {
	printf("%d", number);

	switch (number) {
	case 0:  printf(" ");
		break;
	case 1:  printf("%s ", "st");
		break;
	case 2:  printf("%s ", "nd");
		break;
	case 3:  printf("%s ", "rd");
		break;
	default: printf("%s ", "th");
		break;
	}

	printf("%s\n", "time:");

	printf("\t%s\n", "F0:");
	PrintQueue(F0);

	printf("\t%s\n", "F1:");
	PrintQueue(F1);

	printf("\t%s\n", "F2:");
	PrintQueue(F2);

	printf("\t%s\n", "S:");
	PrintStack(S);

	printf("\t%s\n", "P:");
	if (currentTask.Time > 0) {
		printf("\t--(%s, %d, %d)\n", currentTask.Name, currentTask.Time, currentTask.P);
	}
}

TInquiry generator() {
	TInquiry task;
	std::random_device rd;

	generateName(task.Name);
	task.Time = rd() % 5 + 1;
	task.P = rd() % 3;

	return task;
}

void generateName(char *name) {
	int range = 'Z' - 'A';
	std::random_device rd;

	for (int i = 0; i < 9; i++) {
		name[i] = 'A' + rd() % range;
	}

	name[9] = '\0';
}
#include "Tree.h"

void PrintPrefix(Tree *T);
void PrintPostfix(Tree *T);
void CalcTree(Tree *T, Stack *S);
void Parser(Tree *T);

int main(void)
{
	Tree root, tmp, result;
	Stack S;

	InitTree(&root);
	InitTree(&tmp);
	Parser(&root);
	tmp = root;

	printf("Postfix: ");
	PrintPostfix(&root);
	root = tmp;

	printf("\nPrefix: ");
	PrintPrefix(&root);
	root = tmp;

	InitStack(&S);
	CalcTree(&root, &S);

	InitTree(&result);
	GetStack(&S, &result);
	printf("\nResult: %d\n", result->data);
	
	getchar();
}

void PrintPrefix(Tree *T) {
	if (T != NULL) {
		switch ((*T)->data) {
		case -1:
			printf("%c ", '+');
			break;
		case -2:
			printf("%c ", '-');
			break;
		case -3:
			printf("%c ", '*');
			break;
		case -4:
			printf("%c ", '/');
			break;
		default:
			printf("%c ", (*T)->data + '0');
			break;
		}

		if (isRight(T)) PrintPrefix(&(*T)->right);
		if (isLeft(T)) PrintPrefix(&(*T)->left);
	}
}

void PrintPostfix(Tree *T) {
	if (T != NULL) {
		if (isRight(T)) PrintPostfix(&(*T)->right);
		if (isLeft(T)) PrintPostfix(&(*T)->left);

		switch ((*T)->data) {
		case -1:
			printf("%c ", '+');
			break;
		case -2:
			printf("%c ", '-');
			break;
		case -3:
			printf("%c ", '*');
			break;
		case -4:
			printf("%c ", '/');
			break;
		default:
			printf("%c ", (*T)->data + '0');
			break;
		}
	}
}

void CalcTree(Tree *T, Stack *S) {
	if (isRight(T)) CalcTree(&(*T)->right, S);
	if (isLeft(T)) CalcTree(&(*T)->left, S);
	if ((*T)->data >= 0) {
		PutStack(S, *T);
	}
	else {
		Tree tmpOne, tmpTwo;
		int answer;
		InitTree(&tmpOne);
		InitTree(&tmpTwo);
		GetStack(S, &tmpOne);
		GetStack(S, &tmpTwo);
		switch ((*T)->data) {
		case -1:
			answer = tmpTwo->data + tmpOne->data;
			break;
		case -2:
			answer = tmpTwo->data - tmpOne->data;
			break;
		case -3:
			answer = tmpTwo->data * tmpOne->data;
			break;
		case -4:
			answer = tmpTwo->data / tmpOne->data;
			break;
		}
		InsertTree(&tmpOne, answer);
		PutStack(S, tmpOne);
	}
}

void Parser(Tree *T) {
	char tmp = '.';
	Stack S;
	Tree tmpTree, tmpLSon, tmpRSon, tmpTmp;

	InitStack(&S);
	while (tmp != '\n') {
		tmp = getchar();
		if (tmp == ' ') continue;
		if (tmp >= '0' && tmp <= '9') {
			InitTree(&tmpTree);
			InsertTree(&tmpTree, tmp - '0');
			PutStack(&S, tmpTree);
		}
		if (tmp == '+' || tmp == '-' || tmp == '*' || tmp == '/') {
			InitTree(&tmpTree);
			tmpTmp = tmpTree;
			switch (tmp) {
			case '+':
				InsertTree(&tmpTree, -1);
				break;
			case '-':
				InsertTree(&tmpTree, -2);
				break;
			case '*':
				InsertTree(&tmpTree, -3);
				break;
			case '/':
				InsertTree(&tmpTree, -4);
				break;
			}

			GetStack(&S, &tmpLSon);
			MoveToLeft(&tmpTree, &tmpLSon);
			tmpTree = tmpTmp;

			GetStack(&S, &tmpRSon);
			MoveToRight(&tmpTree, &tmpRSon);
			tmpTree = tmpTmp;

			PutStack(&S, tmpTree);
		}
	}

	GetStack(&S, T);
}
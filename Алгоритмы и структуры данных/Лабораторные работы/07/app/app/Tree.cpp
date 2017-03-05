#include "Tree.h"

const short TreeOk = 0;
const short TreeNotMem = 1;
const short TreeEmpty = 2;

short TreeError;

void InitTree(Tree *T) {
	*T = (Tree)malloc(sizeof(Tree));
	if (*T == NULL) {
		TreeError = TreeNotMem;
		return;
	}
	(*T)->data = NULL;
	(*T)->left = NULL;
	(*T)->right = NULL;

	TreeError = TreeOk;
}

void InsertTree(Tree *T, BaseTypeTree E) {
	if (*T != NULL) {
		(*T)->data = E;
	}
	else {
		TreeError = TreeEmpty;
	}
}

void ReadTree(Tree *T, BaseTypeTree *E) {
	if (*T != NULL) {
		*E = (*T)->data;
	}
	else {
		TreeError = TreeEmpty;
	}
}

bool isLeft(Tree *T) {
	return (*T)->left != NULL;
}

bool isRight(Tree *T) {
	return (*T)->right != NULL;
}

void MoveToLeft(Tree *T, Tree *TS) {
	if (TS != NULL) {
		if (*TS == NULL) {
			TreeError = TreeEmpty;
			return;
		}
		(*T)->left = *TS;
	}
	(*T) = (*T)->left;
}

void MoveToRight(Tree *T, Tree *TS) {
	if (TS != NULL) {
		if (*TS == NULL) {
			TreeError = TreeEmpty;
			return;
		}
		(*T)->right = *TS;
	}
	(*T) = (*T)->right;
}

bool isEmptyTree(Tree *T) {
	return (((*T)->left == NULL) && ((*T)->right == NULL));
}

void DelTree(Tree *T) {
	while ((*T)->left != NULL) {
		MoveToLeft(T, NULL);
		DelTree(T);
	}
	while ((*T)->right != NULL) {
		MoveToRight(T, NULL);
		DelTree(T);
	}
	(*T)->left = NULL;
	(*T)->right = NULL;
	(*T)->data = NULL;
}
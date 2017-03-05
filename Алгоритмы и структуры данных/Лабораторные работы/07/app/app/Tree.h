#ifndef TREE_H
#define TREE_H

#include "StackList.h"

void InitTree(Tree *T);
void InsertTree(Tree *T, BaseTypeTree E);
void ReadTree(Tree *T, BaseTypeTree *E);
bool isLeft(Tree *T);
bool isRight(Tree *T);
void MoveToLeft(Tree *T, Tree *TS);
void MoveToRight(Tree *T, Tree *TS);
bool isEmptyTree(Tree *T);
void DelTree(Tree *T);

#endif
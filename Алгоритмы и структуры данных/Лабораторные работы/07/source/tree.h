#ifndef TREE_H
#define TREE_H

#include <string>

using namespace std;

const TreeOk = 0;
const TreeNotMem = 1;
const TreeEnd = 2;

typedef string BaseType;
typedef struct element *ptrel;
typedef struct element {
	BaseType data;
	ptrel LSon;
	ptrel RSon;
} element;
typedef ptrel *Tree;

short TreeError;

// инициализация дерева, создание корня
void InitTree(Tree *T);

// запись данных
void WriteDataTree(Tree *T, BaseType E);

// чтение данных
void ReadDataTree(Tree *T, BaseType *E);

// проверка наличия левого сына
bool IsLSon(Tree *T);

// проверка наличия правого сына
bool IsRSon(Tree *T);

// переход к левому сыну
void MoveToLSon(Tree *T, Tree *TS);

// переход к правому сыну
void MoveToRSon(Tree *T, Tree *TS);

// проверка дерева на пустоту 
int IsEmptyTree(Tree *T);

// удаление поддерева
void DelTree(Tree *T);

#endif
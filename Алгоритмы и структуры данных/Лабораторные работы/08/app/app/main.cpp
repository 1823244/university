#define _CRT_SECURE_NO_WARNINGS

#include <string.h>
#include "UnsortedTableArray.h"


int main() {
	Table T;
	char * tmp = "";
	char * tmpChar = "";
	int i;
	FILE * fcode;
	FILE * fspaces;
	FILE * fwords;

	if (!(fcode = fopen("code.txt", "r"))) {
		printf("Can't open code file!\n");
		getchar();
		exit(1);
	}
	if (!(fspaces = fopen("spaces.txt", "r"))) {
		printf("Can't open spaces file!\n");
		getchar();
		exit(1);
	}
	if (!(fwords = fopen("words.txt", "r"))) {
		printf("Can't open words file!\n");
		getchar();
		exit(1);
	}

	InitTable(&T);

	while (fscanf(fwords, "%s", &tmp) == 1) {
		PutTable(&T, NULL, tmp);
	}

	while (fscanf(fspaces, "%c", &tmp) == 1) {
		PutTable(&T, NULL, tmp);
	}

	tmp = "";
	while (fscanf(fcode, "%c", &tmpChar) == 1) {
		ReadTable(&T, &i, tmpChar);
		if (GetTableError() != 0) {
			strcat(tmp, tmpChar);
		}
		else {
			tmp += '\0';
			ReadTable(&T, &i, tmp);
			if ((tmp[0] <= '0') && (tmp[0] >= '9') && (GetTableError() != 0)) {
				printf("%s ", tmp);
				PutTable(&T, NULL, tmp);
			}
			tmp = "";
		}
		
	}

	getchar();

	return 0;
}
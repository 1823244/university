#include <stdio.h>
#include <locale.h>

void insertChar(int k, char c, int *j, char *str) {

	char s[100];
	int i = 0;

	if (k > 3) {
		str[(*j)++] = '(';
		while (k) {
			s[i++] = '0' + k % 10;
			k /= 10;
		}
		i--;
		for (;i >= 0; i--) {
			str[(*j)++] = s[i];
		}
		str[(*j)++] = ')';
		str[(*j)++] = c;
	} else {
		for (; k > 0; k--) {
			str[(*j)++] = c;
		}
	}
}

void compress(char *str){
	char c = str[0];
	int k = 1, i, j = 0;

	for (i = 1; str[i]; ++i) {
		if (str[i] == c) {
			k++;
		} else {
			insertChar(k, c, &j, str);
			k = 1;
			c = str[i];
		}
	}
	insertChar(k, c, &j, str);
	str[j] = '\0';
	
}

int main(void) {
	char str[100];

	setlocale(LC_ALL, "RUS");

	printf("%s", "�������: ����� �����, � ������� ��� ������� ������. ��������� ��� ������,\n\t �� ���� �������� ������ ������������������ ���������� ��������\n\t �������� ����� ������ ���� �� (k)s, ��� s � ����������� ������,\n\t � k � ���������� ����������.\n\n������� ������: ");
	gets(str);
	compress(str);
	printf("\n���������: %s\n", str);
	getchar();
	
	return 0;
}
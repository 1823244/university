#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <locale.h>

#define MaxWord 100
#define MaxWordSize 30

void delText(char **text, int n) {
	int i;

	for(i = 0; i < n; i++) {
		free((void *) text[i]);
	}

	free ((void *) text);

}

void funcStrcpy(char *to, char *from) {
	char * save = to;
	while(*from) {
		*to = *from;
		to++;
		from++;
	}

	*to = '\0';
	to = save;
}

size_t funcStrlen(const char *str) {
	const char *save = str;

	while (*save++) ;

	return save - str - 1;
}

void funcStrncat(char *to, char *from, size_t n) {

	char *save = to;

	if(n > 0) {
		
		while(*to++);
		*to--;
		
		while(n-- > 0) {
			*to++ = *from++;
		}

		*to = '\0';
	}

	to = save;

}

int funcStrcmp(char *first, char *second) {

	while(*first++ == *second++)
		if(*first == 0) return 0;

	return (unsigned) *first - (unsigned) * second;
}

int getWord(char *string, int n) {
	char *save = string;
	char c;
	int i;

	while(((c = getchar()) <= ' ') && (c != '\n'));
	while(c > ' ' && n--) {
		*string++ = c;
		c = getchar();
	}
	*string = '\0';

	i = (n < 0) ? 0 : (string - save);
	if(c == '\n') {
		i *= -1;
	}

	return i;
}

char *get_word(int n) {
	char *string = (char *) malloc (n * sizeof(char)), c;
	int m = n;

	while(((c = getchar()) <= ' ') && (c != '\n'));
	while(c > ' ' && n--) {
		*string++ = c;
		c = getchar();
	}
	*string = '\0';

	if (c != ' ') {
		free((void *)string);
		return NULL;
	} else {
		string = realloc(string, (m - n) * sizeof(char));
		return string;
	}

}


char **getText(int words, int letters, int *count) {
	char **mText;
	char *buf = (char *) malloc(words * sizeof(char)), bufLastLetter;
	int len; 
	*count = 0;
	mText = (char **) malloc(sizeof(*mText) * words);
	
	if (mText != NULL && buf != NULL) {
		
		while(*count < words && (len = getWord(buf, letters)) > 0) {
			if ((mText[*count] = (char *) malloc(sizeof(char) * (len + 1))) != NULL) {
				funcStrcpy(mText[(*count)++], buf);
			} else {
				delText(mText, *count);
				return NULL;
			}
		}

		if (len < 0) {
			if ((mText[*count] = (char *) malloc(sizeof(char) * (abs(len) + 1))) != NULL) {
				funcStrcpy(mText[(*count)++], buf);
			} else {
				delText(mText, *count);
				return NULL;
			}
		} else if (len == 0) {
			delText(mText, *count);
			return NULL;
		}
		
		free((void *) buf);
	
	}

	return mText;
}

char *cycle(char *string) {
	char tmp[MaxWordSize];
	size_t length;

	length = funcStrlen(string);
	tmp[0] = string[length - 1];
	tmp[1] = '\0';

	funcStrncat(tmp, string, length - 1);

	funcStrcpy(string, tmp);

	return string;
}

int cycle_compare(char *first, char *second) {
	int n;

	if((n = funcStrlen(first)) == funcStrlen(second)) {
		while(funcStrcmp(first, second) && n--) {
			cycle(second);
		}

		return n>0?1:0; 
	} else {
		return 0;
	}
}

int find_and_delete(char **string, int n) {
	int i, j;
	int m = n;


	for(i = 0; i < n; i++) {
		for(j = i + 1; j < n; j++){
			if(cycle_compare(string[i], string[j])) {
				string[i][0] = '\0';
				string[j][0] = '\0';
				m = m - 2;
			}	
		}
	}

	return m;
}

char **textcpy(char **src, int n, int m) {
	int i, j;
	char **dest = (char **) malloc(m * sizeof(char *));

	for (i = 0, j = 0; i < n, j < m; i++) {
		if(funcStrlen(src[i])) {
			dest[j] = (char *) malloc(sizeof(char) * (funcStrlen(src[i]) + 1));
			funcStrcpy(dest[j], src[i]);
			j++;
		} 
	}

	return dest;

}

int main(void) {
    char **text, **final;
    int i, n, m;

    setlocale(LC_ALL, "RUS");
    printf("%s\n\n%s ", "Задание: Из заданного текста удалить пары слов, таких, что одно из них\n\t может быть получено циклическим сдвигом символов другого.", "Введите строку:");
    text = getText(MaxWord, MaxWordSize, &n);
    m = find_and_delete(text, n);
	final = textcpy(text, n, m);
	
	delText(text, n);
	printf("%s ", "Результат:");
	for(i = 0; i < m; i++) {
		printf("%s ", final[i]);
	}

	delText(final, m);
	printf("\n\n%s", "Нажмите [Enter] для завершения работы программы...");
    _getch();

    return 0;
}
#include "Manager.h"
#include <cstdio>

CManager::CManager()
{
	stream = fopen("Dat.txt", "r+");
}


CManager::~CManager()
{
	fclose(stream);
}
void CManager::Quantity_of_chars()
{
	Num = 0;

	while ((c = getc(stream)) != EOF)
	{
		Num++;
	}
	printf("Quantity of chars=%i\n", Num);
}

void CManager::Number_of_space() {

	stream = fopen("Dat.txt", "r+");

	int i = 0;

	while ((c = getc(stream)) != EOF) {

		if (c == ' ') i++;
	}

	printf("Quantity of space symbols=%i\n", i);

}

void CManager::Number_of_words()
{
	stream = fopen("Dat.txt", "r+");
	char *Mas = new char[Num];
	int i = 0;

	while ((c = getc(stream)) != EOF)
	{
		Mas[i] = c;
		i++;
	}

	UK = 0;
	KIL = 0;

	for (i = 0; i <= Num; i++)
	{
		if ((Mas[i] == ' ') || (Mas[i] == '\n')) UK++;

		if (((Mas[i] != ' ') && (Mas[i] != '\n')) && (UK >= 1))
		{
			KIL++;
			UK = 0;
		}
	}

	printf("Quantity of words=%i\n", KIL + 1);

	delete Mas;

}

void CManager::Delete_wrong_space()
{
	UK = 0;
	stream = freopen("Dat.txt", "r+", stream);
	char *Mas = new char[Num];
	int i = 0;

	while ((c = getc(stream)) != EOF)
	{
		if (c == ' ') UK++;

		else
		{
			Mas[i] = c;
			UK = 0;
		}

		if ((c == ' ') && (UK == 1)) Mas[i] = c;
		if ((c == ' ') && (UK >1)) i--;
		i++;
	}

	UK = i;
	printf("\n");

	for (i = 0; i<UK; i++)
	{
		printf("%c", Mas[i]);
	}
	printf("\n");
}
#pragma once
#include <string>

class CManager
{
	char c;

	int Num, UK, KIL;
	std::string StrMass;

protected:

	FILE* stream;

public:

	CManager();

	~CManager();

	void Quantity_of_chars();

	void Number_of_space();

	void Number_of_words();

	void Delete_wrong_space();
	void Number_of_strings();
};


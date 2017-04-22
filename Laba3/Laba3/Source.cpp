#include <string>
#include <iostream>
#include <fstream>
#include <cstring>
#include <cstdio>
#include <cstdlib>
#include "Paster.h"
#include "Manager.h"

using namespace std;
int main() {


	CManager paster;

	paster.Quantity_of_chars();

	paster.Number_of_space();

	paster.Number_of_words();

	paster.Delete_wrong_space();

	//paster.PasteToText();

	unsigned int line_number = 1;
	unsigned int requested_line_number;
	unsigned int requested_index;
	std::string text;
	//std::cin >> line_number;
	std::cout << "Enter line: ";
	std::cin >> requested_line_number;

	std::cout << "Enter Index: ";
	std::cin >> requested_index;

	std::cout << "Enter text: ";
	std::cin >> text;

	std::ifstream file("Dat.txt");
	//io_errc::stream = fopen("Dat.txt", "w", io_errc::stream);
	std::string line;
	unsigned int line_number1(1);
	const unsigned int requested_line_number1(4);
	while (std::getline(file, line))
	{
		if (line_number == requested_line_number)
		{

			line = line.insert(23, "something new");
			std::cout << line << "\n";
		}
		line_number++;
	}
	system("pause");

	return 0;
}

#include <cstring>
#include <cstdio>
#include <cstdlib>
#include <iostream>
#include <string>
#include <fstream>

//using namespace std;

class CMain {

	/*char c;

	int Num, UK, KIL;
*/
protected:

	//FILE* stream;

public:

	CMain();

	~CMain();
	/*
	void Quantity_of_chars();

	void Number_of_space();

	void Number_of_words();

	void Delete_wrong_space();
	void Number_of_strings();*/
	unsigned int line_number = 1;
	unsigned int requested_line_number;
	unsigned int requested_index;
	std::string text;
	void Input_Text();
};	
CMain::CMain()
{
	//stream = fopen("Dat.txt", "r+");
}

CMain::~CMain()

{

	//fclose(stream);

}


//
//class CSuccessor : public CMain {
//
//public:
//
//	CSuccessor() :CMain() {}
//
//	void Copy_to_file();
//
//};

void CMain::Input_Text()
{
		std::cout << "Input number of line: ";
		std::cin >> requested_line_number;

		std::cout << "Input index: ";
		std::cin >> requested_index;

		std::cout << "Input text: ";
		std::cin >> text;

}


int main() {

	CMain obj;

	obj.Input_Text();

	std::cout << obj.requested_line_number<< std::endl;
	/*obj.Quantity_of_chars();

	obj.Number_of_space();

	obj.Number_of_words();

	obj.Delete_wrong_space();

	obj.Copy_to_file();
*/
	//unsigned int line_number = 1;
	//unsigned int requested_line_number;
	//unsigned int requested_index;
	//std::string text;
	//std::cin >> line_number;
	//std::cout << "Enter line: ";
	//std::cin >> requested_line_number;

	//std::cout << "Enter Index: ";
	//std::cin >> requested_index;

	//std::cout << "Enter text: ";
	//std::cin >> text;
	/*std::ifstream file("file.ext");
	std::string line;
	unsigned int line_number(1);
	const unsigned int requested_line_number(4);
	while (std::getline(file, line))
	{
		if (line_number == requested_line_number)
		{
			std::cout << line << "\n";
		}
		line_number++;
	}*/
	std::ifstream file("Dat.txt");
	//io_errc::stream = fopen("Dat.txt", "w", io_errc::stream);
	std::string line;
	unsigned int line_number1(1);
	const unsigned int requested_line_number1(4);
	while (std::getline(file, line))
	{
		if (line_number1 == requested_line_number1)
		{

			line = line.insert(23, "something new");
			std::cout << line << "\n";
		}
		line_number1++;
	}
	system("pause");
	return 0;
}
//
//CMain::CMain()
//{
//	stream = fopen("Dat.txt", "r+");
//}
//
//CMain::~CMain()
//
//{
//
//	fclose(stream);
//
//}
//
//void CMain::Quantity_of_chars() {
//
//	Num = 0;
//
//	while ((c = getc(stream)) != EOF) {
//
//		Num++;
//	}
//
//	printf("Quantity of chars=%i\n", Num);
//
//}
//
//void CMain::Number_of_space() {
//
//	stream = fopen("Dat.txt", "r+");
//
//	int i = 0;
//
//	while ((c = getc(stream)) != EOF) {
//
//		if (c == ' ') i++;
//	}
//
//	printf("Quantity of space symbols=%i\n", i);
//
//}
//
//void CMain::Number_of_words() {
//
//	stream = fopen("Dat.txt", "r+");
//
//	char *Mas = new char[Num];
//
//	int i = 0;
//
//	while ((c = getc(stream)) != EOF) {
//
//		Mas[i] = c;
//
//		i++;
//
//	}
//
//	UK = 0;
//
//	KIL = 0;
//
//	for (i = 0; i <= Num; i++) {
//
//		if ((Mas[i] == ' ') || (Mas[i] == '\n'))
//
//			UK++;
//
//		if (((Mas[i] != ' ') && (Mas[i] != '\n')) && (UK >= 1))
//
//		{
//
//			KIL++;
//
//			UK = 0;
//
//		}
//
//	}
//
//	printf("Quantity of words=%i\n", KIL + 1);
//
//	delete Mas;
//
//}
//
//void CMain::Delete_wrong_space() {
//
//	UK = 0;
//
//	stream = freopen("Dat.txt", "r+", stream);
//
//	char *Mas = new char[Num];
//
//	int i = 0;
//
//	while ((c = getc(stream)) != EOF) {
//
//		if (c == ' ') UK++;
//
//		else {
//
//			Mas[i] = c;
//
//			UK = 0;
//
//		}
//
//		if ((c == ' ') && (UK == 1)) Mas[i] = c;
//
//		if ((c == ' ') && (UK >1)) i--;
//
//		i++;
//
//	}
//
//	UK = i;
//
//	printf("\n");
//
//	for (i = 0; i<UK; i++) {
//
//		printf("%c", Mas[i]);
//
//	}
//
//	printf("\n");
//
//}
//
//void CSuccessor::Copy_to_file() {
//
//	int pos = 0, kil = 0;
//
//	printf("\nInput position and number of symbols\n ");
//
//	scanf("%i %i", &pos, &kil);
//
//	char c;
//
//	int i = 0, j = 0;
//
//	FILE *stream2;
//
//	stream = freopen("Dat.txt", "r+", stream);
//
//	stream2 = fopen("Dat2.txt", "w");
//
//	while ((c = getc(stream)) != EOF) {
//
//		i++;
//
//		if ((i >= pos) && (j<kil)) {
//
//			putc(c, stream2);
//
//			j++;
//
//		}
//
//	}
//
//}

//void CMain::Number_of_strings(){

//stream = fopen( "Dat.txt", "r+");
/*
char *Massiv = new char[Numbers];


int i = 0;

while ((c = getc(stream)) != EOF){

Massiv[i]=c;

i++;

}

UK=0;

KIL = 0;

for(i=0; i<=Number; i++) {

if(Massiv[i]=='\n')

UK++;


if(((Mas[i]!=' ')&&(Mas[i]!='\n'))&&(UK>=1))

{

KIL++;

UK = 0;

}

}

printf("Quantity of words=%i\n", KIL+1);
*/
///string *StrMass = new string[15];
/*
ifstream myfile( "Dat.txt");
if(!myfile)
{
cout<<"Error opening output file"<<endl;
system("pause");
return -1;
}
int i = 0;
while(!myfile.eof())
{
getline(myfile,StrMass[i], '\n');
cout<<StrMass[i]<<"\n";
}

delete StrMass;

}
*/
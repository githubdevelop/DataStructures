#pragma once
#include <string>
#include <iostream>
#include <fstream>
#include <vector>
using namespace std;

class CABandCSVReader
{
public:
	CABandCSVReader(std::string fileName);
	~CABandCSVReader();
	bool GetNextLineWithBands(vector<int>& bands);
private:
	istream& GetNextLine(string& line);
	istream& GetNextLineWithCommaSeparated(vector<string>& lineContents);
	
	bool IsInteger(const std::string & s, long& output);
	ifstream csvStream;
};


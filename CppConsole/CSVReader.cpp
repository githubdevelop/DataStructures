#include "stdafx.h"
#include "CSVReader.h"
#include <sstream>
#include <iterator>

CABandCSVReader::CABandCSVReader(string fileName):
	csvStream(fileName)
{
}


CABandCSVReader::~CABandCSVReader()
{
	if(csvStream.is_open())
		csvStream.close();
}



istream& CABandCSVReader::GetNextLine(string& line)
{
	getline(csvStream, line);
	return csvStream;
}

istream& CABandCSVReader::GetNextLineWithCommaSeparated(vector<string>& lineContents)
{
	string line;
	//GetNextLine(line);
	istream& retVal = getline(csvStream, line);
	if(line.length() == 0)
		return retVal;
	stringstream lineStream(line);
	string band;
	while (getline(lineStream, band, ','))
	{
		if(band.length() > 0)
			lineContents.push_back(band);
	}
	
	return csvStream;
}

bool CABandCSVReader::GetNextLineWithBands(vector<int>& bands)
{
	vector<string> lineContents;
	istream& retVal = GetNextLineWithCommaSeparated(lineContents);
	if (retVal)
	{
		for (auto content = lineContents.begin(); content != lineContents.end(); content++)
		{
			string band = *content;
			long output;
			if (IsInteger(band, output))
				bands.push_back(output);
		}
		return true;
	}
	return false;
}

bool CABandCSVReader::IsInteger(const std::string & s, long& output)
{
	if (s.empty() || ((!isdigit(s[0])) && (s[0] != '-') && (s[0] != '+'))) return false;

	char * p;
	output = strtol(s.c_str(), &p, 10);

	return (*p == 0);
}
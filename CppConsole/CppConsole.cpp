// CppConsole.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <vector>
#include <string>
#include <iostream>
#include <fstream>
#include "CSVReader.h"
#include "BandCombination.h"
#include <windows.h>

using namespace std;
using namespace std;

void TestCSVBands()
{
	TCHAR szPath[256];
	string path = "";
	if (GetModuleFileName(nullptr, szPath, 256) > 0)
	{
#ifndef UNICODE
		path = szPath;
#else
		std::wstring arr_w(szPath);
		path.assign(arr_w.begin(), arr_w.end());
#endif
	}
	if (path.length() > 0)
	{
		path = path.substr(0, path.find_last_of("\\"));
	}
	string fileName = path+".\\Bands2.csv";
	CABandCSVReader reader(fileName);
	BandCombination bandCombination;
	vector<int> bands;
	while (reader.GetNextLineWithBands(bands))
	{
		bandCombination.AddBandCombination(bands);
		bands.clear();
	}
	auto allBands = bandCombination.GetAllBandCombinations();
	for (auto bands = allBands.begin(); bands != allBands.end(); bands++)
	{
		for (auto band = bands->begin(); band != bands->end(); band++)
		{
			cout << *band << " ";
		}
		cout << "\n";
	}
}

int main()
{
	TestCSVBands();
	return 0;
}


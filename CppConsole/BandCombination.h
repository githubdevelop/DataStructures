#pragma once
#include <vector>
#include <map>
using namespace std;

class BandCombination
{
public:
	BandCombination();
	~BandCombination();
	vector<vector<int>> GetAllBandCombinations();
	unsigned int GetCombinationCountForBand(unsigned int band);
	vector<int> GetCombinations(unsigned int band, unsigned int combinationIndex);
	void AddBandCombination(vector<int> bands);

private:
	// The Key is unique bands (like 1, 2, 3, 5, 7...). The value contains the possible combinations that is associated with each key.
	map<int, vector<vector<int>>> bandCombinations; 
	void AddNewBand(vector<int> bands);
	void AddToExistingBand(vector<int> bands);
	const unsigned int MAX_BAND_COMBINATION = 16;
};


#include "stdafx.h"
#include "BandCombination.h"
#include <algorithm>


BandCombination::BandCombination()
	:bandCombinations()
{
}


BandCombination::~BandCombination()
{
}

unsigned int BandCombination::GetCombinationCountForBand(unsigned int band)
{
	auto bandKey = bandCombinations.find(band);

	if (bandKey == bandCombinations.end())
	{
		return 0;
	}
	return bandKey->second.size();

}

vector<int> BandCombination::GetCombinations(unsigned int band, unsigned int combinationIndex)
{
	vector<int> result;
	if (GetCombinationCountForBand(band) <= 0)
		return result;
	auto combinationSets = bandCombinations[band];
	if (combinationSets.size() < combinationIndex)
		return result;
	auto combinationSet = combinationSets[combinationIndex];
	result.push_back(band);
	result.insert(result.end(), combinationSet.begin(), combinationSet.end());
	return result;
}

vector<vector<int>> BandCombination::GetAllBandCombinations()
{
	vector<vector<int>> result;
	for (auto bandCombination = bandCombinations.begin(); bandCombination != bandCombinations.end(); bandCombination++)
	{
		auto combinationSetsForBand = bandCombination->second;
		for (auto combinationSet = combinationSetsForBand.begin(); combinationSet != combinationSetsForBand.end(); combinationSet++)
		{
			vector<int> temp;
			temp.push_back(bandCombination->first);
			temp.insert(temp.end(), combinationSet->begin(), combinationSet->end());
			result.push_back(temp);
		}
	}
	return result;
}


void BandCombination::AddBandCombination(vector<int> bands)
{
	if (bands.size() <= 1)
		return;
	if (bands.size() > MAX_BAND_COMBINATION || bands.size() > 5)
		return;// TODO show a warning that things are not correct.

	// First sort the bands.
	std::sort(bands.begin(), bands.end());

	auto bandKey = bandCombinations.find(bands[0]);

	if (bandKey == bandCombinations.end())
	{
		AddNewBand(bands);
	}
	else
	{
		AddToExistingBand(bands);
	}
}

void BandCombination::AddNewBand(vector<int> bands)
{
	// Adding for the first time.
	vector<int> temp;
	temp.assign(bands.begin() + 1, bands.end());
	vector<vector<int>> value;
	value.push_back(temp);
	bandCombinations.insert(std::make_pair(bands[0], value));
}

void BandCombination::AddToExistingBand(vector<int> bands)
{
	// Now the currentSetOfBands could 1 or multiple. Each of them can not exceed 15.
	// While adding new ones all of them has to be together in one of the set.
	auto currentSetOfBands = bandCombinations[bands[0]]; 
	vector<int> toAdd;
	for (auto currentSet = currentSetOfBands.begin(); currentSet != currentSetOfBands.end(); currentSet++)
	{
		toAdd.clear();
		for (auto itr = bands.begin() + 1; itr != bands.end(); itr++)
		{
			if (find(currentSet->begin(), currentSet->end(), *itr) == currentSet->end())
			{
				// It is not existing  need to be added in the current set
				toAdd.push_back(*itr);
			}
		}
		if (toAdd.size() == 0)
			break;
		if (toAdd.size() + currentSet->size() < MAX_BAND_COMBINATION) // Doing < instead of <= since another one comes up from band key.
		{
			currentSet->insert(currentSet->end(), toAdd.begin(), toAdd.end());
			toAdd.clear();
			break;
		}
	}
	// If it comes here and there are still things to be added then we need a new set and all the contents from the bands has to be 
	// added to the new set.
	if (toAdd.size() > 0)
	{
		vector<int> newSet;
		newSet.assign(bands.begin() + 1, bands.end());
		currentSetOfBands.push_back(newSet);
	}
	bandCombinations[bands[0]] = currentSetOfBands;
}


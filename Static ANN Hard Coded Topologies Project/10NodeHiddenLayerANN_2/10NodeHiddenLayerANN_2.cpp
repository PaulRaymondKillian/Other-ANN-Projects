/*------------------------------------------------------------------------
    Program :       10NodeHiddenLayerANN
    Written by:     Paul Killian
    Created:        2008-02-13
    Description:    ANN using Backprop
 -------------------------------------------------------------------------*/

#include <iostream>		// Include is like the package statement
#include <conio.h>		// Include Windows console IO header (non-Linux)
#include <math.h>
#include <ctime>

using namespace std;	// Using statement is like Java's import statement

double sums1[10];
double u1[10];
double output;
double desiredOutput;
double error;
double deltaOutput;
double delta1[10];
double fPrime1[10];
double weightsLayer1[10][2];
double weightsLayer2[11];
double input;
int epoch = 1;
int epochLimit = 500000;
double rho = 0.1;
double totalError;

const int INPUT_WEIGHT = 0;
const int BIAS_WEIGHT = 1;

double exemplarSet[5][2] = {
	{1, 1},
	{2, 4},
	{3, 9},
	{4, 16},
	{5, 25}};

double exemplarSet1[13][2] = {
	{1,1},
	{3,9},
	{5,25},
	{7,49},
	{9,81},
	{11,121},
	{13,169},
	{15,225},
	{17,289},
	{19,361},
	{21,441},
	{23,529},
	{25,625}};

double validationSet1[13][2] = {
	{2,4},
	{4,16},
	{6,36},
	{8,64},
	{10,100},
	{12,144},
	{14,196},
	{16,256},
	{18,324},
	{20,400},
	{22,484},
	{24,576},
	{26,676}};


double exemplarSet2[13][2] = {
	{1,0.785398},
	{3,7.06858},
	{5,19.635},
	{7,38.4845},
	{9,63.6173},
	{11,95.0332},
	{13,132.732},
	{15,176.715},
	{17,226.98},
	{19,283.529},
	{21,346.361},
	{23,415.476},
	{25,490.874}};
/*
double validationSet2[13][2] = {
	{2,},
	{4,},
	{6,},
	{8,},
	{10,},
	{12,},
	{14,},
	{16,},
	{18,},
	{20,},
	{22,},
	{24,},
	{26,},
*/
double exemplarSet3[13][2] = {
	{1,1},
	{3,0.333},
	{5,0.2},
	{7,0.142857},
	{9,0.111111},
	{11,0.090909},
	{13,0.076923},
	{15,0.066666},
	{17,0.058824},
	{19,0.052632},
	{21,0.047619},
	{23,0.043478},
	{25,0.04}};
/*
double validationSet3[13][2] = {
	{2,},
	{4,},
	{6,},
	{8,},
	{10,},
	{12,},
	{14,},
	{16,},
	{18,},
	{20,},
	{22,},
	{24,},
	{26,}};
*/

double exemplarSet4[13][2] = {
		{15, 22.3},
		{29, 40.55},
		{44, 52.03},
		{60, 81},
		{65, 79.51},
		{75, 86.1},
		{85, 105},
		{97, 110},
		{142, 130.68},
		{147, 152.2},
		{165, 139.81},
		{195, 161.1},
		{219, 173.54}};

/*
double validationSet4[13][2] = {
	{2,},
	{4,},
	{6,},
	{8,},
	{10,},
	{12,},
	{14,},
	{16,},
	{18,},
	{20,},
	{22,},
	{24,},
	{26,}};
*/


int randomizedIndexArray[13] = {
		0,1,2,3,4,5,6,7,8,9,10,11,12};


double calcNetworkOutput( double input )
{
	double tempOutput;

			//Calculate first layer sums
			for(int i=0; i < 10; i++)
				sums1[i] = weightsLayer1[i][INPUT_WEIGHT] * input + weightsLayer1[i][BIAS_WEIGHT];
			
			//Calculate first layer activation functions
			for(int i=0; i<10; i++)
				u1[i] = 1 / ( 1 + ( pow( 2.718281828, -sums1[i] )));
			
			//Calculate output
			tempOutput =	(weightsLayer2[0] * u1[0]) + 
						(weightsLayer2[1] * u1[1]) + 
						(weightsLayer2[2] * u1[2]) + 
						(weightsLayer2[3] * u1[3]) + 
						(weightsLayer2[4] * u1[4]) + 
						(weightsLayer2[5] * u1[5]) + 
						(weightsLayer2[6] * u1[6]) + 
						(weightsLayer2[7] * u1[7]) + 
						(weightsLayer2[8] * u1[8]) + 
						(weightsLayer2[9] * u1[9]) + 
						weightsLayer2[10];

	return tempOutput;
}


int main(int argc, char *argv[])	// Main - like in Java
{
	srand((unsigned)time(0));

	////////////////////////////////////////////////////////////////////////
	// Randomize Output Weight Vector
	for( int i = 0; i < 11; i++ )
	{
		weightsLayer2[i] = ((double)(rand() % 5)/10) + 0.1;

		if ( (rand() % 100 ) < 50 )
			weightsLayer2[i] = -weightsLayer2[i];
		
	}

	////////////////////////////////////////////////////////////////////////
	// Randomize Hidden Weight Vector
	for( int i = 0; i < 10; i++ )
		for( int j = 0; j < 2; j++ )
		{
			weightsLayer1[i][j] = ((double)(rand() % 5)/10) + 0.1;

			if ( (rand() % 100 ) < 50 )
				weightsLayer1[i][j] = -weightsLayer1[i][j];
		
		}


	while( epoch < epochLimit )
	{

		totalError = 0;

		/////////////////////////////////////////////////////////////////////
		// Randomize the Index so each epoch has a randomized exemplar order
		for( int i = 0; i < 13; i++ )
		{
			int tempIndexA = rand() % 13;
			int tempIndexB = rand() % 13;

			int tempA = randomizedIndexArray[ tempIndexA ];
			int tempB = randomizedIndexArray[ tempIndexB ];

			randomizedIndexArray[ tempIndexA ] = tempB;
			randomizedIndexArray[ tempIndexB ] = tempA;


		}//for

		////////////////////////////////////////////////////////////////////
		// Begin Iterations
		for( int i = 0; i < 13; i++ )
		{
			input = exemplarSet4[ randomizedIndexArray[i] ][0];
			desiredOutput = exemplarSet4[ randomizedIndexArray[i] ][1];

			//Calculate output of the current network
			output = calcNetworkOutput( input );


			error = desiredOutput - output;
			totalError += error;

			deltaOutput = -2 * 1 * error;

			for(int i=0; i<10; i++)
				fPrime1[i] = (1 - u1[i]) * u1[i];

			for( int i=0; i<10; i++)
				delta1[i] = fPrime1[i] * weightsLayer2[i] * deltaOutput;
			
			for( int i=0; i<11; i++)
			{
				if( i<10)
					weightsLayer2[i] = weightsLayer2[i] - rho * deltaOutput * u1[i];
				else
					weightsLayer2[i] = weightsLayer2[i] - rho * deltaOutput;
			}

			for( int i=0; i<10; i++)
			{
				weightsLayer1[i][INPUT_WEIGHT] = weightsLayer1[i][INPUT_WEIGHT] - rho * delta1[i] * input;
				weightsLayer1[i][BIAS_WEIGHT] = weightsLayer1[i][BIAS_WEIGHT] - rho * delta1[i];
			}//for

			if( (epoch % 10000 ) == 0 )
			{	
				cout << epoch << "	desiredOutput: " << desiredOutput << "	output: " << output << "  	error: " << error << endl;
				rho *= 0.999;
			}
		}

		epoch++;
	}//while

	bool cont = true;
	while (cont)
	{
		cin >> input;
		cout << calcNetworkOutput(input) << endl;
		
		if( input == -1 )
			cont = false;
	}

    return 0;
}

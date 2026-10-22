/*------------------------------------------------------------------------
    Program :       3NodeHiddenLayerANN
    Written by:     Paul Killian
    Created:        2008-02-13
    Description:    ANN using Backprop
 -------------------------------------------------------------------------*/

#include <iostream>		// Include is like the package statement
#include <conio.h>		// Include Windows console IO header (non-Linux)
#include <math.h>
#include <ctime>

using namespace std;	// Using statement is like Java's import statement

double sums1[3];
double u1[3];
double output;
double desiredOutput;
double error;
double deltaOutput;
double delta1[3];
double fPrime1[3];
double weightsLayer1[3][2];
double weightsLayer2[4];
double input;
int epoch = 1;
int epochLimit = 5000000;
double rho = 0.01;
double totalError;

const int INPUT_WEIGHT = 0;
const int BIAS_WEIGHT = 1;

double exemplarSet[6][2] = {
	{1, 15},
	{2, 16},
	{3, 17},
	{4, 18},
	{5, 19},
	{6, 20}};

int randomizedIndexArray[6] = {
		0,1,2,3,4,5};

int main(int argc, char *argv[])	// Main - like in Java
{
	srand((unsigned)time(0));

	////////////////////////////////////////////////////////////////////////
	// Randomize Output Weight Vector
	for( int i = 0; i < 4; i++ )
	{
		weightsLayer2[i] = ((double)(rand() % 5)/10) + 0.1;

		if ( (rand() % 100 ) < 50 )
			weightsLayer2[i] = -weightsLayer2[i];
		
	}

	////////////////////////////////////////////////////////////////////////
	// Randomize Hidden Weight Vector
	for( int i = 0; i < 3; i++ )
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
		for( int i = 0; i < 6; i++ )
		{
			int tempIndexA = rand() % 6;
			int tempIndexB = rand() % 6;

			int tempA = randomizedIndexArray[ tempIndexA ];
			int tempB = randomizedIndexArray[ tempIndexB ];

			randomizedIndexArray[ tempIndexA ] = tempB;
			randomizedIndexArray[ tempIndexB ] = tempA;


		}//for

		////////////////////////////////////////////////////////////////////
		// Begin Iterations
		for( int i = 0; i < 6; i++ )
		{
			input = exemplarSet[ randomizedIndexArray[i] ][0];
			desiredOutput = exemplarSet[ randomizedIndexArray[i] ][1];

			sums1[0] = weightsLayer1[0][INPUT_WEIGHT] * input + weightsLayer1[0][BIAS_WEIGHT];
			sums1[1] = weightsLayer1[1][INPUT_WEIGHT] * input + weightsLayer1[1][BIAS_WEIGHT];
			sums1[2] = weightsLayer1[2][INPUT_WEIGHT] * input + weightsLayer1[2][BIAS_WEIGHT];
			
			u1[0] = 1 / ( 1 + ( pow( 2.718281828, -sums1[0] )));
			u1[1] = 1 / ( 1 + ( pow( 2.718281828, -sums1[1] )));
			u1[2] = 1 / ( 1 + ( pow( 2.718281828, -sums1[2] )));

			output = (weightsLayer2[0] * u1[0]) + (weightsLayer2[1] * u1[1]) + (weightsLayer2[2] * u1[2]) + weightsLayer2[3];

			error = desiredOutput - output;
			totalError += error;

			deltaOutput = -2 * 1 * error;

			fPrime1[0] = (1 - u1[0]) * u1[0];
			fPrime1[1] = (1 - u1[1]) * u1[1];
			fPrime1[2] = (1 - u1[2]) * u1[2];

			delta1[0] = fPrime1[0] * weightsLayer2[0] * deltaOutput;
			delta1[1] = fPrime1[1] * weightsLayer2[1] * deltaOutput;
			delta1[2] = fPrime1[2] * weightsLayer2[2] * deltaOutput;

			weightsLayer2[0] = weightsLayer2[0] - rho * deltaOutput * u1[0];
			weightsLayer2[1] = weightsLayer2[1] - rho * deltaOutput * u1[1];
			weightsLayer2[2] = weightsLayer2[2] - rho * deltaOutput * u1[2];
			weightsLayer2[3] = weightsLayer2[3] - rho * deltaOutput;

			weightsLayer1[0][INPUT_WEIGHT] = weightsLayer1[0][INPUT_WEIGHT] - rho * delta1[0] * input;
			weightsLayer1[1][INPUT_WEIGHT] = weightsLayer1[1][INPUT_WEIGHT] - rho * delta1[1] * input;
			weightsLayer1[2][INPUT_WEIGHT] = weightsLayer1[2][INPUT_WEIGHT] - rho * delta1[2] * input;
			
			weightsLayer1[0][BIAS_WEIGHT] = weightsLayer1[0][BIAS_WEIGHT] - rho * delta1[0];
			weightsLayer1[1][BIAS_WEIGHT] = weightsLayer1[1][BIAS_WEIGHT] - rho * delta1[1];
			weightsLayer1[2][BIAS_WEIGHT] = weightsLayer1[2][BIAS_WEIGHT] - rho * delta1[2];
			
			if( (epoch % 100000 ) == 0 )
			{	
				cout << epoch << "	desiredOutput: " << desiredOutput << "	output: " << output << "  	error: " << error << endl;
				rho *= 0.9999;
			}
		}

		epoch++;
	}//while


	cin >> epoch;
    return 0;
}

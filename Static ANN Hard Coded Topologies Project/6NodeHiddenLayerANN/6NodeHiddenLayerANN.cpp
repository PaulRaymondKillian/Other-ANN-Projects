/*------------------------------------------------------------------------
    Program :       6NodeHiddenLayerANN
    Written by:     Paul Killian
    Created:        2008-02-13
    Description:    ANN using Backprop
 -------------------------------------------------------------------------*/

#include <iostream>		// Include is like the package statement
#include <conio.h>		// Include Windows console IO header (non-Linux)
#include <math.h>
#include <ctime>

using namespace std;	// Using statement is like Java's import statement

double sums1[6];
double u1[6];
double output;
double desiredOutput;
double error;
double deltaOutput;
double delta1[6];
double fPrime1[6];
double weightsLayer1[6][2];
double weightsLayer2[7];
double input;
int epoch = 1;
int epochLimit = 1000000;
double rho = 0.3;
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

double exemplarSet4[13][2] = {
	{1,0.785398},
	{2,3.1415926},
	{3,7.06858},
	{4,12.5664},
	{5,19.635},
	{6,28.2743},
	{7,38.4845},
	{8,50.2655},
	{9,63.6173},
	{10,78.5398},
	{11,95.0332},
	{12,113.097},
	{13,132.732}};

int randomizedIndexArray[13] = {
		0,1,2,3,4,5,6,7,8,9,10,11,12};

int main(int argc, char *argv[])	// Main - like in Java
{
	srand((unsigned)time(0));

	////////////////////////////////////////////////////////////////////////
	// Randomize Output Weight Vector
	for( int i = 0; i < 7; i++ )
	{
		weightsLayer2[i] = ((double)(rand() % 5)/10) + 0.1;

		if ( (rand() % 100 ) < 50 )
			weightsLayer2[i] = -weightsLayer2[i];
		
	}

	////////////////////////////////////////////////////////////////////////
	// Randomize Hidden Weight Vector
	for( int i = 0; i < 6; i++ )
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
			input = exemplarSet3[ randomizedIndexArray[i] ][0];
			desiredOutput = exemplarSet3[ randomizedIndexArray[i] ][1];

			sums1[0] = weightsLayer1[0][INPUT_WEIGHT] * input + weightsLayer1[0][BIAS_WEIGHT];
			sums1[1] = weightsLayer1[1][INPUT_WEIGHT] * input + weightsLayer1[1][BIAS_WEIGHT];
			sums1[2] = weightsLayer1[2][INPUT_WEIGHT] * input + weightsLayer1[2][BIAS_WEIGHT];
			sums1[3] = weightsLayer1[3][INPUT_WEIGHT] * input + weightsLayer1[3][BIAS_WEIGHT];
			sums1[4] = weightsLayer1[4][INPUT_WEIGHT] * input + weightsLayer1[4][BIAS_WEIGHT];
			sums1[5] = weightsLayer1[5][INPUT_WEIGHT] * input + weightsLayer1[5][BIAS_WEIGHT];
			
			u1[0] = 1 / ( 1 + ( pow( 2.718281828, -sums1[0] )));
			u1[1] = 1 / ( 1 + ( pow( 2.718281828, -sums1[1] )));
			u1[2] = 1 / ( 1 + ( pow( 2.718281828, -sums1[2] )));
			u1[3] = 1 / ( 1 + ( pow( 2.718281828, -sums1[3] )));
			u1[4] = 1 / ( 1 + ( pow( 2.718281828, -sums1[4] )));
			u1[5] = 1 / ( 1 + ( pow( 2.718281828, -sums1[5] )));
			
			output =	(weightsLayer2[0] * u1[0]) + 
						(weightsLayer2[1] * u1[1]) + 
						(weightsLayer2[2] * u1[2]) + 
						(weightsLayer2[3] * u1[3]) + 
						(weightsLayer2[4] * u1[4]) + 
						(weightsLayer2[5] * u1[5]) + 
						weightsLayer2[6];

			error = desiredOutput - output;
			totalError += error;

			deltaOutput = -2 * 1 * error;

			fPrime1[0] = (1 - u1[0]) * u1[0];
			fPrime1[1] = (1 - u1[1]) * u1[1];
			fPrime1[2] = (1 - u1[2]) * u1[2];
			fPrime1[3] = (1 - u1[3]) * u1[3];
			fPrime1[4] = (1 - u1[4]) * u1[4];
			fPrime1[5] = (1 - u1[5]) * u1[5];


			delta1[0] = fPrime1[0] * weightsLayer2[0] * deltaOutput;
			delta1[1] = fPrime1[1] * weightsLayer2[1] * deltaOutput;
			delta1[2] = fPrime1[2] * weightsLayer2[2] * deltaOutput;
			delta1[3] = fPrime1[3] * weightsLayer2[3] * deltaOutput;
			delta1[4] = fPrime1[4] * weightsLayer2[4] * deltaOutput;
			delta1[5] = fPrime1[5] * weightsLayer2[5] * deltaOutput;
			

			weightsLayer2[0] = weightsLayer2[0] - rho * deltaOutput * u1[0];
			weightsLayer2[1] = weightsLayer2[1] - rho * deltaOutput * u1[1];
			weightsLayer2[2] = weightsLayer2[2] - rho * deltaOutput * u1[2];
			weightsLayer2[3] = weightsLayer2[3] - rho * deltaOutput * u1[3];
			weightsLayer2[4] = weightsLayer2[4] - rho * deltaOutput * u1[4];
			weightsLayer2[5] = weightsLayer2[5] - rho * deltaOutput * u1[5];
			weightsLayer2[6] = weightsLayer2[6] - rho * deltaOutput;

			weightsLayer1[0][INPUT_WEIGHT] = weightsLayer1[0][INPUT_WEIGHT] - rho * delta1[0] * input;
			weightsLayer1[1][INPUT_WEIGHT] = weightsLayer1[1][INPUT_WEIGHT] - rho * delta1[1] * input;
			weightsLayer1[2][INPUT_WEIGHT] = weightsLayer1[2][INPUT_WEIGHT] - rho * delta1[2] * input;
			weightsLayer1[3][INPUT_WEIGHT] = weightsLayer1[3][INPUT_WEIGHT] - rho * delta1[3] * input;
			weightsLayer1[4][INPUT_WEIGHT] = weightsLayer1[4][INPUT_WEIGHT] - rho * delta1[4] * input;
			weightsLayer1[5][INPUT_WEIGHT] = weightsLayer1[5][INPUT_WEIGHT] - rho * delta1[5] * input;

			weightsLayer1[0][BIAS_WEIGHT] = weightsLayer1[0][BIAS_WEIGHT] - rho * delta1[0];
			weightsLayer1[1][BIAS_WEIGHT] = weightsLayer1[1][BIAS_WEIGHT] - rho * delta1[1];
			weightsLayer1[2][BIAS_WEIGHT] = weightsLayer1[2][BIAS_WEIGHT] - rho * delta1[2];
			weightsLayer1[3][BIAS_WEIGHT] = weightsLayer1[3][BIAS_WEIGHT] - rho * delta1[3];
			weightsLayer1[4][BIAS_WEIGHT] = weightsLayer1[4][BIAS_WEIGHT] - rho * delta1[4];
			weightsLayer1[5][BIAS_WEIGHT] = weightsLayer1[5][BIAS_WEIGHT] - rho * delta1[5];
			
			if( (epoch % 10 ) == 0 )
			{	
				cout << epoch << " " << rho << "	desiredOutput: " << desiredOutput << "	output: " << output << "  	error: " << error << endl;
				rho *= 0.999999;
			}
		}

		epoch++;
	}//while


	cin >> epoch;
    return 0;
}

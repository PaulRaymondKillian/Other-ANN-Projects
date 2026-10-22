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
double sums2[10];
double u1[10];
double u2[10];
double output;
double desiredOutput;
double error;
double deltaOutput;
double delta1[10][11];
double delta2[10];
double fPrime1[10];
double fPrime2[10];
double weightsLayer1[10][2];
double weightsLayer2[10][11];
double weightsLayer3[11];
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

double exemplarSet4[7][2] = {
	{1,1},
	{3,111},
	{5,11111},
	{7,1111111},
	{9,111111111},
	{11,11111111111},
	{13,1111111111111}};

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

			//Calculate second layer sums
			for(int i=0; i<10; i++)
				sums2[i] = weightsLayer2[i][0] * u1[0] +
							weightsLayer2[i][1] * u1[1] +
							weightsLayer2[i][2] * u1[2] +
							weightsLayer2[i][3] * u1[3] +
							weightsLayer2[i][4] * u1[4] +
							weightsLayer2[i][5] * u1[5] +
							weightsLayer2[i][6] * u1[6] +
							weightsLayer2[i][7] * u1[7] +
							weightsLayer2[i][8] * u1[8] +
							weightsLayer2[i][9] * u1[9] +
							weightsLayer2[i][10];

			//Calculate second layer activation functions
			//Calculate first layer activation functions
			for(int i=0; i<10; i++)
				u2[i] = 1 / ( 1 + ( pow( 2.718281828, -sums2[i] )));
			
			//Calculate output
			tempOutput =	(weightsLayer3[0] * u2[0]) + 
						(weightsLayer3[1] * u2[1]) + 
						(weightsLayer3[2] * u2[2]) + 
						(weightsLayer3[3] * u2[3]) + 
						(weightsLayer3[4] * u2[4]) + 
						(weightsLayer3[5] * u2[5]) + 
						(weightsLayer3[6] * u2[6]) + 
						(weightsLayer3[7] * u2[7]) + 
						(weightsLayer3[8] * u2[8]) + 
						(weightsLayer3[9] * u2[9]) + 
						weightsLayer3[10];

	return tempOutput;
}


int main(int argc, char *argv[])	// Main - like in Java
{
	srand((unsigned)time(0));

	////////////////////////////////////////////////////////////////////////
	// Randomize Output Weight Vector
	for( int i = 0; i < 11; i++ )
	{
		weightsLayer3[i] = ((double)(rand() % 5)/10) + 0.1;

		if ( (rand() % 100 ) < 50 )
			weightsLayer3[i] = -weightsLayer3[i];
		
	}

	////////////////////////////////////////////////////////////////////////
	// Randomize Second Hidden Layer Weight Vector
	for( int i = 0; i < 10; i++ )
		for( int j = 0; j < 11; j++ )
		{
			weightsLayer2[i][j] = ((double)(rand() % 5)/10) + 0.1;

			if ( (rand() % 100 ) < 50 )
				weightsLayer2[i][j] = -weightsLayer2[i][j];
		
		}

	////////////////////////////////////////////////////////////////////////
	// Randomize First Hidden Layer Weight Vector
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
			input = exemplarSet2[ randomizedIndexArray[i] ][0];
			desiredOutput = exemplarSet2[ randomizedIndexArray[i] ][1];

			//Calculate output of the current network
			output = calcNetworkOutput( input );


			error = desiredOutput - output;
			totalError += error;

			deltaOutput = -2 * 1 * error;

			//Calculate new weights for Output Layer
			for( int i=0; i<11; i++)
			{
				if( i<10)
					weightsLayer3[i] = weightsLayer3[i] - rho * deltaOutput * u2[i];
				else
					weightsLayer3[i] = weightsLayer3[i] - rho * deltaOutput;
			}

			for(int i=0; i<10; i++)
				fPrime2[i] = (1 - u2[i]) * u2[i];

			for( int i=0; i<10; i++)
				delta2[i] = fPrime2[i] * weightsLayer3[i] * deltaOutput;


			//Calculate weights for second hidden layer
			for( int i=0; i<10; i++)
				for( int j=0; j<11; j++ )
				{
					if( j<10 )
						weightsLayer2[i][j] = weightsLayer2[i][j] - rho * delta2[i] * u1[i];
					else				
						weightsLayer2[i][j] = weightsLayer2[i][j] - rho * delta2[i];
				
				}//for

			for(int i=0; i<10; i++)
				fPrime1[i] = (1 - u1[i]) * u1[i];

			for( int i=0; i<10; i++)
				for( int j=0; j<11; j++ )
					delta1[i][j] = fPrime1[i] * weightsLayer2[i][j] * delta2[i];

			for( int i=0; i<10; i++)
			{
				weightsLayer1[i][INPUT_WEIGHT] = weightsLayer1[i][INPUT_WEIGHT] - rho * ( delta1[0][i] + delta1[1][i] + delta1[2][i] + delta1[3][i] + delta1[4][i] + delta1[5][i] + delta1[6][i] + delta1[7][i] + delta1[8][i] + delta1[9][i]) * input;
				weightsLayer1[i][BIAS_WEIGHT] = weightsLayer1[i][BIAS_WEIGHT] - rho * ( delta1[0][i] + delta1[1][i] + delta1[2][i] + delta1[3][i] + delta1[4][i] + delta1[5][i] + delta1[6][i] + delta1[7][i] + delta1[8][i] + delta1[9][i]);
			}
			if( (epoch % 100 ) == 0 )
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

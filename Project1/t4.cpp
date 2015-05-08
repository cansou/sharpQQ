#include "h1.h"
#include "h2.h"
#include <iostream>
using namespace std;
void push();
int i ;
void add2( const int &n){
	num+=2+n;
	static int n1;
	char *cp= "aaaa";

	walk();
	push();
	walk();
	cout<<"add2:  " <<(int *)cp<<endl;
}
static void add3(){
	add2(8);

}
inline void push(){
	Direction d = {3,4.56f};
	Direction ds[2] ={
		{12,34},
		{5,6}
	};
	for ( auto i=0;i<_countof(ds);i++)
	{
		d = ds[i];
		cout<<d.x<<" , "<<d.y<<endl;
	}
	
	Color c;
	c=Color(100);
	cout<<"push..."<<blue<<endl;
}
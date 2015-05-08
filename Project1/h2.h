#include <iostream>
inline void walk(){
	static int g;
	g++;
	std::cout<<"walking"<<g<<std::endl;
}
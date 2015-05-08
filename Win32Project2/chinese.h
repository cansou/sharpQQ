#ifndef CHINESE_H
#define CHINESE_H
#include "person.h"
#include <string>
#include "pub.h"
#include <iostream>
using namespace std;
class Chinese : public Person
{
public:
	Chinese(int id = -1, string name = "null", int money = 0);
	friend ostream& operator<<(ostream & out, Chinese &c);
	virtual void showMoney();
	virtual void walk();
	virtual void walk(int i);
	~Chinese(){
		cout<<"Ïú»Ùchinese"<<endl;
	}
private:
	SkinColor skin;
};
#endif
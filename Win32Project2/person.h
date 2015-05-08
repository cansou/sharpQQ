#ifndef PERSON_H
#define  PERSON_H
#include <string>
#include <iostream>
using namespace std;
class Person
{
public:
	Person(int id = -1, string name = "null", int money = 0);
	virtual void walk();
	virtual void walk(int i);
	virtual void showMoney();
	friend ostream& operator<<(ostream&out, Person &p);
	int getId();
	string getName();
	virtual ~Person(){cout<<"Ïú»Ùperson "<<endl;}
protected:
	int money ;

private:
	int id;
	string name;
};
#endif
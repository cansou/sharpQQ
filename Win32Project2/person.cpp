#include "stdafx.h"
#include <iostream>
#include "person.h"
using namespace std;
Person::Person(int id, string name , int money){
	this->id = id;
	this->name = name;
	this->money = money;
}
ostream& operator<<(ostream&out,Person &p){
	cout<<"person[id="<<p.id<<",name="<<p.name<<",money="<<p.money<<"]";
	return out;
}
void Person::walk(){
	cout<<name<<"会走路。"<<endl;
}
void Person::walk(int i){
	cout<<name<<"会走路。"<<i<<endl;
}
void Person::showMoney(){
	cout<<name<<"展示money。"<<endl;
}
int Person::getId(){
	return id;
}
string Person::getName(){
	return name;
}
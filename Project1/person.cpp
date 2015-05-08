#include "h1.h"
#include <string>
#include <iostream>
using namespace std;
void Person::hit(string name, int age){
	id--;
	cout<<id<<"hit "<<name<<"   "<<this->age<<endl;
}
int Person::operator+(const Person p){
	return p.id+id;
}
Person::Person(int id, int age){
	this->id = id;
	this->age = age;
}
Person::~Person(){
	cout<<"destroy person..."<<id<<endl;
}
Person::operator int()const{
	return int(id);
}
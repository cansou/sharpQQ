#include "stdafx.h"
#include <string>
#include "chinese.h"
#include "pub.h"

#include <iostream>
using namespace std;
Chinese::Chinese(int id, string name, int money):Person(id, name, money){
		this->skin = black;
}
void Chinese::showMoney(){

	cout<<money<<endl;
}
void Chinese::walk(){
	cout<<"chinese会走路"<<endl;
}
void Chinese::walk(int i){
	cout<<"chinese会走路"<<i<<endl;
}
ostream& operator<<(ostream&out,Chinese &p){
	cout<<"person[id="<<p.getId()<<",name="<<p.getName()<<",money="<<p.money<<",skin="<<p.skin<<"]";
	return out;
}

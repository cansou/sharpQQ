#include <iostream>
#include <fstream>
#include "h1.h"
#include <cstring>
#include <string>
#include <vector>
#include <array>
#include<ctime>
using namespace  std;
extern void add2(const int &n);
extern void push();
template<typename T>
void mswap(T &a, T &b);
template<> void mswap(int &a, int &b);
double refcube(const double &a);

void add(){
	num++;
	extern int i;
	i++;
	//push();
	cout<<num<<endl;
}
void show1( string &s){
	s.append("dd");
	cout<<"show "<<s<<endl;
}
void show1(const string &s){
	cout<<"show const"<<endl;
}

int operator*(int i, const Person &p){
	return i*p.id;
}
void run(Person &p){
	p.age;
}
ostream& operator<<(ostream &o, const Person &p){
	o<<p.id<<","<<p.age;
	return o;
}
int main(){
	{
	 Person *p = new Person(34);
	 p->hit("lee");
	
	 (*p).hit("gean");
	  /*delete p;*/
	 Person p1= Person();
	 p1.hit("u know");
	 cout<<"add person:  "<<2*(*p)<<endl;
	 cout<<p1<<endl;
	 ofstream fout;
	 fout.open("saveid.txt");
	 if(fout.is_open()){
		 fout<<*p<<"============";
	 }
	 fout.close();
	 Person p2 = Person(2);
	 int id = (int)p2;
	 cout<<id<<endl;
	}
	  string s6 = "";
	 string &s = s6;
	show1(s);
	double result = refcube(2);
	cout<<result<<endl;

	char  iArray[5]="ab\0c";
	add();
	 int m = 3;
	 int &n = m;
	add2(n);
	string s1 = "aa";
	string s2 = "kk";
	vector<int> v1;
	v1.push_back(3);
	v1.insert(v1.begin(),4);
	clock_t start = clock();
	while (clock()-start<1*CLOCKS_PER_SEC);
	array<int,5> a1 = {3,3};
	
	for (int &var : v1)
	{
		var++;
		cout<<var<<endl;
	}
	for (int var : v1)
	{
		cout<<var<<endl;
	}
	cout<<strlen(iArray)<<a1[3]<<endl;
	string s3 = "======";
	string *s4 = &s3;
	*s4 = "222";
	char *aa = "0000";
	string *s5 = new string;
	*s5 = "99";
	int a = 5;
	int b =7;
	cout<<a<<","<<b<<endl;
	mswap(a,b);
	cout<<a<<","<<b<<endl;
	std::cin.get();
}
template <typename T>
void mswap(T &a, T &b){
	T temp = a;
	a = b;
	b = temp;
}
void mswap(int &a, int &b){
	int temp = a;
	a = b;
	b = temp;
	decltype(a+b)  sum;
	sum = a+b;
	cout<<sum<<endl;
	cout<<"swap finished"<<endl;
}
double refcube(const double &a){
	return a*a*a;
}
int* get(int a[]){

	return a;
}
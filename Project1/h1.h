#ifndef H1
#define  H1
#include <string>
static int num;
struct Direction
{
	int x;
	int y;

};
enum Color{
	red,green=9,blue
};
class Person
{
public:
	enum class MyEnum1
	{
		red = 6
	};
	explicit Person(int id = 2, int age=22);
	void hit(std::string name, int age=23);
	void cry();
	void add(const int &a, const int &b);
	int operator+(const Person p);
	operator int() const;
	friend int operator*(int i, const Person &p);
	friend std::ostream& operator<<(std::ostream &o, const Person &p);
	friend void run(Person &p);
	~Person();
protected:
	int age;
private:

	int id  ;
	enum class MyEnum
	{
		red = 4
	};
	
	std::string address[MyEnum1::red];
};

#endif
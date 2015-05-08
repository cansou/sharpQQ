#include "baseEllipse.h"
class Circle:public BaseEllipse{
private:
	double r;
	const double rt;
public:
	Circle(double x,double y, double r ):BaseEllipse(x,y),rt(0.5){
		this->r = r;
	}

	 double area(){
		return 3.14159*r*r;
	}
	void scale(){
		r *=rt;
	}
};
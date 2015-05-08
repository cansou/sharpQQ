class BaseEllipse{
public:
	BaseEllipse(double x =0, double y =0){}
	void move(double nx,double ny){
		x = nx;
		y = ny;
	}
	virtual double area() = 0;
private:
	double x;
	double y;
};
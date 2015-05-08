using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
    public class ThreadActorDispatcher
    {

        
	private SizeQueue<HttpActor> actorQueue;
	private Thread dispatchThread;
        private static ApacheHttpService service;


	/**
	 * 默认构造函数，不会自动启动action循环
	 */
	public ThreadActorDispatcher(){
		this.actorQueue = new SizeQueue<HttpActor>(int.MaxValue);
	}
	
	/* (non-Javadoc)
	 * @see iqq.im.actor.HttpActorDispatcher#pushActor(iqq.im.actor.HttpActor)
	 */
	/** {@inheritDoc} */
	public void pushActor(HttpActor actor){
		this.actorQueue.Enqueue(actor);
	}
	
	/**
	 * 执行一个HttpActor，返回是否继续下一个actor
	 */
	private Boolean dispatchAction(HttpActor actor){
		try {
			actor.execute();
//			System.out.println("take:    "+actor);
		} catch (Exception e) {
		}
		return !(actor is ExitActor);
	}
	
	/** {@inheritDoc} */
	public void run(){
		try {
			while(dispatchAction(this.actorQueue.Dequeue ())){}
		} catch (Exception e) {
		}
	}

	/** {@inheritDoc} */
	
	public void init(QQContext context)  {
		dispatchThread = new Thread(new ThreadStart(this.run));
		dispatchThread.Start();
	}

	/** {@inheritDoc} */
	
	public void destroy()  {
		pushActor(new ExitActor());
		try {
			if(Thread.CurrentThread != dispatchThread){
				dispatchThread.Join();
			}
		} catch (Exception e) {
			throw new QQException(QQException.QQErrorCode.UNKNOWN_ERROR, e);
		}
	}
	
	/**
	 * 
	 * 一个伪Actor只是为了让ActorLoop停下来
	 *
	 * @author solosky
	 *
	 */
    }
    public class ExitActor : HttpActor
    {

        public void execute()
        {
            //do nothing
            //service.destroy();
        }
    }
}

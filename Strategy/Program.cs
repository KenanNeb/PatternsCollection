namespace Strategy;
#region SortStrategyExample1
//public abstract class SortStrategy
//{
//    public abstract void Sort(List<string> list);
//}

//public class QuickSort : SortStrategy
//{
//    public override void Sort(List<string> list)
//    {
//        list.Sort();  // Default is Quicksort
//        Console.WriteLine("QuickSorted list ");
//    }
//}

//public class ShellSort : SortStrategy
//{
//    public override void Sort(List<string> list)
//    {
//        //list.ShellSort();  not-implemented
//        Console.WriteLine("ShellSorted list ");
//    }
//}

//public class BubbleSort : SortStrategy
//{
//    public override void Sort(List<string> list)
//    {
//        //list.BubbleSort();  not-implemented
//        Console.WriteLine("BubbleSorted list ");
//    }
//}

//public class MergeSort : SortStrategy
//{
//    public override void Sort(List<string> list)
//    {
//        //list.MergeSort(); not-implemented
//        Console.WriteLine("MergeSorted list ");
//    }
//}

//public class SortedList
//{
//    private List<string> list = new List<string>();
//    private SortStrategy sortstrategy;
//    public void SetSortStrategy(SortStrategy sortstrategy)
//    {
//        this.sortstrategy = sortstrategy;
//    }
//    public void Add(string name)
//    {
//        list.Add(name);
//    }
//    public void Sort()
//    {
//        sortstrategy.Sort(list);
//        // Siyahi uzre sort edib neticeni gosterir
//        foreach (string name in list)
//        {
//            Console.WriteLine(" " + name);
//        }
//        Console.WriteLine();
//    }
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            // Fərqli strategiyaları üzrə kontekstlər
//            SortedList studentRecords = new SortedList();
//            studentRecords.Add("Samual");
//            studentRecords.Add("Jimmy");
//            studentRecords.Add("Sandra");
//            studentRecords.Add("Vivek");
//            studentRecords.Add("Anna");
//            studentRecords.SetSortStrategy(new QuickSort());
//            studentRecords.Sort();
//            studentRecords.SetSortStrategy(new ShellSort());
//            studentRecords.Sort();
//            studentRecords.SetSortStrategy(new BubbleSort());
//            studentRecords.Sort();
//            studentRecords.SetSortStrategy(new MergeSort());
//            studentRecords.Sort();
//            Console.ReadKey();
//        }
//    }
//}
#endregion

#region SortStrategyExample2
public interface IBehaviour
{
	public void moveCommand();
}

public class AgressiveBehaviour : IBehaviour
{
	public void moveCommand()
    {
     Console.WriteLine("\tAgressive Behaviour: if find another robot attack it!");
    }
}

public class DefensiveBehaviour: IBehaviour
{
	public void moveCommand()
    {
	 Console.WriteLine("\tDefensive Behaviour: if find another robot run from it!");
    }
}

public class NormalBehaviour: IBehaviour
{
	public void moveCommand()
    {
	 Console.WriteLine("\tNormal Behaviour: if find another robot ignore it!");
    }
}

public class Robot
{
	IBehaviour behaviour;
	String name;

	public Robot(String name)
	{
		this.name = name;
	}

	public void setBehaviour(IBehaviour behaviour)
	{
		this.behaviour = behaviour;
	}

	public IBehaviour getBehaviour()
	{
		return behaviour;
	}

	public void move()
	{
        Console.WriteLine(this.name + ": Based on current position" +
					 "the behaviour object decide the next move:");
		// ... send the command to mechanisms
		Console.WriteLine("\tThe result returned by behaviour object " +
					"is sent to the movement mechanisms " +
					" for the robot '" + this.name + "'");
	}

	public String getName()
	{
		return name;
	}

	public void setName(String name)
	{
		this.name = name;
	}
}


public class Program
{

	public static void Main(String[] args)
	{

		Robot r1 = new Robot("Big Robot");
		Robot r2 = new Robot("Rehim v.2.1");
		Robot r3 = new Robot("R2");

		r1.setBehaviour(new AgressiveBehaviour());
		r2.setBehaviour(new DefensiveBehaviour());
		r3.setBehaviour(new NormalBehaviour());

		r1.move();
		r2.move();
		r3.move();

		Console.WriteLine("\nNew behaviours: " +
				"\n\t'Big Robot' gets really scared" +
				"\n\t, 'Rehim v.2.1' becomes really mad because" +
				"it's always attacked by other robots" +
				"\n\t and R2 keeps its calm\n");

		r1.setBehaviour(new DefensiveBehaviour());
		r2.setBehaviour(new AgressiveBehaviour());

		r1.move();
		r2.move();
		r3.move();
	}
}

#endregion
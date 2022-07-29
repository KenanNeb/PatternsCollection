namespace Visitor;

public interface IVisitor
{
    void Visit(Element element);
}

public class IncomeVisitor : IVisitor
{
    public void Visit(Element element)
    {
        Employee employee = element as Employee;
        // 10% maaş artımı verilir
        employee.Income *= 1.10;
        Console.WriteLine("{0} {1}'s new income: {2:C}",
            employee.GetType().Name, employee.Name,
            employee.Income);
    }
}
 
public class VacationVisitor : IVisitor
{
    public void Visit(Element element)
    {
        Employee employee = element as Employee;
        // Əlavə 3 gün tətil verilir
        employee.VacationDays += 3;
        Console.WriteLine("{0} {1}'s new vacation days: {2}",
            employee.GetType().Name, employee.Name,
            employee.VacationDays);
    }
}

public abstract class Element
{
    public abstract void Accept(IVisitor visitor);
}

public class Employee : Element
{
    private string name;
    private double income;
    private int vacationDays;
    // Constructor
    public Employee(string name, double income,
        int vacationDays)
    {
        this.name = name;
        this.income = income;
        this.vacationDays = vacationDays;
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public double Income
    {
        get { return income; }
        set { income = value; }
    }
    public int VacationDays
    {
        get { return vacationDays; }
        set { vacationDays = value; }
    }
    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Obyektin Struktur klası
public class Employees
{
    private List<Employee> employees = new List<Employee>();
    public void Attach(Employee employee)
    {
        employees.Add(employee);
    }
    public void Detach(Employee employee)
    {
        employees.Remove(employee);
    }
    public void Accept(IVisitor visitor)
    {
        foreach (Employee employee in employees)
        {
            employee.Accept(visitor);
        }
        Console.WriteLine();
    }
}
// Üç işçi tipi yaradılır
public class Clerk : Employee
{
    // Constructor
    public Clerk()
        : base("Kevin", 25000.0, 14)
    {
    }
}
public class Director : Employee
{
    // Constructor
    public Director()
        : base("Elly", 35000.0, 16)
    {
    }
}
public class President : Employee
{
    // Constructor
    public President()
        : base("Eric", 45000.0, 21)
    {
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // İşçilərin kolleksiyasını qurulması
        Employees employee = new Employees();
        employee.Attach(new Clerk());
        employee.Attach(new Director());
        employee.Attach(new President());
        // İşçilər ziyarət edildi 'visited'
        employee.Accept(new IncomeVisitor());
        employee.Accept(new VacationVisitor());
        
        Console.ReadKey();
    }
}